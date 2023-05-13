Imports System.Runtime.InteropServices
Public Class Form1 ' объявление класса формы
    ' объявление переменных
    Private WithEvents GameTimer As New Timer ' Таймер для игры
    Private player As PictureBox, ' Игрок
            obstaclesList As List(Of PictureBox), ' Список препятсвий
            score As Double = 0, ' Счет игрока
            gameOver As Boolean = False, ' Флаг, означающий, что игра
            gravity As Integer = 10, ' Сила гравитации
            jumpForce As Integer = 0 ' Сила прыжка
    Private ReadOnly obstacleSpeed As Integer = 12 ' Скорость движения преград
    ' импорт метода GetKeyDown
    <DllImport("User32.dll")>
    Private Shared Function GetKeyState(nVirtKey As Integer) As Short
    End Function
    ' процедура, вызываемая при создании формы
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Hide()
        ' инициализация игрока
        player = New PictureBox With {
            .Size = New Size(50, 50),
            .Location = New Point(100, Me.Height - 100),
            .BackColor = Color.Black
        }
        Controls.Add(player)
        ' инициализация списка препятствий
        obstaclesList = New List(Of PictureBox)
        ' Запускаем таймер для игры
        GameTimer.Interval = 30
        GameTimer.Enabled = True
    End Sub
    ' процедура, вызываемая компонентом GameTimer
    Private Sub GameTimer_Tick(sender As Object, e As EventArgs) Handles GameTimer.Tick
        Dim i As Integer = 0 ' обработка прыжка и падения игрока
        If player.Bottom < Me.Height - 50 Then
            player.Top += gravity
            gravity += 2
        End If
        If GetKeyState(Keys.Space) < 0 And player.Bottom >= Me.Height - 50 Then
            jumpForce = -30
            gravity = 4
        End If
        If jumpForce < 0 Then
            player.Top += jumpForce
            jumpForce += 1
        End If
        If player.Bottom > Me.Height - 50 Then
            player.Top = Me.Height - 100
            gravity = 0
            jumpForce = 0
        End If
        ' смещение препятствий влево
        For Each obstacle In obstaclesList
            obstacle.Left -= obstacleSpeed
            ' Проверяем коллизию игрока с препятствиями
            If player.Bounds.IntersectsWith(obstacle.Bounds) Then
                EndGame()
                Exit Sub
            End If
            ' Если препятствие ушло за пределы экрана, то удаляем из списка и с экрана
            If obstacle.Right < 0 Then
                Controls.Remove(obstacle)
                obstaclesList.Remove(obstacle)
                Exit For ' Выходим из цикла, чтобы избежать ошибки, когда мы удаляем элемент из списка, по которому идет цикл
            End If
        Next
        ' генерация новых препятствий с некоторой вероятностью
        If Int(score) Mod 10 = 0 And Rnd() < 0.23 And obstaclesList.Count < 3 Then
            Dim obstacle As New PictureBox With {
                .Size = New Size(30, 50),
                .Location = New Point(Me.Width, Me.Height - player.Height - 50),
                .BackColor = Color.Red
            }
            Controls.Add(obstacle)
            obstaclesList.Add(obstacle)
        End If
        ' инкремент счета и вывод на экран
        score += 0.1
        Label1.Text = $"{score:000}"
    End Sub
    ' процедура, вызываемая при нажатии на любую клавишу
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode And gameOver Then
            ' начало новой игры, если игра окончена
            For Each obstacle In obstaclesList
                Controls.Remove(obstacle)
            Next
            obstaclesList.Clear()
            GameTimer.Enabled = True
            score = 0
            gameOver = False
            Label2.Hide()
        End If
    End Sub
    '
    Private Sub EndGame()
        ' завершение игры и вывод сообщения о проигрыше
        GameTimer.Enabled = False
        gameOver = True
        Label2.Show()
    End Sub
End Class