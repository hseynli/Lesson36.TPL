using System;
using System.Threading;
using System.Threading.Tasks;

// Taskda lambda-ifadədən istifadə.

namespace TPL
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Əsas thread işə düşdü.");

            // Taskı təyin etmək üçün lambda-operatordan istifadə.
            Task task = Task.Factory.StartNew(new Action(() =>
            {
                for (int i = 0; i < 80; i++)
                {
                    Thread.Sleep(20);
                    Console.Write(".");
                }
            }));

            // Taskın işini bitirməsini gözləmək.
            task.Wait();

            // Taskı azad etmək. 
            task.Dispose();

            Console.WriteLine("Əsas thread işini bitirdi.");

            // Delay
            Console.ReadKey();
        }
    }
}
