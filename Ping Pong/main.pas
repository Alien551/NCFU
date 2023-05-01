unit Main; // заголовок программы
{$mode objfpc}{$H+}

interface

uses
  Classes, SysUtils, Forms, Controls,
  Graphics, Dialogs, ExtCtrls, StdCtrls, Windows;

type
  { TForm1 }

  TForm1 = class(TForm)
    Label1, Label2, p2Score, p1Score: TLabel;
    Player1, Player2, Line, Ball: TShape;
    Timer1: TTimer;

    procedure FormCreate(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
  end;

var
  Form1: TForm1;
  VelX, VelY: integer;           // объявление переменных для ускорения шарика,
  pSpeed: integer;               // переменной для скорости движения игрока,
  Goal: array[1..2] of integer;  // и массива целых чисел, хранящего очки игроков

implementation
{$R *.lfm}

// процедура, вызываемая при создании формы
procedure TForm1.FormCreate(Sender: TObject);
begin  // начало процедуры


  Ball.Left:= ClientWidth div 2;  // начальная позиция шарика
  Ball.Top:= ClientHeight div 2;  // по середине формы

  pSpeed:= 4; // скорость движения игрока
  VelX:= 2; // начальная скорость шарика по оси X (влево-вправо)
  VelY:= 2; // начальная скорость шарика по оси Y (вверх-вниз)

  Goal[1]:= 0; // Устанавливаем счет обоих игроков
  Goal[2]:= 0; // на ноль

  Form1.DoubleBuffered:= true; // задаем свойству формы "Двойная буферизация" значение
                               // true чтобы картинка не мерцала при обновлении
end; // конец процедуры

// процедура, вызываемая компонентом Timer1 с заданным интервалом
procedure TForm1.Timer1Timer(Sender: TObject);
begin // начало процедуры

  // управление первым игроком
  if GetKeyState(VK_W) < 0 then Player1.Top:= Player1.Top - pSpeed
  else if GetKeyState(VK_S) < 0 then Player1.Top:= Player1.Top + pSpeed;

  // управление вторым игроком
  if GetKeyState(VK_Up) < 0 then Player2.Top:= Player2.Top - pSpeed
  else if GetKeyState(VK_Down) < 0 then Player2.Top:= Player2.Top + pSpeed;


  // проверка условия, что шарик коснулся правого края поля
  if (Ball.Left > ClientWidth - Ball.Width) then
  begin
    Goal[1]:= Goal[1] + 1;
     VelX:= -VelX;

     Ball.Left:= ClientWidth div 2;  // возрват шарика в
     Ball.Top:= ClientHeight div 2;  // центр поля
  end;

  // проверка условия, что шарик коснулся левого края поля
  if (Ball.Left < 0) then
  begin
     Goal[2]:= Goal[2] + 1;
     VelX:= -VelX;

     Ball.Left:= ClientWidth div 2;  // возрват шарика в
     Ball.Top:= ClientHeight div 2;  // центр поля
  end;

  // проверка условия, что шарик коснулся верхнего или нижнего края поля
  if (Ball.Top > ClientHeight - Ball.Height) or (Ball.Top < 0) then VelY:= -VelY;

  // проверка условия, что шарик коснулся первого игрока
  if (Ball.Left <= Player1.Left + 15)
       and ((Ball.Top >= Player1.Top - 5)
            and (Ball.Top <= Player1.Top + Player1.Height + 5)) then
  begin
    VelX:= -VelX;
    VelY:= VelY;
  end;

  // проверка, что шарик коснулся второго игрока
  if (Ball.Left >= Player2.Left - 15)
       and ((Ball.Top >= Player2.Top - 5)
            and (Ball.Top <= Player2.Top + Player2.Height + 5)) then
  begin
    VelX:= -VelX;
    VelY:= VelY;
  end;

  // движение шарика
  Ball.Left:= Ball.Left + VelX;
  Ball.Top:= Ball.Top + VelY;

  // вывод очков, набранныз игроками
  p1Score.Caption:= IntToStr(Goal[1]);
  p2Score.Caption:= IntToStr(Goal[2]);
end; // конец процедуры

end. // конец программы
