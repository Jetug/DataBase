// Программа для работы с базой данных посёлков
//               Вариант №20
//     Выполнил Сергеев Кирилл Дмитриевич 
//                Группа 206

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BaseOutPut;
using Shell;
using Correct_Input;
using MyLib;

namespace DataBase
{
    public class IntMenu
    {

        // Вывести назначение программы
        
        ConsoleColor col = ConsoleColor.Green;
        public IntMenu()
        {
            Console.CursorVisible = false;
        }
        /// <summary>
        /// Выводит экранное меню
        /// </summary>
        public void MainMenu()
        {
            Frames frame = new Frames();
            Input inp = new Input();
            Tables table = new Tables();
            //table.fileName = "Villages.xml";
            DlllClass test = new DlllClass();

            ConsoleKey? mKey = null;
            int main_x = 30, main_y = 3;
            bool cont = true;

            frame.Menu(30, 3, 30, "Ввод базы данных", "Редактирование базы данных", "Вывод базы данных", "Выбор файла", "Выход из программы");
            frame.Choice(main_x, main_y, col, 30);
            while (cont)
            {
                mKey = inp.InputKey(ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Enter);

                if ((mKey == ConsoleKey.DownArrow) & (main_y != 15))
                {
                    //if (main_y != 3)
                        frame.Choice(main_x, main_y, ConsoleColor.White, 30);
                    main_y += 3;
                    frame.Choice(main_x, main_y, col, 30);
                }
                else if ((mKey == ConsoleKey.UpArrow) & (main_y > 3))
                {
                    frame.Choice(main_x, main_y, ConsoleColor.White, 30);
                    main_y -= 3;
                    frame.Choice(main_x, main_y, col, 30);
                }
                else if (mKey == ConsoleKey.Enter)
                {
                    ConsoleKey? sKey = null;
                    int x = 30, y = 3;
                    switch (main_y)
                    {
                        case 12: // Ввод имени файла
                            Console.Clear();
                            frame.Menu(30, 3, 30, "Выброр существующего файла", "Создание нового файла");
                            ConsoleKey? key = null;
                            y = 3;
                            frame.Choice(30, y, ConsoleColor.Green, 30);
                            while (key != ConsoleKey.Escape)
                            {
                                key = inp.InputKey(ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Enter, ConsoleKey.Escape);
                                switch (key)
                                {
                                    case ConsoleKey.DownArrow:
                                        if(y != 6)
                                        {
                                            frame.Choice(30, y, ConsoleColor.White, 30);
                                            y += 3;
                                            frame.Choice(30, y, ConsoleColor.Green, 30);
                                        }
                                        break;
                                    case ConsoleKey.UpArrow:
                                        if (y != 3)
                                        {
                                            frame.Choice(30, y, ConsoleColor.White, 30);
                                            y -= 3;
                                            frame.Choice(30, y, ConsoleColor.Green, 30);
                                        } 
                                        break;
                                }
                            }
                            Console.SetCursorPosition(30, 9);
                            frame.Continuous(30, "Введите имя файла", "");  
                            Console.SetCursorPosition(32, 12);
                            Console.CursorVisible = true;
                            inp.ReadValid(ref table.fileName, 28);
                            Console.CursorVisible = false;
                            {
                                XmlDocument xDoc = new XmlDocument();
                                xDoc.Load($"C:/C#/RunDll/XMLfiles/FileList.xml");
                                XmlElement xRoot = xDoc.DocumentElement;
                                XmlElement xElem = xDoc.CreateElement("file");
                                XmlAttribute xAttr = xDoc.CreateAttribute("name");
                                xAttr.AppendChild(xDoc.CreateTextNode(table.fileName));
                                xElem.Attributes.Append(xAttr);
                                xRoot.AppendChild(xElem);
                                xDoc.Save($"C:/C#/RunDll/XMLfiles/FileList.xml");
                            }
                            table.fileName += ".xml";
                            Console.Clear();
                            frame.Menu(30, 3, 30, "Ввод базы данных", "Редактирование базы данных", "Вывод базы данных", "Выбор файла", "Выход из программы");
                            //main_y = 0;
                            frame.Choice(main_x, main_y, col, 30);
                            break;
                        case 15: // Выход из программы
                            cont = false;
                            break;
                        default:
                            Console.Clear();
                            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
                            frame.Choice(x, y, col, 18);
                            while (sKey != ConsoleKey.Escape)
                            {
                                sKey = inp.InputKey(ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Enter, ConsoleKey.Escape);

                                switch (sKey)
                                {
                                    case ConsoleKey.DownArrow:
                                        if (y != 9)
                                        {
                                            //if (y != 3)
                                            frame.Choice(x, y, ConsoleColor.White, 18);
                                            y += 3;
                                            frame.Choice(x, y, col, 18);
                                        }
                                        break;
                                    case ConsoleKey.UpArrow:
                                        if (y > 3)
                                        {
                                            frame.Choice(x, y, ConsoleColor.White, 18);
                                            y -= 3;
                                            frame.Choice(x, y, col, 18);
                                        }
                                        break;
                                    case ConsoleKey.Enter:
                                        try
                                        {
                                            switch (y)
                                            {
                                                case 3:
                                                    if (main_y == 3)
                                                        table.ReadVellage();
                                                    else if (main_y == 9)
                                                        table.WriteVillage();
                                                    break;
                                                case 6:
                                                    if (main_y == 3)
                                                        table.ReadHouse();
                                                    else if (main_y == 9)
                                                        table.WriteHouse();
                                                    break;
                                                case 9:
                                                    if (main_y == 3)
                                                        table.ReadDeveloper();
                                                    else if (main_y == 9)
                                                        table.WriteDeveloper();
                                                    break;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            ConsoleKey back;
                                            //if(table.fileName == "")
                                            Console.Clear();
                                            frame.Menu(30, 5, 31, "Ошибка! Имя файла не выбранно");
                                            //frame.Menu(30, 5, 31, e);

                                            back = inp.InputKey(ConsoleKey.Escape);
                                            if (back == ConsoleKey.Escape)
                                            {
                                                Console.Clear();
                                                frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
                                            }
                                            //else

                                        }
                                        //y = 0;
                                        frame.Choice(x, y, col, 18);

                                        break;
                                    case ConsoleKey.Escape:
                                        Console.Clear();
                                        frame.Menu(30, 3, 30, "Ввод базы данных", "Редактирование базы данных", "Вывод базы данных", "Выбор файла", "Выход из программы");
                                        //main_y = 3;
                                        frame.Choice(main_x, main_y, col, 30);
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}