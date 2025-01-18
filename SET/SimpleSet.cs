using System;
using CS_SET.Exc;
/*
 2. Создать класс SimpleSet, наследующий от класса Set – реализация работы с
множествами на основе логического массива (m[i]=true  элемент i принадлежит
множеству). Описать конструктор с 1 параметром – максимальным числом,
представимым в множестве.
3. В классе SimpleSet описать операторный метод «+», оба параметра типа SimpleSet,
выполняющий объединение множеств. Аналогично – операторный метод «*»,
выполняющий пересечение множеств.
 */
namespace CS_SET.SET
{
    class SimpleSet:Set
    {
        bool[] simpleset;
        public SimpleSet (int Lenght) //в конструкторе выделяем память для массива и указываем размер массива
        {
            simpleset = new bool[Lenght + 1];
            this.MaxLength = Lenght;
        }
        public override void Add(int item) //реализация метода вставки
        {
            if (item >= simpleset.Length) throw new OutArray($"Выход за пределы массива " + item + ".");
            simpleset[item] = true;
        }
        public override int GetNumber(int item)
        {           
            return simpleset[item]?1:0;
        }
        public override bool Find(int item) //реализация метода поиска
        {
            if (item >= simpleset.Length) throw new OutArray($"Выход за пределы массива " + item + ".");
            return simpleset[item];  //возвращаем значение  (принадлежит элемент множеству или нет)
        }
        public override void Remove(int item) //реализация метода удаления
        {
            if (item >= simpleset.Length) throw new OutArray($"Выход за пределы массива " + item + ".");
            if (simpleset[item]) simpleset[item] = false; //меняем значение "принадлежит множеству" на "не принадлежит множеству"
            else throw new OutArray("Элемент " + item + " отсутствует в множестве."); //если элемент "не принадлежит", выводим соответствующее сообщение
        }

        public static SimpleSet operator +(SimpleSet One,SimpleSet Two) //операторный метод объединения множеств
        {
            int MaxLength = Math.Max(One.simpleset.Length, Two.simpleset.Length); //выбираем наибольший размер из множеств
            SimpleSet intersection = new SimpleSet(MaxLength-1); //выделяем память для нового объекта

            for (int i = 0; i < One.simpleset.Length && i < MaxLength; i++)  //заполняем значениями из первого множества
                intersection.simpleset[i] = One.simpleset[i];
            for (int i = 0; i < Two.simpleset.Length && i < MaxLength; i++) 
                intersection.simpleset[i] = Two.simpleset[i] || intersection.simpleset[i]; //если элемент "принадлежит" первому или второму множеству, то в результат записываем "принадлежит"

            return intersection;
        }

        public static SimpleSet operator *(SimpleSet One, SimpleSet Two) //операторный метод пересечения множеств
        {
            int MinLength = Math.Min(One.simpleset.Length, Two.simpleset.Length); //выбираем наименьший размер из множеств, так как значение "принадлежит" 
                                                                      //должно быть и у элемента первого множества и у элемента второго множества, 
                                                                      //а кол-во таких пар не превзойдет минимальный размер из двух множеств
            SimpleSet unification = new SimpleSet(MinLength-1);

            for (int i = 0; i < MinLength; i++)
                unification.simpleset[i] = One.simpleset[i] && Two.simpleset[i]; //если элемент "принадлежит" и первому, и второму множеству, то в результат записываем "принадлежит"

            return unification;
        }

    } 
}
