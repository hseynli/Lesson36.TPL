using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
    class Program
    {
        static void MyTask(object arg)
        {
            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(25);
                Console.Write(arg as string);
            }
        }

        static void Main()
        {
            // Taskın ikinci arqumenti MyTask metodunun arqumentinə düşəcəkdir
            Action<object> myTask = MyTask;
            Task task = new Task(myTask, ".");
            task.Start();

            Thread.Sleep(500);

            // AsyncState-in null olmaması üçün Task(Action<object> action, object state)
            // konstruktorundan istifadə etmək lazımdır.
            // Konstruktorun ikinci arqumenti Task - ".", 
            // AsyncState-in dəyəri kimi ötürülür.
            Console.WriteLine("\n[{0}]", task.AsyncState as string);

            // Delay
            Console.ReadKey();
        }
    }
}
