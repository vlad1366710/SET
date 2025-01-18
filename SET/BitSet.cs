using System;
using CS_SET.Exc;
/* 4. Создать класс BitSet, наследующий от класса Set – реализация работы с множествами
на основе битового массива (32 элемента «упаковываются» в 1 ячейку типа int).
Описать конструктор и операторные методы, перечисленные в п. 3.
--конструктор с 1 параметром – максимальным числом,представимым в множестве;
--операторный метод «+»,операторный метод «*»
 */
namespace CS_SET.SET
{
    class BitSet : Set
    {
        public int[] bitset = null;
        public int Length;

        public BitSet(int Length)
        {
            this.Length = this.MaxLength = Length + 1;
            if (Length % 32 != 0)  //если значение не упаковывается полностью в целое число ячеек, то создаём массив с доп ячейкой
                bitset = new int[Length / 32 + 1];  
            else 
                bitset = new int[Length / 32];
            for (int i = 0; i < bitset.Length; i++) //заполняем значения ячейки по умолчанию равным нулю
                bitset[i] = 0;
        }
        public override void Add(int item) //реализация метода вставки
        {
            if (item >= Length || item <0) throw new OutArray($"Выход за пределы массива " + item + ".");
            else
                bitset[item / 32] = bitset[item / 32] | (1 << item%32); //берем значения из массива(по умолчанию ячейки были заполнены null) ,
            //далее вставляется требуемое число путём сравнения побитово значения из ячеек массива и значения вставляемого числа в двоичном представлении
                                                     //и добавления с помощью логичского сложения (логического ИЛИ)
        }
        public override bool Find(int item) //реализация метода поиска
        {
            //if (item >= Length) throw new OutArray();
            return (bitset[item / 32] & (1 << item % 32))!=0; //если ячейки существующего битового массива для запрашиваемого числа равны 
            //двоичному представлению запрашиваемого числа - возвращаем true
        }
        public override void Remove(int item) //реализация метода удаления
        {
            if (Find(item))
                bitset[item/32]=bitset[item/32]&(~(1<<item%32));  //берем значения из массива,
            //далее удаляем требуемое число путём сравнения побитово значения из ячеек массива и значения числа в двоичном представлении(при этом используется поразрядное отрицание для противоположности каждого разряда между двумя битовыми представлениями),
                                                 //и изменения значений путём логического умножения(логического И) на значение null
            else throw new OutArray("Элемент " + item + " отсутствует в множестве."); 
        }
        public override int GetNumber(int item)
        {
            return Find(item)?1:0;  //(bitset[item / 32] & (1 << item % 32))/(int)(Math.Pow(2,item));
        }
        public static BitSet operator +(BitSet One, BitSet Two) //операторный метод объединения множеств
        {
            int MaxLength = Math.Max(One.MaxLength, Two.MaxLength); //для объединения множеств берём наибольшую длину двух множеств
            BitSet intersection = new BitSet(MaxLength - 1); //выделяем память под новый объект
            for (int i = 0; i < MaxLength; i++)
                if (One.MaxLength >= i && One.Find(i) || Two.MaxLength >= i && Two.Find(i)) //если битовое предствление числа найдено в певром или во втором множестве, то добавляем в результирующее множество
                    intersection.Add(i);
            return intersection;
        }
        public static BitSet operator *(BitSet One, BitSet Two) //операторный метод пересечения множеств
        {
            int MaxLength = Math.Min(One.MaxLength, Two.MaxLength);
            BitSet intersection = new BitSet(MaxLength - 1);
            for (int i = 0; i < MaxLength; i++)
                if (One.MaxLength >= i && One.Find(i) && Two.MaxLength >= i && Two.Find(i)) //если битовое предствление числа найдено и в певром, и во втором множестве, то добавляем в результирующее множество
                    intersection.Add(i);
            return intersection;
        }
    }
}
