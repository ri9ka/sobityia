using System;
using System.Windows.Forms;

namespace Sobityia
{
    public delegate void GoingBeyond(int x, int y);
    class Sob
    {
        public event GoingBeyond Exit;
        public void Warning(int x, int y)
        {
            MessageBox.Show(
                 $"Выход за поле на [{x};{y}]",
                "Сообщение",
                 MessageBoxButtons.OK,
                 MessageBoxIcon.Warning);
        }
        static void Main(string[] args)
        {
            Sob Action = new Sob();
            Action.Exit += new GoingBeyond(Action.Warning);
            bool up = true, left = true, right = true, down = true;
            int x = 15;
            int y = 15;
            Action.Draw(x, y, ref up, ref left, ref right, ref down);
            Console.SetCursorPosition(x, y);
            Console.Write("|");
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow: if (up) y--; 
                        break;
                    case ConsoleKey.DownArrow: if (down) y++;
                        break;
                    case ConsoleKey.LeftArrow: if (left) x--; 
                        break;
                    case ConsoleKey.RightArrow: if (right) x++;
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default: 
                        break;
                }
                up = true; left = true; right = true; down = true;
                Console.Clear();
                Action.Draw(x, y, ref up, ref left, ref right, ref down);
                if (x > 78)
                { 
                    right = false; 
                    Action.Exit(x, y);
                    x -= 77; 
                }
                if (x < 1) 
                { left = false; 
                    Action.Exit(x, y); x += 77;
                }
                if (y < 1) 
                { up = false; Action.Exit(x, y); 
                    y += 22; }
                if (y > 23) { down = false; Action.Exit(x, y); 
                    y -= 22; 
                }
                Console.SetCursorPosition(x, y);
                Console.Write("|");
            }
        }
        void DrawHLine(int x, int y, int from, int to, int yLine,
                       ref bool up, ref bool down)
        {
            for (int i = from; i <= to; i++)
            {
                if ((y - yLine == -1) && (x >= from) && (x <= to)) down = false;
                if ((y - yLine == 1) && (x >= from) && (x <= to)) up = false;
                Console.SetCursorPosition(i, yLine);
                Console.Write("-");
            }
        }
        void DrawVLine(int x, int y, int from, int to, int xLine,
                       ref bool left, ref bool right)
        {
            for (int i = from; i <= to; i++)
            {
                if ((x - xLine == -1) && (y >= from) && (y <= to)) right = false;
                if ((x - xLine == 1) && (y >= from) && (y <= to)) left = false;
                Console.SetCursorPosition(xLine, i);
                Console.Write("-");
            }
        }
        void Draw(int x, int y, ref bool up, ref bool left, ref bool right, ref bool down)
        {
            DrawHLine(x, y, 10, 30, 5, ref up, ref down);
            DrawVLine(x, y, 5, 10, 30, ref left, ref right);
            DrawHLine(x, y, 30, 43, 10, ref up, ref down);
            DrawVLine(x, y, 5, 10, 40, ref left, ref right);
            DrawHLine(x, y, 40, 45, 5, ref up, ref down);
            DrawVLine(x, y, 5, 16, 45, ref left, ref right);
            DrawVLine(x, y, 18, 20, 45, ref left, ref right);
            DrawHLine(x, y, 10, 45, 20, ref up, ref down);
            DrawVLine(x, y, 5, 15, 10, ref left, ref right);
            DrawVLine(x, y, 17, 20, 10, ref left, ref right);
        }
    }
}