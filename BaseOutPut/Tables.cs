// Программа для работы с базой данных посёлков
//                Вариант №20
//     Выполнил Сергеев Кирилл Дмитриевич 
//                Группа 206
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml;
using Shell;
using Correct_Input;
using System.IO;

namespace BaseOutPut
{
    public struct Village
    {
        public string name;
        public string Name
        {
            get { return name; }
            set { name = Name; }
        }
        public string dev;
        public string Dev
        {
            get { return dev; }
            set { dev = Dev; }
        }
        public float area;
        public float Area
        {
            get { return area; }
            set { area = Area; }
        }
        public uint people;
        public uint People
        {
            get { return people; }
            set { people = People; }
        }

        public Village(string name = "", string dev = "", float area = 0, uint people = 0)
        {
            this.name = name;
            this.dev = dev;
            this.area = area;
            this.people = people;
        }
    }

    public struct House
    {
        public string name;
        public string Name { get { return name; } }
        public ushort num;
        public ushort Num { get { return num; } }
        public float area;
        public float Area { get { return area; } }
        public byte floor;
        public byte Floor { get { return floor; } }
        public string type;
        public string Type { get { return type; } }
        public House(string name = "", ushort num = 0, float area = 0, byte floor = 0, string type = "")
        {
            this.name = name;
            this.num = num;
            this.area = area;
            this.floor = floor;
            this.type = type;
        }
    }

    public struct Developer
    {
        public string name;
        public string Name { get { return name; } }
        public float inc;
        public float Inc { get { return inc; } }
        public string addr;
        public string Addr { get { return addr; } }

        public Developer(string name = "", float inc = 0, string addr = "")
        {
            this.name = name;
            this.inc = inc;
            this.addr = addr;
        }
    }

    public class Tables
    {
        public List<Village> villages = new List<Village>();
        public List<House> houses = new List<House>();
        public List<Developer> developers = new List<Developer>();

        public Tables()
        {
            if (! File.Exists($"C:/C#/RunDll/XMLfiles/Villages.xml"))
            {
                Create_XmlFile("Villages");
            }
            Load_DataBase(ref villages);
            Load_DataBase(ref houses);
            Load_DataBase(ref developers);
        }

        public static string fileName = "Villages";

        public void LoadAll()
        {
            Load_DataBase(ref villages);
            Load_DataBase(ref houses);
            Load_DataBase(ref developers);
        }

        /// <summary>
        /// Сохраняет списки в файл
        /// </summary>
        public void SaveAll()
        {
            XmlTextWriter textWritter = new XmlTextWriter($"C:/C#/RunDll/XMLfiles/{fileName}.xml", Encoding.UTF8);
            textWritter.WriteStartDocument();
            textWritter.WriteStartElement("VillagesBase");
            textWritter.WriteEndElement();
            textWritter.Close();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load($"C:/C#/RunDll/XMLfiles/{fileName}.xml");

            foreach (Village v in villages)
            {
                XmlElement villRoot = xDoc.DocumentElement;
                XmlElement villElem = xDoc.CreateElement("village");
                XmlElement nameElem = xDoc.CreateElement("name");
                XmlElement areaElem = xDoc.CreateElement("area");
                XmlElement peopleElem = xDoc.CreateElement("people");
                XmlElement devElem = xDoc.CreateElement("dev");

                nameElem.AppendChild(xDoc.CreateTextNode(v.name));
                devElem.AppendChild(xDoc.CreateTextNode(v.dev));
                areaElem.AppendChild(xDoc.CreateTextNode(v.area.ToString()));
                peopleElem.AppendChild(xDoc.CreateTextNode(v.people.ToString()));
                
                villElem.AppendChild(nameElem);
                villElem.AppendChild(devElem);
                villElem.AppendChild(areaElem);
                villElem.AppendChild(peopleElem);
                villRoot.AppendChild(villElem);
            }
            xDoc.Save($"C:/C#/RunDll/XMLfiles/{fileName}.xml");

            foreach (House h in houses)
            {
                XmlElement houseRoot = xDoc.DocumentElement;
                XmlElement houseElem = xDoc.CreateElement("house");
                XmlElement nameElem = xDoc.CreateElement("name");
                XmlElement numElem = xDoc.CreateElement("num");
                XmlElement areaElem = xDoc.CreateElement("area");
                XmlElement floorElem = xDoc.CreateElement("floor");
                XmlElement typeElem = xDoc.CreateElement("type");

                nameElem.AppendChild(xDoc.CreateTextNode(h.name));
                numElem.AppendChild(xDoc.CreateTextNode(h.num.ToString()));
                areaElem.AppendChild(xDoc.CreateTextNode(h.area.ToString()));
                floorElem.AppendChild(xDoc.CreateTextNode(h.floor.ToString()));
                typeElem.AppendChild(xDoc.CreateTextNode(h.type));
                
                houseElem.AppendChild(nameElem);
                houseElem.AppendChild(numElem);
                houseElem.AppendChild(areaElem);
                houseElem.AppendChild(floorElem);
                houseElem.AppendChild(typeElem);
                houseRoot.AppendChild(houseElem);
            }
            xDoc.Save($"C:/C#/RunDll/XMLfiles/{fileName}.xml");

            foreach (Developer d in developers)
            {
                XmlElement devRoot = xDoc.DocumentElement;
                XmlElement devElem = xDoc.CreateElement("developer");
                XmlElement nameElem = xDoc.CreateElement("name");
                XmlElement incomeElem = xDoc.CreateElement("income");
                XmlElement addressElem = xDoc.CreateElement("address");

                nameElem.AppendChild(xDoc.CreateTextNode(d.name));
                incomeElem.AppendChild(xDoc.CreateTextNode(d.inc.ToString()));
                addressElem.AppendChild(xDoc.CreateTextNode(d.addr));
                
                devElem.AppendChild(nameElem);
                devElem.AppendChild(incomeElem);
                devElem.AppendChild(addressElem);
                devRoot.AppendChild(devElem);
            }
            xDoc.Save($"C:/C#/RunDll/XMLfiles/{fileName}.xml");
        }

