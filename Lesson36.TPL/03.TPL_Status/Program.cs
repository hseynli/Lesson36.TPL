using System;
using System.Threading;
using System.Threading.Tasks;

// TaskStatus - taskın statusu.

namespace TPL
{
    class Program
    {
        static void MyTask()
        {
            Thread.Sleep(3000);
        }

        static void Main()
        {           
            Task task = new Task(MyTask);
            Console.WriteLine("1. " + task.Status); // Task işə başlamayıb.

            task.Start();
            Console.WriteLine("2. " + task.Status); // Task işə başlama statusundadır.

            Thread.Sleep(1000);
            Console.WriteLine("3. " + task.Status); // Task icra olunur.

            Thread.Sleep(3000);
            Console.WriteLine("4. " + task.Status); // Task işini yekunlaşdırıb.

            // Delay
            Console.ReadKey();
        }
    }
}
