// Программа для работы с базой данных посёлков
//               Вариант №20
//     Выполнил Сергеев Кирилл Дмитриевич 
//                Группа 206
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace Correct_Input
{
    public class Input
    {
        public Input()
        {
            //Console.CursorVisible = false;
        }
        /// <summary>
        /// Считывает нажатые клавишы и возвращает только те, которые были переданны в метод
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public ConsoleKey InputKey(params ConsoleKey?[] keys)
        {
            ConsoleKeyInfo keyInfo;
            ConsoleKey? key = null;

            while (!keys.Contains(key))
            {
                keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;
            }
            return (ConsoleKey)key;
        }

        /// <summary>
        /// Ограничивает количество вводимых символов и проверяет данные на корректность
        /// </summary>
        public bool ReadValid<T>(ref T inp, byte len)
        {
            int x = Console.CursorLeft;
            string str;
            bool cont = true;
            ConsoleKeyInfo key;
            ConsoleKey? k;
            while (cont)
            {
                str = "";
                k = null;
                while (k != ConsoleKey.Enter)
                {
                    key = Console.ReadKey(true);
                    k = key.Key;
                    //if ( ((char.IsLetterOrDigit(key.KeyChar)) || (char.IsPunctuation(key.KeyChar)) ) && (str.Length < len))
                    if ( (!char.IsControl(key.KeyChar) || (key.KeyChar.Equals(" ")) ) && (str.Length < len))
                    {
                        str += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                    else if ((key.Key == ConsoleKey.Backspace) & (str.Length != 0))
                    {
                        str = str.Remove(str.Length - 1);
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        return false;
                        //b = false;
                    }
                }
                str = str.Trim();
                try
                {
                    if (inp.GetType() == typeof(uint))
                    {
                        inp = (T)(object)uint.Parse(str);
                    }
                    if (inp.GetType() == typeof(ushort))
                    {
                        inp = (T)(object)ushort.Parse(str);
                    }
                    else if (inp.GetType() == typeof(float))
                    {
                        inp = (T)(object)float.Parse(str);
                    }
                    else if (inp.GetType() == typeof(byte))
                    {
                        inp = (T)(object)byte.Parse(str);
                    }
                    else if (inp.GetType() == typeof(string))
                    {
                        if (str == "")
                            throw new Exception();
                        inp = (T)(object)str;
                    }
                    cont = false;
                }
                catch
                {
                    string clear = new string(' ', len);
                    Console.SetCursorPosition(x, Console.CursorTop);
                    Console.Write(clear);
                    Console.SetCursorPosition(x, Console.CursorTop);
                    Console.Write("Ошибка");
                    Thread.Sleep(400);
                    Console.SetCursorPosition(x, Console.CursorTop);
                    Console.Write(clear);
                    Console.SetCursorPosition(x, Console.CursorTop);
                }
                Console.SetCursorPosition(x, Console.CursorTop);
            }
            return true;
            //return b;
        }
    }
}