        public void SaveInFile(List<Village> list)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load($"C:/C#/RunDll/XMLfiles/{fileName}.xml");

            foreach (Village v in villages)
            {
                XmlElement villRoot = xDoc.DocumentElement;
                XmlElement villElem = xDoc.CreateElement("village");
                XmlAttribute numAttr = xDoc.CreateAttribute("number");
                XmlElement nameElem = xDoc.CreateElement("name");
                XmlElement areaElem = xDoc.CreateElement("area");
                XmlElement peopleElem = xDoc.CreateElement("people");
                XmlElement devElem = xDoc.CreateElement("dev");

                nameElem.AppendChild(xDoc.CreateTextNode(v.name));
                devElem.AppendChild(xDoc.CreateTextNode(v.dev));
                areaElem.AppendChild(xDoc.CreateTextNode(v.area.ToString()));
                peopleElem.AppendChild(xDoc.CreateTextNode(v.people.ToString()));

                villElem.Attributes.Append(numAttr);
                villElem.AppendChild(nameElem);
                villElem.AppendChild(devElem);
                villElem.AppendChild(areaElem);
                villElem.AppendChild(peopleElem);
                villRoot.AppendChild(villElem);
            }
            xDoc.Save($"C:/C#/RunDll/XMLfiles/{fileName}.xml");
        }

        public void SaveInFile(List<House> list)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load($"C:/C#/RunDll/XMLfiles/{fileName}.xml");

