// Программа для работы с базой данных посёлков
//               Вариант №20
//     Выполнил Сергеев Кирилл Дмитриевич 
//                Группа 206
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    //public class test
    //{
    //    public void tes()
    //    {
    //        Console.WriteLine("Test");
    //    }
    //}
    public class Frames
    {
        public Frames()
        {
            Console.CursorVisible = false;
        }
        public void Menu(int x, int y, byte len, params string[] titles)
        {
            int start_y = y;
            string line = new string('═', len);
            Console.SetCursorPosition(x, y);
            //Console.Clear();
            foreach (string str in titles)
            {
                //Console.CursorVisible = false;
                y = (byte)Console.CursorTop;
                Console.SetCursorPosition(x, y);
                Console.WriteLine($"╔{line}╗");
                Console.SetCursorPosition(x, y + 1);
                Console.Write("║");
                Console.SetCursorPosition(x+1+(len-str.Length)/2, y+1);
                Console.Write(str);
                Console.SetCursorPosition(x + len + 1, y + 1);
                Console.Write("║");
                Console.SetCursorPosition(x, y + 2);
                Console.WriteLine($"╚{line}╝");
            }
            Console.SetCursorPosition(x, start_y);
        }

        public void Continuous(byte len, params string[] titles)
        {
            string slimLine = new string('─', len);
            string thickLine = new string('═', len);
            int y = Console.CursorTop;
            int x = Console.CursorLeft;
            Console.WriteLine($"╔{thickLine}╗");
            Console.SetCursorPosition(x, Console.CursorTop);
            Console.Write("║");
            Console.SetCursorPosition(x + 1 + (len - titles[0].Length) / 2, Console.CursorTop);
            Console.Write(titles[0]);
            Console.SetCursorPosition(x + len + 1, Console.CursorTop);
            Console.WriteLine("║");
            Console.SetCursorPosition(x, Console.CursorTop);
            Console.WriteLine($"╠{thickLine}╣");
            for (int i = 1; i < titles.Length; i++)
            {
                //Console.CursorVisible = false;
                Console.SetCursorPosition(x, Console.CursorTop);
                Console.Write("║");
                Console.SetCursorPosition(x + 1 + (len - titles[i].Length) / 2, Console.CursorTop);
                Console.Write(titles[i]);
                Console.SetCursorPosition(x + len + 1, Console.CursorTop);
                Console.WriteLine("║");
                Console.SetCursorPosition(x, Console.CursorTop);
                Console.WriteLine($"╟{slimLine}╢");
            }
            Console.SetCursorPosition(x, Console.CursorTop-1);
            Console.Write($"╚{thickLine}╝");
            //Console.SetCursorPosition(x, y);

        }
        public void Choice(int x, int y, ConsoleColor Col, byte len)
        {
            string line = new string('═', len);
            Console.ForegroundColor = Col;
            Console.SetCursorPosition(x, y);
            Console.WriteLine($"╔{line}╗");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("║");
            Console.SetCursorPosition(x + len + 1, y + 1);
            Console.Write("║");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine($"╚{line}╝");
            Console.ResetColor();
        }

        public void ContinuousChoice(int x, int y, ConsoleColor Col, byte len)
        {
            string line = new string('─', len);
            Console.ForegroundColor = Col;
            Console.SetCursorPosition(x, y);
            Console.WriteLine($"╟{line}╢");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("║");
            Console.SetCursorPosition(x + len + 1, y + 1);
            Console.Write("║");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine($"╟{line}╢");
            Console.ResetColor();
        }
    }
}
