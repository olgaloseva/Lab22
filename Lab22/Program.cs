using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22
{
    class Program
    {
        //Сформировать массив случайных целых чисел(размер задается пользователем). 
        //Вычислить сумму чисел массива и максимальное число в массиве.
        //Реализовать решение  задачи с  использованием механизма  задач продолжения.
        static void Main(string[] args)
        {
            Console.Write("Введите размерность массива: ");
            int n = Convert.ToInt32(Console.ReadLine());    //размерность массива

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(SummArray);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(MaxArray);
            Task<int> task3 = task1.ContinueWith<int>(func3);

            task1.Start();

            Console.WriteLine($"Сумма чисел массива равна {task2.Result}");
            Console.WriteLine($"Максимальное число массива равно {task3.Result}");
            Console.ReadKey();
        }

              
        static int[] GetArray(object a)       //метод формирует массив и возвращает его
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            Console.Write("Массив: ");
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(-50, 50);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
            return array;
        }

        static int SummArray(Task<int[]> task)     //метод вычисляет сумму чисел массива и возвращает ее
        {
            int[] array = task.Result;
            int summ = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                summ = summ + array[i];
            }
            return summ;
        }

        static int MaxArray(Task<int[]> task)         //метод находит максимальное значение чисел массива и возвращает его
        {
            int[] array = task.Result;
            int max = array[0];
            for (int i = 1; i < array.Count(); i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            return max;
        }
    }
}
