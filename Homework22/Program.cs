using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Action<Task<int[]>> action = new Action<Task<int[]>>(Print);
            Task print = task1.ContinueWith(action);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(GetSum);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(GetMax);
            Task<int> task3 = task1.ContinueWith<int>(func3);


            task1.Start();
            Console.ReadKey();
        }

        static void Print(Task<int[]> task)
        {
            int[] array = task.Result;
            Console.WriteLine("Массив: ");
            foreach (int item in array) Console.Write($"{item} ");
            Console.WriteLine();
        }
        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(-30, 150);
            }
            return array;
        }

        static int GetSum(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;
            for (int i = 0;i < array.Length;i++)
            {
                sum += array[i];
            }
            Console.WriteLine($"Сумма элементов: {sum}");
            return sum;
        }

        static int GetMax(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = 0;
            foreach (int num in array)
            {   

                if (num > max)
                {
                    max = num;
                }
            }
            Console.WriteLine($"Максимальный элемент {max}");
            return max;
        }
    }
}
