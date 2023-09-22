using System;
using System.Threading;
using System.Threading.Tasks;

// Dövrün paralel icra olunması üçün ParallelLoopResult və ParallelLoopState tiplərindən
// və Break() metodundan istifadə.

// ParallelLoopState - dövlərin iterasiyalarını idarə etməyə imkan verir

// parallelLoopState.Break() metodu - dövrün icra olunmasının qarşısını alır.

// ParallelLoopResult - Parallel dövrünün icra olunmasının nəticəsini özündə saxlayır.

// parallelLoopResult.IsCompleted == true xassəsi əgər dövr tam olaraq icra olunubsa,
// əks halda, əgər dövr yarıda kəsilibsə, onda IsCompleted == false.

namespace TPL
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int[] data = new int[100000000];

            // Massivin ilkin dəyərlərə mənimsədilməsi.
            Parallel.For(0, data.Length, i => data[i] = i);

            data[300] = -1; // Mənfi ədədin massivə yerləşdirilməsi.

            Action<int, ParallelLoopState> transform = (int i, ParallelLoopState state) =>
            {
                if (data[i] < 0)   // ƏGƏR: Mənfi dəyərdisə
                    state.Break(); // ONDA: Dövrü dayandırmaq

                Thread.Sleep(1);

                data[i] = i * i * i / 123;
            };

            ParallelLoopResult loopResult = Parallel.For(0, data.Length, transform);

            if (!loopResult.IsCompleted)
            {
                Console.WriteLine("\nDövr vaxtından əvvəl öz işini yekunlaşdırdı." +
                    " Element {0} mənfi dəyərə bərabərdir.\n",
                    loopResult.LowestBreakIteration);
            }

            Console.WriteLine("Əsas thread işini yekunlaşdırdı.");
        }
    }
}
