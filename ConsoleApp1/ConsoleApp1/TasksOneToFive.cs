using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp5
{
    public static class TasksOneToFive
    {
        private const string BasePath = "C:\\Users\\User\\Desktop\\яп пересдача\\Laboratorka77";
        private static readonly Random Rand = new Random();

        private static void EnsureFolderExists()
        {
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
        }

        public static void RunTask1()
        {
            EnsureFolderExists();
            Console.WriteLine("\n ЗАДАНИЕ 1");
            string filePath = BasePath + "task1_numbers.txt";
            GenerateTask1File(filePath, 20);
            int count = CountMaxOccurrences(filePath);
            Console.WriteLine("Количество вхождений максимального элемента: " + count);
        }

        private static void GenerateTask1File(string path, int count)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < count; i++)
                {
                    int num = Rand.Next(1, 50);
                    sw.WriteLine(num);
                }
            }
            Console.WriteLine("Создан файл: " + path);
        }

        private static int CountMaxOccurrences(string path)
        {
            int max = int.MinValue;
            List<int> numbers = new List<int>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    int number;
                    if (int.TryParse(line.Trim(), out number))
                    {
                        numbers.Add(number);
                        if (number > max)
                            max = number;
                    }
                }
            }

            int count = 0;
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] == max)
                    count++;
            }
            return count;
        }

        public static void RunTask2()
        {
            EnsureFolderExists();
            Console.WriteLine("\n ЗАДАНИЕ 2");
            string filePath = BasePath + "task2_numbers.txt";
            GenerateTask2File(filePath, 5, 4);
            int count = CountEvenElements(filePath);
            Console.WriteLine("Количество чётных элементов: " + count);
        }

        private static void GenerateTask2File(string path, int rows, int cols)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < rows; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < cols; j++)
                    {
                        sb.Append(Rand.Next(1, 100));
                        if (j < cols - 1)
                            sb.Append(' ');
                    }
                    sw.WriteLine(sb.ToString());
                }
            }
            Console.WriteLine("Создан файл: " + path);
        }

        private static int CountEvenElements(string path)
        {
            int count = 0;

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(new char[] { ' ', '\t' },
                        StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        int number;
                        if (int.TryParse(part, out number))
                        {
                            if (number % 2 == 0)
                                count++;
                        }
                    }
                }
            }
            return count;
        }

        public static void RunTask3()
        {
            EnsureFolderExists();
            Console.WriteLine("\n ЗАДАНИЕ 3");
            string source = BasePath + "task3_source.txt";
            string dest = BasePath + "task3_result.txt";
            GenerateTask3File(source);

            Console.Write("Введите искомую комбинацию символов: ");
            string combination = Console.ReadLine();
            while (string.IsNullOrEmpty(combination))
            {
                Console.Write("Комбинация не может быть пустой. Повторите: ");
                combination = Console.ReadLine();
            }

            CopyLinesContaining(source, dest, combination);
            Console.WriteLine("Результат сохранен в файл: " + dest);
        }

        private static void GenerateTask3File(string path)
        {
            string[] lines = new string[]
            {
                "Сегодня старшеклассники выполняли ЕГЭ по информатике и ИКТ",
                "Простые строки без комбинаций",
                "Здесь есть слово трансформация",
                "Погода сегодня хорошая",
                "Информатика — интересный предмет"
            };
            File.WriteAllLines(path, lines);
            Console.WriteLine("Создан файл: " + path);
        }

        private static void CopyLinesContaining(string source, string dest, string combination)
        {
            List<string> resultLines = new List<string>();

            using (StreamReader sr = new StreamReader(source))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(combination))
                        resultLines.Add(line);
                }
            }

            File.WriteAllLines(dest, resultLines);
        }

        public static void RunTask4()
        {
            EnsureFolderExists();
            Console.WriteLine("\n ЗАДАНИЕ 4");
            string source = BasePath + "task4_source.bin";
            GenerateTask4File(source, 20);
            int diff = MaxMinDifference(source);
            Console.WriteLine("Разность максимального и минимального элементов: " + diff);
        }

        private static void GenerateTask4File(string path, int count)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    int num = Rand.Next(1, 100);
                    bw.Write(num);
                }
            }
            Console.WriteLine("Создан бинарный файл: " + path);
        }

        private static int MaxMinDifference(string path)
        {
            int max = int.MinValue;
            int min = int.MaxValue;

            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    int num = br.ReadInt32();
                    if (num > max) max = num;
                    if (num < min) min = num;
                }
            }

            return max - min;
        }

        [Serializable]
        [XmlRoot("Toys")]
        public struct Toy
        {
            [XmlAttribute("name")]
            public string Name;

            [XmlElement("price")]
            public int Price;

            [XmlElement("minAge")]
            public int AgeFrom;

            [XmlElement("maxAge")]
            public int AgeTo;

            public Toy(string name, int price, int ageFrom, int ageTo)
            {
                Name = name;
                Price = price;
                AgeFrom = ageFrom;
                AgeTo = ageTo;
            }
        }

        public static void RunTask5()
        {
            EnsureFolderExists();
            Console.WriteLine("\n ЗАДАНИЕ 5");
            string filePath = BasePath + "task5_toys.xml";
            GenerateTask5File(filePath);

            Console.Write("Введите k (допустимое отклонение от цены самой дорогой игрушки): ");
            int k;
            while (!int.TryParse(Console.ReadLine(), out k) || k < 0)
            {
                Console.Write("Некорректный ввод. Введите целое неотрицательное число: ");
            }

            List<string> names = GetExpensiveToys(filePath, k);
            Console.WriteLine("Наиболее дорогие игрушки (цена отличается от максимальной не более, чем на " + k + "):");
            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine("  " + names[i]);
            }
        }

        private static void GenerateTask5File(string path)
        {
            Toy[] toys = new Toy[]
            {
                new Toy("Мяч", 300, 2, 6),
                new Toy("Кубики", 450, 1, 4),
                new Toy("Пазл", 200, 3, 7),
                new Toy("Машинка", 600, 2, 4),
                new Toy("Лото", 150, 4, 10),
                new Toy("Пирамидка", 350, 1, 3),
                new Toy("Робот", 580, 5, 10),
                new Toy("Кукла", 610, 3, 8)
            };

            XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fs, toys, ns);
            }
            Console.WriteLine("Создан XML-файл: " + path);
        }

        private static List<string> GetExpensiveToys(string path, int k)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
            Toy[] toys;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                toys = (Toy[])serializer.Deserialize(fs);
            }

            int maxPrice = -1;
            for (int i = 0; i < toys.Length; i++)
            {
                if (toys[i].Price > maxPrice)
                    maxPrice = toys[i].Price;
            }

            List<string> result = new List<string>();
            for (int i = 0; i < toys.Length; i++)
            {
                if (maxPrice - toys[i].Price <= k)
                    result.Add(toys[i].Name + " (цена: " + toys[i].Price + ")");
            }

            return result;
        }
    }
}