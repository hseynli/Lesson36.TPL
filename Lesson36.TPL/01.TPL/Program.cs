using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
    class Program
    {
        // Asinxron icra olunacaq metod.
        static void MyTask()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine("\nMyTask: # {0} nömrəli thread-da yaradıldı", threadId);

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                Console.Write("+ ");
            }

            Console.WriteLine("\nMyTask: # {0} nömrəli thread-da işini bitirdi", threadId);
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int threadId = Thread.CurrentThread.ManagedThreadId;

            Console.WriteLine("Main: # {0} nömrəli thread-da yaradıldı", threadId);

            Action action = new Action(MyTask);

            Task task = new Task(action); // Taskın instance-nın yaradılması.            
            task.Start();                 // Taskı asinxron olaraq işə salmaq.

            //task.RunSynchronously();    // Taskı sinxron olaraq işə salmaq.

            for (int i = 0; i < 10; i++)
            {
                Console.Write(". ");
                Thread.Sleep(200);
            }

            Console.WriteLine("\nMain: # {0} nömrəli thread-da işini bitirdi", threadId);

            // Delay
            Console.ReadKey();
        }
    }
}
