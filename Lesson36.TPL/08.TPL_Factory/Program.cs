using System;
using System.Threading;
using System.Threading.Tasks;

// TaskFactory-dən istifadə edərək instance-ın yaradılması

namespace TPL
{
    class Program
    {
        static void MyTask()
        {
            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(20);
                Console.Write(".");
            }
        }

        static void Main()
        {
            // Variant 1.
            Task task = Task.Factory.StartNew(MyTask);
            // TaskFactory-dən istifadə edən zaman Start() metodunu çağırmaq lazım deyil.
            //task.Start();

            // Variant 2.
            //TaskFactory factory = new TaskFactory();
            //Task task = factory.StartNew(MyTask);

            // Delay
            Console.ReadKey();
        }
    }
}
