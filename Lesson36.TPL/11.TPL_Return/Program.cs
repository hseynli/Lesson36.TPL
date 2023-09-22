using System;
using System.Threading;
using System.Threading.Tasks;

// Taskdan nəticənin geri qaytarılması.

namespace TPL
{
    struct Context
    {
        public int a;
        public int b;
    }

    class Program
    {
        // Nəticə qaytaracaq metod.
        static int Sum(object arg)
        {
            int a = ((Context)arg).a;
            int b = ((Context)arg).b;

            Thread.Sleep(2000);

            return a + b;
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Əsas thread işə düşdü.");

            Context context;
            context.a = 2;
            context.b = 3;

            Task<int> task;

            // 1-ci variant
            task = new Task<int>(Sum, context);
            task.Start();

            // 2-ci variant
            //TaskFactory<int> factory = new TaskFactory<int>();
            //task = factory.StartNew(Sum, context);

            // 3-cü varinat
            //task = Task<int>.Factory.StartNew(Sum, context);

            Console.WriteLine("Sum taskının nəticəsi = " + task.Result);

            Console.WriteLine("Əsas thread işini yekunlaşdırdı.");

            // Delay
            Console.ReadKey();
        }
    }
}