            foreach (House h in houses)
            {
                XmlElement houseRoot = xDoc.DocumentElement;
                XmlElement houseElem = xDoc.CreateElement("house");
                XmlAttribute numberAttr = xDoc.CreateAttribute("number");
                XmlElement nameElem = xDoc.CreateElement("name");
                XmlElement numElem = xDoc.CreateElement("num");
                XmlElement areaElem = xDoc.CreateElement("area");
                XmlElement floorElem = xDoc.CreateElement("floor");
                XmlElement typeElem = xDoc.CreateElement("type");

                nameElem.AppendChild(xDoc.CreateTextNode(h.name));
                numElem.AppendChild(xDoc.CreateTextNode(h.num.ToString()));
                areaElem.AppendChild(xDoc.CreateTextNode(h.area.ToString()));
                floorElem.AppendChild(xDoc.CreateTextNode(h.floor.ToString()));
                typeElem.AppendChild(xDoc.CreateTextNode(h.type));

                houseElem.Attributes.Append(numberAttr);
                houseElem.AppendChild(nameElem);
                houseElem.AppendChild(numElem);
                houseElem.AppendChild(areaElem);
                houseElem.AppendChild(floorElem);
                houseElem.AppendChild(typeElem);
                houseRoot.AppendChild(houseElem);
            }
            xDoc.Save($"C:/C#/RunDll/XMLfiles/{fileName}.xml");
        }

        public void SaveInFile(List<Developer> list)
        {

            XmlTextWriter textWritter = new XmlTextWriter($"C:/C#/RunDll/XMLfiles/{fileName}.xml", Encoding.UTF8);
            textWritter.WriteStartDocument();
            textWritter.WriteStartElement("VillagesBase");
            textWritter.WriteEndElement();
            textWritter.Close();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load($"C:/C#/RunDll/XMLfiles/{fileName}.xml");

            foreach (Developer d in developers)
            {
                XmlElement devRoot = xDoc.DocumentElement;
                XmlElement devElem = xDoc.CreateElement("developer");
                XmlAttribute numberAttr = xDoc.CreateAttribute("number");
                XmlElement nameElem = xDoc.CreateElement("name");
                XmlElement incomeElem = xDoc.CreateElement("income");
                XmlElement addressElem = xDoc.CreateElement("address");

                nameElem.AppendChild(xDoc.CreateTextNode(d.name));
                incomeElem.AppendChild(xDoc.CreateTextNode(d.inc.ToString()));
                addressElem.AppendChild(xDoc.CreateTextNode(d.addr));

                devElem.Attributes.Append(numberAttr);
                devElem.AppendChild(nameElem);
                devElem.AppendChild(incomeElem);
                devElem.AppendChild(addressElem);
                devRoot.AppendChild(devElem);
            }
            xDoc.Save($"C:/C#/RunDll/XMLfiles/{fileName}.xml");
        }

        public List<Village> Search(string name)
        {
            List<Village> sortedList = new List<Village>();

            foreach(Village vill in villages)
            {
                if(vill.name == name)
                {
                    sortedList.Add(vill);
                }
            }
            return sortedList;
        }

        /// <summary>
        /// Выводит таблицу, внутри которой считывает данные о посёлке 
        /// </summary>
        public void ReadVellage()
        {
            Console.Clear();
            Load_DataBase(ref developers);
            Frames frame = new Frames();
            if (developers.Count == 0)
            {
                Console.SetCursorPosition(14, 6);
                frame.Continuous(50, "Ошибка!", "Необходимо сначала заполнить таблицу девелоперов");
                Console.ReadKey(true);
            }
            else
            {
                Load_DataBase(ref villages);
                Input inp = new Input();
                
                Console.WriteLine("╔════════════════════════╤═══════════════════════╤═══════════════╤═══════════╗");
                Console.WriteLine("║    Назвение посёлка    │       Девелопер       │ Площадь в м^2 │ Население ║");
                Console.WriteLine("╠════════════════════════╪═══════════════════════╪═══════════════╪═══════════╣");
                Console.WriteLine("║                        │                       │               │           ║");
                Console.Write    ("╚════════════════════════╧═══════════════════════╧═══════════════╧═══════════╝");
                bool canСontinue = true;
                while (canСontinue)
                {
                    Village vill = new Village("", "", 0, 0);
                    Console.SetCursorPosition(2, Console.CursorTop - 1);
                    Console.CursorVisible = true;
                    if (inp.ReadValid(ref vill.name, 22))
                    {
                        Console.SetCursorPosition(27, Console.CursorTop);
                        Console.CursorVisible = false;
                        vill.dev = ChoiceDeveloper(Console.CursorTop);
                        Console.CursorVisible = true;
                        Console.SetCursorPosition(51, Console.CursorTop);
                        inp.ReadValid(ref vill.area, 13);
                        Console.SetCursorPosition(67, Console.CursorTop);
                        inp.ReadValid(ref vill.people, 9);
                        Console.CursorVisible = false;
                        Console.SetCursorPosition(0, Console.CursorTop + 1);
                        Console.WriteLine("╟────────────────────────┼───────────────────────┼───────────────┼───────────╢");
                        Console.WriteLine("║                        │                       │               │           ║");
                        Console.Write("╚════════════════════════╧═══════════════════════╧═══════════════╧═══════════╝");
                        villages.Add(vill);
                    }
                    else
                    {
                        canСontinue = false;
                        if (frame.Call_MassageBox(30, 10, "Cозранить изменения?"))
                        {
                            SaveAll();
                        }
                    }
                }
            }
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        /// <summary>
        /// Выводит таблицу, внутри которой считывает данные о домах
        /// </summary>
        public void ReadHouse()
        {
            Load_DataBase(ref houses);
            Input inp = new Input();
            Frames frame = new Frames();
            Console.Clear();
            Console.WriteLine("╔════════════════════════╤════════════╤═══════════════╤═══════════════╤══════════════════════╗");
            Console.WriteLine("║    Назвение посёлка    │ Номер дома │ Площадь в м^2 │ Кол-во этажей │       Тип дома       ║");
            Console.WriteLine("╠════════════════════════╪════════════╪═══════════════╪═══════════════╪══════════════════════╣");
            Console.WriteLine("║                        │            │               │               │                      ║");
            Console.Write    ("╚════════════════════════╧════════════╧═══════════════╧═══════════════╧══════════════════════╝");
            bool canСontinue = true;
            while (canСontinue)
            {
                House house = new House("", 0, 0, 0, "");
                Console.SetCursorPosition(2, Console.CursorTop - 1);
                Console.CursorVisible = true;
                if (inp.ReadValid(ref house.name, 22))
                {
                    Console.SetCursorPosition(27, Console.CursorTop);
                    inp.ReadValid(ref house.num, 10);
                    Console.SetCursorPosition(40, Console.CursorTop);
                    inp.ReadValid(ref house.area, 13);
                    Console.SetCursorPosition(56, Console.CursorTop);
                    inp.ReadValid(ref house.floor, 13);
                    Console.SetCursorPosition(72, Console.CursorTop);
                    inp.ReadValid(ref house.type, 20);
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                    Console.WriteLine("╟────────────────────────┼────────────┼───────────────┼───────────────┼──────────────────────╢");
                    Console.WriteLine("║                        │            │               │               │                      ║");
                    Console.Write    ("╚════════════════════════╧════════════╧═══════════════╧═══════════════╧══════════════════════╝");
                    houses.Add(house);

                }
                else
                {
                    canСontinue = false;
                    
                    if (frame.Call_MassageBox(30, 10, "Cозранить изменения?"))
                    {
                        SaveAll();
                    }
                }
                    
            }
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        /// <summary>
        /// Выводит таблицу, внутри которой считывает данные о девелоперах
        /// </summary>
        public void ReadDeveloper()
        {
            Load_DataBase(ref developers);
            Input inp = new Input();
            Frames frame = new Frames();
            Console.Clear();
            Console.WriteLine("╔═══════════════════════╤═══════════════╤══════════════════════════════════╗");
            Console.WriteLine("║       Девелопер       │ Годовой доход │         Адрес девелопера         ║");
            Console.WriteLine("╠═══════════════════════╪═══════════════╪══════════════════════════════════╣");
            Console.WriteLine("║                       │               │                                  ║");
            Console.Write    ("╚═══════════════════════╧═══════════════╧══════════════════════════════════╝");
            bool canСontinue = true;
            while (canСontinue)
            {
                Developer dev = new Developer("", 0, "");
                Console.SetCursorPosition(2, Console.CursorTop - 1);
                Console.CursorVisible = true;
                if (inp.ReadValid(ref dev.name, 21))
                {
                    Console.SetCursorPosition(26, Console.CursorTop);
                    inp.ReadValid(ref dev.inc, 13);
                    Console.SetCursorPosition(42, Console.CursorTop);
                    inp.ReadValid(ref dev.addr, 32);
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                    Console.WriteLine("╟───────────────────────┼───────────────┼──────────────────────────────────╢");
                    Console.WriteLine("║                       │               │                                  ║");
                    Console.Write("╚═══════════════════════╧═══════════════╧══════════════════════════════════╝");
                    developers.Add(dev);
                }
                else
                {
                    canСontinue = false;
                    if (frame.Call_MassageBox(30, 10, "Cозранить изменения?"))
                    {
                        SaveAll();
                    }
                }
            }
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        /// <summary>
        /// Выводит таблицу с данными о посёлках
        /// </summary>
        public void WriteVillage()
        {
            //List<Village> villages = new List<Village>();
            Load_DataBase(ref villages);
            Frames frame = new Frames();

            if (villages.Count() == 0)
            {
                Console.Clear();
                frame.Menu(35, 6, 12, "Файл пуст!");
                Console.ReadKey(true);

            }
            else
            {
                Input inp = new Input();
                ConsoleKey? key = ConsoleKey.RightArrow;
                int index = -10;
                while (key != ConsoleKey.Escape)
                {
                    if ((key == ConsoleKey.LeftArrow) && (index != 0))
                    {
                        index -= 10;
                        Write_Page(index, villages);
                    }
                    else if ((key == ConsoleKey.RightArrow) && (index + 10 < villages.Count))
                    {
                        index += 10;
                        Write_Page(index, villages);
                    }
                    else if (key == ConsoleKey.Enter)
                        Choice(ref villages, index, 7, 32, 56, 72);

                    key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Enter, ConsoleKey.Escape);
                }
            }
            if (frame.Call_MassageBox(30, 6, "Сохранить изменения?"))
                SaveAll();
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        /// <summary>
        /// Выводит таблицу с данными о домах
        /// </summary>
        public void WriteHouse()
        {
            //List<House> houses = new List<House>();
            Load_DataBase(ref houses);
            Frames frame = new Frames();

            if (houses.Count() == 0)
            {
                Console.Clear();
                frame.Menu(35, 6, 12, "Файл пуст!");
                Console.ReadKey(true);
            }
            else
            {
                Input inp = new Input();
                ConsoleKey? key = ConsoleKey.RightArrow;
                int index = -10;
                while (key != ConsoleKey.Escape)
                {
                    if ((key == ConsoleKey.LeftArrow) && (index != 0))
                    {
                        index -= 10;
                        Write_Page(index, houses);
                    }
                    else if ((key == ConsoleKey.RightArrow) && (index + 10 < houses.Count))
                    {
                        index += 10;
                        Write_Page(index, houses);
                    }
                    else if (key == ConsoleKey.Enter)
                        Choice(ref houses, index, 7, 32, 56, 72);

                    key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Enter, ConsoleKey.Escape);
                }
            }
            if (frame.Call_MassageBox(30, 6, "Сохранить изменения?"))
                SaveAll();
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        /// <summary>
        /// Выводит таблицу с данными о девелоперах
        /// </summary>
        public void WriteDeveloper()
        {
            //List<Developer> developers = new List<Developer>();
            Load_DataBase(ref developers);
            Frames frame = new Frames();

            if (developers.Count() == 0)
            {
                Console.Clear();
                frame.Menu(35, 6, 12, "Файл пуст!");
                Console.ReadKey(true);
            }
            else
            {
                Input inp = new Input();
                ConsoleKey? key = ConsoleKey.RightArrow;
                int index = -10;
                while (key != ConsoleKey.Escape)
                {
                    if ((key == ConsoleKey.LeftArrow) && (index != 0))
                    {
                        index -= 10;
                        Write_Page(index, developers);
                    }
                    else if ((key == ConsoleKey.RightArrow) && (index + 10 < developers.Count))
                    {
                        index += 10;
                        Write_Page(index, developers);
                    }
                    else if (key == ConsoleKey.Enter)
                        Choice(ref developers, index, 7, 32, 56, 72);

                    key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Enter, ConsoleKey.Escape);
                }
            }
            if (frame.Call_MassageBox(30, 6, "Сохранить изменения?"))
                SaveAll();
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        private List<Village> Load_VillageBase()
        {
            try
            {
                List<Village> villages = new List<Village>();
                XmlDocument xVill = new XmlDocument();
                xVill.Load($"C:/C#/RunDll/XMLfiles/{fileName}.xml");
                XmlElement villRoot = xVill.DocumentElement;

                foreach (XmlElement xnode in villRoot)
                {
                    if (xnode.Name == "village")
                    {
                        Village vill = new Village();
                        //XmlNode attr = xnode.Attributes.GetNamedItem("number");
                        //if (attr != null)
                        //    vill.number = UInt32.Parse(attr.Value);

                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            switch (childnode.Name)
                            {
                                case "name":
                                    vill.name = childnode.InnerText;
                                    break;
                                case "dev":
                                    vill.dev = childnode.InnerText;
                                    break;
                                case "area":
                                    vill.area = float.Parse(childnode.InnerText);
                                    break;
                                case "people":
                                    vill.people = UInt32.Parse(childnode.InnerText);
                                    break;
                            }
                        }
                        villages.Add(vill);
                    }
                }
            }
            catch
            {
                villages = null;
            }
            return villages;
        }

        public void Load_DataBase (ref List<Village> villages)
        {
            try
            {
                XmlDocument xVill = new XmlDocument();
                xVill.Load($"C:/C#/RunDll/XMLfiles/{fileName}.xml");
                XmlElement villRoot = xVill.DocumentElement;

                villages.Clear();
                foreach (XmlElement xnode in villRoot)
                {
                    if (xnode.Name == "village")
                    {
                        Village vill = new Village();
                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            switch (childnode.Name)
                            {
                                case "name":
                                    vill.name = childnode.InnerText;
                                    break;
                                case "dev":
                                    vill.dev = childnode.InnerText;
                                    break;
                                case "area":
                                    vill.area = float.Parse(childnode.InnerText);
                                    break;
                                case "people":
                                    vill.people = UInt32.Parse(childnode.InnerText);
                                    break;
                            }
                        }
                        villages.Add(vill);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                villages = null;
            }
            
        }

        public void Load_DataBase(ref List<House> houses)
        {
            try
            {
                XmlDocument xHouse = new XmlDocument();
                xHouse.Load($"C:/C#/RunDll/XMLfiles/{fileName}.xml");
                XmlElement houseRoot = xHouse.DocumentElement;

                houses.Clear();

                foreach (XmlElement xnode in houseRoot)
                {
                    if (xnode.Name == "house")
                    {
                        House house = new House();
                        //XmlNode attr = xnode.Attributes.GetNamedItem("number");
                        //if (attr != null)
                        //    house.number = UInt32.Parse(attr.Value);

                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            switch (childnode.Name)
                            {
                                case "name":
                                    house.name = childnode.InnerText;
                                    break;
                                case "num":
                                    house.num = UInt16.Parse(childnode.InnerText);
                                    break;
                                case "area":
                                    house.area = float.Parse(childnode.InnerText);
                                    break;
                                case "floor":
                                    house.floor = Byte.Parse(childnode.InnerText);
                                    break;
                                case "type":
                                    house.type = childnode.InnerText;
                                    break;
                            }
                        }
                        houses.Add(house);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                houses = null;
            }
            
        }

        public void Load_DataBase(ref List<Developer> developers)
        {
            try
            {
                XmlDocument xDev = new XmlDocument();
                xDev.Load($"C:/C#/RunDll/XMLfiles/{fileName}.xml");
                XmlElement devRoot = xDev.DocumentElement;

                developers.Clear();
                foreach (XmlElement xnode in devRoot)
                {
                    if (xnode.Name == "developer")
                    {

                        Developer dev = new Developer();
                        //XmlNode attr = xnode.Attributes.GetNamedItem("number");
                        //if (attr != null)
                        //    dev.number = UInt32.Parse(attr.Value);

                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            switch (childnode.Name)
                            {
                                case "name":
                                    dev.name = childnode.InnerText;
                                    break;
                                case "income":
                                    dev.inc = float.Parse(childnode.InnerText);
                                    break;
                                case "address":
                                    dev.addr = childnode.InnerText;
                                    break;
                            }
                        }
                        developers.Add(dev);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                developers = null;
            }            
        }

        private void Choice<T>(ref List<T> list, int page, params byte[] coordinates)
        {
            coordinates.ToList();
            Console.CursorVisible = true;
            int x = 0, y = 3;
            int i = page;
            Console.SetCursorPosition(coordinates[x], y);
            Input inp = new Input();
            ConsoleKey? key = null;
            while ((key != ConsoleKey.Escape) && (key != ConsoleKey.Delete))
            {
                key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Delete, ConsoleKey.Escape);
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (x + 1 <= coordinates.Count() - 1)
                            x++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (x - 1 >= 0)
                            x--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y + 1 <= 20)
                        {
                            y += 2;
                            i++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y - 1 >= 3)
                        {
                            y -= 2;
                            i--;
                        }
                        break;
                    case ConsoleKey.Delete:
                        list.RemoveAt(i);
                        Write_Page(page, list);
                        break;
                    case ConsoleKey.Enter:

                        break;
                }
                //Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(coordinates[x], y);
                //Console.Write("█");
            }
            
            //Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
        }

        //private void Write_Page<T>(int index, List<T> list)
        //{
        //    if (list.GetType() == typeof(List<Village>))
        //    {
        //        Console.Clear();
        //        Console.WriteLine("╔═════╦════════════════════════╤═══════════════════════╤═══════════════╤═══════════╗");
        //        Console.WriteLine("║  №  ║    Назвение посёлка    │       Девелопер       │ Площадь в м^2 │ Население ║");
        //        Console.WriteLine("╠═════╬════════════════════════╪═══════════════════════╪═══════════════╪═══════════╣");
                
        //        for (byte i = 0; i < 10; i++, index++)
        //        {
        //            if (index < villages.Count())
        //            {
        //                Console.WriteLine("║     ║                        │                       │               │           ║");
        //                Console.Write    ("╟─────╫────────────────────────┼───────────────────────┼───────────────┼───────────╢");
        //                Console.SetCursorPosition(2, Console.CursorTop - 1);
        //                Console.Write(index + 1);
        //                Console.SetCursorPosition(8, Console.CursorTop);
        //                Console.Write(villages[index].name);
        //                Console.SetCursorPosition(33, Console.CursorTop);
        //                Console.Write(villages[index].dev);
        //                Console.SetCursorPosition(57, Console.CursorTop);
        //                Console.Write(villages[index].area);
        //                Console.SetCursorPosition(73, Console.CursorTop);
        //                Console.WriteLine(villages[index].people);
        //                Console.SetCursorPosition(0, Console.CursorTop + 1);
        //            }
        //        }
        //        Console.SetCursorPosition(0, Console.CursorTop - 1);
        //        Console.WriteLine("╚═════╩════════════════════════╧═══════════════════════╧═══════════════╧═══════════╝");
        //    }
        //    else if (list.GetType() == typeof(List<House>))
        //    {
        //        Console.Clear();
        //        Console.WriteLine        ("╔═════╦════════════════════════════╤════════════╤═════════════════╤═══════════════════╤══════════════════════╗");
        //        Console.WriteLine        ("║  №  ║      Назвение посёлка      │ Номер дома │ Площадь в м ^ 2 │  Кол - во этажей  │       Тип дома       ║");
        //        Console.WriteLine        ("╠═════╬════════════════════════════╪════════════╪═════════════════╪═══════════════════╪══════════════════════╣");
        //        for (byte i = 0; i < 10; i++, index++)
        //        {
        //            if (index < houses.Count())
        //            {
        //                Console.WriteLine("║     ║                            │            │                 │                   │                      ║");  
        //                Console.Write    ("╟─────╫────────────────────────────┼────────────┼─────────────────┼───────────────────┼──────────────────────╢");

        //                Console.SetCursorPosition(2, Console.CursorTop - 1);
        //                Console.Write(index + 1);
        //                Console.SetCursorPosition(8, Console.CursorTop);
        //                Console.Write(houses[index].name);
        //                Console.SetCursorPosition(37, Console.CursorTop);
        //                Console.Write(houses[index].num);
        //                Console.SetCursorPosition(50, Console.CursorTop);
        //                Console.Write(houses[index].area);
        //                Console.SetCursorPosition(68, Console.CursorTop);
        //                Console.Write(houses[index].floor);
        //                Console.SetCursorPosition(88, Console.CursorTop);
        //                Console.WriteLine(houses[index].type);
        //                Console.SetCursorPosition(0, Console.CursorTop + 1);
        //            }
        //        }
        //        Console.SetCursorPosition(0, Console.CursorTop - 1);
        //        Console.WriteLine       ("╚═════╩════════════════════════════╧════════════╧═════════════════╧═══════════════════╧══════════════════════╝");
        //    }
        //    else if (list.GetType() == typeof(List<Developer>))
        //    {
        //        Console.Clear();
        //        Console.WriteLine("╔═════╦═══════════════════════╤═══════════════╤══════════════════════════════════╗");
        //        Console.WriteLine("║  №  ║     Девелопер         │ Годовой доход │         Адрес девелопера         ║");
        //        Console.WriteLine("╠═════╬═══════════════════════╪═══════════════╪══════════════════════════════════╣");
        //        for (byte i = 0; i < 10; i++, index++)
        //        {
        //            if (index < developers.Count())
        //            {
        //                Console.WriteLine("║     ║                       │               │                                  ║");
        //                Console.Write    ("╟─────╫───────────────────────┼───────────────┼──────────────────────────────────╢");
        //                Console.SetCursorPosition(2, Console.CursorTop - 1);
        //                Console.Write(index + 1);
        //                Console.SetCursorPosition(8, Console.CursorTop);
        //                Console.Write(developers[index].name);
        //                Console.SetCursorPosition(32, Console.CursorTop);
        //                Console.Write(developers[index].inc);
        //                Console.SetCursorPosition(48, Console.CursorTop);
        //                Console.WriteLine(developers[index].addr);
        //                Console.SetCursorPosition(0, Console.CursorTop + 1);
        //            }
        //        }
        //        Console.SetCursorPosition(0, Console.CursorTop - 1);
        //        Console.WriteLine("╚═════╩═══════════════════════╧═══════════════╧══════════════════════════════════╝");
        //    }

        //    int lastPage = list.Count() % 10 == 0 ? (list.Count() / 10) : (list.Count() / 10 + 1);
        //    int x = Console.CursorLeft, y = Console.CursorTop;
        //    Frames frame = new Frames();
        //    frame.Menu(x, y, 20, "Стр. " + index / 10 + " из " + lastPage);
        //}

        private void Write_Page(int index, List<Village> villages)
        {
            Console.Clear();
            Console.WriteLine("╔═════╦════════════════════════╤═══════════════════════╤═══════════════╤═══════════╗");
            Console.WriteLine("║  №  ║    Назвение посёлка    │       Девелопер       │ Площадь в м^2 │ Население ║");
            Console.WriteLine("╠═════╬════════════════════════╪═══════════════════════╪═══════════════╪═══════════╣");

            for (byte i = 0; i < 10; i++, index++)
            {
                if (index < villages.Count())
                {
                    Console.WriteLine("║     ║                        │                       │               │           ║");
                    Console.Write("╟─────╫────────────────────────┼───────────────────────┼───────────────┼───────────╢");
                    Console.SetCursorPosition(2, Console.CursorTop - 1);
                    Console.Write(index + 1);
                    Console.SetCursorPosition(8, Console.CursorTop);
                    Console.Write(villages[index].name);
                    Console.SetCursorPosition(33, Console.CursorTop);
                    Console.Write(villages[index].dev);
                    Console.SetCursorPosition(57, Console.CursorTop);
                    Console.Write(villages[index].area);
                    Console.SetCursorPosition(73, Console.CursorTop);
                    Console.WriteLine(villages[index].people);
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("╚═════╩════════════════════════╧═══════════════════════╧═══════════════╧═══════════╝");
            Frames frame = new Frames();
            int lastPage;
            lastPage = villages.Count() % 10 == 0 ? (villages.Count() / 10) : (villages.Count() / 10 + 1);
            int x = Console.CursorLeft, y = Console.CursorTop;
            frame.Menu(x, y, 20, "Стр. " + index / 10 + " из " + lastPage);
        }

        private void Write_Page(int index, List<House> houses)
        {
            Console.Clear();
            Console.WriteLine("╔═════╦════════════════════╤════════════╤═══════════════╤═══════════════╤════════════════╗");
            Console.WriteLine("║  №  ║  Назвение посёлка  │ Номер дома │ Площадь в м^2 │ Кол-во этажей │    Тип дома    ║");
            Console.WriteLine("╠═════╬════════════════════╪════════════╪═══════════════╪═══════════════╪════════════════╣");
            for (byte i = 0; i < 10; i++, index++)
            {
                if (index < houses.Count())
                {
                    Console.WriteLine("║     ║                    │            │               │               │                ║");
                    Console.Write("╟─────╫────────────────────┼────────────┼───────────────┼───────────────┼────────────────╢");

                    Console.SetCursorPosition(2, Console.CursorTop - 1);
                    Console.Write(1);
                    Console.SetCursorPosition(8, Console.CursorTop);
                    Console.Write(houses[index].name);
                    Console.SetCursorPosition(29, Console.CursorTop);
                    Console.Write(houses[index].num);
                    Console.SetCursorPosition(42, Console.CursorTop);
                    Console.Write(houses[index].area);
                    Console.SetCursorPosition(58, Console.CursorTop);
                    Console.Write(houses[index].floor);
                    Console.SetCursorPosition(74, Console.CursorTop);
                    Console.WriteLine(houses[index].type);
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("╚═════╩════════════════════╧════════════╧═══════════════╧═══════════════╧════════════════╝");
            Frames frame = new Frames();
            int lastPage;
            lastPage = houses.Count() % 10 == 0 ? (houses.Count() / 10) : (houses.Count() / 10 + 1);
            int x = Console.CursorLeft, y = Console.CursorTop;
            frame.Menu(x, y, 20, "Стр. " + index / 10 + " из " + lastPage);
        }

        private void Write_Page(int index, List<Developer> developers)
        {
            Console.Clear();
            Console.WriteLine("╔═════╦═══════════════════════╤═══════════════╤══════════════════════════════════╗");
            Console.WriteLine("║  №  ║     Девелопер         │ Годовой доход │         Адрес девелопера         ║");
            Console.WriteLine("╠═════╬═══════════════════════╪═══════════════╪══════════════════════════════════╣");
            for (byte i = 0; i < 10; i++, index++)
            {
                if (index < developers.Count())
                {

                    Console.WriteLine("║     ║                       │               │                                  ║");
                    Console.Write("╟─────╫───────────────────────┼───────────────┼──────────────────────────────────╢");

                    Console.SetCursorPosition(2, Console.CursorTop - 1);
                    Console.Write(1);
                    Console.SetCursorPosition(8, Console.CursorTop);
                    Console.Write(developers[index].name);
                    Console.SetCursorPosition(32, Console.CursorTop);
                    Console.Write(developers[index].inc);
                    Console.SetCursorPosition(48, Console.CursorTop);
                    Console.WriteLine(developers[index].addr);
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("╚═════╩═══════════════════════╧═══════════════╧══════════════════════════════════╝");
            Frames frame = new Frames();
            int lastPage;
            lastPage = developers.Count() % 10 == 0 ? (developers.Count() / 10) : (developers.Count() / 10 + 1);
            int x = Console.CursorLeft, y = Console.CursorTop;
            frame.Menu(x, y, 20, "Стр. " + index / 10 + " из " + lastPage);
        }

        public List<string> GetDevNames()
        {
            List<string> names = new List<string>();
            try
            {
                XmlDocument xDev = new XmlDocument();
                xDev.Load($"C:/C#/RunDll/XMLfiles/{fileName}.xml");
                XmlElement devRoot = xDev.DocumentElement;
                foreach (XmlElement xnode in devRoot)
                {
                    if (xnode.Name == "developer")
                    {
                        string str = "";
                        foreach (XmlNode childnode in xnode.ChildNodes)
                        {
                            if (childnode.Name == "name")
                            {
                                str = childnode.InnerText;
                            }
                        }
                        names.Add(str);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                names = null;
            }
            
            return names;
        }
        
        private string ChoiceDeveloper(int readPosY)
        {
            Console.SetCursorPosition(80, 0);
            List<string> developerNames = GetDevNames();
            Frames frame = new Frames();
            frame.Continuous(25, "Выберите девелопера", developerNames.ToArray());
            ushort x = 80;
            ushort y = 2;
            int i = 0;
            frame.Choice(x, y, ConsoleColor.Green, 25);
            Input inp = new Input();
            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = inp.InputKey(ConsoleKey.Escape, ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Enter);
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if ((y / 2) < developerNames.Count - 1)
                        {
                            i++;
                            frame.ContinuousChoice(x, y, ConsoleColor.White, 25);
                            y += 2;
                            frame.Choice(x, y, ConsoleColor.Green, 25);
                            if (y == 4)
                            {
                                Console.SetCursorPosition(x, 2);
                                string line = new string('═', 25);
                                Console.WriteLine($"╠{line}╣");
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if ((y / 2) >= 2)
                        {
                            i--;
                            frame.ContinuousChoice(x, y, ConsoleColor.White, 25);
                            y -= 2;
                            //Console.WriteLine(y);
                            //Console.WriteLine(developers.Count);
                            frame.Choice(x, y, ConsoleColor.Green, 25);
                            if ((y / 2) == developerNames.Count - 2)
                            {
                                Console.SetCursorPosition(x, (developerNames.Count) * 2);
                                string line = new string('═', 25);
                                Console.WriteLine($"╚{line}╝");
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(27, readPosY);
                        Console.Write(developerNames[i]);
                        break;
                }
            }
            return developerNames[i];
        }

        /// <summary>
        /// Создаёт новый файл с введённым пользователем именем
        /// </summary>
        public void CreateFile()
        {
            Frames frame = new Frames();
            Console.SetCursorPosition(30, 9);
            frame.Continuous(34, "Введите имя файла", "");
            Input inp = new Input();
            bool canСontinue = true;
            while (canСontinue)
            {
                Console.SetCursorPosition(32, 12);
                Console.CursorVisible = true;
                if (inp.ReadValid(ref fileName, 31))
                {
                    if (!File.Exists($"C:/C#/RunDll/XMLfiles/{fileName}.xml"))
                    {
                        Console.CursorVisible = false;
                        try
                        {
                            Create_XmlFile(fileName);
                            canСontinue = false;
                        }
                        catch (Exception)
                        {
                            Console.CursorVisible = false;
                            string clear = new string(' ', 32);
                            Console.SetCursorPosition(32, 12);
                            Console.Write(clear);
                            Console.SetCursorPosition(32, 12);
                            Console.Write("Недопустимые знаки в имени файла");
                            Thread.Sleep(600);
                            Console.SetCursorPosition(32, 12);
                            Console.Write(clear);
                        }
                    }
                    else
                    {
                        Console.CursorVisible = false;
                        string clear = new string(' ', 28);
                        Console.SetCursorPosition(32, 12);
                        Console.Write(clear);
                        Console.SetCursorPosition(32, 12);
                        Console.Write("Файл уже существует");
                        Thread.Sleep(600);
                        Console.SetCursorPosition(32, 12);
                        Console.Write(clear);
                    }
                }
                else
                    canСontinue = false;
            }
        }

        /// <summary>
        /// Выводит список существующих файлов
        /// </summary>
        public void Write_FileList(bool del = false)
        {
            List<string> files = Load_FileList();
            
            Console.SetCursorPosition(30, 3);
            Frames frame = new Frames();
            Input inp = new Input();
            frame.Continuous(30, "Выберете файл", files.ToArray());
            int y = 5;
            int x = 30;
            int i = 0;
            ConsoleKey? key = null;
            frame.Choice(x, y, ConsoleColor.Green, 30);
            while ((key != ConsoleKey.Enter) && (key != ConsoleKey.Escape))
            {
                key = inp.InputKey(ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Enter, ConsoleKey.Escape);
                switch (key)
                {
                    case ConsoleKey.DownArrow:

                        if ((y / 2) < files.Count + 1)
                        {
                            frame.ContinuousChoice(x, y, ConsoleColor.White, 30);
                            y += 2;
                            ++i;
                            frame.Choice(x, y, ConsoleColor.Green, 30);
                            if (y == 7)
                            {
                                Console.SetCursorPosition(x, 5);
                                string line = new string('═', 30);
                                Console.WriteLine($"╠{line}╣");
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y != 5)
                        {
                            frame.ContinuousChoice(x, y, ConsoleColor.White, 30);
                            y -= 2;
                            --i;
                            frame.Choice(x, y, ConsoleColor.Green, 30);

                            if ((y / 2) == files.Count)
                            {
                                Console.SetCursorPosition(x, (files.Count + 1) * 2 + 3);
                                string line = new string('═', 30);
                                Console.WriteLine($"╚{line}╝");
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        if ((del) && (frame.Call_MassageBox(30, 3, "Удалить файл?")))
                        {
                            File.Delete($"C:/C#/RunDll/XMLfiles/{files[i]}.xml");
                        }
                        else
                            fileName = files[i];
                        break;
                }

            }

        }

        /// <summary>
        /// Создаёт новый XML файл
        /// </summary>
        public void Create_XmlFile(string name)
        {
            XmlTextWriter textWritter = new XmlTextWriter($"C:/C#/RunDll/XMLfiles/{name}.xml", Encoding.UTF8);
            textWritter.WriteStartDocument();
            textWritter.WriteStartElement("Villages");
            textWritter.WriteEndElement();
            textWritter.Close();
        }

        /// <summary>
        /// Возвращает список существующих XML файлов
        /// </summary>
        /// <returns></returns>
        public List<string> Load_FileList()
        {
            //List<string> fileNames = new List<string>();
            //fileNames = Directory.GetFiles(@"C:/C#/RunDll/XMLfiles/", "*.xml").ToList<string>();
            DirectoryInfo dinfo = new DirectoryInfo(@"C:/C#/RunDll/XMLfiles");
            FileInfo[] fileInfo = dinfo.GetFiles();
            List<string> files = new List<string>();

            foreach (FileInfo f in fileInfo)
                files.Add(f.ToString().Replace(".xml", ""));

            return files;
        }
    }
}