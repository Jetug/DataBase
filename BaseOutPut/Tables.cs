// Программа для работы с базой данных посёлков
//                Вариант №20
//     Выполнил Сергеев Кирилл Дмитриевич 
//                Группа 206
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Shell;
using Correct_Input;

namespace BaseOutPut
{
    public class Tables
    {
        public Tables()
        {
            Console.CursorVisible = false;
        }
        public string fileName = "Villages.xml";
        ConsoleColor col = ConsoleColor.Green;

        public struct Village
        {
            public uint number;
            public string name;
            public string dev;
            public float area;
            public uint people;
            public Village(uint number, string name, string dev, float area, uint people)
            {
                this.number = number;
                this.name = name;
                this.dev = dev;
                this.area = area;
                this.people = people;
            }
        }
        public struct House
        {
            public uint number;
            public string name;
            public ushort num;
            public float area;
            public byte floor;
            public string type;
            public House(uint number, string name, ushort num, float area, byte floor, string type)
            {
                this.number = number;
                this.name = name;
                this.num = num;
                this.area = area;
                this.floor = floor;
                this.type = type;
            }
        }
        public struct Developer
        {
            public uint number;
            public string name;
            public float inc;
            public string addr;
            public Developer(uint number, string name, float inc, string addr)
            {
                this.number = number;
                this.name = name;
                this.inc = inc;
                this.addr = addr;
            }
        }
        //public struct DataBase
        //{
        //    public Village vill;
        //    public House hs;
        //    public Developer dev;
        //}
        Frames frame = new Frames();
        Input inp = new Input();

