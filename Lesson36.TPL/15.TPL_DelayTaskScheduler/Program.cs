using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TPL
{
    class Program
    {
        static void MyTask()
        {
            Console.WriteLine("MyTask ThreadID {0}", Thread.CurrentThread.ManagedThreadId);

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(200);
                Console.Write("+ ");
            }
        }

        static void Main()
        {
            Console.WriteLine("Main ThreadID {0}", Thread.CurrentThread.ManagedThreadId);

            TaskScheduler scheduler = new DelayTaskScheduler();
            TaskFactory factory = new TaskFactory(scheduler);
            Task task = factory.StartNew(MyTask);

            TaskAwaiter awaiter = task.GetAwaiter();

            while (!awaiter.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }

            //task.Wait(); // Çağırmaq lazım deyil, çünki DelayTaskScheduler AutoResetEvent-dən isifadə edir

            Console.WriteLine("\nBütün tasklar işini bitirdi.");
        }
    }

    class DelayTaskScheduler : TaskScheduler
    {
        Queue<Task> queue = new Queue<Task>();
        AutoResetEvent auto = new AutoResetEvent(false);

        protected override void QueueTask(Task task) // Task Factory tərəfindən avtomatik olaraq çağırılacaq
        {
            Console.WriteLine("QueueTask ThreadID {0}", Thread.CurrentThread.ManagedThreadId);
            queue.Enqueue(task);

            WaitOrTimerCallback callback = (object state, bool timedOut) => base.TryExecuteTask(queue.Dequeue());

            // 2 saniyə gözləməklə taskların asinxron çağırılması
            #region Arqumentlər
            /*     1. auto - kimdən siqnal gözləmək lazımdır.
                   2. callback - nəyi icra etmək lazımdır.
                   3. null - Callback metodun birinci arqumenti.
                   4. 2000 - Callback metodun çağırılma intervalı.
                   5. true - Callback metodu bir dəfə çağırmaq. false - Callback metodu intervallarla çağırmaq.  */
            #endregion
            ThreadPool.RegisterWaitForSingleObject(auto, callback, null, 2000, true);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return false;
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return queue;
        }
    }
}
