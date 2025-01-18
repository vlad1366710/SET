/*6. Создать собственное исключение – выход за пределы множества. Генерировать это
исключение, когда в множество пытаются добавить элемент, превышающий
максимально допустимое значение. 
 */
using System;
namespace CS_SET.Exc
{
    public class OutArray:Exception //наследуем класс Exception для собственного класса исключения
    {
        //new string Message;
        public OutArray(string e)
        {
            this.Error = e;
        }
        public string Error
        {
            get;
            set;
        }
        //public string GetMessage()
        //{
        //    return this.Message;
        //}
        //public void Show() //метод для вывода сгенерированной ошибки
        //{
        //    Console.WriteLine(Message);
        //}
    }
}
