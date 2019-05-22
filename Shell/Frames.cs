// Программа для работы с базой данных посёлков
//               Вариант №20
//     Выполнил Сергеев Кирилл Дмитриевич 
//                Группа 206
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Correct_Input;

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
        //public Frames()
        //{
        //    //Console.CursorVisible = false;
        //}
        public void Menu(int x, int y, byte len, params string[] titles)
        {
            Console.CursorVisible = false;
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

        public void Continuous(byte len, string title, params string[] lines)
        {
            string slimLine = new string('─', len);
            string thickLine = new string('═', len);
            int y = Console.CursorTop;
            int x = Console.CursorLeft;
            Console.WriteLine($"╔{thickLine}╗");
            Console.SetCursorPosition(x, Console.CursorTop);
            Console.Write("║");
            Console.SetCursorPosition(x + 1 + (len - title.Length) / 2, Console.CursorTop);
            Console.Write(title);
            Console.SetCursorPosition(x + len + 1, Console.CursorTop);
            Console.WriteLine("║");
            Console.SetCursorPosition(x, Console.CursorTop);
            Console.WriteLine($"╠{thickLine}╣");
            for (int i = 0; i < lines.Length; i++)
            {
                //Console.CursorVisible = false;
                Console.SetCursorPosition(x, Console.CursorTop);
                Console.Write("║");
                Console.SetCursorPosition(x + 1 + (len - lines[i].Length) / 2, Console.CursorTop);
                Console.Write(lines[i]);
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
            Console.CursorVisible = false;
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

        public bool Call_MassageBox(int x, int y, string title)
        {
            Console.Clear();
            Frames frame = new Frames();
            frame.Menu(x, y, 22, title);
            frame.Menu(x + 5, y + 3, 5, "Да");
            frame.Menu(x + 12, y + 3, 5, "Нет");

            ConsoleKey? key = null;
            int x1 = x + 5;
            int y1 = y + 3;
            Input inp = new Input();
            frame.Choice(x1, y1, ConsoleColor.Green, 5);
            while (key != ConsoleKey.Enter)
            {
                key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Enter);

                if ((key == ConsoleKey.RightArrow) & (x1 != x + 12))
                {
                    x1 += 7;
                    frame.Choice(x1 - 7, y1, ConsoleColor.White, 5);
                    frame.Choice(x1, y1, ConsoleColor.Green, 5);
                }
                else if ((key == ConsoleKey.LeftArrow) & (x1 != x + 5))
                {
                    x1 -= 7;
                    frame.Choice(x1, y1, ConsoleColor.Green, 5);
                    frame.Choice(x1 + 7, y1, ConsoleColor.White, 5);
                }
                else if (key == ConsoleKey.Enter)
                {
                    if (x1 == x + 5)
                        return true;
                    else
                        return false;
                }
            }
            return true;
        }
    }
}
