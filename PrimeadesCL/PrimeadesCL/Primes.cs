using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PrimeadesCL
{
    /// <summary>
    /// Класс обработки массива.
    /// Содержит функции заполнения массива и нахождения максимальных десятков 
    /// с максимальным и минимальным количеством простых чисел
    /// </summary>
    public class ArrayHandler
    {
        /// <summary>
        /// Функция заполнения списка целыми числами от 1 до N
        /// </summary>
        /// <param name="N">Входное число, последнее в списке</param>
        /// <returns>Список целых чисел от 1 до N</returns>
        public static List<int> ArrayFiller(int N)
        {
            //Решение проблемы обработки последнего десятка при вводе числа,
            //оканчивающегося на 9
            //(Последний десяток не обрабатывается,
            //если мы не достигаем круглого числа(оканчивается на 10),
            //поэтому таким образом мы как бы закрываем последний десяток)
            if (N % 10 == 9) N++;

            List<int> array = new List<int>();

            //Формирование массива целых чисел 1..N
            for (int i = 1; i <= N; i++)
            {
                array.Add(i);
            }
            return array;
        }

        public static List<int> Segment(List<int> NewArray)
        {
            //sigma[первое простое число, второе простое число, длина отрезка]
            List<int> sigma = new List<int> { 0, 0, 0 };
            int k, s=0;
            for (int i = 0; i < NewArray.Count; i++)
                if (NewArray[i] != 0)
                {
                    k = i - s;
                    if (k >= sigma[2]) { sigma[2] = k-1; sigma[0]=s+1; sigma[1] = i+1; }
                    s = i;
                }
            return sigma;
        }

        /// <summary>
        /// Функция нахождения максимальных десятков 
        /// с максимальным и минимальным количеством простых чисел
        /// </summary>
        /// 
        /// <param name="array">Список простых чисел, 
        /// в котором все непростые числа заменены на 0</param>
        /// 
        /// <returns>
        /// Список, содержащий информацию о максимальных 
        /// десятках с максимальным и минимальным количеством простых чисел 
        /// 
        /// [0 максимальный десяток с минимальным количеством простых чисел,
        /// 1 минимальное количество простых чисел,
        /// 2 минимальный десяток с максимальным количеством простых чисел,
        /// 3 максимальное количество простых чисел] 
        /// </returns>
        public static List<int> SplitPrimes(List<int> array)
        {
            //minmax[десяток с минимумом, минимум, десяток с максимумом,максимум]
            List<int> minmax = new List<int> { 0, 10, 0, 0 }; 

            int counter = 0; //Счётчик простых чисел в десятке

            //Проходим по массиву, и каждые 10 шагов
            //сравниваем counter c максимумом и минимумом
            for (int i = 0; i < array.Count; i++)
            {
                if ((i + 1) % 10 == 0)
                {
                    //Проверка на максимум
                    if (counter >= minmax[3])
                    {
                        minmax[2] = i / 10;
                        minmax[3] = counter;
                    }

                    //Проверка на минимум
                    if (counter <= minmax[1])
                    {
                        minmax[0] = i / 10;
                        minmax[1] = counter;
                    }

                    //Обнуление счётчика для нового десятка
                    counter = 0;
                }
                //Подсчёт простого числа
                if (array[i] != 0) counter++;

            }
            return minmax;
        }
    }
    
    /// <summary>
    /// Класс работы с простыми числами. 
    /// Содержит функцию поиска простых чисел в списке методом решета Эратосфена
    /// </summary>
    public class Primes
    {
        //Список для хранения простых чисел
        static List<int> PrimeArray = new List<int>();

        /// <summary>
        /// Функция поиска простых чисел в списке методом решета Эратосфена
        /// </summary>
        /// 
        /// <param name="InputArray">Список целых чисел от 1 до N</param>
        /// 
        /// <returns>
        /// Список целых чисел от 1 до N, 
        /// в котором все непростые числа заменены на 0
        /// </returns>
        public static List<int> doEratosfen(List<int> InputArray)
        {
            //Перенос входного списка в объект класса
            PrimeArray = InputArray;

            //Поиск простых чисел решетом Эратосфена
            EratosfenHandleArray();
            return PrimeArray;
        }
        /// <summary>
        /// Функция обработки списка целых чисел решетом Эратосфена
        /// </summary>
        private static void EratosfenHandleArray()
        {
            int i = 1;
            //Для каждого ненулевого элемента списка
            //выполняем "выкалывание" по алгоритму решета Эратосфена
            while (i < PrimeArray.Count())
            {
                if (PrimeArray[i] != 0) Step(PrimeArray[i], i);
                i++;
            }
        }

        /// <summary>
        /// Функция, реализующая "выкалывание" чисел по алгоритму решета Эратосфена
        /// </summary>
        /// <param name="prime">Стартовое простое число</param>
        /// <param name="start">индекс стартового числа в списке</param>
        private static void Step(int prime, int start)
        {
            //Зануляем элементы списка с шагом в значение текущего стартового простого числа
            for (int i = start + prime; i < PrimeArray.Count(); i += prime) PrimeArray[i] = 0;
        }
    }
    public class ForTest
    {
        /// <summary>
        /// Функция для проверки работы программы
        /// </summary>
        /// <param name="num">Входное число, последнее в списке</param>
        /// <returns>Список, содержащий информацию о максимальных 
        /// десятках с максимальным и минимальным количеством простых чисел 
        /// [0 максимальный десяток с минимальным количеством простых чисел,
        /// 1 минимальное количество простых чисел,
        /// 2 минимальный десяток с максимальным количеством простых чисел,
        /// 3 максимальное количество простых чисел] </returns>
        public static string WorkPrimes(int N)
        {
            //Создаем массив с целыми числами от 1 до N
            List<int> NewArray = ArrayHandler.ArrayFiller(N);

            //Применяем алгоритм решета Эратосфена
            //для поиска простых чисел
            NewArray = Primes.doEratosfen(NewArray);
            //PrintArray(NewArray);

            //Находим максимальные десятки
            //с минимальным и максимальным количеством простых чисел
            List<int> Answer = ArrayHandler.SplitPrimes(NewArray);
            Console.WriteLine(PrintArray(Answer)); 
            return PrintArray(Answer);
            
        }
        /// <summary>
        /// Функция вывода списка 
        /// </summary>
        /// <param name="answer">Входной список</param>
        private static string PrintArray(List<int> answer)
        {
            string StrArray = "";
            foreach (int element in answer) StrArray += element + " ";
            return StrArray;
        }
    }
}
