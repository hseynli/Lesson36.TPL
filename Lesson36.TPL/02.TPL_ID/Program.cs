using System;
using System.Threading;
using System.Threading.Tasks;

// Id və CurrentId xassələri.
// Id - Hər bir Task instance-nın unikal Id-dir.
// CurrentId - İcra olunan taskın unikal identifikatorudur.

namespace TPL
{
    class Program
    {
        static void MyTask()
        {
            Console.WriteLine("MyTask: CurrentId {0} ManagedThreadId {1} işə başladı.",
                Task.CurrentId, Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(2000);

            Console.WriteLine("MyTask: CurrentId " + Task.CurrentId + " işini bitirdi.");
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Main: Task.CurrentId = {0}",  // Main - taskdır?
                Task.CurrentId == null ? "null" : Task.CurrentId.ToString());

            Task task1 = new Task(MyTask);
            Task task2 = new Task(MyTask);

            task1.Start();
            task2.Start();

            Console.WriteLine("task1-in Id-i: " + task1.Id);
            Console.WriteLine("task2-in Id-i: " + task2.Id);

            // Delay
            Console.ReadKey();
        }
    }
}
