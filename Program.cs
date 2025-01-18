using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using CS_SET.Exc;
/* 
7. В основной программе предусмотреть диалог с пользователем.
     a. Пользователь выбирает вариант представления (перечисление элементов,
        логический или битовый массив).
     b. Исходное множество вводится либо с клавиатуры в виде одной строки
        элементов (используется метод заполнения с параметром типа string), либо из
        файла, в котором каждый элемент располагается в отдельной строке
        (используется метод заполнения с параметром типа int[]).
     c. Далее пользователь может добавлять/исключать элементы и проверять наличие
        элемента в множестве до тех пор, пока не выберет пункт «Выход». При
        добавлении элемента обрабатывать возможное исключение.
        Для выполнения указанных действий программный код не должен
        дублироваться (для разных вариантов представления)!
        Примечание: можно реализовать как графический интерфейс, так и режим
        командной строки.
8. Для тестирования операторных методов (пункт 3) предусмотреть отдельный тест. С
   клавиатуры вводятся два множества А и В (в виде строк элементов). Для каждого из
   двух вариантов представления (логический или битовый массив) создаются множества
   С = А  В и D = А  В и выводятся на экран.
9. Предусмотреть возможность «быстрого» (с вводом всех данных, включая операции, из
   файла) и обычного (с вводом с консоли или формы) тестирования.*/

namespace CS_SET.SET
{
    class Program
    {
            static int MyCheck(string s) //проверка на введение именно числа, а не букв или посторонних знаков
            {
                int x;
                int.TryParse(s, out x);
                return (x);
            }
            static void CheckShow(bool x) 
            {
                if (x)
                {
                    Console.Write("Ошибка! Повторите ввод!"); //вывод о том, что введено недопустимое число
                Thread.Sleep(1000);
                    Console.Clear();
                }
            }
            static int MinMenu(string str, int n)
            {
                bool begin = true;
                int x = 0;
                while (begin)
                {
                    Console.Write(str);
                    x = MyCheck(Console.ReadLine()); //выбор действий
                    begin = !(x > 0 && x < n); //значение на допустимость числа
                    CheckShow(begin);
                }
                Console.Clear();
                return x;
            }
        static void Main(string[] args)
        {
            SimpleSet SetOne = new SimpleSet(11);
            SimpleSet SetTwo = new SimpleSet(4);
            ////8
            SimpleSet Set;
            try { SetOne.Fill("1 2 3 4 5 2 1 21"); }
            catch (OutArray e)
            {
                Console.WriteLine(e.Error);
            }
            SetOne.Show();
            try { SetTwo.Fill("1 2 5 0 11"); }
            catch (OutArray e)
            {
                Console.WriteLine(e.Error);
            }
            SetTwo.Show();
            Set = SetOne + SetTwo;
            Console.Write("Сумма 1 и 2 множества ");
            Set.Show();
            Set = SetOne * SetTwo;
            Console.Write("Умножение 1 и 2 множества ");
            Set.Show();
            BitSet BsTwo = new BitSet(11);
            BitSet BsOne = new BitSet(4);
            try { BsOne.Fill("1 2 5 0 11"); }
            catch (OutArray e)
            {
                Console.WriteLine(e.Error);
            }
            BsOne.Show();
            try { BsTwo.Fill("1 2 3 4 5 2 1 2"); }
            catch (OutArray e)
            {
                Console.WriteLine(e.Error);
            }
            BsTwo.Show();
            BitSet Bs;
            Bs = BsOne + BsTwo;
            Console.Write("Сумма 1 и 2 множества ");
            Bs.Show();
            Bs = BsOne * BsTwo;
            Console.Write("Умножение 1 и 2 множества ");
            Bs.Show();
            Console.ReadLine();


            Set set = null;
            int chose = 0;
            bool begin = true;
            chose = MinMenu("  Выберите тип представления множества:\n" +
                                "1 - MultiSet,\n" +
                                "2 - SimpleSet,\n" +
                                "3 - BitSet. \n", 4);
            while (begin)
            {
                Console.Write("Введите размер множества: ");
                int MaxLenght = MyCheck(Console.ReadLine()) - 1;
                begin = MaxLenght < 0;
                CheckShow(begin);
                if (!begin)
                    switch (chose)
                    {
                        case 1:
                            set = new MultiSet(MaxLenght);
                            break;
                        case 2:
                            set = new SimpleSet(MaxLenght);
                            break;
                        case 3:
                            set = new BitSet(MaxLenght);
                            break;
                    }
            }
            Console.Clear();
            try
            {
                chose = MinMenu("  Выберите способ ввода:\n" +
                        "1 - Ввод с клавиатуры\n" +
                        "2 - Ввод из файла.\n", 3);
                switch (chose)
                {
                    case 1:
                        string s = Console.ReadLine();
                        char[] charSeparators = new char[] { ' ' };
                        string[] st = s.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                        s = "";
                        int i = 0;
                        while (st.Length > i)
                        {
                            if (MyCheck(st[i]) > 0 || st[i] == "0") s += st[i++] + " ";
                            else i++;
                        }
                        set.Fill(s);
                        break;
                    case 2:
                        using (StreamReader fs = new StreamReader("in.txt"))
                        {
                            List<int> list = new List<int>();
                            int n = 0;
                            while ((s = fs.ReadLine()) != null)
                            {
                                n = MyCheck(s);
                                if (n > 0 || s == "0")
                                    list.Add(n);
                            }
                            set.Fill(list.ToArray());
                        }
                        break;
                }
            }catch(OutArray e)
            {
                Console.WriteLine(e.Error);
            }
            bool end = begin = true;
            int item = 0;
            while (end)
            {
                begin = true;
                set.Show();
                chose = MinMenu("  Выберите способ работы:\n" +
                                "1 - Добавить элемент\n" +
                                "2 - Удалить элемент\n" +
                                "3 - Выход.\n", 4);
                while (begin)
                {
                    if (chose < 3)
                    {
                        Console.Write("Введите элемент множества: ");
                        string s = Console.ReadLine();
                        item = MyCheck(s);
                        begin = item <= 0 && !(s == "0" && item == 0);
                        CheckShow(begin);
                    }
                    else
                    { begin = false; }
                    if (!begin)
                        switch (chose)
                        {
                            case 1:
                                try
                                { set.Add(item); }
                                catch (OutArray e)
                                { Console.WriteLine(e.Error); }
                                break;
                            case 2:
                                try
                                { set.Remove(item); }
                                catch (OutArray e)
                                { Console.WriteLine(e.Error); }
                                break;
                            case 3:
                                begin = end = false;
                                break;
                        }
                }
            }
            Console.ReadKey(true);
        }
    }
}
