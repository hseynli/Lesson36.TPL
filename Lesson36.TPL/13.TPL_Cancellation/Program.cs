using System;
using System.Threading;
using System.Threading.Tasks;

// Taskın işindən imtina etmək. (Nümunəni CTRL+F5 ilə işə salın)

namespace TPL
{
    class Program
    {
        static void MyTask(object arg)
        {
            CancellationToken token = (CancellationToken)arg;

            // Əgər task işə düşən kimi imtina olunubsa, onda OperationCanceledException xətasını yaratmaq.
            token.ThrowIfCancellationRequested();

            Console.WriteLine("MyTask işə düşdü.");

            for (int i = 0; i < 80; i++)
            {
                if (token.IsCancellationRequested) // Taskdan imtina olunub?
                {
                    Console.WriteLine("\nTaskdan imtina sorğusu alırıq.");
                    token.ThrowIfCancellationRequested(); // OperationCanceledException xətasını yaratmaq.
                }

                Thread.Sleep(100);
                Console.Write(".");
            }

            Console.WriteLine("\nMyTask işini yekunlaşdırdı.");
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Əsas thread işə düşdü.");

            // İmtina tokeni yaratmaq.
            CancellationTokenSource cancellation = new CancellationTokenSource();
            CancellationToken token = cancellation.Token;

            Task task = new Task(MyTask, token);
            task.Start();

            Thread.Sleep(2000);

            try
            {
                cancellation.Cancel(); // İcra olunan taskdan imtina etmək.
                task.Wait();           // Xətanın emalı üçün mütləq Wait metodunu çağırmaq!
            }
            catch (AggregateException e)
            {
                if (task.IsCanceled)
                    Console.WriteLine("\nTaskdan imtina edildi.\n");

                Console.WriteLine("Inner Exception : " + e.InnerException.GetType());
                Console.WriteLine("Message         : " + e.InnerException.Message);
                Console.WriteLine("Taskln statusu  : " + task.Status);
            }

            Console.WriteLine("Əsas thread işini yekunlaşdırdı.");

            // Delay
            Console.ReadKey();
        }
    }
}
