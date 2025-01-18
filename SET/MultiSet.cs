using System;
using CS_SET.Exc;
/*
 5. Создать класс MultiSet, наследующий от класса Set – реализация работы с
мультимножествами на основе целочисленного массива (m[i] = количеству вхождений
элемента i в мультимножество, в том числе если m[i] = 0, то элемент в
мультимножестве отсутствует). Описать конструктор с 1 параметром – максимальным
числом, представимым в множестве.
Примечание 2. При проверке наличия элемента в множестве и печати всех элементов
количество вхождений не важно (важно только есть ли хотя бы одно вхождение).
 */
namespace CS_SET.SET
{
    class MultiSet:Set
    {
        int[] multiSet;

        public MultiSet(int Length)
        {
            multiSet = new int[Length + 1];
            this.MaxLength = Length;
        }

        public override void Add(int item) //реализация метода вставки
        {
            if (item >= multiSet.Length) throw new OutArray($"Выход за пределы массива " + item + ".");
            else
            multiSet[item]++; //увеличиваем кол-во вхождений элемента в множестве при добавлении его в множестве
        }

        public override bool Find(int item) //реализация метода поиска
        {
           // if (item >= multiSet.Length) throw new OutArray();
            return multiSet[item] > 0;         //если значение в массиве больше нуля, значит элемент есть в множестве - возвращаем true
        }
        public override int GetNumber(int item)
        {
            return multiSet[item];
        }
        public override void Remove(int item) //реализация метода удаления
        {
            if (Find(item))              //если элемент есть во множестве, то при удалении уменьшаем значение кол-ва вхождений этого элемента во множество
                multiSet[item]--;
            else throw new OutArray("Элемент " + item + " отсутствует в множестве.");
                //Console.WriteLine("Удаление невозможно. Элемент " + item + " отсутствует в множестве.");
        }
    }
}
