/*
    Вариант 23
       Базовый класс:
          Млекопитающее
       Производные классы:
          Наземное
          Водное
          Китообразное
*/

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        /*
           Параметры:
              args[0] - путь до файла с объектами
        */
        {
            // Проверка параметров командной строки
            if (args.Length != 1)
            {
                Console.WriteLine("Введите путь до файла с объектами!");
                Console.ReadLine();
                return;
            }

            // Чтение объектов из файла
            Mammal[] Mammals = ReadDataFromFile(args[0]);
            if (Mammals.Length == 0)
            {
                Console.WriteLine("Файл пуст!");
                Console.ReadLine();
                return;
            }
            foreach (Mammal OneMammal in Mammals)
            {
                if (OneMammal == null)
                {
                    Console.WriteLine("Ошибка чтения объектов из файла!");
                    Console.WriteLine("Шаблон заполнения:");
                    Console.WriteLine("   Наземное:");
                    Console.WriteLine("      Наземное,<Название>,<Тип передвижения>,<Вес>,<Количество лап>");
                    Console.WriteLine("   Водное:");
                    Console.WriteLine("      Водное,<Название>,<Тип передвижения>,<Вес>,<Количество плавников и ласт>");
                    Console.WriteLine("   Китообразное:");
                    Console.WriteLine("      Китообразное,<Название>,<Тип передвижения>,<Вес>,<Количество плавников и ласт>,<Длина фонтана>");
                    Console.ReadLine();
                    return;
                }
            }

            // Демонстрация работы методов класса у всех объектов
            Console.WriteLine("Вывод информации о всех млекопитающих: \n");
            foreach (Mammal MyMammal in Mammals)
            {
                MyMammal.OutAll();
                Console.WriteLine();
            }
            foreach (Mammal MyMammal in Mammals) // по всем объектам
            {
                MyMammal.ChangeWeight();
            }
            Console.WriteLine("Применение виртуального метода - изменение веса:");
            foreach (Mammal MyMammal in Mammals) // по всем объектам
            {
                MyMammal.OutAll();
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        // Чтение объектов из файла
        static Mammal[] ReadDataFromFile(string Path)
        /*
           Параметры:
              Path - путь до файла
           Возвращаемое значение:
              Массив объектов
        */
        {
            // Подсчет количества объектов
            string Row;
            int Count = 0;
            try
            {
                StreamReader TempFile = new StreamReader(Path, Encoding.UTF8);
                while ((Row = TempFile.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(Row))
                    {
                        Count++;
                    }
                }
                TempFile.Close();
            }
            catch (Exception Error)
            {
                Console.WriteLine("Ошибка чтения содержимого файла!");
                Console.WriteLine(Error.Message);
                Console.ReadKey();
                Environment.Exit(1);
            }

            // Чтение объектов из файла в массив
            Mammal[] Mammals = new Mammal[Count];
            if (Count == 0)
            {
                return Mammals;
            }
            Count = 0;
            try
            {
                StreamReader File = new StreamReader(Path, Encoding.UTF8);
                while ((Row = File.ReadLine()) != null)
                {
                    string[] Fields = Row.Split(',');
                    switch (Fields[0])
                    {
                        case "Наземное":
                             Mammals[Count++] = new Earth(Fields);
                             break;

                        case "Водное":
                             Mammals[Count++] = new Water(Fields);
                             break;

                        case "Китообразное":
                             Mammals[Count++] = new Whale(Fields);
                             break;
                    }
                }
                File.Close();
            }
            catch (Exception Error)
            {
                Console.WriteLine("Ошибка чтения содержимого файла!");
                Console.WriteLine(Error.Message);
                Console.ReadKey();
                Environment.Exit(1);
            }
            return Mammals;
        }

        // Млекопитающее
        abstract class Mammal
        {
            protected string Name; // название
            protected string TypeOfMove; // тип передвижения
            protected int    Weight; // вес

            // конструктор с параметрами
            public Mammal(string[] ArrayMammal)
            /*
               Параметры: 
                  ArrayMammal[1] - название
                  ArrayMammal[2] - тип передвижения
                  ArrayMammal[3] - вес
            */
            {
                Name = ArrayMammal[1];
                TypeOfMove = ArrayMammal[2];
                if (!int.TryParse(ArrayMammal[3], out Weight))
                {
                    Weight = 0;
                }
            }

            // Изменение веса (увеличение в 2 раза)
            public virtual void ChangeWeight()
            {
                Weight = Weight * 2;
            }

            // Вывод всех полей
            public abstract void OutAll();
        }

        // Наземные
        class Earth : Mammal
        {
            protected int NumberOfPaws; // количество лап

            // конструктор с параметрами
            public Earth(string[] ArrayMammal) : base(ArrayMammal)
            /*
                Параметры:
                   ArrayMammal[1] - название
                   ArrayMammal[2] - способ передвижения
                   ArrayMammal[3] - вес
                   ArrayMammal[4] - количество лап
            */
            {
                if (!int.TryParse(ArrayMammal[4], out this.NumberOfPaws))
                {
                    this.NumberOfPaws = 0;
                }
            }

            // Изменение веса (увеличение в 3 раза)
            public override void ChangeWeight()
            {
                Weight = Weight * 3;
            }

            // Вывод всех полей
            public override void OutAll()
            {
                Console.WriteLine($"Название:            {this.Name}");
                Console.WriteLine($"Способ передвижения: {this.TypeOfMove}");
                Console.WriteLine($"Вес:                 {this.Weight}");
                Console.WriteLine($"Количество лап:      {this.NumberOfPaws}");
            }
        }

        // Водные
        class Water : Mammal
        {
            protected int NumberOfFlippers; // количество плавников и ласт

            // конструктор с параметрами
            public Water(string[] ArrayMammal) : base(ArrayMammal)
            /*
                Параметры:
                   ArrayMammal[1] - название
                   ArrayMammal[2] - способ передвижения
                   ArrayMammal[3] - вес 
                   ArrayMammal[4] - количество плавников и ласт
            */
            {
                if (!int.TryParse(ArrayMammal[4], out this.NumberOfFlippers))
                {
                    this.NumberOfFlippers = 0;
                }
            }
   
            // Изменение веса (увеличение в 4 раза)
            public override void ChangeWeight()
            {
                Weight = Weight * 4;
            }

            // Вывод всех полей
            public override void OutAll()
            {
                Console.WriteLine($"Название:                {this.Name}");
                Console.WriteLine($"Способ передвижения:     {this.TypeOfMove}");
                Console.WriteLine($"Вес:                     {this.Weight}");
                Console.WriteLine($"Кол-во плавников и ласт: {this.NumberOfFlippers}");
            }
        }

        // Китообразные
        class Whale : Water
        {
            protected int HeightOfFountains; // высота фонтана

            // конструктор с параметрами
            public Whale(string[] ArrayMammal) : base(ArrayMammal)
            /*
                Параметры:
                   ArrayMammal[1] - название
                   ArrayMammal[2] - способ передвижения
                   ArrayMammal[3] - вес 
                   ArrayMammal[4] - количество плавников и ласт
                   ArrayMammal[5] - высота фонтана
            */
            {
                if (!int.TryParse(ArrayMammal[5], out this.HeightOfFountains))
                {
                    this.HeightOfFountains = 0;
                }
            }

            // Изменение веса (увеличение в 5 раз)
            public override void ChangeWeight()
            {
                Weight = Weight * 5;
            }

            // Вывод всех полей
            public override void OutAll()
            {
                Console.WriteLine($"Название:                {this.Name}");
                Console.WriteLine($"Способ передвижения:     {this.TypeOfMove}");
                Console.WriteLine($"Вес:                     {this.Weight}");
                Console.WriteLine($"Кол-во плавников и ласт: {this.NumberOfFlippers}");
                Console.WriteLine($"Высота фонтана:          {this.HeightOfFountains}");
            }
        }
    }
}