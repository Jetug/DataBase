// Программа для работы с базой данных посёлков
//               Вариант №20
//     Выполнил Сергеев Кирилл Дмитриевич 
//               Группа 206
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseOutPut;
using Shell;
using DataBase;
using Correct_Input;

namespace RunDll
{

    class Program
    {
        static void Main(string[] args)
        {
            //Console.SetWindowSize(Console.WindowWidth, 30);


            //List<Village> DataBase = new List<Village>();
            Frames f = new Frames();
            //f.Menu(30, 3, 23, "1", "База Посёлков");

            Tables t = new Tables();
            List<Tables.Village> DataBase = new List<Tables.Village>();

            //t.ReadVellage();
            //t.WriteVillage();
            //key = Console.ReadKey(true);
            //Console.WriteLine(key.Key);
            //Console.WriteLine(key.Modifiers);

            /*ConsoleKeyInfo key;
            ConsoleKey k = ConsoleKey.A;
            while(k != ConsoleKey.Escape)
            {
                key = Console.ReadKey(true);
                k = key.Key;
                Console.WriteLine(k);
            }*/
            Console.CursorVisible = false;
            IntMenu data = new IntMenu();
            //Console.SetWindowSize(110, 34);
            data.MainMenu();

            //Tables.Village vill = new Tables.Village();
            //Console.WriteLine(vill.name.GetType());
            //Input inp = new Input();
            Console.SetCursorPosition(30, 20);
        }
    }
}
