using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeadesCL;

namespace PrimeadesCA
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ввод числа N в консоль
            Console.WriteLine("Введите число N");
            int N = int.Parse(Console.ReadLine());


            //Создаем массив с целыми числами от 1 до N
            List<int> NewArray = ArrayHandler.ArrayFiller(N);
            
            //Применяем алгоритм решета Эратосфена
            //для поиска простых чисел
            NewArray = Primes.doEratosfen(NewArray);
            Console.WriteLine(PrintArray(NewArray));

            //Находим максимальные десятки
            //с минимальным и максимальным количеством простых чисел
            List<int> Answer = ArrayHandler.SplitPrimes(NewArray);
            Console.WriteLine(PrintArray(Answer));

            Answer = ArrayHandler.Segment(NewArray);
            Console.WriteLine(PrintArray(Answer));
            //Console.WriteLine("дсмин, мин, дсмакс, макс");
            //ForTest.WorkPrimes(N);

            Console.ReadKey();
        }

        /// <summary>
        /// Функция преобразования списка в строку чисел через пробел
        /// </summary>
        /// <param name="array">преобразуемый список</param>
        /// <returns>Строку - список целых чисел через пробел</returns>
        private static string PrintArray(List<int> array)
        {
            string StrArray = "";
            foreach (int element in array) StrArray += element + " ";
            return StrArray;
        }
    }
}
