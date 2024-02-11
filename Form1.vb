'The top left square has the tag coordinates (-1,-1) because a coordinate of (0,0) would be the same as an empty point
'Need to fix checking, king moves and checkmate as
'Everything to do with the king is very slow

Public Class Form1
    Private SelectedPiecePosition As Point
    Public Board As New ChessBoard
    
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Board.InitializeBoard()
    End Sub

    Private Sub ChessSquare_Click(sender As Object, e As EventArgs) Handles PicA8.Click, PicB8.Click, PicC8.Click, PicD8.Click, PictureBox3.Click, PictureBox4.Click, PictureBox5.Click, PictureBox6.Click, PictureBox7.Click, PictureBox8.Click, PictureBox9.Click, PictureBox10.Click, PicD7.Click, PictureBox12.Click, PictureBox13.Click, PictureBox14.Click, PictureBox15.Click, PictureBox16.Click, PictureBox17.Click, PictureBox18.Click, PicD5.Click, PictureBox20.Click, PictureBox21.Click, PictureBox22.Click, PictureBox23.Click, PictureBox24.Click, PictureBox25.Click, PictureBox26.Click, PicD6.Click, PictureBox28.Click, PictureBox29.Click, PictureBox30.Click, PictureBox39.Click, PictureBox40.Click, PictureBox41.Click, PictureBox42.Click, PicD2.Click, PictureBox44.Click, PictureBox45.Click, PictureBox46.Click, PictureBox47.Click, PictureBox48.Click, PictureBox49.Click, PictureBox50.Click, PicD3.Click, PictureBox52.Click, PictureBox53.Click, PictureBox54.Click, PictureBox55.Click, PictureBox56.Click, PictureBox57.Click, PictureBox58.Click, PicD4.Click, PictureBox60.Click, PictureBox61.Click, PictureBox62.Click, PictureBox31.Click, PictureBox32.Click, PictureBox33.Click, PictureBox34.Click, PicD1.Click, PictureBox36.Click, PictureBox37.Click, PictureBox38.Click
        Dim ClickedPicture As PictureBox = DirectCast(sender, PictureBox)
        Dim ClickedPosition As New Point
        Dim SelectedPiece As ChessPiece = Board.GetPiece(SelectedPiecePosition)
        Dim ClickedPiece As ChessPiece
        If ClickedPicture.Tag(0) <> "-" Then
            ClickedPosition.X = Val(ClickedPicture.Tag(0))
            ClickedPosition.Y = Val(ClickedPicture.Tag(2))
            ClickedPiece = Board.GetPiece(ClickedPosition)
        Else
            ClickedPosition.X = -1
            ClickedPosition.Y = -1
            ClickedPiece = Board.GetPiece(New Point(0, 0))
        End If

        If SelectedPiecePosition = ClickedPosition Then
            SelectedPiecePosition = Point.Empty
            ResetMovesOnUI()
            Exit Sub
        End If

        If SelectedPiecePosition <> Point.Empty AndAlso ClickedPicture.BackColor <> Color.Red Then
            If Board.GetPiece(ClickedPosition) IsNot Nothing Then
                SelectedPiecePosition = ClickedPosition
                ResetMovesOnUI()
                ShowMovesOnUI(ClickedPiece)
                Exit Sub
            End If
        End If

        ResetMovesOnUI()

        Dim ActualSelect, ActualClick As Point
        If SelectedPiecePosition.X = -1 Then
            ActualSelect = New Point With {
                .X = 0,
            .Y = 0}
            ActualClick = ClickedPosition

        ElseIf ClickedPosition.X = -1 Then
            ActualClick = New Point With {
                .X = 0,
            .Y = 0}
            ActualSelect = SelectedPiecePosition
        Else
            ActualClick = ClickedPosition
            ActualSelect = SelectedPiecePosition
        End If

        If SelectedPiecePosition.IsEmpty Then
            SelectedPiece = ClickedPiece
            If SelectedPiece IsNot Nothing Then
                SelectedPiecePosition = ClickedPosition
                ShowMovesOnUI(SelectedPiece)
                Exit Sub
            End If
        Else
            If ClickedPiece IsNot Nothing Then
                If ClickedPiece.Colour = SelectedPiece.Colour AndAlso ClickedPiece.GetType <> GetType(Rook) Then
                    ShowMovesOnUI(ClickedPiece)
                    SelectedPiecePosition = ClickedPosition
                    Exit Sub
                End If
            End If

            If Board.CanMove(ActualClick, ActualSelect) Then
                MovePieceOnUI(ClickedPicture, ActualSelect)
                Board.MovePiece(ActualClick, ActualSelect)
            End If

            If Board.DeliveredCheck(SelectedPiece) Then
                If Board.DeliveredCheckMate(SelectedPiece) Then
                    If Board.WhiteTurn Then
                        MsgBox("Checkmate, Black Wins!!!")
                    Else
                        MsgBox("Checkmate, White Wins!!!")
                    End If
                    End
                End If
            End If
        End If
        SelectedPiecePosition = Point.Empty
    End Sub
    Public Sub ShowMovesOnUI(Piece As ChessPiece)
        If Not Board.KingInCheck(Piece) Then
            Piece.UpdateLegalMoves(Board)
        Else
            Piece.ShowOnlyCheckBlocks(Board)
        End If

        For y = 0 To 7
            For x = 0 To 7
                If Piece.LegalMoves(x, y) Then
                    If x = 0 AndAlso y = 0 Then
                        PicA8.BackColor = Color.Red
                    End If
                    For Each cntrl As Control In Controls
                        If TypeOf cntrl Is PictureBox Then
                            Dim i As Integer = Val(cntrl.Tag(0))
                            Dim j As Integer = Val(cntrl.Tag(2))

                            If x = i AndAlso y = j Then
                                cntrl.BackColor = Color.Red
                            End If
                        End If
                    Next
                End If
            Next
        Next
    End Sub

    Public Sub ResetMovesOnUI()
        For Each cntrl As Control In Controls
            If TypeOf cntrl Is PictureBox Then
                cntrl.BackColor = Color.Transparent
            End If
        Next
    End Sub

    Public Sub MovePieceOnUI(ClickedPicture As PictureBox, selectedPiecePosition As Point)
        'Putting the new piece in the old pieces spot and removing the new piece from where it was on the ui
        'King and pawns have special moves (en passant) (castling) so checking for those
        Dim X As Integer = Val(ClickedPicture.Tag(0))
        Dim Y As Integer = Val(ClickedPicture.Tag(2))
        If X = -1 Then
            X = 0
            Y = 0
        End If

        Dim MovingPiece As ChessPiece = Board.GetPiece(selectedPiecePosition)
        Dim SpotToFill As ChessPiece = Board.GetPiece(New Point(X, Y))
        If MovingPiece.GetType() = GetType(King) AndAlso SpotToFill IsNot Nothing AndAlso SpotToFill.GetType() = GetType(Rook) Then
            If MovingPiece.Colour = ChessColour.White Then
                If X = 0 Then
                    ClickedPicture.Image = Nothing
                    PictureBox34.Image = Nothing
                    PictureBox36.Image = My.Resources.White_King
                    PicD1.Image = My.Resources.White_Rook
                Else
                    PictureBox34.Image = Nothing
                    ClickedPicture.Image = Nothing
                    PictureBox32.Image = My.Resources.White_King
                    PictureBox33.Image = My.Resources.White_Rook
                End If
            Else
                Dim xString As Char = ClickedPicture.Tag(0)
                If xString = "-" Then
                    PictureBox3.Image = Nothing
                    ClickedPicture.Image = Nothing
                    PicC8.Image = My.Resources.Black_King
                    PicD8.Image = My.Resources.Black_Rook
                Else
                    PictureBox3.Image = Nothing
                    ClickedPicture.Image = Nothing
                    PictureBox5.Image = My.Resources.Black_King
                    PictureBox4.Image = My.Resources.Black_Rook
                End If

            End If
        ElseIf MovingPiece.GetType() = GetType(Pawn) AndAlso SpotToFill Is Nothing AndAlso X <> MovingPiece.PosX Then
            Dim PieceToRemove As New PictureBox
            Dim MovingPawn As New PictureBox
            Dim MovingFound As Boolean = False
            Dim RemovingFound As Boolean = False
            Dim OffsetY As Integer
            If MovingPiece.Colour = ChessColour.White Then
                OffsetY = 1
            Else
                OffsetY = -1
            End If

            For Each img As PictureBox In Controls
                Dim picx As Integer = Val(img.Tag(0))
                Dim picy As Integer = Val(img.Tag(2))

                If picx = X AndAlso picy = Y + OffsetY Then
                    PieceToRemove = img
                    RemovingFound = True

                ElseIf picx = MovingPiece.PosX AndAlso picy = MovingPiece.PosY Then
                    MovingPawn = img
                    MovingFound = True
                End If

                If MovingFound AndAlso RemovingFound Then
                    ClickedPicture.Image = MovingPawn.Image
                    PieceToRemove.Image = Nothing
                    MovingPawn.Image = Nothing
                    Exit For
                End If

            Next

        Else
            NormalUISwap(ClickedPicture, selectedPiecePosition)
        End If
    End Sub

    Public Sub NormalUISwap(ClickedPicture As PictureBox, selectedPiecePosition As Point)
        If selectedPiecePosition.X = -1 Then
            ClickedPicture.Image = PicA8.Image
            PicA8.Image = Nothing
            Exit Sub
        End If

        For Each cntrl As Control In Controls
            If TypeOf cntrl Is PictureBox Then
                Dim x As Integer = Val(cntrl.Tag(0))
                Dim y As Integer = Val(cntrl.Tag(2))

                If x = selectedPiecePosition.X AndAlso y = selectedPiecePosition.Y Then
                    ClickedPicture.Image = DirectCast(cntrl, PictureBox).Image
                    DirectCast(cntrl, PictureBox).Image = Nothing
                    Exit For
                End If
            End If
        Next
    End Sub

    Public Enum ChessColour
        Black
        White
    End Enum

    Public Class ChessBoard
        Public Board As ChessPiece(,) = New ChessPiece(7, 7) {}
        Public Pieces As New List(Of ChessPiece)
        Public BlackPieces As New List(Of ChessPiece)
        Public WhitePieces As New List(Of ChessPiece)
        Public MoveStack As New Stack(Of ChessMove)
        Public WhiteTurn As Boolean = True

        Public Sub InitializeBoard()
            'File is basically the x coordinate
            Dim Kings(1) As King
            For file As Integer = 0 To 7
                Select Case file
                    Case 0, 7 ' Rooks
                        Board(file, 0) = New Rook With
                            {.Colour = ChessColour.Black,
                            .PosX = file,
                            .PosY = 0}
                        Board(file, 7) = New Rook With
                            {.Colour = ChessColour.White,
                            .PosX = file,
                            .PosY = 7}
                    Case 1, 6 ' Knights
                        Board(file, 0) = New Knight With
                            {.Colour = ChessColour.Black,
                            .PosX = file,
                            .PosY = 0}
                        Board(file, 7) = New Knight With
                            {.Colour = ChessColour.White,
                             .PosX = file,
                             .PosY = 7}
                    Case 2, 5 ' Bishops
                        Board(file, 0) = New Bishop With
                            {.Colour = ChessColour.Black,
                            .PosX = file,
                            .PosY = 0}
                        Board(file, 7) = New Bishop With
                           {.Colour = ChessColour.White,
                           .PosX = file,
                           .PosY = 7}
                    Case 3 ' Queen
                        Board(file, 0) = New Queen With
                            {.Colour = ChessColour.Black,
                            .PosX = file,
                            .PosY = 0}
                        Board(file, 7) = New Queen With
                            {.Colour = ChessColour.White,
                            .PosX = file,
                            .PosY = 7}
                    Case 4 ' King
                        Board(file, 0) = New King With
                            {.Colour = ChessColour.Black,
                            .PosX = file,
                            .PosY = 0}

                        Board(file, 7) = New King With
                           {.Colour = ChessColour.White,
                           .PosX = file,
                           .PosY = 7}
                        Kings(0) = Board(file, 0)
                        Kings(1) = Board(file, 7)
                End Select
                ' Initialize black and white pawns
                Board(file, 1) = New Pawn With
                            {.Colour = ChessColour.Black,
                            .PosX = file,
                            .PosY = 1}
                Board(file, 6) = New Pawn With
                            {.Colour = ChessColour.White,
                            .PosX = file,
                            .PosY = 6}
            Next

            For x = 0 To 7
                For y = 0 To 7
                    If Board(x, y) IsNot Nothing Then
                        Select Case Board(x, y).Colour
                            Case ChessColour.Black
                                BlackPieces.Add(Board(x, y))
                                Board(x, y).King = Kings(0)
                                Board(x, y).OpposingKing = Kings(1)
                            Case Else
                                WhitePieces.Add(Board(x, y))
                                Board(x, y).King = Kings(1)
                                Board(x, y).OpposingKing = Kings(0)
                        End Select
                        Pieces.Add(Board(x, y))
                    End If
                Next
            Next
        End Sub
        Function DeliveredCheck(Piece As ChessPiece)
            Dim King As King = Piece.OpposingKing
            Piece.UpdatePseudoLegalMoves(Me)
            If Piece.LegalMoves(King.PosX, King.PosY) Then
                Return True
            End If
            Return False
        End Function
        Function DeliveredCheckMate(Piece As ChessPiece)
            Dim King As King = Piece.OpposingKing
            Dim ListToSearch As List(Of ChessPiece)
            If Piece.Colour = ChessColour.Black Then
                ListToSearch = WhitePieces
            Else
                ListToSearch = BlackPieces
            End If
            For Each ChessPiece In ListToSearch
                ChessPiece.UpdateLegalMoves(Me)
                For y = 0 To 7
                    For x = 0 To 7
                        If Not ChessPiece.Out AndAlso ChessPiece.LegalMoves(x, y) Then
                            MovePiece(New Point(x, y), New Point(ChessPiece.PosX, ChessPiece.PosY))
                            If KingInCheck(ChessPiece) = False Then
                                UndoMove()
                                ChessPiece.UpdateLegalMoves(Me)
                                Return False
                            End If
                            UndoMove()
                            ChessPiece.UpdateLegalMoves(Me)

                        End If
                    Next
                Next
            Next
            Return True
        End Function

        Public Function KingInCheck(Piece As ChessPiece)
            Dim King As King = Piece.King
            Dim ListToSearch As List(Of ChessPiece)
            If Piece.Colour = ChessColour.Black Then
                ListToSearch = WhitePieces
            Else
                ListToSearch = BlackPieces
            End If

            For Each ChessPiece In ListToSearch
                ChessPiece.UpdatePseudoLegalMoves(Me)
                If Not ChessPiece.Out AndAlso ChessPiece.LegalMoves(King.PosX, King.PosY) Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Sub MovePiece(SpotToFill As Point, OldPieceSpot As Point)
            'Putting the new piece in the old pieces spot and removing the new piece from where it was
            Dim MovingPiece As ChessPiece = GetPiece(OldPieceSpot)
            Dim TakenPiece As ChessPiece = GetPiece(SpotToFill)
            Dim Move As New ChessMove With {
                .SpotToFill = SpotToFill,
                .OldPieceSpot = OldPieceSpot,
                .TakenPiece = TakenPiece,
                .MovingPiece = MovingPiece
            }
            MoveStack.Push(Move)
            If MovingPiece.GetType = GetType(Pawn) Then
                DirectCast(MovingPiece, Pawn).HasMoved = True
                If Math.Abs(SpotToFill.Y - OldPieceSpot.Y) = 2 Then
                    DirectCast(MovingPiece, Pawn).JustMoved2 = True
                Else
                    DirectCast(MovingPiece, Pawn).JustMoved2 = False
                End If
            ElseIf MovingPiece.GetType = GetType(King) Then
                DirectCast(MovingPiece, King).HasMoved = True
            ElseIf MovingPiece.GetType = GetType(Rook) Then
                DirectCast(MovingPiece, Rook).HasMoved = True
            End If
            'Different for castling
            If TakenPiece IsNot Nothing AndAlso MovingPiece.GetType() = GetType(King) AndAlso TakenPiece.GetType = GetType(Rook) AndAlso MovingPiece.Colour = TakenPiece.Colour Then
                Castle(MovingPiece, TakenPiece)
                'different for en passant
            ElseIf MovingPiece.GetType = GetType(Pawn) AndAlso TakenPiece Is Nothing AndAlso SpotToFill.X <> OldPieceSpot.X Then
                EnPassant(SpotToFill, OldPieceSpot, MovingPiece)
            Else

                'The spot was empty so we can do a normal swap
                If TakenPiece IsNot Nothing Then
                    TakenPiece.Out = True
                End If

                Board(SpotToFill.X, SpotToFill.Y) = MovingPiece
                Board(OldPieceSpot.X, OldPieceSpot.Y) = Nothing
                MovingPiece.PosX = SpotToFill.X
                MovingPiece.PosY = SpotToFill.Y

                'After we do a move every other pawn of that colour didnt (just move 2) as it would have moved 2 on the last turn
                Dim ListToSearch As List(Of ChessPiece)
                If MovingPiece.Colour = ChessColour.Black Then
                    ListToSearch = BlackPieces
                Else
                    ListToSearch = WhitePieces
                End If
                For Each p As ChessPiece In ListToSearch
                    If Not p.Out AndAlso p.GetType() = GetType(Pawn) Then
                        If p IsNot MovingPiece Then
                            DirectCast(p, Pawn).JustMoved2 = False
                        End If
                    End If
                Next

            End If

            WhiteTurn = Not WhiteTurn
        End Sub

        Public Sub UndoMove()
            Dim Move As ChessMove = MoveStack.Pop()
            Dim OldPiece As ChessPiece = Move.TakenPiece
            Dim MovingPiece As ChessPiece = Move.MovingPiece
            Dim OldPieceSpot As Point = Move.OldPieceSpot
            Dim SpotToFill As Point = Move.SpotToFill
            MovingPiece.PosX = OldPieceSpot.X
            MovingPiece.PosY = OldPieceSpot.Y
            Board(OldPieceSpot.X, OldPieceSpot.Y) = Board(SpotToFill.X, SpotToFill.Y)
            Board(SpotToFill.X, SpotToFill.Y) = OldPiece
            MovingPiece.ResetAttributes(MoveStack)
            If OldPiece IsNot Nothing Then
                OldPiece.Out = False
            End If
            WhiteTurn = Not WhiteTurn
        End Sub
        Public Sub EnPassant(SpotToFill As Point, OldPieceSpot As Point, MovingPiece As ChessPiece)
            If MovingPiece.Colour = ChessColour.White Then
                Board(SpotToFill.X, SpotToFill.Y + 1) = Nothing
                Board(SpotToFill.X, SpotToFill.Y) = MovingPiece
                Board(OldPieceSpot.X, OldPieceSpot.Y) = Nothing

                Dim PieceToRemove = Pieces.Find(Function(item) item.PosX = SpotToFill.X AndAlso item.PosY = SpotToFill.Y + 1)
                PieceToRemove.Out = True
            Else
                Board(SpotToFill.X, SpotToFill.Y - 1) = Nothing
                Board(SpotToFill.X, SpotToFill.Y) = MovingPiece
                Board(OldPieceSpot.X, OldPieceSpot.Y) = Nothing
                Dim PieceToRemove = Pieces.Find(Function(item) item.PosX = SpotToFill.X AndAlso item.PosY = SpotToFill.Y - 1)
                PieceToRemove.Out = True


            End If
            Board(SpotToFill.X, SpotToFill.Y).PosX = SpotToFill.X
            Board(SpotToFill.X, SpotToFill.Y).PosY = SpotToFill.Y
        End Sub
        Public Sub Castle(ByRef King As King, ByRef Rook As Rook)
            Board(King.PosX, King.PosY) = Nothing
            Board(Rook.PosX, Rook.PosY) = Nothing
            If Rook.PosX < King.PosX Then
                Board(King.PosX - 2, King.PosY) = King
                Board(Rook.PosX + 3, King.PosY) = Rook
                King.PosX -= 2
                Rook.PosX += 3
            Else
                Board(King.PosX + 2, King.PosY) = King
                Board(Rook.PosX - 2, Rook.PosY) = Rook
                King.PosX += 2
                Rook.PosX -= 2
            End If
            King.HasMoved = True
            Rook.HasMoved = True
        End Sub

        Public Function CanMove(SpotToFill As Point, OldPieceSpot As Point)
            Dim Piece As ChessPiece = GetPiece(OldPieceSpot)
            Dim LegalMove As Boolean
            If Piece.Colour = ChessColour.White AndAlso Not WhiteTurn Then
                Return False
            ElseIf Piece.Colour = ChessColour.Black AndAlso WhiteTurn Then
                Return False
            End If

            LegalMove = Piece.GetIfLegalMove(SpotToFill, Me)

            If LegalMove Then
                If KingInCheck(Piece) Then
                    Dim Move As New ChessMove With {
                    .SpotToFill = SpotToFill,
                    .OldPieceSpot = OldPieceSpot,
                    .MovingPiece = GetPiece(OldPieceSpot),
                    .TakenPiece = GetPiece(SpotToFill)
                }

                    MovePiece(SpotToFill, OldPieceSpot)

                    If KingInCheck(Piece) Then
                        LegalMove = False
                    Else
                        LegalMove = True
                    End If
                    UndoMove()
                End If
            End If
            Return LegalMove

        End Function

        Public Function GetPiece(position As Point) As ChessPiece
            Return Board(position.X, position.Y)
        End Function
    End Class

    Public Structure ChessMove
        Public SpotToFill As Point
        Public OldPieceSpot As Point
        Public MovingPiece As ChessPiece
        Public TakenPiece As ChessPiece
    End Structure

    Public MustInherit Class ChessPiece
        Public PosX As Integer
        Public PosY As Integer
        Public Colour As ChessColour
        Public Pinned As Boolean = False
        Public LegalMoves(7, 7) As Boolean
        Public King As King
        Public OpposingKing As King
        Public Out As Boolean = False
        Public Sub UpdateLegalMoves(Board As ChessBoard)
            UpdatePseudoLegalMoves(Board)
            UpdatePinned(Board)
        End Sub
        Public MustOverride Sub UpdatePseudoLegalMoves(Board As ChessBoard)
        Protected Sub ResetLegalMoves()
            For X = 0 To 7
                For Y = 0 To 7
                    LegalMoves(X, Y) = False
                Next
            Next
        End Sub
        Public Sub ShowOnlyCheckBlocks(Board As ChessBoard)
            Dim ListToSearch As List(Of ChessPiece)
            If Colour = ChessColour.Black Then
                ListToSearch = Board.WhitePieces
            Else
                ListToSearch = Board.BlackPieces
            End If
            Dim MovesThatBlock As New List(Of ChessMove)
            For Each Piece In ListToSearch
                If Piece.LegalMoves(King.PosX, King.PosY) Then
                    For x = 0 To 7
                        For y = 0 To 7
                            Dim SpotToFill = New Point(x, y)
                            Dim OldPieceSpot = New Point(PosX, PosY)
                            If Board.CanMove(SpotToFill, OldPieceSpot) Then
                                Board.MovePiece(SpotToFill, OldPieceSpot)
                                If Board.KingInCheck(Me) = False Then
                                    Dim Move As New ChessMove With {
                                        .MovingPiece = Me,
                                        .TakenPiece = Piece,
                                        .OldPieceSpot = OldPieceSpot,
                                        .SpotToFill = SpotToFill}
                                    MovesThatBlock.Add(Move)
                                End If
                                Board.UndoMove()
                            End If
                        Next
                    Next
                End If
            Next
            ResetLegalMoves()
            For Each M In MovesThatBlock
                LegalMoves(M.SpotToFill.X, M.SpotToFill.Y) = True
            Next
        End Sub
        Public Overridable Sub ResetAttributes(MoveStack As Stack(Of ChessMove))
            Out = False
        End Sub

        Private Sub UpdatePinned(Board As ChessBoard)
            Pinned = False
            Dim ListToSearch As List(Of ChessPiece)
            If Colour = ChessColour.Black Then
                ListToSearch = Board.WhitePieces
            Else
                ListToSearch = Board.BlackPieces
            End If
            Dim MovesCanMake As New List(Of ChessMove)
            For Each Piece As ChessPiece In ListToSearch
                Dim T As Type = Piece.GetType()
                If Not Piece.Out AndAlso T = GetType(Bishop) OrElse T = GetType(Rook) OrElse T = GetType(Queen) Then
                    Piece.UpdatePseudoLegalMoves(Board)
                    If Piece.LegalMoves(PosX, PosY) AndAlso Not Piece.LegalMoves(King.PosX, King.PosY) Then
                        Board.Board(PosX, PosY) = Nothing
                        If Board.KingInCheck(Me) Then
                            Pinned = True
                            Board.Board(PosX, PosY) = Me
                            If LegalMoves(Piece.PosX, Piece.PosY) Then
                                Board.MovePiece(New Point(Piece.PosX, Piece.PosY), New Point(PosX, PosY))
                                If Not Board.KingInCheck(Me) Then
                                    Dim Move As New ChessMove With {
                                    .SpotToFill = New Point(Piece.PosX, Piece.PosY),
                                    .TakenPiece = Piece,
                                    .MovingPiece = Me,
                                    .OldPieceSpot = New Point(PosX, PosY)}
                                    MovesCanMake.Add(Move)
                                End If
                            End If
                            Board.UndoMove()
                            End If
                            Board.Board(PosX, PosY) = Me
                    End If
                End If
            Next
            If Pinned Then
                ResetLegalMoves()
                For Each M In MovesCanMake
                    LegalMoves(M.SpotToFill.X, M.SpotToFill.Y) = True
                Next
            End If
        End Sub
        Public Function GetIfLegalMove(SpotToFill As Point, Board As ChessBoard) As Boolean
            UpdateLegalMoves(Board)
            Return LegalMoves(SpotToFill.X, SpotToFill.Y)
        End Function
        Protected Sub CheckBottomRight(Board As ChessBoard)
            Dim col, row As Integer
            'Below to right
            row = PosY + 1
            col = PosX + 1
            While row <= 7 AndAlso col <= 7
                If Board.Board(col, row) Is Nothing Then
                    LegalMoves(col, row) = True

                ElseIf Board.Board(col, row).Colour <> Colour Then
                    LegalMoves(col, row) = True
                    Exit While
                Else
                    Exit While
                End If
                row += 1
                col += 1
            End While
        End Sub

        Protected Sub CheckBottomLeft(Board As ChessBoard)
            Dim col, row As Integer
            'Below to left
            row = PosY + 1
            col = PosX - 1
            While row <= 7 AndAlso col >= 0
                If Board.Board(col, row) Is Nothing Then
                    LegalMoves(col, row) = True

                ElseIf Board.Board(col, row).Colour <> Colour Then
                    LegalMoves(col, row) = True
                    Exit While
                Else
                    Exit While
                End If
                row += 1
                col -= 1
            End While
        End Sub

        Protected Sub CheckTopRight(Board As ChessBoard)
            Dim col, row As Integer
            'Above to right
            row = PosY - 1
            col = PosX + 1
            While row >= 0 AndAlso col <= 7
                If Board.Board(col, row) Is Nothing Then
                    LegalMoves(col, row) = True
                ElseIf Board.Board(col, row).Colour <> Colour Then
                    LegalMoves(col, row) = True
                    Exit While
                Else
                    Exit While
                End If
                row -= 1
                col += 1
            End While
        End Sub

        Protected Sub CheckTopLeft(Board As ChessBoard)
            Dim col, row As Integer
            'Above to left
            row = PosY - 1
            col = PosX - 1
            While row >= 0 AndAlso col >= 0
                If Board.Board(col, row) Is Nothing Then
                    LegalMoves(col, row) = True

                ElseIf Board.Board(col, row).Colour <> Colour Then
                    LegalMoves(col, row) = True
                    Exit While
                Else
                    Exit While
                End If
                row -= 1
                col -= 1
            End While
        End Sub
        Protected Sub CheckDownwards(Board As ChessBoard)
            'Checking downwards
            For row = PosY + 1 To 7
                If Board.Board(PosX, row) Is Nothing Then
                    LegalMoves(PosX, row) = True
                ElseIf Board.Board(PosX, row).Colour <> Colour Then
                    LegalMoves(PosX, row) = True
                    Exit For
                Else
                    Exit For

                End If
            Next
        End Sub

        Protected Sub CheckUpwards(Board As ChessBoard)
            'Checking upwards
            For row = PosY - 1 To 0 Step -1
                If Board.Board(PosX, row) Is Nothing Then
                    LegalMoves(PosX, row) = True
                ElseIf Board.Board(PosX, row).Colour <> Colour Then
                    LegalMoves(PosX, row) = True
                    Exit For

                Else
                    Exit For

                End If
            Next
        End Sub
        Protected Sub CheckLeft(Board As ChessBoard)
            'Checking left
            For col = PosX - 1 To 0 Step -1
                If PosX = 0 Then
                    Exit For
                End If
                If Board.Board(col, PosY) Is Nothing Then
                    LegalMoves(col, PosY) = True
                ElseIf Board.Board(col, PosY).Colour <> Colour Then
                    LegalMoves(col, PosY) = True
                    Exit For
                Else
                    Exit For

                End If
            Next
        End Sub

        Protected Sub CheckRight(board As ChessBoard)
            'checking right
            For col = PosX + 1 To 7
                If PosX = 7 Then
                    Exit For
                End If
                If board.Board(col, PosY) Is Nothing Then
                    LegalMoves(col, PosY) = True

                ElseIf board.Board(col, PosY).Colour <> Colour Then
                    LegalMoves(col, PosY) = True
                    Exit For

                Else
                    Exit For

                End If
            Next
        End Sub
    End Class

    Public Class Pawn
        Inherits ChessPiece
        'If it hasnt moved it can move 2 squares
        Public HasMoved As Boolean = False
        Public JustMoved2 As Boolean = False

        Public Overrides Sub ResetAttributes(MoveStack As Stack(Of ChessMove))
            MyBase.ResetAttributes(MoveStack)
            Dim Moved = False
            For Each M In MoveStack
                If M.MovingPiece Is Me Then
                    Moved = True
                    Exit For
                End If
            Next
            If Not Moved Then
                HasMoved = False
                Dim LastMove As ChessMove = MoveStack.Peek
                If LastMove.MovingPiece Is Me AndAlso Math.Abs(LastMove.SpotToFill.Y - LastMove.OldPieceSpot.Y) = 2 Then
                    JustMoved2 = True
                Else
                    JustMoved2 = False
                End If
            Else
                HasMoved = False
            End If
        End Sub
        Public Overrides Sub UpdatePseudoLegalMoves(Board As ChessBoard)
            ResetLegalMoves()
            Dim Offset1 As Integer
            Dim Offset2 As Integer
            If Colour = ChessColour.Black Then
                Offset1 = 1
                Offset2 = 2
            Else
                Offset1 = -1
                Offset2 = -2
            End If

            Try
                If Board.Board(PosX, PosY + Offset1) Is Nothing Then
                    LegalMoves(PosX, PosY + Offset1) = True
                End If
                If HasMoved = False Then
                    If Board.Board(PosX, PosY + Offset2) Is Nothing AndAlso Board.Board(PosX, PosY + Offset1) Is Nothing Then
                        LegalMoves(PosX, PosY + Offset2) = True
                    End If
                End If
            Catch

            End Try

            Try
                If PosX = 0 Then
                    Dim PieceUpperRight As ChessPiece = Board.Board(PosX + 1, PosY + Offset1)
                    If PieceUpperRight Is Nothing Then
                        If Board.Board(PosX + 1, PosY) IsNot Nothing Then
                            If Board.Board(PosX + 1, PosY).GetType = GetType(Pawn) AndAlso Board.Board(PosX + 1, PosY).Colour <> Colour Then
                                If DirectCast(Board.Board(PosX + 1, PosY), Pawn).JustMoved2 = True Then
                                    LegalMoves(PosX + 1, PosY - 1) = True
                                End If
                            End If
                        End If
                    ElseIf PieceUpperRight.Colour <> Colour Then
                        LegalMoves(PosX + 1, PosY + Offset1) = True
                    End If

                ElseIf PosX = 7 Then
                    Dim PieceUpperLeft As ChessPiece = Board.Board(PosX - 1, PosY + Offset1)
                    If PieceUpperLeft Is Nothing Then
                        If Board.Board(PosX - 1, PosY) IsNot Nothing Then
                            If Board.Board(PosX - 1, PosY).GetType = GetType(Pawn) AndAlso Board.Board(PosX - 1, PosY).Colour <> Colour Then
                                If DirectCast(Board.Board(PosX - 1, PosY), Pawn).JustMoved2 = True Then
                                    LegalMoves(PosX - 1, PosY + Offset1) = True
                                End If
                            End If
                        End If
                    ElseIf PieceUpperLeft.Colour <> Colour Then
                        LegalMoves(PosX - 1, PosY - 1) = True
                    End If
                Else
                    Dim PieceUpperRight As ChessPiece = Board.Board(PosX + 1, PosY + Offset1)
                    Dim PieceUpperLeft As ChessPiece = Board.Board(PosX - 1, PosY + Offset1)
                    If PieceUpperRight Is Nothing Then
                        If Board.Board(PosX + 1, PosY) IsNot Nothing Then
                            If Board.Board(PosX + 1, PosY).GetType = GetType(Pawn) AndAlso Board.Board(PosX + 1, PosY).Colour <> Colour Then
                                If DirectCast(Board.Board(PosX + 1, PosY), Pawn).JustMoved2 = True Then
                                    LegalMoves(PosX + 1, PosY + Offset1) = True
                                End If
                            End If
                        End If
                    ElseIf PieceUpperRight.Colour <> Colour Then
                        LegalMoves(PosX + 1, PosY + Offset1) = True
                    End If
                    If PieceUpperLeft Is Nothing Then
                        If Board.Board(PosX - 1, PosY) IsNot Nothing Then
                            If Board.Board(PosX - 1, PosY).GetType = GetType(Pawn) AndAlso Board.Board(PosX - 1, PosY).Colour <> Colour Then
                                If DirectCast(Board.Board(PosX - 1, PosY), Pawn).JustMoved2 = True Then
                                    LegalMoves(PosX - 1, PosY + Offset1) = True
                                End If
                            End If
                        End If
                    ElseIf PieceUpperLeft.Colour <> Colour Then
                        LegalMoves(PosX - 1, PosY + Offset1) = True
                    End If
                End If
            Catch
            End Try
        End Sub
    End Class


    Class Queen
        Inherits ChessPiece
        Public Overrides Sub UpdatePseudoLegalMoves(Board As ChessBoard)
            ResetLegalMoves()
            CheckLeft(Board)
            CheckRight(Board)
            CheckUpwards(Board)
            CheckDownwards(Board)
            CheckTopRight(Board)
            CheckTopLeft(Board)
            CheckBottomLeft(Board)
            CheckBottomRight(Board)
        End Sub
    End Class
    Class Rook
        Inherits ChessPiece
        Public HasMoved As Boolean = False
        Public Overrides Sub ResetAttributes(MoveStack As Stack(Of ChessMove))
            MyBase.ResetAttributes(MoveStack)
            Dim Moved = False
            For Each M In MoveStack
                If M.MovingPiece Is Me Then
                    Moved = True
                    Exit For
                End If
            Next
            If Not Moved Then
                HasMoved = False
            End If
        End Sub

        Public Overrides Sub UpdatePseudoLegalMoves(Board As ChessBoard)
            ResetLegalMoves()
            CheckDownwards(Board)
            CheckUpwards(Board)
            CheckLeft(Board)
            CheckRight(Board)
        End Sub
    End Class
    Class King
        Inherits ChessPiece
        Public InCheck As Boolean = False
        Public HasMoved As Boolean = False
        Public Overrides Sub ResetAttributes(MoveStack As Stack(Of ChessMove))
            MyBase.ResetAttributes(MoveStack)
            Dim Moved = False
            For Each M In MoveStack
                If M.MovingPiece Is Me Then
                    Moved = True
                    Exit For
                End If
            Next
            If Not Moved Then
                HasMoved = False
            End If
        End Sub
        Public Overrides Sub UpdatePseudoLegalMoves(Board As ChessBoard)
            ResetLegalMoves()
            Try
                If Board.Board(PosX + 1, PosY) Is Nothing OrElse Board.Board(PosX + 1, PosY).Colour <> Colour Then
                    LegalMoves(PosX + 1, PosY) = True
                End If
            Catch ex As Exception
            End Try
            Try
                If Board.Board(PosX - 1, PosY) Is Nothing OrElse Board.Board(PosX - 1, PosY).Colour <> Colour Then
                    LegalMoves(PosX - 1, PosY) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX, PosY + 1) Is Nothing OrElse Board.Board(PosX, PosY + 1).Colour <> Colour Then
                    LegalMoves(PosX, PosY + 1) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX, PosY - 1) Is Nothing OrElse Board.Board(PosX, PosY - 1).Colour <> Colour Then
                    LegalMoves(PosX, PosY - 1) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX + 1, PosY - 1) Is Nothing OrElse Board.Board(PosX + 1, PosY - 1).Colour <> Colour Then
                    LegalMoves(PosX + 1, PosY - 1) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX + 1, PosY + 1) Is Nothing OrElse Board.Board(PosX + 1, PosY + 1).Colour <> Colour Then
                    LegalMoves(PosX + 1, PosY + 1) = True
                End If
            Catch ex As Exception
            End Try
            Try
                If Board.Board(PosX - 1, PosY - 1) Is Nothing OrElse Board.Board(PosX - 1, PosY - 1).Colour <> Colour Then
                    LegalMoves(PosX - 1, PosY - 1) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX - 1, PosY + 1) Is Nothing OrElse Board.Board(PosX - 1, PosY + 1).Colour <> Colour Then
                    LegalMoves(PosX - 1, PosY + 1) = True
                End If
            Catch ex As Exception
            End Try
            Dim ListToSearch As List(Of ChessPiece)
            If Colour = ChessColour.Black Then
                ListToSearch = Board.WhitePieces
            Else
                ListToSearch = Board.BlackPieces
            End If

            For Each Piece In ListToSearch
                If Not Piece.Out Then
                    Select Case Piece.GetType
                        Case GetType(King)
                            Try
                                LegalMoves(Piece.PosX - 1, Piece.PosY - 1) = False
                            Catch ex As Exception
                            End Try
                            Try
                                LegalMoves(Piece.PosX - 1, Piece.PosY) = False
                            Catch ex As Exception
                            End Try
                            Try
                                LegalMoves(Piece.PosX - 1, Piece.PosY + 1) = False
                            Catch ex As Exception
                            End Try
                            Try
                                LegalMoves(Piece.PosX, Piece.PosY - 1) = False
                            Catch ex As Exception
                            End Try
                            Try
                                LegalMoves(Piece.PosX, Piece.PosY + 1) = False
                            Catch ex As Exception
                            End Try
                            Try
                                LegalMoves(Piece.PosX + 1, Piece.PosY - 1) = False
                            Catch ex As Exception
                            End Try
                            Try
                                LegalMoves(Piece.PosX + 1, Piece.PosY + 1) = False
                            Catch ex As Exception
                            End Try
                            Try
                                LegalMoves(Piece.PosX + 1, Piece.PosY) = False
                            Catch ex As Exception
                            End Try
                        Case GetType(Pawn)
                            If Colour = ChessColour.White Then 'the pawn would be black
                                Dim DownLeft As New Point(Piece.PosX - 1, Piece.PosY + 1)
                                Dim DownRight As New Point(Piece.PosX + 1, Piece.PosY + 1)
                                Try
                                    LegalMoves(DownLeft.X, DownLeft.Y) = False
                                Catch ex As Exception

                                End Try

                                Try
                                    LegalMoves(DownRight.X, DownRight.Y) = False
                                Catch ex As Exception

                                End Try

                            Else
                                Dim UpLeft As New Point(Piece.PosX - 1, Piece.PosY - 1)
                                Dim UpRight As New Point(Piece.PosX + 1, Piece.PosY - 1)
                                Try
                                    LegalMoves(UpLeft.X, UpLeft.Y) = False
                                Catch ex As Exception

                                End Try

                                Try
                                    LegalMoves(UpRight.X, UpRight.Y) = False
                                Catch ex As Exception

                                End Try
                            End If
                        Case Else
                            Piece.UpdatePseudoLegalMoves(Board)
                            For i = 0 To 7
                                For j = 0 To 7
                                    If Piece.GetType IsNot GetType(King) Then
                                        If Piece.LegalMoves(i, j) Then
                                            LegalMoves(i, j) = False
                                        End If
                                    End If
                                Next
                            Next
                    End Select
                End If
            Next


            If Not HasMoved Then
                Dim LeftEmpty As Boolean = True
                Dim RightEmpty As Boolean = True
                For x = PosX - 1 To 1 Step -1
                    If Board.Board(x, PosY) IsNot Nothing Then
                        LeftEmpty = False
                        Exit For
                    End If
                Next
                For x = PosX + 1 To 6
                    If Board.Board(x, PosY) IsNot Nothing Then
                        RightEmpty = False
                        Exit For
                    End If
                Next
                Dim CanShortCastle As Boolean = True
                Dim CanLongCastle As Boolean = True
                If Colour = ChessColour.Black Then
                    ListToSearch = Board.WhitePieces
                Else
                    ListToSearch = Board.BlackPieces
                End If
                If RightEmpty Then
                    If Board.Board(PosX + 3, PosY).GetType = GetType(Rook) AndAlso DirectCast(Board.Board(PosX + 3, PosY), Rook).HasMoved = False Then
                        For x = PosX To PosX + 2
                            For Each piece In ListToSearch
                                If Not piece.Out AndAlso piece.LegalMoves(x, PosY) = True Then
                                    CanShortCastle = False
                                    Exit For
                                End If
                            Next
                        Next

                    End If
                Else
                    CanShortCastle = False
                End If

                If LeftEmpty Then
                    If Board.Board(PosX - 4, PosY).GetType = GetType(Rook) AndAlso DirectCast(Board.Board(PosX - 4, PosY), Rook).HasMoved = False Then
                        For x = PosX - 1 To PosX - 3 Step -1
                            For Each piece In ListToSearch
                                If Not piece.Out AndAlso piece.LegalMoves(x, PosY) = True Then
                                    CanLongCastle = False
                                    Exit For
                                End If
                            Next
                        Next

                    End If
                Else
                    CanLongCastle = False
                End If
                If CanLongCastle Then
                    LegalMoves(PosX - 4, PosY) = True
                End If
                If CanShortCastle Then
                    LegalMoves(PosX + 3, PosY) = True
                End If
            End If
        End Sub
    End Class


    Class Bishop
        Inherits ChessPiece

        Public Overrides Sub UpdatePseudoLegalMoves(Board As ChessBoard)
            ResetLegalMoves()
            CheckTopRight(Board)
            CheckTopLeft(Board)
            CheckBottomLeft(Board)
            CheckBottomRight(Board)
        End Sub
    End Class

    Class Knight
        Inherits ChessPiece
        Public Overrides Sub UpdatePseudoLegalMoves(Board As ChessBoard)
            ResetLegalMoves()

            Try
                If Board.Board(PosX + 2, PosY + 1) Is Nothing OrElse Board.Board(PosX + 2, PosY + 1).Colour <> Colour Then
                    LegalMoves(PosX + 2, PosY + 1) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX + 2, PosY - 1) Is Nothing OrElse Board.Board(PosX + 2, PosY - 1).Colour <> Colour Then
                    LegalMoves(PosX + 2, PosY - 1) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX - 2, PosY + 1) Is Nothing OrElse Board.Board(PosX - 2, PosY + 1).Colour <> Colour Then
                    LegalMoves(PosX - 2, PosY + 1) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX - 2, PosY - 1) Is Nothing OrElse Board.Board(PosX - 2, PosY - 1).Colour <> Colour Then
                    LegalMoves(PosX - 2, PosY - 1) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX + 1, PosY + 2) Is Nothing OrElse Board.Board(PosX + 1, PosY + 2).Colour <> Colour Then
                    LegalMoves(PosX + 1, PosY + 2) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX - 1, PosY + 2) Is Nothing OrElse Board.Board(PosX - 1, PosY + 2).Colour <> Colour Then
                    LegalMoves(PosX - 1, PosY + 2) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX + 1, PosY - 2) Is Nothing OrElse Board.Board(PosX + 1, PosY - 2).Colour <> Colour Then
                    LegalMoves(PosX + 1, PosY - 2) = True
                End If
            Catch ex As Exception

            End Try
            Try
                If Board.Board(PosX - 1, PosY - 2) Is Nothing OrElse Board.Board(PosX - 1, PosY - 2).Colour <> Colour Then
                    LegalMoves(PosX - 1, PosY - 2) = True
                End If
            Catch ex As Exception

            End Try
        End Sub
    End Class
End Class
