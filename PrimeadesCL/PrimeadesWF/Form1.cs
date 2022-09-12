using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PrimeadesCL;


namespace PrimeadesWF
{
    public partial class Form1 : Form
    {
        static List<int> PrimeArray = new List<int>();
        public Form1()
        {
            InitializeComponent();
            //Привязка кнопки Enter к копке "Вычислить"
            this.AcceptButton = buttonCalculate;
        }

        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            int N = 0;

            //Проверка некорректного ввода
            try
            {
                N = int.Parse(textBoxInputN.Text);
                if (N < 1) throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректный ввод.","Ошибка!");
                return;
            }

            //Создаем массив с целыми числами от 1 до N
            List<int> NewArray = ArrayHandler.ArrayFiller(N);

            //Применяем алгоритм решета Эратосфена
            //для поиска простых чисел
            NewArray = Primes.doEratosfen(NewArray);

            //Находим максимальные десятки
            //с минимальным и максимальным количеством простых чисел
            List<int> Answer = ArrayHandler.SplitPrimes(NewArray);
            //Answer[десяток с минимумом, минимум, десяток с максимумом,максимум]

            //Заполняем текстовые поля на форме
            textBoxMin.Text=MakeAnswer(Answer,NewArray,true);
            textBoxMax.Text=MakeAnswer(Answer, NewArray,false);
            Answer = ArrayHandler.Segment(NewArray);
            textBox1.Text= string.Join(" ", Answer);
        }

        /// <summary>
        /// Функция формирования строки-ответа с десятком чисел и списком простых чисел в нём
        /// </summary>
        /// <param name="TaskAns">Информация о максимальных десятках с минимальным и 
        /// максимальным количеством простых чисел</param>
        /// <param name="newArray">Массив целых чисел, в котором непростые числа заменены нулями</param>
        /// <param name="minmax">флаг, определяющий десяток с минимальным или максимальным количеством чисел</param>
        /// <returns></returns>
        private string MakeAnswer(List<int> TaskAns, List<int> newArray, bool minmax)
        {
            string tmp = minmax ? "минимальным" : "максимальным";

            string Answer = "Список с " + tmp + " количеством чисел:"+Environment.NewLine;
            //Восстановление десятка
            Answer += RecoverDecade(minmax ? TaskAns[0] : TaskAns[2])+ Environment.NewLine;
            Answer += "Простые числа:"+ Environment.NewLine;
            //восстановление списка простых чисел из необходимого десятка
            string tmpPrimes = PrintPrimes(minmax ? TaskAns[0] : TaskAns[2], newArray);
            //если чисел в десятке нет, вносится сообщение об этом
            if (tmpPrimes == "") Answer += "Простых чисел нет";
            else Answer += tmpPrimes;

            return Answer;
        }

        /// <summary>
        /// Функция восстановления списка простых чисел из указанного десятка
        /// </summary>
        /// <param name="StartDecade">Номер десятка</param>
        /// <param name="Primes">Массив простых чисел</param>
        /// <returns>Строка - Простые числа из определённого десятка через пробел</returns>
        private string PrintPrimes(int StartDecade, List<int> Primes)
        {
            string PrimesDecade = "";
            
            for (int i = StartDecade*10; i < StartDecade*10+10; i++)
                if (Primes[i] != 0) PrimesDecade += Convert.ToString(Primes[i]) + " ";

            return PrimesDecade;
        }

        /// <summary>
        /// Функция восстановления списка целых чисел из указанного десятка 
        /// </summary>
        /// <param name="StartDecade">Номер десятка</param>
        /// <returns>Строка - десяток чисел</returns>
        private string RecoverDecade(int StartDecade)
        {
            string Decade = "";

            for (int i = StartDecade*10; i < StartDecade*10+10; i++)
                Decade += Convert.ToString(i) + " ";

            return Decade;
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
                MessageBox.Show("ПРОГРАММА ПОИСКА ДЕСЯТКА С МАКСИМАЛЬНЫМ И МИНИМАЛЬНЫМ КОЛИЧЕСТВОМ ПРОСТЫХ ЧИСЕЛ\n" +
        "Программа осуществляет поиск максимальных десятков с максимальным и минимальным количеством чисел в ряде целых чисел от 1 до N" +
        " с помощью алгоритма \"Решето Эратосфена\"\n" +
        "В окне \"Введите количество чисел \" введите натуральное количество чисел","Справка");
        }

    }
}
