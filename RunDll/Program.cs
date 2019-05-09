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

            //Console.CursorVisible = false;
            IntMenu data = new IntMenu();
            //Console.SetWindowSize(110, 34);
            Console.CursorVisible = false;
            data.MainMenu();
            
            Console.SetCursorPosition(30, 20);
        }
    }
}
