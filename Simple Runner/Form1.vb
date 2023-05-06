Imports System.Runtime.InteropServices
Public Class Form1
    <DllImport("User32.dll")>
    Private Shared Function GetKeyState(nVirtKey As Integer) As Short
    End Function

    Public _
        Dinosaur As Player,
        Score As Integer = 0,
        rd As New Random()

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dinosaur.x = 40
        Dinosaur.y = ClientSize.Height - 100
        Dinosaur.Draw()

        Timer1.Interval = 10
        Timer1.Start()
        Timer2.Interval = 300
        Timer2.Start()

        Me.DoubleBuffered = True
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If GetKeyState(Keys.W) < 0 Then
            Dinosaur.Jump()
        End If
        Dinosaur.Update()
    End Sub

End Class