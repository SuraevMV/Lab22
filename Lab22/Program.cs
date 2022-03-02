using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22
{
    class Program

    {
        static int[] Array(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 50);
            }
            return array;
        }
        static int[] Summa_Max(Task<int[]> task)
        {
            int[] array = task.Result;
            int summa = 0;
            int max = array[0];
            for (int i = 0; i < array.Count(); i++)
            {
                summa += array[i];
                if (max < array[i])
                    max = array[i];
            }
            Console.WriteLine($"Сумма элементов массива {summa}");
            Console.WriteLine($"Максимальное число в массиве {max}");
            return array;
        }
        static void Print(Task<int[]> task)
        {
            int[] array = task.Result;
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]} ");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(Array);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int[]> func2 = new Func<Task<int[]>, int[]>(Summa_Max);
            Task<int[]> task2 = task1.ContinueWith(func2);

            Action<Task<int[]>> action2 = new Action<Task<int[]>>(Print);
            Task task3 = task2.ContinueWith(action2);
            task1.Start();
                        
            Console.ReadKey();
        }
    }
}
