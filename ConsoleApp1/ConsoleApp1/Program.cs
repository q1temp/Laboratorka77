using System;

namespace ConsoleApp5
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №7. Вариант 7");
            Console.WriteLine("Файлы будут созданы в папке:");
            Console.WriteLine("C:\\Users\\User\\Desktop\\яп пересдача\\Laboratorka77");
            Console.WriteLine();

            TasksOneToFive.RunTask1();
            TasksOneToFive.RunTask2();
            TasksOneToFive.RunTask3();
            TasksOneToFive.RunTask4();
            TasksOneToFive.RunTask5();

            TasksSixToTen.RunTask6();
            TasksSixToTen.RunTask7();
            TasksSixToTen.RunTask8();
            TasksSixToTen.RunTask9();
            TasksSixToTen.RunTask10();
        }
    }
}