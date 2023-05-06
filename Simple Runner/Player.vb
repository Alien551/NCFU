Public Class Player
    Public _
        x, y As Integer,
        speed, jumpHeight As Integer,
        isJumping As Boolean,
        gravity As Double,
        Img As Image = My.Resources.DynoTemp
    Public Sub Jump()
        If Not isJumping Then isJumping = True
    End Sub
    Public Sub Update()
        If isJumping Then
            y -= jumpHeight
            jumpHeight -= gravity
            If jumpHeight < 0 Then
                isJumping = False
                jumpHeight = 10
            End If
        Else y += speed
        End If
    End Sub
    Public Sub Draw()
        Dim Dino As New PictureBox With {
            .Image = Img,
            .SizeMode = PictureBoxSizeMode.StretchImage,
            .Size = New Size(50, 50),
            .Location = New Point(x, y)
            }
        Form1.Controls.Add(Dino)
        Dino.BringToFront()
    End Sub
End Class