        /// <summary>
        /// Выводит таблицу, внутри которой считывает данные о посёлке 
        /// </summary>
        public void ReadVellage()
        {
            List<Village> villages = new List<Village>();
            Village vill = new Village(0, "", "", 0, 0);
            Console.Clear();
            Console.WriteLine   ("╔════════════════════════╤═══════════════════════╤═══════════════╤═══════════╗");
            Console.WriteLine   ("║    Назвение посёлка    │       Девелопер       │ Площадь в м^2 │ Население ║");
            Console.WriteLine   ("╠════════════════════════╪═══════════════════════╪═══════════════╪═══════════╣");
            Console.WriteLine   ("║                        │                       │               │           ║");
            Console.Write       ("╚════════════════════════╧═══════════════════════╧═══════════════╧═══════════╝");
            ConsoleKey? key = null;
            while (key != ConsoleKey.Escape)
            {
                Console.SetCursorPosition(2, Console.CursorTop-1);
                Console.CursorVisible = true;
                inp.ReadValid(ref vill.name, 22);
                Console.SetCursorPosition(27, Console.CursorTop);
                Console.CursorVisible = false;
                vill.dev = ChoiceDeveloper(Console.CursorTop);
                Console.CursorVisible = true;
                //inp.ReadValid(ref vill.dev, 21);
                Console.SetCursorPosition(51, Console.CursorTop);
                inp.ReadValid(ref vill.area, 13);
                Console.SetCursorPosition(67, Console.CursorTop);
                inp.ReadValid(ref vill.people, 9);
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.WriteLine("╟────────────────────────┼───────────────────────┼───────────────┼───────────╢");
                Console.WriteLine("║                        │                       │               │           ║");
                Console.Write    ("╚════════════════════════╧═══════════════════════╧═══════════════╧═══════════╝");
                villages.Add(vill);

                key = inp.InputKey(ConsoleKey.Tab, ConsoleKey.Escape);
                if ((key == ConsoleKey.Escape) && (Call_SaveMassage(30, 10)))
                {
                    foreach (Village v in villages)
                    {
                        XmlDocument xVill = new XmlDocument();
                        xVill.Load($"C:/C#/RunDll/XMLfiles/{fileName}");
                        XmlElement villRoot = xVill.DocumentElement;
                        XmlElement villElem = xVill.CreateElement("village");
                        XmlAttribute numAttr = xVill.CreateAttribute("number");
                        XmlElement nameElem = xVill.CreateElement("name");
                        XmlElement areaElem = xVill.CreateElement("area");
                        XmlElement peopleElem = xVill.CreateElement("people");
                        XmlElement devElem = xVill.CreateElement("dev");

                        uint number = 0;
                        foreach (XmlElement xnode in villRoot)
                        {
                            if (xnode.Name == "village")
                            {
                                XmlNode attr = xnode.Attributes.GetNamedItem("number");
                                if (attr != null)
                                    number = UInt32.Parse(attr.Value) + 1;
                            }
                        }
                        numAttr.AppendChild(xVill.CreateTextNode(number.ToString()));
                        nameElem.AppendChild(xVill.CreateTextNode(v.name));
                        devElem.AppendChild(xVill.CreateTextNode(v.dev));
                        areaElem.AppendChild(xVill.CreateTextNode(v.area.ToString()));
                        peopleElem.AppendChild(xVill.CreateTextNode(v.people.ToString()));

                        villElem.Attributes.Append(numAttr);
                        villElem.AppendChild(nameElem);
                        villElem.AppendChild(devElem);
                        villElem.AppendChild(areaElem);
                        villElem.AppendChild(peopleElem);
                        villRoot.AppendChild(villElem);
                        xVill.Save($"C:/C#/RunDll/XMLfiles/{fileName}");
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
            List<House> houses = new List<House>();
            House house = new House(0,"",0,0,0,"");
            Console.Clear();
            Console.WriteLine  ("╔════════════════════════╤════════════╤═══════════════╤═══════════════╤══════════════════════╗");
            Console.WriteLine  ("║    Назвение посёлка    │ Номер дома │ Площадь в м^2 │ Кол-во этажей │       Тип дома       ║");
            Console.WriteLine  ("╠════════════════════════╪════════════╪═══════════════╪═══════════════╪══════════════════════╣");
            Console.WriteLine  ("║                        │            │               │               │                      ║");
            Console.Write      ("╚════════════════════════╧════════════╧═══════════════╧═══════════════╧══════════════════════╝");
            
            ConsoleKey? key = null;
            while (key != ConsoleKey.Escape)
            {
                Console.SetCursorPosition(2, Console.CursorTop - 1);
                Console.CursorVisible = true;
                inp.ReadValid(ref house.name, 22);
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

                key = inp.InputKey(ConsoleKey.Tab, ConsoleKey.Escape);
                if ((key == ConsoleKey.Escape) && (Call_SaveMassage(30, 10)))
                {
                    foreach (House h in houses)
                    {
                        XmlDocument xHouse = new XmlDocument();
                        xHouse.Load($"C:/C#/RunDll/XMLfiles/{fileName}");
                        XmlElement houseRoot = xHouse.DocumentElement;
                        XmlElement houseElem = xHouse.CreateElement("house");
                        XmlAttribute numberAttr = xHouse.CreateAttribute("number");
                        XmlElement nameElem = xHouse.CreateElement("name");
                        XmlElement numElem = xHouse.CreateElement("num");
                        XmlElement areaElem = xHouse.CreateElement("area");
                        XmlElement floorElem = xHouse.CreateElement("floor");
                        XmlElement typeElem = xHouse.CreateElement("type");

                        uint number = 0;
                        foreach (XmlElement xnode in houseRoot)
                        {
                            if (xnode.Name == "house")
                            {
                                XmlNode attr = xnode.Attributes.GetNamedItem("number");
                                if (attr != null)
                                    number = UInt32.Parse(attr.Value) + 1;
                            }
                        }

                        numberAttr.AppendChild(xHouse.CreateTextNode(number.ToString()));
                        nameElem.AppendChild(xHouse.CreateTextNode(h.name));
                        numElem.AppendChild(xHouse.CreateTextNode(h.num.ToString()));
                        areaElem.AppendChild(xHouse.CreateTextNode(h.area.ToString()));
                        floorElem.AppendChild(xHouse.CreateTextNode(h.floor.ToString()));
                        typeElem.AppendChild(xHouse.CreateTextNode(h.type));

                        houseElem.Attributes.Append(numberAttr);
                        houseElem.AppendChild(nameElem);
                        houseElem.AppendChild(numElem);
                        houseElem.AppendChild(areaElem);
                        houseElem.AppendChild(floorElem);
                        houseElem.AppendChild(typeElem);
                        houseRoot.AppendChild(houseElem);
                        xHouse.Save("C:/C#/RunDll/XMLfiles/Villages.xml");
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
            List<Developer> developers = new List<Developer>();
            Developer dev = new Developer(0,"",0,"");
            Console.Clear();
            Console.WriteLine    ("╔═══════════════════════╤═══════════════╤══════════════════════════════════╗");
            Console.WriteLine    ("║       Девелопер       │ Годовой доход │         Адрес девелопера         ║");
            Console.WriteLine    ("╠═══════════════════════╪═══════════════╪══════════════════════════════════╣");
            Console.WriteLine    ("║                       │               │                                  ║");
            Console.Write        ("╚═══════════════════════╧═══════════════╧══════════════════════════════════╝");
            ConsoleKey? key = null;
            while (key != ConsoleKey.Escape)
            {
                Console.SetCursorPosition(2, Console.CursorTop - 1);
                Console.CursorVisible = true;
                inp.ReadValid(ref dev.name, 21);
                Console.SetCursorPosition(26, Console.CursorTop);
                inp.ReadValid(ref dev.inc, 13);
                Console.SetCursorPosition(42, Console.CursorTop);
                inp.ReadValid(ref dev.addr, 32);
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, Console.CursorTop + 1);
                Console.WriteLine("╟───────────────────────┼───────────────┼──────────────────────────────────╢");
                Console.WriteLine("║                       │               │                                  ║");
                Console.Write    ("╚═══════════════════════╧═══════════════╧══════════════════════════════════╝");
                developers.Add(dev);

                key = inp.InputKey(ConsoleKey.Tab, ConsoleKey.Escape);
                if ((key == ConsoleKey.Escape) && (Call_SaveMassage(30, 10)))
                {
                    foreach (Developer d in developers)
                    {
                        XmlDocument xDev = new XmlDocument();
                        xDev.Load($"C:/C#/RunDll/XMLfiles/{fileName}");
                        XmlElement devRoot = xDev.DocumentElement;
                        XmlElement devElem = xDev.CreateElement("developer");
                        XmlAttribute numberAttr = xDev.CreateAttribute("number");
                        XmlElement nameElem = xDev.CreateElement("name");
                        XmlElement incomeElem = xDev.CreateElement("income");
                        XmlElement addressElem = xDev.CreateElement("address");

                        uint number = 0;
                        foreach (XmlElement xnode in devRoot)
                        {
                            if (xnode.Name == "house")
                            {
                                XmlNode attr = xnode.Attributes.GetNamedItem("number");
                                if (attr != null)
                                    number = UInt32.Parse(attr.Value) + 1;
                            }
                        }

                        numberAttr.AppendChild(xDev.CreateTextNode(number.ToString()));
                        nameElem.AppendChild(xDev.CreateTextNode(d.name));
                        incomeElem.AppendChild(xDev.CreateTextNode(d.inc.ToString()));
                        addressElem.AppendChild(xDev.CreateTextNode(d.addr));

                        devElem.Attributes.Append(numberAttr);
                        devElem.AppendChild(nameElem);
                        devElem.AppendChild(incomeElem);
                        devElem.AppendChild(addressElem);
                        devRoot.AppendChild(devElem);
                        xDev.Save($"C:/C#/RunDll/XMLfiles/{fileName}");
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
            List<Village> villages = new List<Village>();
            XmlDocument xVill = new XmlDocument();
            xVill.Load($"C:/C#/RunDll/XMLfiles/{fileName}");
            XmlElement villRoot = xVill.DocumentElement;

            foreach (XmlElement xnode in villRoot)
            {
                if (xnode.Name == "village")
                {
                    Village vill = new Village();
                    XmlNode attr = xnode.Attributes.GetNamedItem("number");
                    if (attr != null)
                        vill.number = UInt32.Parse(attr.Value);

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
            //villages = LoadBase<Village>("village");
            //villages = FillList("village");
            ConsoleKey? key = ConsoleKey.RightArrow;
            int index = -10;
            while (key != ConsoleKey.Escape)
            {
                if ((key == ConsoleKey.LeftArrow) && (index != 0))
                {
                    index -= 10;
                    VillageList(index, villages);
                }
                else if((key == ConsoleKey.RightArrow) && (index + 10 < villages.Count))
                {
                    index += 10;
                    VillageList(index, villages);
                }
                {
                    //switch (key)
                    //{
                    //    case ConsoleKey.LeftArrow:
                    //        if (index != 0)
                    //        {
                    //            index -= 10;
                    //            VillageList(index, villages);
                    //        }
                    //        break;
                    //    case ConsoleKey.RightArrow:
                    //        if (index + 10 < villages.Count)
                    //        {
                    //            index += 10;
                    //            VillageList(index, villages);
                    //        }
                    //        break;
                    //    //case ConsoleKey.Escape:
                    //    //    Console.Clear();
                    //    //    frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
                    //    //    break;
                    //}
                }
                key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Escape);
            }
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }
        
        /// <summary>
        /// Выводит таблицу с данными о домах
        /// </summary>
        public void WriteHouse()
        {
            List<House> houses = new List<House>();

            XmlDocument xHouse = new XmlDocument();
            xHouse.Load($"C:/C#/RunDll/XMLfiles/{fileName}");
            XmlElement houseRoot = xHouse.DocumentElement;


            foreach (XmlElement xnode in houseRoot)
            {
                if (xnode.Name == "house")
                {
                    House house = new House();
                    XmlNode attr = xnode.Attributes.GetNamedItem("number");
                    if (attr != null)
                        house.number = UInt32.Parse(attr.Value);

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
            ConsoleKey? key = ConsoleKey.RightArrow;
            int index = -10;
            while (key != ConsoleKey.Escape)
            {
                if ((key == ConsoleKey.LeftArrow) && (index != 0))
                {
                    index -= 10;
                    HouseList(index, houses);
                }
                else if ((key == ConsoleKey.RightArrow) && (index + 10 < houses.Count))
                {
                    index += 10;
                    HouseList(index, houses);
                }
               
                key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Escape);
            }
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        /// <summary>
        /// Выводит таблицу с данными о девелоперах
        /// </summary>
        public void WriteDeveloper()
        {
            List<Developer> developers = new List<Developer>();

            XmlDocument xDev = new XmlDocument();
            xDev.Load($"C:/C#/RunDll/XMLfiles/{fileName}");
            XmlElement devRoot = xDev.DocumentElement;


            foreach (XmlElement xnode in devRoot)
            {
                if (xnode.Name == "developer")
                {

                    Developer dev = new Developer();
                    XmlNode attr = xnode.Attributes.GetNamedItem("number");
                    if (attr != null)
                        dev.number = UInt32.Parse(attr.Value);

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

            ConsoleKey? key = ConsoleKey.RightArrow;
            int index = -10;
            while (key != ConsoleKey.Escape)
            {
                if ((key == ConsoleKey.LeftArrow) && (index != 0))
                {
                    index -= 10;
                    DeveloperList(index, developers);
                }
                else if ((key == ConsoleKey.RightArrow) && (index + 10 < developers.Count))
                {
                    index += 10;
                    DeveloperList(index, developers);
                }

                key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Escape);
            }
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");

            //Console.Clear();
            //Console.WriteLine    ("╔═════╦═══════════════════════╤═══════════════╤══════════════════════════════════╗");
            //Console.WriteLine    ("║  №  ║     Девелопер         │ Годовой доход │         Адрес девелопера         ║");
            //Console.WriteLine    ("╠═════╬═══════════════════════╪═══════════════╪══════════════════════════════════╣");
            //foreach (Developer v in developers)
            //{
            //    Console.WriteLine("║     ║                       │               │                                  ║");
            //    Console.Write    ("╟─────╫───────────────────────┼───────────────┼──────────────────────────────────╢");

            //    Console.SetCursorPosition(2, Console.CursorTop - 1);
            //    Console.Write(v.number);
            //    Console.SetCursorPosition(8, Console.CursorTop);
            //    Console.Write(v.name);
            //    Console.SetCursorPosition(32, Console.CursorTop);
            //    Console.Write(v.inc);
            //    Console.SetCursorPosition(48, Console.CursorTop);
            //    Console.WriteLine(v.addr);
            //    Console.SetCursorPosition(0, Console.CursorTop + 1);
            //}
            //Console.SetCursorPosition(0, Console.CursorTop - 1);
            //Console.WriteLine    ("╚═════╩═══════════════════════╧═══════════════╧══════════════════════════════════╝");
        }

        //private List<T> LoadBase<T>(string name)
        //{

        //    XmlDocument xVill = new XmlDocument();
        //    xVill.Load($"C:/C#/RunDll/XMLfiles/{fileName}");
        //    XmlElement villRoot = xVill.DocumentElement;
        //    List<Village> villages = new List<Village>();
        //    List<T> genList = new List<T>();



        //    foreach (XmlElement xnode in villRoot)
        //    {
        //        if (xnode.Name == "village")
        //        {
        //            Village vill = new Village();
        //            XmlNode attr = xnode.Attributes.GetNamedItem("number");
        //            if (attr != null)
        //                vill.number = UInt32.Parse(attr.Value);
        //            foreach (XmlNode childnode in xnode.ChildNodes)
        //            {
        //                switch (childnode.Name)
        //                {
        //                    case "name":
        //                        vill.name = childnode.InnerText;
        //                        break;
        //                    case "dev":
        //                        vill.dev = childnode.InnerText;
        //                        break;
        //                    case "area":
        //                        vill.area = float.Parse(childnode.InnerText);
        //                        break;
        //                    case "people":
        //                        vill.people = UInt32.Parse(childnode.InnerText);
        //                        break;
        //                }
        //                genList.Add((T)(object)vill);
        //                villages.Add(vill);
        //            }
        //        }
        //    }
        //    //List<T> tList = new List<T>();
        //        return genList;
        //    }
        
        private void VillageList(int index, List<Village> villages)
        {
            Console.Clear();
            Console.WriteLine        ("╔═════╦════════════════════════╤═══════════════════════╤═══════════════╤═══════════╗");
            Console.WriteLine        ("║  №  ║    Назвение посёлка    │       Девелопер       │ Площадь в м^2 │ Население ║");
            Console.WriteLine        ("╠═════╬════════════════════════╪═══════════════════════╪═══════════════╪═══════════╣");
            for (byte i = 0; i < 10; i++, index++)
            {
                if (index < villages.Count())
                {
                    Console.WriteLine("║     ║                        │                       │               │           ║");
                    Console.Write    ("╟─────╫────────────────────────┼───────────────────────┼───────────────┼───────────╢");

                    Console.SetCursorPosition(2, Console.CursorTop - 1);
                    Console.Write(villages[index].number);
                    Console.SetCursorPosition(8, Console.CursorTop);
                    Console.Write(villages[index].name);
                    Console.SetCursorPosition(33, Console.CursorTop);
                    Console.Write(villages[index].dev);
                    Console.SetCursorPosition(56, Console.CursorTop);
                    Console.Write(villages[index].area);
                    Console.SetCursorPosition(72, Console.CursorTop);
                    Console.WriteLine(villages[index].people);
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine        ("╚═════╩════════════════════════╧═══════════════════════╧═══════════════╧═══════════╝");
            int lastPage;
            lastPage = villages.Count() % 10 == 0 ? (villages.Count() / 10) : (villages.Count() / 10 + 1);
            int x = Console.CursorLeft, y = Console.CursorTop;
            frame.Menu(x, y , 20, "Стр. " + index/10+" из " + lastPage);
        }

        private void HouseList(int index, List<House> houses)
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
                    Console.Write    ("╟─────╫────────────────────┼────────────┼───────────────┼───────────────┼────────────────╢");

                    Console.SetCursorPosition(2, Console.CursorTop - 1);
                    Console.Write(houses[index].number);
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

            int lastPage;
            lastPage = houses.Count() % 10 == 0 ? (houses.Count() / 10) : (houses.Count() / 10 + 1);
            int x = Console.CursorLeft, y = Console.CursorTop;
            frame.Menu(x, y, 20, "Стр. " + index / 10 + " из " + lastPage);
        }

        private void DeveloperList(int index, List<Developer> developers)
        {
            Console.Clear();
            Console.WriteLine    ("╔═════╦═══════════════════════╤═══════════════╤══════════════════════════════════╗");
            Console.WriteLine    ("║  №  ║     Девелопер         │ Годовой доход │         Адрес девелопера         ║");
            Console.WriteLine    ("╠═════╬═══════════════════════╪═══════════════╪══════════════════════════════════╣");
            for (byte i = 0; i < 10; i++, index++)
            {
                if (index < developers.Count())
                {

                    Console.WriteLine("║     ║                       │               │                                  ║");
                    Console.Write    ("╟─────╫───────────────────────┼───────────────┼──────────────────────────────────╢");

                    Console.SetCursorPosition(2, Console.CursorTop - 1);
                    Console.Write(developers[index].number);
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
            Console.WriteLine    ("╚═════╩═══════════════════════╧═══════════════╧══════════════════════════════════╝");
            int lastPage;
            lastPage = developers.Count() % 10 == 0 ? (developers.Count() / 10) : (developers.Count() / 10 + 1);
            int x = Console.CursorLeft, y = Console.CursorTop;
            frame.Menu(x, y, 20, "Стр. " + index / 10 + " из " + lastPage);
        }

        private string ChoiceDeveloper(int readPosY)
        {
            Console.SetCursorPosition(80, 0);
            List<string> developers = new List<string>();
            developers.Add("Выберите девелопера");
            XmlDocument xDev = new XmlDocument();
            xDev.Load($"C:/C#/RunDll/XMLfiles/{fileName}");
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
                    developers.Add(str);
                }
            }
            frame.Continuous(25, developers.ToArray());
            ushort x = 80;
            ushort y = 2;
            frame.Choice(x, y, ConsoleColor.Green, 25);
            ConsoleKey? key = null;
            while(key != ConsoleKey.Enter)
            {
                key = inp.InputKey(ConsoleKey.Escape, ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Enter);
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if((y/2) < developers.Count - 1)
                        {
                            frame.ContinuousChoice(x, y, ConsoleColor.White, 25);
                            y += 2;
                            //Console.WriteLine(y);
                            frame.Choice(x, y, ConsoleColor.Green, 25);
                            if(y == 4)
                            {
                                Console.SetCursorPosition(x, 2);
                                string line = new string('═', 25);
                                Console.WriteLine($"╠{line}╣");
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if ((y/2) >= 2)
                        {
                            frame.ContinuousChoice(x, y, ConsoleColor.White, 25);
                            y -= 2;
                            //Console.WriteLine(y);
                            //Console.WriteLine(developers.Count);
                            frame.Choice(x, y, ConsoleColor.Green, 25);
                            if ((y / 2) == developers.Count - 2);
                            {
                                Console.SetCursorPosition(x, (developers.Count) * 2);
                                string line = new string('═', 25);
                                Console.WriteLine($"╚{line}╝");
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(27, readPosY);
                        Console.Write(developers[y / 2]);
                        break;
                }
            }
            return developers[y / 2];
        }

        private bool Call_SaveMassage(int x, int y)
        {
            Console.Clear();
            frame.Menu(x, y, 22, "Cозранить изменения?");
            frame.Menu(x + 5, y + 3, 5, "Да");
            frame.Menu(x + 12, y + 3, 5, "Нет");

            ConsoleKey? key = null;
            int x1 = x + 5;
            int y1 = y + 3;
            frame.Choice(x1, y1, col, 5);
            while (key != ConsoleKey.Enter)
            {
                key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Enter);

                if ((key == ConsoleKey.RightArrow) & (x1 != x + 12))
                {
                    x1 += 7;
                    frame.Choice(x1 - 7, y1, ConsoleColor.White, 5);
                    frame.Choice(x1, y1, col, 5);
                }
                else if ((key == ConsoleKey.LeftArrow) & (x1 != x + 5))
                {
                    x1 -= 7;
                    frame.Choice(x1, y1, col, 5);
                    frame.Choice(x1 + 7, y1, ConsoleColor.White, 5);
                }
                else if (key == ConsoleKey.Enter)
                {
                    if(x1 == x + 5 )
                        return true;
                    else
                        return false;
                }
            }
            return true;
        }

        private void GoBack(ref ConsoleKey? key)
        {
            ConsoleKeyInfo buf;
            key = null;
            while ((key != ConsoleKey.Tab) & (key != ConsoleKey.Escape))
            {
                buf = Console.ReadKey(true);
                key = buf.Key;
                if (key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
                }
            }
        }
    }
}
