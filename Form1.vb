'The top left square has the tag coordinates (-1,-1) because a coordinate of (0,0) would be the same as an empty point
Public Class Form1
    Private SelectedPiecePosition As Point
    Public Board As New ChessBoard
    Public PicTable As New Dictionary(Of Point, PictureBox)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Board.InitializeBoard()
        PicTable(New Point(0, 0)) = PicA8
        PicTable(New Point(1, 0)) = PicB8
        PicTable(New Point(2, 0)) = PicC8
        PicTable(New Point(3, 0)) = PicD8
        PicTable(New Point(4, 0)) = PicE8
        PicTable(New Point(5, 0)) = PicF8
        PicTable(New Point(6, 0)) = PicG8
        PicTable(New Point(7, 0)) = PicH8
        PicTable(New Point(0, 1)) = PicA7
        PicTable(New Point(1, 1)) = PicB7
        PicTable(New Point(2, 1)) = PicC7
        PicTable(New Point(3, 1)) = PicD7
        PicTable(New Point(4, 1)) = PicE7
        PicTable(New Point(5, 1)) = PicF7
        PicTable(New Point(6, 1)) = PicG7
        PicTable(New Point(7, 1)) = PicH7
        PicTable(New Point(0, 2)) = PicA6
        PicTable(New Point(1, 2)) = PicB6
        PicTable(New Point(2, 2)) = PicC6
        PicTable(New Point(3, 2)) = PicD6
        PicTable(New Point(4, 2)) = PicE6
        PicTable(New Point(5, 2)) = PicF6
        PicTable(New Point(6, 2)) = PicG6
        PicTable(New Point(7, 2)) = PicH6
        PicTable(New Point(0, 3)) = PicA5
        PicTable(New Point(1, 3)) = PicB5
        PicTable(New Point(2, 3)) = PicC5
        PicTable(New Point(3, 3)) = PicD5
        PicTable(New Point(4, 3)) = PicE5
        PicTable(New Point(5, 3)) = PicF5
        PicTable(New Point(6, 3)) = PicG5
        PicTable(New Point(7, 3)) = PicH5
        PicTable(New Point(0, 4)) = PicA4
        PicTable(New Point(1, 4)) = PicB4
        PicTable(New Point(2, 4)) = PicC4
        PicTable(New Point(3, 4)) = PicD4
        PicTable(New Point(4, 4)) = PicE4
        PicTable(New Point(5, 4)) = PicF4
        PicTable(New Point(6, 4)) = PicG4
        PicTable(New Point(7, 4)) = PicH4
        PicTable(New Point(0, 5)) = PicA3
        PicTable(New Point(1, 5)) = PicB3
        PicTable(New Point(2, 5)) = PicC3
        PicTable(New Point(3, 5)) = PicD3
        PicTable(New Point(4, 5)) = PicE3
        PicTable(New Point(5, 5)) = PicF3
        PicTable(New Point(6, 5)) = PicG3
        PicTable(New Point(7, 5)) = PicH3
        PicTable(New Point(0, 6)) = PicA2
        PicTable(New Point(1, 6)) = PicB2
        PicTable(New Point(2, 6)) = PicC2
        PicTable(New Point(3, 6)) = PicD2
        PicTable(New Point(4, 6)) = PicE2
        PicTable(New Point(5, 6)) = PicF2
        PicTable(New Point(6, 6)) = PicG2
        PicTable(New Point(7, 6)) = PicH2
        PicTable(New Point(0, 7)) = PicA1
        PicTable(New Point(1, 7)) = PicB1
        PicTable(New Point(2, 7)) = PicC1
        PicTable(New Point(3, 7)) = PicD1
        PicTable(New Point(4, 7)) = PicE1
        PicTable(New Point(5, 7)) = PicF1
        PicTable(New Point(6, 7)) = PicG1
        PicTable(New Point(7, 7)) = PicH1
    End Sub
    Private Sub ChessSquare_Click(sender As Object, e As EventArgs) Handles PicA8.Click, PicB8.Click, PicC8.Click, PicD8.Click, PicE8.Click, PicF8.Click, PicG8.Click, PicH8.Click, PicH7.Click, PicG7.Click, PicF7.Click, PicE7.Click, PicD7.Click, PicC7.Click, PicB7.Click, PicA7.Click, PicH5.Click, PicG5.Click, PicF5.Click, PicE5.Click, PicD5.Click, PicC5.Click, PicB5.Click, PicA5.Click, PicH6.Click, PicG6.Click, PicF6.Click, PicE6.Click, PicD6.Click, PicC6.Click, PicB6.Click, PicA6.Click, PicH2.Click, PicG2.Click, PicF2.Click, PicE2.Click, PicD2.Click, PicC2.Click, PicB2.Click, PicA2.Click, PicH3.Click, PicG3.Click, PicF3.Click, PicE3.Click, PicD3.Click, PicC3.Click, PicB3.Click, PicA3.Click, PicH4.Click, PicG4.Click, PicF4.Click, PicE4.Click, PicD4.Click, PicC4.Click, PicB4.Click, PicA4.Click, PicH1.Click, PicG1.Click, PicF1.Click, PicE1.Click, PicD1.Click, PicC1.Click, PicB1.Click, PicA1.Click
        Dim ClickedPicture As PictureBox = DirectCast(sender, PictureBox)
        Dim ClickedPosition As New Point
        Dim ClickedPiece As ChessPiece
        Dim SelectedPiece As ChessPiece = If(SelectedPiecePosition.X = -1, Board.GetPiece(New Point(0, 0)), Board.GetPiece(SelectedPiecePosition))

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
            If ClickedPiece IsNot Nothing Then
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
                DidGameEnd(SelectedPiece)
            End If
        End If

        SelectedPiecePosition = Point.Empty
    End Sub
    Public Sub ShowMovesOnUI(Piece As ChessPiece)
        'Consider returning that piece that checked when using king in check to speed it up
        Dim Result As (Boolean, ChessPiece) = Board.KingInCheck(Piece)
        If Result.Item1 Then
            Piece.ShowOnlyCheckBlocks(Board, Result.Item2)
        Else
            Piece.UpdateLegalMoves(Board)
        End If

        For y = 0 To 7
            For x = 0 To 7
                If Piece.LegalMoves(x, y) Then
                    PicTable(New Point(x, y)).BackColor = Color.Red
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
        Dim XofClick As Integer = Val(ClickedPicture.Tag(0))
        Dim YofClick As Integer = Val(ClickedPicture.Tag(2))
        If XofClick = -1 Then
            XofClick = 0
            YofClick = 0
        End If

        Dim MovingPiece As ChessPiece = Board.GetPiece(selectedPiecePosition)
        Dim TakenPiece As ChessPiece = Board.GetPiece(New Point(XofClick, YofClick))

        If MovingPiece.GetType() = GetType(King) AndAlso TakenPiece IsNot Nothing AndAlso TakenPiece.GetType() = GetType(Rook) AndAlso MovingPiece.Colour = TakenPiece.Colour Then
            CastleOnUI(MovingPiece, ClickedPicture, XofClick)

        ElseIf MovingPiece.GetType() = GetType(Pawn) AndAlso TakenPiece Is Nothing AndAlso XofClick <> MovingPiece.PosX Then
            EnPassantOnUI(MovingPiece, ClickedPicture, XofClick, YofClick)

        ElseIf MovingPiece.GetType() = GetType(Pawn) AndAlso (YofClick = 0 OrElse YofClick = 7) Then
            PawnPromotionOnUI(ClickedPicture, selectedPiecePosition)

        Else
            NormalUISwap(ClickedPicture, selectedPiecePosition)
        End If
    End Sub
    Public Sub CastleOnUI(MovingPiece As ChessPiece, ClickedPicture As PictureBox, XofClick As Integer)
        If MovingPiece.Colour = ChessColour.White Then
            If XofClick = 0 Then
                ClickedPicture.Image = Nothing
                PicE1.Image = Nothing
                PicC1.Image = My.Resources.White_King
                PicD1.Image = My.Resources.White_Rook
            Else
                PicE1.Image = Nothing
                ClickedPicture.Image = Nothing
                PicG1.Image = My.Resources.White_King
                PicF1.Image = My.Resources.White_Rook
            End If
        Else
            Dim xString As Char = ClickedPicture.Tag(0)
            If xString = "-" Then
                PicE8.Image = Nothing
                ClickedPicture.Image = Nothing
                PicC8.Image = My.Resources.Black_King
                PicD8.Image = My.Resources.Black_Rook
            Else
                PicE8.Image = Nothing
                ClickedPicture.Image = Nothing
                PicG8.Image = My.Resources.Black_King
                PicF8.Image = My.Resources.Black_Rook
            End If
        End If
    End Sub
    Public Sub EnPassantOnUI(MovingPiece As ChessPiece, ClickedPicture As PictureBox, X As Integer, Y As Integer)
        Dim OffsetY = If(MovingPiece.Colour = ChessColour.White, 1, -1)
        Dim PieceToRemove = PicTable(New Point(X, Y + OffsetY))
        Dim MovingPawn = PicTable(New Point(MovingPiece.PosX, MovingPiece.PosY))
        ClickedPicture.Image = MovingPawn.Image
        PieceToRemove.Image = Nothing
        MovingPawn.Image = Nothing
    End Sub
    Public Sub NormalUISwap(ClickedPicture As PictureBox, selectedPiecePosition As Point)
        If selectedPiecePosition.X = -1 Then
            ClickedPicture.Image = PicA8.Image
            PicA8.Image = Nothing
            Exit Sub
        End If

        ClickedPicture.Image = PicTable(selectedPiecePosition).Image
        PicTable(selectedPiecePosition).Image = Nothing
    End Sub
    Public Sub PawnPromotionOnUI(ClickedPicture As PictureBox, selectedPiecePosition As Point)
        Dim Img As Bitmap = If(Board.GetPiece(selectedPiecePosition).Colour = ChessColour.Black, My.Resources.Black_Queen, My.Resources.White_Queen)
        ClickedPicture.Image = Img
        PicTable(selectedPiecePosition).Image = Nothing
    End Sub
    Public Sub DidGameEnd(SelectedPiece As ChessPiece)
        If Board.KingInCheck(SelectedPiece.OpposingKing).Item1 Then
            If Board.PieceDeliveredCheckMate(SelectedPiece) Then
                If Board.WhiteTurn Then
                    MsgBox("Checkmate, Black Wins!!!")
                Else
                    MsgBox("Checkmate, White Wins!!!")
                End If
                End
            End If
        End If
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
        Public Function PieceDeliveredCheck(Piece As ChessPiece)
            Dim King As King = Piece.OpposingKing
            Piece.UpdatePseudoLegalMoves(Me)
            Return Piece.LegalMoves(King.PosX, King.PosY)
        End Function
        Public Function PieceDeliveredCheckMate(Piece As ChessPiece)
            Dim King As King = Piece.OpposingKing
            Dim ListToSearch = If(Piece.Colour = ChessColour.Black, WhitePieces, BlackPieces)
            For Each ChessPiece In ListToSearch
                ChessPiece.UpdateLegalMoves(Me)
                For y = 0 To 7
                    For x = 0 To 7
                        Dim SpotToFill = New Point(x, y)
                        Dim OldPieceSpot = New Point(ChessPiece.PosX, ChessPiece.PosY)
                        If Not ChessPiece.Out AndAlso ChessPiece.LegalMoves(x, y) AndAlso CanMove(SpotToFill, OldPieceSpot) Then
                            MovePiece(SpotToFill, OldPieceSpot)
                            If KingInCheck(ChessPiece).Item1 = False Then
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
        Public Function KingInCheck(Piece As ChessPiece) As (Boolean, ChessPiece)
            Dim King As King = Piece.King
            Dim ListToSearch = If(Piece.Colour = ChessColour.Black, WhitePieces, BlackPieces)
            For Each ChessPiece In ListToSearch
                ChessPiece.UpdatePseudoLegalMoves(Me)
                If Not ChessPiece.Out AndAlso ChessPiece.GetType() <> GetType(King) AndAlso ChessPiece.LegalMoves(King.PosX, King.PosY) Then
                    Return (True, ChessPiece)
                End If
            Next
            Return (False, Nothing)
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
                DirectCast(MovingPiece, Pawn).JustMoved2 = Math.Abs(SpotToFill.Y - OldPieceSpot.Y) = 2
            ElseIf MovingPiece.GetType = GetType(King) Then
                DirectCast(MovingPiece, King).HasMoved = True
            ElseIf MovingPiece.GetType = GetType(Rook) Then
                DirectCast(MovingPiece, Rook).HasMoved = True
            End If

            If MovingPiece.GetType = GetType(Pawn) AndAlso (SpotToFill.Y = 0 OrElse SpotToFill.Y = 7) Then
                NormalMove(SpotToFill, OldPieceSpot, MovingPiece, TakenPiece)
                PawnPromotion(MovingPiece, SpotToFill)
            ElseIf MovingPiece.GetType = GetType(Pawn) AndAlso TakenPiece Is Nothing AndAlso SpotToFill.X <> OldPieceSpot.X Then
                EnPassant(SpotToFill, OldPieceSpot, MovingPiece)
            ElseIf TakenPiece IsNot Nothing AndAlso MovingPiece.GetType() = GetType(King) AndAlso TakenPiece.GetType = GetType(Rook) AndAlso MovingPiece.Colour = TakenPiece.Colour Then
                'Different for castling
                Castle(MovingPiece, TakenPiece)
            Else
                NormalMove(SpotToFill, OldPieceSpot, MovingPiece, TakenPiece)
            End If

            'After we do a move every other pawn of that colour didnt (just move 2) as it would have moved 2 on the last turn
            Dim ListToSearch = If(MovingPiece.Colour = ChessColour.Black, BlackPieces, WhitePieces)
            For Each p As ChessPiece In ListToSearch
                If Not p.Out AndAlso p.GetType() = GetType(Pawn) AndAlso p IsNot MovingPiece Then
                    DirectCast(p, Pawn).JustMoved2 = False
                End If
            Next
            WhiteTurn = Not WhiteTurn
        End Sub
        Public Sub NormalMove(SpotToFill As Point, OldPieceSpot As Point, MovingPiece As ChessPiece, TakenPiece As ChessPiece)
            'The spot was empty so we can do a normal swap
            If TakenPiece IsNot Nothing Then
                TakenPiece.Out = True
            End If
            Board(SpotToFill.X, SpotToFill.Y) = MovingPiece
            Board(OldPieceSpot.X, OldPieceSpot.Y) = Nothing
            MovingPiece.PosX = SpotToFill.X
            MovingPiece.PosY = SpotToFill.Y
        End Sub

        Public Sub UndoMove()
            Dim Move As ChessMove = MoveStack.Pop()
            Dim OldPiece As ChessPiece = Move.TakenPiece
            Dim MovingPiece As ChessPiece = Move.MovingPiece
            Dim OldPieceSpot As Point = Move.OldPieceSpot
            Dim SpotToFill As Point = Move.SpotToFill

            If OldPiece IsNot Nothing AndAlso MovingPiece.GetType() = GetType(King) AndAlso OldPiece.GetType = GetType(Rook) AndAlso MovingPiece.Colour = OldPiece.Colour Then
                UndoCastle(MovingPiece, OldPiece)

            ElseIf MovingPiece.GetType = GetType(Pawn) AndAlso OldPiece Is Nothing AndAlso SpotToFill.X <> OldPieceSpot.X Then
                UndoEnPassant(SpotToFill, OldPieceSpot, MovingPiece, OldPiece)

            ElseIf MovingPiece.GetType = GetType(Pawn) AndAlso (SpotToFill.Y = 0 OrElse SpotToFill.Y = 7) Then
                UndoPawnPromotion(MovingPiece, SpotToFill, OldPieceSpot)
            Else
                MovingPiece.PosX = OldPieceSpot.X
                MovingPiece.PosY = OldPieceSpot.Y
                Board(OldPieceSpot.X, OldPieceSpot.Y) = Board(SpotToFill.X, SpotToFill.Y)
                Board(SpotToFill.X, SpotToFill.Y) = OldPiece
            End If

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
        Public Sub UndoEnPassant(SpotToFill As Point, OldPieceSpot As Point, MovingPiece As ChessPiece, OldPiece As ChessPiece)
            If MovingPiece.Colour = ChessColour.White Then
                Board(OldPieceSpot.X, OldPieceSpot.Y) = MovingPiece
                Board(SpotToFill.X, SpotToFill.Y + 1) = OldPiece
                Board(SpotToFill.X, SpotToFill.Y) = Nothing
                MovingPiece.PosX = OldPieceSpot.X
                MovingPiece.PosY = OldPieceSpot.Y
                OldPiece.PosX = SpotToFill.X
                OldPiece.PosY = SpotToFill.Y + 1
            Else
                Board(OldPieceSpot.X, OldPieceSpot.Y) = MovingPiece
                Board(SpotToFill.X, SpotToFill.Y - 1) = OldPiece
                Board(SpotToFill.X, SpotToFill.Y) = Nothing
                MovingPiece.PosX = OldPieceSpot.X
                MovingPiece.PosY = OldPieceSpot.Y
                OldPiece.PosX = SpotToFill.X
                OldPiece.PosY = SpotToFill.Y - 1
            End If
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

        Public Sub UndoCastle(MovingPiece As ChessPiece, OldPiece As ChessPiece)
            Board(MovingPiece.PosX, MovingPiece.PosY) = Nothing
            Board(OldPiece.PosX, OldPiece.PosY) = Nothing
            If OldPiece.PosX > MovingPiece.PosX Then
                Board(MovingPiece.PosX + 2, MovingPiece.PosY) = MovingPiece
                Board(OldPiece.PosX - 3, MovingPiece.PosY) = OldPiece
                MovingPiece.PosX += 2
                OldPiece.PosX -= 3
            Else
                Board(MovingPiece.PosX - 2, MovingPiece.PosY) = MovingPiece
                Board(OldPiece.PosX + 2, OldPiece.PosY) = OldPiece
                MovingPiece.PosX -= 2
                OldPiece.PosX += 2
            End If

        End Sub

        Public Sub PawnPromotion(MovingPiece As ChessPiece, SpotToFill As Point)
            Dim Colour As ChessColour = MovingPiece.Colour
            Dim ListToRemoveFrom As List(Of ChessPiece) = If(Colour = ChessColour.Black, BlackPieces, WhitePieces)
            Pieces.Remove(MovingPiece)
            ListToRemoveFrom.Remove(MovingPiece)
            MovingPiece.Out = True

            Dim Q As New Queen With {
                .Colour = Colour,
                .King = MovingPiece.King,
                .OpposingKing = MovingPiece.OpposingKing,
                .PosX = MovingPiece.PosX,
                .PosY = MovingPiece.PosY
               }

            ListToRemoveFrom.Add(Q)
            Pieces.Add(Q)
            Board(SpotToFill.X, SpotToFill.Y) = Q
            Q.UpdateLegalMoves(Me)
        End Sub

        Public Sub UndoPawnPromotion(MovingPiece As ChessPiece, SpotToFill As Point, OldPieceSpot As Point)
            Dim Colour As ChessColour = MovingPiece.Colour
            Dim ListToRemoveFrom As List(Of ChessPiece) = If(Colour = ChessColour.Black, BlackPieces, WhitePieces)
            Pieces.Remove(Board(SpotToFill.X, SpotToFill.Y))
            ListToRemoveFrom.Remove(Board(SpotToFill.X, SpotToFill.Y))

            Board(OldPieceSpot.X, OldPieceSpot.Y) = MovingPiece
            MovingPiece.Out = False
            Board(SpotToFill.X, SpotToFill.Y) = Nothing
        End Sub

        Public Function CanMove(SpotToFill As Point, OldPieceSpot As Point)
            Dim MovingPiece As ChessPiece = GetPiece(OldPieceSpot)
            Dim TakenPiece As ChessPiece = GetPiece(SpotToFill)
            Dim LegalMove As Boolean

            If MovingPiece.Colour = ChessColour.White AndAlso Not WhiteTurn Then
                Return False
            ElseIf MovingPiece.Colour = ChessColour.Black AndAlso WhiteTurn Then
                Return False
            End If
            If TakenPiece IsNot Nothing AndAlso MovingPiece.Colour = TakenPiece.Colour Then
                If MovingPiece.GetType() <> GetType(King) OrElse TakenPiece.GetType <> GetType(Rook) Then
                    Return False
                End If
            End If

            LegalMove = MovingPiece.GetIfLegalMove(SpotToFill, Me)
            If LegalMove Then
                If KingInCheck(MovingPiece).Item1 Then
                    Dim Move As New ChessMove With {
                    .SpotToFill = SpotToFill,
                    .OldPieceSpot = OldPieceSpot,
                    .MovingPiece = GetPiece(OldPieceSpot),
                    .TakenPiece = GetPiece(SpotToFill)
                }
                    MovePiece(SpotToFill, OldPieceSpot)
                    LegalMove = Not KingInCheck(MovingPiece).Item1
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
        Public MustOverride Sub UpdatePseudoLegalMoves(Board As ChessBoard)
        Protected Sub ResetLegalMoves()
            For X = 0 To 7
                For Y = 0 To 7
                    LegalMoves(X, Y) = False
                Next
            Next
        End Sub
        Public Overridable Sub ResetAttributes(MoveStack As Stack(Of ChessMove))
            Out = False
        End Sub

        Public Sub UpdateLegalMoves(Board As ChessBoard)
            UpdatePseudoLegalMoves(Board)
            UpdatePinned(Board)
        End Sub

        Public Function GetIfLegalMove(SpotToFill As Point, Board As ChessBoard) As Boolean
            UpdateLegalMoves(Board)
            Return LegalMoves(SpotToFill.X, SpotToFill.Y)
        End Function

        Public Sub ShowOnlyCheckBlocks(Board As ChessBoard, Checker As ChessPiece)
            UpdatePseudoLegalMoves(Board)
            Dim MovesThatBlock As New List(Of ChessMove)
            For x = 0 To 7
                For y = 0 To 7
                    If LegalMoves(x, y) Then
                        Dim SpotToFill = New Point(x, y)
                        Dim OldPieceSpot = New Point(PosX, PosY)
                        Dim AttackedPiece = Board.GetPiece(SpotToFill)
                        If (AttackedPiece Is Nothing OrElse AttackedPiece.Colour <> Colour) AndAlso Board.CanMove(SpotToFill, OldPieceSpot) Then
                            Board.MovePiece(SpotToFill, OldPieceSpot)
                            If x = Checker.PosX AndAlso y = Checker.PosY Then
                                Dim Move As New ChessMove With {
                                            .SpotToFill = New Point(x, y),
                                            .TakenPiece = Checker,
                                            .MovingPiece = Me,
                                            .OldPieceSpot = New Point(PosX, PosY)}
                                MovesThatBlock.Add(Move)
                            Else
                                If Not Board.PieceDeliveredCheck(Checker) Then
                                    Dim Move As New ChessMove With {
                                                .SpotToFill = New Point(x, y),
                                                .TakenPiece = Checker,
                                                .MovingPiece = Me,
                                                .OldPieceSpot = New Point(PosX, PosY)}
                                    MovesThatBlock.Add(Move)
                                End If
                            End If
                            Board.UndoMove()
                        End If
                        UpdatePseudoLegalMoves(Board)
                    End If
                Next
            Next

            ResetLegalMoves()
            For Each M In MovesThatBlock
                LegalMoves(M.SpotToFill.X, M.SpotToFill.Y) = True
            Next
        End Sub
        Private Sub UpdatePinned(Board As ChessBoard)
            Pinned = False
            Dim ListToSearch = If(Colour = ChessColour.Black, Board.WhitePieces, Board.BlackPieces)
            Dim MovesCanMake As New List(Of ChessMove)
            UpdatePseudoLegalMoves(Board)
            For Each AttackingPiece As ChessPiece In ListToSearch
                Dim T As Type = AttackingPiece.GetType()
                If Not AttackingPiece.Out AndAlso (T = GetType(Bishop) OrElse T = GetType(Rook) OrElse T = GetType(Queen)) Then
                    AttackingPiece.UpdatePseudoLegalMoves(Board)
                    If AttackingPiece.LegalMoves(PosX, PosY) AndAlso Not AttackingPiece.LegalMoves(King.PosX, King.PosY) Then
                        Board.Board(PosX, PosY) = Nothing
                        If Board.PieceDeliveredCheck(AttackingPiece) Then
                            Pinned = True
                            Board.Board(PosX, PosY) = Me
                            For x = 0 To 7
                                For y = 0 To 7
                                    Dim SpotToFill = New Point(x, y)
                                    Dim OldPieceSpot = New Point(PosX, PosY)
                                    Dim PieceAttacked = Board.GetPiece(SpotToFill)
                                    If LegalMoves(x, y) AndAlso (PieceAttacked Is Nothing OrElse PieceAttacked.Colour <> Colour) Then
                                        Board.MovePiece(SpotToFill, OldPieceSpot)
                                        If x = AttackingPiece.PosX AndAlso y = AttackingPiece.PosY Then
                                            Dim Move As New ChessMove With {
                                            .SpotToFill = SpotToFill,
                                            .TakenPiece = AttackingPiece,
                                            .MovingPiece = Me,
                                            .OldPieceSpot = OldPieceSpot}
                                            MovesCanMake.Add(Move)
                                        Else
                                            If Not Board.PieceDeliveredCheck(AttackingPiece) Then
                                                Dim Move As New ChessMove With {
                                                .SpotToFill = SpotToFill,
                                                .TakenPiece = AttackingPiece,
                                                .MovingPiece = Me,
                                                .OldPieceSpot = OldPieceSpot}
                                                MovesCanMake.Add(Move)
                                            End If
                                        End If
                                        Board.UndoMove()
                                    End If
                                Next
                            Next
                        End If
                        Board.Board(PosX, PosY) = Me
                    End If
                End If
                If Pinned Then
                    Exit For
                End If
            Next
            If Pinned Then
                ResetLegalMoves()
                For Each M In MovesCanMake
                    LegalMoves(M.SpotToFill.X, M.SpotToFill.Y) = True
                Next
            End If
        End Sub

        Protected Sub CheckBottomRight(Board As ChessBoard)
            Dim col, row As Integer
            'Below to right
            row = PosY + 1
            col = PosX + 1
            While row <= 7 AndAlso col <= 7
                If Board.Board(col, row) Is Nothing Then
                    LegalMoves(col, row) = True
                Else
                    LegalMoves(col, row) = True
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
                Else
                    LegalMoves(col, row) = True
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
                Else
                    LegalMoves(col, row) = True
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
                Else
                    LegalMoves(col, row) = True
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
                Else
                    LegalMoves(PosX, row) = True
                    Exit For
                End If
            Next
        End Sub

        Protected Sub CheckUpwards(Board As ChessBoard)
            'Checking upwards
            For row = PosY - 1 To 0 Step -1
                If Board.Board(PosX, row) Is Nothing Then
                    LegalMoves(PosX, row) = True
                Else
                    LegalMoves(PosX, row) = True
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
                Else
                    LegalMoves(col, PosY) = True
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
                Else
                    LegalMoves(col, PosY) = True
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
                JustMoved2 = LastMove.MovingPiece Is Me AndAlso Math.Abs(LastMove.SpotToFill.Y - LastMove.OldPieceSpot.Y) = 2
            Else
                HasMoved = True
            End If
        End Sub
        Private Function IsEnPassantLegal(Board As ChessBoard, PawnToRemove As ChessPiece)
            Dim Valid As Boolean = True
            Board.Board(PosX, PosY) = Nothing
            Board.Board(PawnToRemove.PosX, PawnToRemove.PosY) = Nothing
            Dim ListToSearch = If(Colour = ChessColour.Black, Board.WhitePieces, Board.BlackPieces)

            For Each Piece In ListToSearch
                If Not Piece.Out AndAlso Board.PieceDeliveredCheck(Piece) Then
                    Valid = False
                    Exit For
                End If
            Next

            Board.Board(PosX, PosY) = Me
            Board.Board(PawnToRemove.PosX, PawnToRemove.PosY) = PawnToRemove
            Return Valid
        End Function
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
                Dim PieceUpperRight As ChessPiece = Board.Board(PosX + 1, PosY + Offset1)
                If PieceUpperRight Is Nothing Then
                    If Board.Board(PosX + 1, PosY) IsNot Nothing Then
                        If Board.Board(PosX + 1, PosY).GetType = GetType(Pawn) AndAlso Board.Board(PosX + 1, PosY).Colour <> Colour Then
                            If DirectCast(Board.Board(PosX + 1, PosY), Pawn).JustMoved2 = True Then
                                If IsEnPassantLegal(Board, Board.Board(PosX + 1, PosY)) Then
                                    LegalMoves(PosX + 1, PosY + Offset1) = True
                                End If
                            End If
                        End If
                    End If
                ElseIf PieceUpperRight.Colour <> Colour Then
                    LegalMoves(PosX + 1, PosY + Offset1) = True
                End If
            Catch
            End Try

            Try
                Dim PieceUpperLeft As ChessPiece = Board.Board(PosX - 1, PosY + Offset1)
                If PieceUpperLeft Is Nothing Then
                    If Board.Board(PosX - 1, PosY) IsNot Nothing Then
                        If Board.Board(PosX - 1, PosY).GetType = GetType(Pawn) AndAlso Board.Board(PosX - 1, PosY).Colour <> Colour Then
                            If DirectCast(Board.Board(PosX - 1, PosY), Pawn).JustMoved2 = True Then
                                If IsEnPassantLegal(Board, Board.Board(PosX - 1, PosY)) Then
                                    LegalMoves(PosX - 1, PosY + Offset1) = True
                                End If
                            End If
                        End If
                    End If
                ElseIf PieceUpperLeft.Colour <> Colour Then
                    LegalMoves(PosX - 1, PosY + Offset1) = True
                End If
            Catch
            End Try
        End Sub
    End Class

    Public Class Queen
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

    Public Class Rook
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

    Public Class King
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
            HasMoved = Moved
        End Sub
        Public Overrides Sub UpdatePseudoLegalMoves(Board As ChessBoard)
            ResetLegalMoves()
            Try
                LegalMoves(PosX + 1, PosY) = True
            Catch
            End Try

            Try
                LegalMoves(PosX - 1, PosY) = True
            Catch
            End Try

            Try
                LegalMoves(PosX, PosY + 1) = True
            Catch
            End Try

            Try
                LegalMoves(PosX, PosY - 1) = True
            Catch
            End Try

            Try
                LegalMoves(PosX + 1, PosY - 1) = True
            Catch
            End Try

            Try
                LegalMoves(PosX + 1, PosY + 1) = True
            Catch
            End Try

            Try
                LegalMoves(PosX - 1, PosY - 1) = True
            Catch
            End Try

            Try
                LegalMoves(PosX - 1, PosY + 1) = True
            Catch
            End Try

            Dim ListToSearch As List(Of ChessPiece)
            Dim CanCastle As Boolean = True
            ListToSearch = If(Colour = ChessColour.Black, Board.WhitePieces, Board.BlackPieces)
            For Each Piece In ListToSearch
                If Not Piece.Out Then
                    Select Case Piece.GetType
                        Case GetType(King)
                            For y = Piece.PosY - 1 To Piece.PosY + 1
                                For x = Piece.PosX - 1 To Piece.PosX + 1
                                    Try
                                        LegalMoves(x, y) = False
                                    Catch
                                    End Try
                                Next
                            Next
                        Case GetType(Pawn)
                            If Colour = ChessColour.White Then 'the pawn would be black
                                Dim DownLeft As New Point(Piece.PosX - 1, Piece.PosY + 1)
                                Try
                                    LegalMoves(DownLeft.X, DownLeft.Y) = False
                                Catch
                                End Try

                                Dim DownRight As New Point(Piece.PosX + 1, Piece.PosY + 1)
                                Try
                                    LegalMoves(DownRight.X, DownRight.Y) = False
                                Catch
                                End Try
                            Else
                                Dim UpLeft As New Point(Piece.PosX - 1, Piece.PosY - 1)
                                Try
                                    LegalMoves(UpLeft.X, UpLeft.Y) = False
                                Catch
                                End Try

                                Dim UpRight As New Point(Piece.PosX + 1, Piece.PosY - 1)
                                Try
                                    LegalMoves(UpRight.X, UpRight.Y) = False
                                Catch
                                End Try
                            End If
                        Case Else
                            Piece.UpdatePseudoLegalMoves(Board)
                            For x = PosX - 1 To PosX + 1
                                For y = PosY - 1 To PosY + 1
                                    Try
                                        If x <> PosX OrElse y <> PosY Then
                                            If Piece.LegalMoves(x, y) Then
                                                LegalMoves(x, y) = False
                                            End If
                                        End If
                                    Catch
                                    End Try
                                Next
                            Next

                            If Piece.LegalMoves(PosX, PosY) Then
                                Try
                                    LegalMoves(PosX - 3, PosY) = False
                                Catch
                                End Try
                                Try
                                    LegalMoves(PosX + 2, PosY) = False
                                Catch
                                End Try
                                CanCastle = False
                            End If

                    End Select
                End If
            Next

            If CanCastle Then
                If Not HasMoved Then
                    Dim LeftEmpty As Boolean = True
                    For x = PosX - 1 To 1 Step -1
                        If Board.Board(x, PosY) IsNot Nothing Then
                            LeftEmpty = False
                            Exit For
                        End If
                    Next

                    Dim RightEmpty As Boolean = True
                    For x = PosX + 1 To 6
                        If Board.Board(x, PosY) IsNot Nothing Then
                            RightEmpty = False
                            Exit For
                        End If
                    Next

                    Dim CanShortCastle As Boolean = True
                    ListToSearch = If(Colour = ChessColour.Black, Board.WhitePieces, Board.BlackPieces)
                    If RightEmpty AndAlso Board.Board(PosX + 3, PosY).GetType = GetType(Rook) AndAlso DirectCast(Board.Board(PosX + 3, PosY), Rook).HasMoved = False Then
                        For x = PosX + 1 To PosX + 2
                            For Each piece In ListToSearch
                                If Not piece.Out AndAlso piece.LegalMoves(x, PosY) = True Then
                                    CanShortCastle = False
                                    Exit For
                                End If
                            Next
                        Next
                    Else
                        CanShortCastle = False
                    End If

                    Dim CanLongCastle As Boolean = True
                    If LeftEmpty AndAlso Board.Board(PosX - 4, PosY).GetType = GetType(Rook) AndAlso DirectCast(Board.Board(PosX - 4, PosY), Rook).HasMoved = False Then
                        For x = PosX - 1 To PosX - 3 Step -1
                            For Each piece In ListToSearch
                                If Not piece.Out AndAlso piece.LegalMoves(x, PosY) = True Then
                                    CanLongCastle = False
                                    Exit For
                                End If
                            Next
                        Next
                    Else
                        CanLongCastle = False
                    End If

                    LegalMoves(PosX - 4, PosY) = CanLongCastle
                    LegalMoves(PosX + 3, PosY) = CanShortCastle
                End If
            End If
        End Sub
    End Class

    Public Class Bishop
        Inherits ChessPiece
        Public Overrides Sub UpdatePseudoLegalMoves(Board As ChessBoard)
            ResetLegalMoves()
            CheckTopRight(Board)
            CheckTopLeft(Board)
            CheckBottomLeft(Board)
            CheckBottomRight(Board)
        End Sub
    End Class

    Public Class Knight
        Inherits ChessPiece
        Public Overrides Sub UpdatePseudoLegalMoves(Board As ChessBoard)
            ResetLegalMoves()
            Try
                LegalMoves(PosX + 2, PosY + 1) = True
            Catch
            End Try

            Try
                LegalMoves(PosX + 2, PosY - 1) = True
            Catch
            End Try

            Try
                LegalMoves(PosX - 2, PosY + 1) = True
            Catch
            End Try

            Try
                LegalMoves(PosX - 2, PosY - 1) = True
            Catch
            End Try

            Try
                LegalMoves(PosX + 1, PosY + 2) = True
            Catch
            End Try

            Try
                LegalMoves(PosX - 1, PosY + 2) = True
            Catch
            End Try

            Try
                LegalMoves(PosX + 1, PosY - 2) = True
            Catch
            End Try

            Try
                LegalMoves(PosX - 1, PosY - 2) = True
            Catch
            End Try
        End Sub
    End Class
End Class
