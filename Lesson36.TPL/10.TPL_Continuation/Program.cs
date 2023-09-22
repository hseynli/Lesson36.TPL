using System;
using System.Threading;
using System.Threading.Tasks;

// Davametmə - birini task işini bitirdikdən sonra avtomatik olaraq ikinci taskın işini yerinə yetirməsi.

namespace TPL
{
    class Program
    {
        // Task kimi icra olunacaq metod.
        static void MyTask()
        {
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(200);
                Console.Write("+");
            }
        }

        // Taskın davamı kimi icra olunacaq metod.
        static void ContinuationTask(Task task)
        {
            for (int count = 0; count < 10; count++)
            {
                Thread.Sleep(200);
                Console.Write("-");
            }
        }

        static void Main()
        {
            // Taskın yaradılması.
            Action action = new Action(MyTask);
            Task task = new Task(action);

            // Taskın davamının yaradılması.
            Action<Task> continuation = new Action<Task>(ContinuationTask);
            Task taskContinuation = task.ContinueWith(continuation);

            // Task ardıcıllığının yerinə yetirilməsi.
            task.Start();

            // Delay.
            Console.ReadKey();
        }
    }
}
