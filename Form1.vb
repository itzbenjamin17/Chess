'The top left square has the tag coordinates (-1,-1) because a coordinate of (0,0) would be the same as an empty point
Imports System.Diagnostics.Eventing.Reader
Imports System.DirectoryServices.ActiveDirectory
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Reflection
Imports System.Runtime.Intrinsics.X86
Imports System.Runtime.Serialization.Formatters.Binary

Public Class Form1
    Private SelectedPiecePosition As Point
    Public Board As New ChessBoard
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Board.InitializeBoard()
    End Sub

    Private Sub ChessSquare_Click(sender As Object, e As EventArgs) Handles PicA8.Click, PicB8.Click, PicC8.Click, PicD8.Click, PictureBox3.Click, PictureBox4.Click, PictureBox5.Click, PictureBox6.Click, PictureBox7.Click, PictureBox8.Click, PictureBox9.Click, PictureBox10.Click, PicD7.Click, PictureBox12.Click, PictureBox13.Click, PictureBox14.Click, PictureBox15.Click, PictureBox16.Click, PictureBox17.Click, PictureBox18.Click, PicD5.Click, PictureBox20.Click, PictureBox21.Click, PictureBox22.Click, PictureBox23.Click, PictureBox24.Click, PictureBox25.Click, PictureBox26.Click, PicD6.Click, PictureBox28.Click, PictureBox29.Click, PictureBox30.Click, PictureBox39.Click, PictureBox40.Click, PictureBox41.Click, PictureBox42.Click, PicD2.Click, PictureBox44.Click, PictureBox45.Click, PictureBox46.Click, PictureBox47.Click, PictureBox48.Click, PictureBox49.Click, PictureBox50.Click, PicD3.Click, PictureBox52.Click, PictureBox53.Click, PictureBox54.Click, PictureBox55.Click, PictureBox56.Click, PictureBox57.Click, PictureBox58.Click, PicD4.Click, PictureBox60.Click, PictureBox61.Click, PictureBox62.Click, PictureBox31.Click, PictureBox32.Click, PictureBox33.Click, PictureBox34.Click, PicD1.Click, PictureBox36.Click, PictureBox37.Click, PictureBox38.Click, PictureBox20.Click
        ResetMovesOnUI()
        Dim ClickedPicture As PictureBox = DirectCast(sender, PictureBox)
        Dim ClickedPosition As New Point
        Dim SelectedPiece As ChessPiece
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
            Exit Sub
        End If

        If ClickedPosition.X = -1 Then
            Dim ActualClick As New Point With {
                .X = 0,
                .Y = 0}
            If SelectedPiecePosition.IsEmpty Then
                SelectedPiece = ClickedPiece
                If SelectedPiece IsNot Nothing Then
                    SelectedPiecePosition = ClickedPosition
                    ShowMovesOnUI(SelectedPiece)
                End If
            Else
                SelectedPiece = Board.GetPiece(SelectedPiecePosition)
                If ClickedPiece IsNot Nothing Then
                    If ClickedPiece.Colour = SelectedPiece.Colour AndAlso ClickedPiece.GetType <> GetType(Rook) Then
                        ShowMovesOnUI(ClickedPiece)
                        SelectedPiecePosition = ClickedPosition
                        Exit Sub
                    End If
                End If

                If Board.CanMove(ActualClick, SelectedPiecePosition) Then
                    MovePieceOnUI(ClickedPicture, SelectedPiecePosition)
                    Board.MovePiece(ActualClick, SelectedPiecePosition, False)
                End If
                SelectedPiecePosition = Point.Empty
            End If
        ElseIf SelectedPiecePosition.X = -1 Then
            Dim ActualSelect As New Point With {
               .X = 0,
               .Y = 0}
            If SelectedPiecePosition.IsEmpty Then
                SelectedPiece = ClickedPiece
                If SelectedPiece IsNot Nothing Then
                    SelectedPiecePosition = ClickedPosition
                    ShowMovesOnUI(SelectedPiece)
                End If

            Else
                SelectedPiece = Board.GetPiece(ActualSelect)
                If ClickedPiece IsNot Nothing Then
                    If ClickedPiece.Colour = SelectedPiece.Colour AndAlso ClickedPiece.GetType <> GetType(Rook) Then
                        ShowMovesOnUI(ClickedPiece)
                        SelectedPiecePosition = ClickedPosition
                        Exit Sub
                    End If
                End If
                If Board.CanMove(ClickedPosition, ActualSelect) Then
                    MovePieceOnUI(ClickedPicture, SelectedPiecePosition)
                    Board.MovePiece(ClickedPosition, ActualSelect, False)
                End If
                SelectedPiecePosition = Point.Empty
            End If

        Else
            If SelectedPiecePosition.IsEmpty Then
                SelectedPiece = ClickedPiece
                If SelectedPiece IsNot Nothing Then
                    SelectedPiecePosition = ClickedPosition
                    ShowMovesOnUI(SelectedPiece)
                End If
            Else
                SelectedPiece = Board.GetPiece(SelectedPiecePosition)
                If ClickedPiece IsNot Nothing Then
                    If ClickedPiece.Colour = SelectedPiece.Colour AndAlso ClickedPiece.GetType <> GetType(Rook) Then
                        ShowMovesOnUI(ClickedPiece)
                        SelectedPiecePosition = ClickedPosition
                        Exit Sub
                    End If
                End If
                If Board.CanMove(ClickedPosition, SelectedPiecePosition) Then
                    MovePieceOnUI(ClickedPicture, SelectedPiecePosition)
                    Board.MovePiece(ClickedPosition, SelectedPiecePosition, False)
                End If
                SelectedPiecePosition = Point.Empty
                End If
            End If

    End Sub
    Public Sub ShowMovesOnUI(Piece As ChessPiece)
        Piece.CheckIfPinned(Board)
        If Piece.Pinned Then
            Exit Sub
        End If
        Piece.UpdateMoves(Board)
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
        If Board.Board(selectedPiecePosition.X, selectedPiecePosition.Y).GetType = GetType(King) AndAlso ClickedPicture.Image IsNot Nothing Then
            Dim King As ChessPiece = Board.GetPiece(selectedPiecePosition)
            Dim clickedImageBytes As Byte() = ImageToByteArray(ClickedPicture.Image)
            Dim areImagesEqual As Boolean
            ' Compare the image byte arrays
            If King.Colour = ChessColour.White Then
                Dim whiteRookImageBytes As Byte() = ImageToByteArray(My.Resources.White_Rook)
                areImagesEqual = ByteArrayEquals(whiteRookImageBytes, clickedImageBytes)
            Else
                Dim blackRookImageBytes As Byte() = ImageToByteArray(My.Resources.Black_Rook)
                areImagesEqual = ByteArrayEquals(blackRookImageBytes, clickedImageBytes)

            End If

            If King.Colour = ChessColour.White AndAlso areImagesEqual Then
                Dim x As Integer = Val(ClickedPicture.Tag(0))
                Dim y As Integer = Val(ClickedPicture.Tag(2))
                If x = 0 Then
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
            ElseIf King.Colour = ChessColour.Black AndAlso areImagesEqual Then
                Dim x As Integer = Val(ClickedPicture.Tag(0))
                Dim xString As Char = ClickedPicture.Tag(0)
                Dim y As Integer = Val(ClickedPicture.Tag(2))
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
            Else
                NormalUISwap(ClickedPicture, selectedPiecePosition)
            End If
        ElseIf Board.Board(selectedPiecePosition.X, selectedPiecePosition.Y).GetType = GetType(Pawn) AndAlso ClickedPicture.Image Is Nothing AndAlso Val(ClickedPicture.Tag(0)) <> selectedPiecePosition.X Then
            Dim Pawn As Pawn = Board.GetPiece(selectedPiecePosition)
            For Each oldimg As Control In Controls
                If TypeOf oldimg Is PictureBox Then
                    Dim x As Integer = Val(oldimg.Tag(0))
                    Dim y As Integer = Val(oldimg.Tag(2))

                    If x = selectedPiecePosition.X AndAlso y = selectedPiecePosition.Y Then
                        ClickedPicture.Image = DirectCast(oldimg, PictureBox).Image
                        DirectCast(oldimg, PictureBox).Image = Nothing
                        For Each cntrl As Control In Controls
                            If Pawn.Colour = ChessColour.Black Then
                                If Val(cntrl.Tag(2)) = Val(ClickedPicture.Tag(2)) - 1 AndAlso Val(cntrl.Tag(0)) = Val(ClickedPicture.Tag(0)) Then
                                    DirectCast(cntrl, PictureBox).Image = Nothing
                                    Exit For
                                End If
                            Else
                                If Val(cntrl.Tag(2)) = Val(ClickedPicture.Tag(2)) + 1 AndAlso Val(cntrl.Tag(0)) = Val(ClickedPicture.Tag(0)) Then
                                    DirectCast(cntrl, PictureBox).Image = Nothing
                                    Exit For
                                End If
                            End If

                        Next
                    End If
                Else
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
    Private Function ImageToByteArray(image As Image) As Byte()
        Dim ms As New MemoryStream()
        image.Save(ms, ImageFormat.Png)
        Return ms.ToArray()
    End Function

    Private Function ByteArrayEquals(array1 As Byte(), array2 As Byte()) As Boolean
        If array1.Length <> array2.Length Then
            Return False
        End If

        For i As Integer = 0 To array1.Length - 1
            If array1(i) <> array2(i) Then
                Return False
            End If
        Next

        Return True
    End Function

    Public Enum ChessColour
        Black
        White
    End Enum

    Public Class ChessBoard
        Public Board As ChessPiece(,) = New ChessPiece(7, 7) {}
        Public Pieces As New List(Of ChessPiece)
        Public MoveStack As New Stack(Of ChessMove)
        Dim WhiteTurn As Boolean = True

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
                                Board(x, y).King = Kings(0)
                                Board(x, y).OpposingKing = Kings(1)
                            Case Else
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
            Piece.UpdateMoves(Me)
            If Piece.LegalMoves(King.PosX, King.PosY) Then
                Return True
            End If
            Return False
        End Function
        Function DeliveredCheckMate(Piece As ChessPiece)
            Dim King As King = Piece.OpposingKing
            For ChessPiece = 0 To Pieces.Count - 1
                If Pieces(ChessPiece).Colour <> Piece.Colour Then
                    Pieces(ChessPiece).UpdateMoves(Me)
                    For y = 0 To 7
                        For x = 0 To 7
                            If Pieces(ChessPiece).LegalMoves(x, y) Then
                                MovePiece(New Point(x, y), New Point(Pieces(ChessPiece).PosX, Pieces(ChessPiece).PosY), True)
                                If KingInCheck(Pieces(ChessPiece)) = False Then
                                    Return False
                                End If
                            End If
                        Next
                    Next
                End If
            Next
            Return True
        End Function

        Function KingInCheck(Piece As ChessPiece)
            Dim King As King = Piece.King
            For Each ChessPiece In Pieces
                If ChessPiece.Colour <> Piece.Colour Then
                    ChessPiece.UpdateMoves(Me)
                    If ChessPiece.LegalMoves(King.PosX, King.PosY) Then
                        Return True
                    End If
                End If
            Next
            Return False
        End Function

        Public Sub MovePiece(SpotToFill As Point, OldPieceSpot As Point, OnlyChecking As Boolean)
            'Putting the new piece in the old pieces spot and removing the new piece from where it was
            Dim MovingPiece As ChessPiece = GetPiece(OldPieceSpot)
            Dim Move As New ChessMove With {
                .SpotToFill = SpotToFill,
                .OldPieceSpot = OldPieceSpot,
                .TakenPiece = GetPiece(SpotToFill),
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
            If GetPiece(SpotToFill) IsNot Nothing AndAlso MovingPiece.GetType() = GetType(King) AndAlso GetPiece(SpotToFill).GetType = GetType(Rook) AndAlso MovingPiece.Colour = GetPiece(SpotToFill).Colour Then
                Castle(MovingPiece, GetPiece(SpotToFill))
                WhiteTurn = Not WhiteTurn
                'different for en passant
            ElseIf MovingPiece.GetType = GetType(Pawn) AndAlso GetPiece(SpotToFill) Is Nothing AndAlso SpotToFill.X <> OldPieceSpot.X Then
                EnPassant(SpotToFill, OldPieceSpot, MovingPiece)

                WhiteTurn = Not WhiteTurn
            Else

                'The spot was empty so we can do a normal swap
                If GetPiece(SpotToFill) IsNot Nothing Then
                    Dim OldPiece As ChessPiece = GetPiece(SpotToFill)
                    If Not OnlyChecking Then
                        Pieces.Remove(OldPiece)
                    End If
                End If
                Board(SpotToFill.X, SpotToFill.Y) = MovingPiece
                Board(OldPieceSpot.X, OldPieceSpot.Y) = Nothing
                Board(SpotToFill.X, SpotToFill.Y).PosX = SpotToFill.X
                Board(SpotToFill.X, SpotToFill.Y).PosY = SpotToFill.Y


                'After we do a move every other pawn of that colour didnt (just move 2) as it would have moved 2 on the last turn
                For Each p As ChessPiece In Pieces
                    If p.GetType() = GetType(Pawn) Then
                        If p.Colour = MovingPiece.Colour AndAlso p IsNot MovingPiece Then
                            DirectCast(p, Pawn).JustMoved2 = False
                        End If
                    End If
                Next
            End If
            If Not OnlyChecking Then
                If DeliveredCheck(MovingPiece) Then
                    If DeliveredCheckMate(MovingPiece) Then
                        If WhiteTurn Then
                            MsgBox("Checkmate, White Wins!!!")
                        Else
                            MsgBox("Checkmate, Black Wins!!!")
                        End If
                        End
                    End If
                End If
            Else
                UndoMove()
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
            WhiteTurn = Not WhiteTurn
        End Sub
        Public Sub EnPassant(SpotToFill As Point, OldPieceSpot As Point, MovingPiece As ChessPiece)
            If MovingPiece.Colour = ChessColour.White Then
                Board(SpotToFill.X, SpotToFill.Y + 1) = Nothing
                Board(SpotToFill.X, SpotToFill.Y) = MovingPiece
                Board(OldPieceSpot.X, OldPieceSpot.Y) = Nothing
                Pieces.Remove(Pieces.Find(Function(item) item.PosX = SpotToFill.X AndAlso item.PosY = SpotToFill.Y + 1))
            Else
                Board(SpotToFill.X, SpotToFill.Y - 1) = Nothing
                Board(SpotToFill.X, SpotToFill.Y) = MovingPiece
                Board(OldPieceSpot.X, OldPieceSpot.Y) = Nothing
                Pieces.Remove(Pieces.Find(Function(item) item.PosX = SpotToFill.X AndAlso item.PosY = SpotToFill.Y - 1))

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
            Piece.CheckIfPinned(Me)
            If Piece.Pinned Then
                Return False
            End If

            LegalMove = Piece.GetMoves(SpotToFill, Me)

            If KingInCheck(Piece) Then
                Dim Move As New ChessMove With {
                    .SpotToFill = SpotToFill,
                    .OldPieceSpot = OldPieceSpot,
                    .MovingPiece = GetPiece(OldPieceSpot),
                    .TakenPiece = GetPiece(SpotToFill)
                }

                MovePiece(SpotToFill, OldPieceSpot, True)

                If KingInCheck(Piece) Then
                    LegalMove = False
                Else
                    LegalMove = True
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
        Public MustOverride Function GetMoves(SpotToFill As Point, Board As ChessBoard) As Boolean
        Public MustOverride Sub UpdateMoves(Board As ChessBoard)
        Public PosX As Integer
        Public PosY As Integer
        Public Colour As ChessColour
        Public Pinned As Boolean = False
        Public LegalMoves(7, 7) As Boolean
        Public King As King
        Public OpposingKing As King

        Protected Sub ResetLegalMoves()
            For X = 0 To 7
                For Y = 0 To 7
                    LegalMoves(X, Y) = False
                Next
            Next
        End Sub

        Public Sub CheckIfPinned(Board As ChessBoard)
            For Each piece In Board.Pieces
                Dim T As Type = piece.GetType()
                If piece.Colour <> Colour AndAlso (T = GetType(Rook) OrElse T = GetType(Bishop) OrElse T = GetType(Queen)) Then
                    piece.UpdateMoves(Board)
                    If piece.LegalMoves(PosX, PosY) = True AndAlso piece.LegalMoves(King.PosX, King.PosY) = False Then
                        Board.Board(PosX, PosY) = Nothing
                        piece.UpdateMoves(Board)
                        If piece.LegalMoves(King.PosX, King.PosY) = True Then
                            Pinned = True
                        Else
                            Pinned = False
                        End If
                        Board.Board(PosX, PosY) = Me
                    End If

                End If
            Next
        End Sub
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
        Public Overrides Function GetMoves(SpotToFill As Point, Board As ChessBoard) As Boolean
            UpdateMoves(Board)
            Return LegalMoves(SpotToFill.X, SpotToFill.Y)
        End Function
        Public Overrides Sub UpdateMoves(Board As ChessBoard)
            Dim Offset1 As Integer
            Dim Offset2 As Integer
            If Colour = ChessColour.Black Then
                Offset1 = 1
                Offset2 = 2
            Else
                Offset1 = -1
                Offset2 = -2
            End If

            ResetLegalMoves()
            If Board.Board(PosX, PosY + Offset1) Is Nothing Then
                LegalMoves(PosX, PosY + Offset1) = True
            End If
            If HasMoved = False Then
                If Board.Board(PosX, PosY + Offset2) Is Nothing AndAlso Board.Board(PosX, PosY + Offset1) Is Nothing Then
                    LegalMoves(PosX, PosY + Offset2) = True
                End If
            End If

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
        End Sub
    End Class


    Class Queen
        Inherits ChessPiece
        Public Overrides Function GetMoves(SpotToFill As Point, Board As ChessBoard) As Boolean
            UpdateMoves(Board)
            Return LegalMoves(SpotToFill.X, SpotToFill.Y)
        End Function

        Public Overrides Sub UpdateMoves(Board As ChessBoard)
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
        Public Overrides Function GetMoves(SpotToFill As Point, Board As ChessBoard) As Boolean
            UpdateMoves(Board)
            Return LegalMoves(SpotToFill.X, SpotToFill.Y)
        End Function

        Public Overrides Sub UpdateMoves(Board As ChessBoard)
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

        Public Overrides Function GetMoves(SpotToFill As Point, Board As ChessBoard) As Boolean
            UpdateMoves(Board)
            Return LegalMoves(SpotToFill.X, SpotToFill.Y)
        End Function
        Public Overrides Sub UpdateMoves(Board As ChessBoard)
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

            For Each Piece In Board.Pieces
                If Piece.Colour <> Colour Then
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
                            Piece.UpdateMoves(Board)
                            Try
                                For i = 0 To 7
                                    For j = 0 To 7
                                        If Piece.GetType IsNot GetType(King) Then
                                            If Piece.LegalMoves(i, j) = True Then
                                                LegalMoves(i, j) = False
                                            End If
                                        End If
                                    Next
                                Next
                            Catch
                            End Try
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
                If RightEmpty Then
                    If Board.Board(PosX + 3, PosY).GetType = GetType(Rook) AndAlso DirectCast(Board.Board(PosX + 3, PosY), Rook).HasMoved = False Then
                        For x = PosX To PosX + 2
                            For Each piece In Board.Pieces
                                If piece.LegalMoves(x, PosY) = True AndAlso piece.Colour <> Colour Then
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
                            For Each piece In Board.Pieces
                                If piece.LegalMoves(x, PosY) = True AndAlso piece.Colour <> Colour Then
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
        Public Overrides Function GetMoves(SpotToFill As Point, Board As ChessBoard) As Boolean
            UpdateMoves(Board)
            Return LegalMoves(SpotToFill.X, SpotToFill.Y)
        End Function
        Public Overrides Sub UpdateMoves(Board As ChessBoard)
            ResetLegalMoves()
            CheckTopRight(Board)
            CheckTopLeft(Board)
            CheckBottomLeft(Board)
            CheckBottomRight(Board)
        End Sub
    End Class

    Class Knight
        Inherits ChessPiece
        Public Overrides Function GetMoves(SpotToFill As Point, Board As ChessBoard) As Boolean
            UpdateMoves(Board)
            Return LegalMoves(SpotToFill.X, SpotToFill.Y)
        End Function

        Public Overrides Sub UpdateMoves(Board As ChessBoard)
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
