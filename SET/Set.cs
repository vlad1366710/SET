using System;
using CS_SET.Exc;
/* 1.
Абстрактные:
a. включение элемента в множество;
b. исключение элемента из множества;
c. проверка наличия элемента в множестве.
 Реализованные:
три перегруженных метода заполнения:
один – для заполнения из строки элементов (с параметром типа string),
второй – для заполнения из массива элементов (с параметром типа int[]),
Примечание 1. Этот массив элементов может быть не отсортированным и
содержать повторяющиеся элементы.
третий – заполнение массива случайными числами в диапазоне от min до max
(параметры – int, int).
d. метод вывода (печати) всех элементов множества на экране.
Реализованные методы не могут учитывать внутреннюю организацию множества,
вместо этого должны использоваться имеющиеся абстрактные методы.
 */
namespace CS_SET.SET
{
    abstract class Set
    {
        Random rand = new Random();
        protected int MaxLength;
        public abstract void Add(int item); //абстрактный метод включения элемента в множество
        public abstract void Remove(int item); //абстрактный метод исключения элемента из множества
        public abstract bool Find(int item); //абстрактный метод проверки наличия элемента в множестве
        public abstract int GetNumber(int item);
        public void Fill(string str) //метод заполнения : заполнение из строки элементов (с параметром типа string)
        {
            char[] charSeparators = new char[] { ' ' };
            foreach (var item in str.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries)) //строка разделяется пробелом на отдельные элементы 
            {                                                                                      //с помощью foreach проходится каждый элемент
                if (Int32.TryParse(item,out int m))
                    Add(Convert.ToInt32(item)); //элемент приводим к целочисленному типу и добавляем во множество
                //если сгенерировалась ошибка выхода за пределы множества вызываем метод из собственного исключения
                else
                    throw new OutArray($"Выход за пределы массива " + item + ".");

            }
        }

        public void Fill(int[] mass) //метод заполнения : заполнение из массива элементов (с параметром типа int[])
        {
            foreach(var item in mass)
            {               
                    Add(item);                      
            }
        }
        public void Fill(int min,int max) //метод заполнения: заполнение массива случайными числами в диапазоне от min до max (параметры – int, int)
        {
            for (int i=0,item= min + rand.Next(max + 1);
                     i<MaxLength; 
                     i++, item = min + rand.Next(max + 1))
                Add(item);


        }

        public void Show() // метод вывода (печати) всех элементов множества на экране
        {
            if (MaxLength == 0) 
            {
                Console.WriteLine("Пустое множество.");
            }
            else
            {
                Console.Write("Вывожу элементы множества: { ");
                for (int i = 0; i <= MaxLength; i++)
                    //if (Find(i))
                   // Console.Write(i + " ");
                Console.Write(i + ":" +GetNumber(i) +", ");
                Console.WriteLine(" }");
                
            }
        }
    }
}
