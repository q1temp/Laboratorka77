using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp5
{
    public static class TasksSixToTen
    {
        private const string BasePath = "C:\\Users\\User\\Desktop\\яп пересдача\\Laboratorka77";

        private static void EnsureFolderExists()
        {
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
        }

        public static void RunTask6()
        {
            Console.WriteLine("\n ЗАДАНИЕ 6");
            List<int> list = ReadListFromKeyboard("L");
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            Console.Write("Исходный список: ");
            PrintList(list);

            int first = list[0];
            list.RemoveAt(0);
            list.Add(first);

            Console.Write("Результат: ");
            PrintList(list);
        }

        private static List<int> ReadListFromKeyboard(string listName)
        {
            List<int> result = new List<int>();
            Console.WriteLine("Введите элементы списка " + listName + " (целые числа через пробел):");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return result;

            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                int number;
                if (int.TryParse(parts[i], out number))
                    result.Add(number);
            }
            return result;
        }

        private static void PrintList(List<int> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("(пусто)");
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                    Console.Write(" ");
                Console.Write(list[i]);
            }
            Console.WriteLine();
        }

        public static void RunTask7()
        {
            Console.WriteLine("\n ЗАДАНИЕ 7");
            LinkedList<int> list = ReadLinkedListFromKeyboard();

            if (list.Count < 3)
            {
                Console.WriteLine("Недостаточно элементов (нужно минимум 3).");
                return;
            }

            Console.Write("Исходный список: ");
            PrintLinkedList(list);

            RemoveWithEqualNeighbors(list);

            Console.Write("Результат: ");
            PrintLinkedList(list);
        }

        private static LinkedList<int> ReadLinkedListFromKeyboard()
        {
            LinkedList<int> result = new LinkedList<int>();
            Console.WriteLine("Введите элементы списка (целые числа через пробел):");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return result;

            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                int number;
                if (int.TryParse(parts[i], out number))
                    result.AddLast(number);
            }
            return result;
        }

        private static void RemoveWithEqualNeighbors(LinkedList<int> list)
        {
            LinkedListNode<int> current = list.First;
            while (current != null)
            {
                LinkedListNode<int> left = current.Previous;
                LinkedListNode<int> right = current.Next;

                if (left == null)
                    left = list.Last;
                if (right == null)
                    right = list.First;

                LinkedListNode<int> next = current.Next;

                if (left.Value == right.Value)
                    list.Remove(current);

                current = next;
            }
        }

        private static void PrintLinkedList(LinkedList<int> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("(пусто)");
                return;
            }
            LinkedListNode<int> current = list.First;
            while (current != null)
            {
                Console.Write(current.Value);
                if (current.Next != null)
                    Console.Write(" ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        public static void RunTask8()
        {
            Console.WriteLine("\n ЗАДАНИЕ 8");

            List<string> factories = ReadStringListFromKeyboard("названий мебельных фабрик");
            int n = ReadPositiveInt("Введите количество покупателей: ");

            List<HashSet<string>> purchases = new List<HashSet<string>>();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Введите фабрики, у которых покупал покупатель " + (i + 1) + " (через пробел):");
                purchases.Add(ReadStringSetFromKeyboardLine());
            }

            HashSet<string> boughtByAll = new HashSet<string>();
            HashSet<string> boughtBySome = new HashSet<string>();
            HashSet<string> boughtByNone = new HashSet<string>();

            for (int i = 0; i < factories.Count; i++)
            {
                string factory = factories[i];
                int count = 0;
                for (int j = 0; j < purchases.Count; j++)
                {
                    if (purchases[j].Contains(factory))
                        count++;
                }

                if (count == purchases.Count)
                    boughtByAll.Add(factory);
                else if (count > 0)
                    boughtBySome.Add(factory);
                else
                    boughtByNone.Add(factory);
            }

            Console.WriteLine("\nПриобретались всеми покупателями: " + FormatSet(boughtByAll));
            Console.WriteLine("Приобретались некоторыми: " + FormatSet(boughtBySome));
            Console.WriteLine("Не приобретались никем: " + FormatSet(boughtByNone));
        }

        private static List<string> ReadStringListFromKeyboard(string description)
        {
            List<string> result = new List<string>();
            Console.WriteLine("Введите " + description + " (через пробел):");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                    result.Add(parts[i]);
            }
            return result;
        }

        private static HashSet<string> ReadStringSetFromKeyboardLine()
        {
            HashSet<string> result = new HashSet<string>();
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                    result.Add(parts[i]);
            }
            return result;
        }

        private static int ReadPositiveInt(string message)
        {
            int number;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (int.TryParse(input, out number) && number > 0)
                    return number;
                Console.WriteLine("Ошибка: введите положительное целое число.");
            }
        }

        private static string FormatSet(HashSet<string> set)
        {
            if (set.Count == 0)
                return "(нет)";

            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (string s in set)
            {
                if (!first)
                    sb.Append(", ");
                sb.Append(s);
                first = false;
            }
            return sb.ToString();
        }
        public static void RunTask9()
        {
            Console.WriteLine("\n ЗАДАНИЕ 9");
            EnsureFolderExists();

            string filePath = BasePath + "task9_text.txt";
            GenerateTask9File(filePath);
            Console.WriteLine("Создан файл с текстом: " + filePath);

            HashSet<char> result = FindLettersInOddNotInEven(filePath);

            Console.WriteLine("Глухие согласные, входящие в каждое нечётное слово и не входящие хотя бы в одно чётное:");
            if (result.Count == 0)
            {
                Console.WriteLine("  (нет)");
            }
            else
            {
                char[] sorted = SortChars(result);
                for (int i = 0; i < sorted.Length; i++)
                    Console.Write(sorted[i] + " ");
                Console.WriteLine();
            }
        }

        private static void GenerateTask9File(string path)
        {
            string[] lines = new string[]
            {
                "Петух поёт, каша варится, кот спит",
                "Стук топот шаг хлопок",
                "Тут как там, пёс и кот"
            };
            File.WriteAllLines(path, lines);
        }

        private static HashSet<char> FindLettersInOddNotInEven(string path)
        {
            string[] deafConsonants = new string[]
            {
                "п", "ф", "к", "т", "ш", "с", "х", "ц", "ч", "щ"
            };

            List<string> words = new List<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                string text = sr.ReadToEnd().ToLower();
                char[] separators = new char[] { ' ', ',', '.', '!', '?', ';', ':', '-', '\n', '\r', '\t' };
                string[] parts = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                    words.Add(parts[i]);
            }

            HashSet<char> result = new HashSet<char>();

            for (int i = 0; i < deafConsonants.Length; i++)
            {
                string letter = deafConsonants[i];
                bool inAllOdd = true;
                bool notInAtLeastOneEven = false;
                bool hasOdd = false;

                for (int w = 0; w < words.Count; w++)
                {
                    bool wordHasLetter = words[w].Contains(letter);

                    if (w % 2 == 0)
                    {
                        hasOdd = true;
                        if (!wordHasLetter)
                            inAllOdd = false;
                    }
                    else
                    {
                        if (!wordHasLetter)
                            notInAtLeastOneEven = true;
                    }
                }

                if (hasOdd && inAllOdd && notInAtLeastOneEven)
                    result.Add(letter[0]);
            }

            return result;
        }

        private static char[] SortChars(HashSet<char> set)
        {
            char[] arr = new char[set.Count];
            int index = 0;
            foreach (char c in set)
            {
                arr[index] = c;
                index++;
            }

            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j])
                    {
                        char temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }

            return arr;
        }

        public static void RunTask10()
        {
            Console.WriteLine("\n ЗАДАНИЕ 10");
            EnsureFolderExists();

            string filePath = BasePath + "task10_smetana.txt";
            GenerateTask10File(filePath);
            Console.WriteLine("Создан файл с данными: " + filePath);

            int[] counts = CountCheapestByFat(filePath);
            Console.WriteLine("Количество магазинов с самой дешёвой сметаной 15%, 20%, 25%:");
            Console.WriteLine(counts[0] + " " + counts[1] + " " + counts[2]);
        }

        private static void GenerateTask10File(string path)
        {
            string[] lines = new string[]
            {
                "Перекресток Короленко 25 3200",
                "Магнит Ленина 20 2800",
                "Пятерочка Мира 15 2500",
                "Перекресток Победы 25 3100",
                "Магнит Советская 20 2900",
                "Пятерочка Гагарина 15 2600",
                "Лента Кирова 20 2750",
                "Лента Пушкина 25 3050",
                "Магнит Гоголя 15 2450"
            };
            File.WriteAllLines(path, lines);
        }

        private static int[] CountCheapestByFat(string path)
        {
            int[] minPrices = new int[] { int.MaxValue, int.MaxValue, int.MaxValue };
            int[] fats = new int[] { 15, 20, 25 };

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 4)
                        continue;

                    int fat = int.Parse(parts[2]);
                    int price = int.Parse(parts[3]);

                    for (int i = 0; i < fats.Length; i++)
                    {
                        if (fat == fats[i] && price < minPrices[i])
                            minPrices[i] = price;
                    }
                }
            }

            int[] counts = new int[] { 0, 0, 0 };

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 4)
                        continue;

                    int fat = int.Parse(parts[2]);
                    int price = int.Parse(parts[3]);

                    for (int i = 0; i < fats.Length; i++)
                    {
                        if (fat == fats[i] && price == minPrices[i])
                            counts[i]++;
                    }
                }
            }

            return counts;
        }
    }
}