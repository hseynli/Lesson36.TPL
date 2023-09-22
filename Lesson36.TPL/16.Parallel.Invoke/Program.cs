using System;
using System.Threading;
using System.Threading.Tasks;

// Parallel.Invoke() metodudan bir neçə metodu paralel şəkildə icra etmək üçün istifadə etmək olar.

namespace TPL
{
    static class Program
    {
        static void MyTask1()
        {
            Console.WriteLine("MyTask1: işə düşdü.");
            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(10);
                Console.Write("+");
            }
            Console.WriteLine("MyTask1: işini yekunlaşdırdı.");
        }

        static void MyTask2()
        {
            Console.WriteLine("MyTask2: işə düşdü.");
            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(10);
                Console.Write("-");
            }
            Console.WriteLine("MyTask2: işini yekunlaşdırdı.");
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Əsas thread işə düşdü.");

            ParallelOptions options = new ParallelOptions();

            // Prosessorun müəyyən nüvə sayını ayırmaq.
            //options.MaxDegreeOfParallelism = Environment.ProcessorCount > 2
            //                          ? Environment.ProcessorCount - 1 : 1;

            options.MaxDegreeOfParallelism = 2; // 1 və 2-ni yoxlamaq

            Console.WriteLine("Prosessorun məntiqi nüvə sayı:" + Environment.ProcessorCount);

            Console.ReadKey();

            // Paralel olaraq 2 metodu icra etmək.
            Parallel.Invoke(options, MyTask1, MyTask2);

            // Paralel olaraq 4 metodu icra etmək.
            //Parallel.Invoke(options, MyTask1, MyTask2, MyTask1, MyTask2);

            // DİQQƏT!
            // Main() metodunun işi tasklar işini bitirənə kimi dayanacaqdır.

            Console.WriteLine("Əsas thread işini bitirdi.");

            // Delay
            Console.ReadKey();
        }
    }
}
