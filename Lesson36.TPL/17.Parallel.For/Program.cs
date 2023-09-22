using System;
using System.Threading.Tasks;
using System.Diagnostics;

// Paralel dövrlərdən istifadə etmək istədikdə Parallel.For() metodundan istifadə olunur.

namespace TPL
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int[] data = new int[1000000];

            Stopwatch timer = new Stopwatch();

            timer.Start();

            for (int i = 0; i < data.Length; i++)
            {
                // Adi for dövründən məlumarların mənimsədilməsi.
                data[i] = i * i * i / 123;
            }

            timer.Stop();
            Console.WriteLine("Adi for dövrü      : " + timer.ElapsedTicks);
            timer.Reset();

            Action<int> transform = (int i) => { data[i] = i * i * i / 123; };

            timer.Start();

            // Paralel for dövrdündə məlumatların mənimsədilməsi.
            Parallel.For(0, data.Length, new ParallelOptions() { MaxDegreeOfParallelism = 2 }, transform);

            timer.Stop();
            Console.WriteLine("Paralel for dövrü : " + timer.ElapsedTicks);

            // DİQQƏT!
            // For() metodu işini bitirməyənə kimi Main() metodu işini dayanadıracaqdır

            Console.WriteLine("Əsas thread işini bitirdi.");
            Console.ReadKey();
        }
    }
}
