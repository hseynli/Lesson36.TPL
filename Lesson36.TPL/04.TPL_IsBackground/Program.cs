using System;
using System.Threading;
using System.Threading.Tasks;

// Main metodu işini bitirdikdən sonra işini bitirməyən MyTask da işini yekunlaşdıracaqdır
// [ikinci thread işini bitirir].
// Default olaraq IsBackground == true

namespace TPL
{
    class Program
    {
        static void MyTask()
        {
            Thread.CurrentThread.IsBackground = false; // Şərhən çıxarmaq.

            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(100);
                Console.Write(".");
            }
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Task task = new Task(MyTask);
            task.Start();

            Thread.Sleep(500); // Taskın işləməsinə vaxt vermək.

            Console.WriteLine("\nMain işini bitirdi.");

            // Delay
            //Console.ReadKey();
        }
    }
}
