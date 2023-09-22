using System;
using System.Threading;
using System.Threading.Tasks;

// WaitAll() - Taskın bütün obyektlərinin işini bitirməsini gözləyir.
// WaitAny() - Taskın qeyd edilən istənilən obyektinin işini bitirməsini gözləyir.

namespace TPL
{
    class Program
    {
        static void MyTask1()
        {
            Console.WriteLine("MyTask: CurrentId " + Task.CurrentId + " işə düşdü.");
            Thread.Sleep(2000);
            Console.WriteLine("MyTask: CurrentId " + Task.CurrentId + " işini yekunlaşdırdı.");
        }
        static void MyTask2()
        {
            Console.WriteLine("MyTask: CurrentId " + Task.CurrentId + " işə düşdü.");
            Thread.Sleep(3000);
            Console.WriteLine("MyTask: CurrentId " + Task.CurrentId + " işini yekunlaşdırdı.");
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Əsas thread işə düşdü.");

            Task task1 = new Task(MyTask1);
            Task task2 = new Task(MyTask2);

            task1.Start();
            task2.Start();

            Console.WriteLine("task1-in Id-si: " + task1.Id);
            Console.WriteLine("task2-in Id-si: " + task2.Id);

            Task.WaitAll(task1, task2);
            //Task.WaitAny(task1, task2);

            Console.WriteLine("Əsas thread işini yekunlaşdırdı.");

            // Delay
            Console.ReadKey();
        }
    }
}
