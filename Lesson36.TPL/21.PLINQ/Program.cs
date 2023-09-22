using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// Paralel sorğunun imtina edilməsi.

namespace PLINQ
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            CancellationTokenSource cancellation = new CancellationTokenSource();
            int[] array = new int[10000000];

            // Massivi müsbət ədədlərə mənimsətmək.
            Parallel.For(0, 10000000, (i) => array[i] = i);

            // Massivin bəzi elementlərini mənfi ədədlərə mənimsətmək.
            array[1000] = -1;
            array[14000] = -2;
            array[15000] = -3;
            array[676000] = -4;
            array[8024540] = -5;
            array[9908000] = -6;

            // Mənfi ədədləri əldə etmək üçün PLINQ sorğusu.
            ParallelQuery<int> negatives = from element in array
                                               .AsParallel()
                                               .WithCancellation(cancellation.Token)
                                           where element < 0
                                           select element;

            // PLINQ sorğusundan 10 millisaniyə müddəti keçəndən sonra imtina edilməsi.
            cancellation.CancelAfter(10);

            try
            {
                foreach (int element in negatives)
                    Console.Write(element + " ");

                Console.WriteLine("Ardıcıllıq müvəfəqiyyətlə bitdi!");
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cancellation.Dispose();
            }

            // Delay
            Console.ReadKey();
        }
    }
}
