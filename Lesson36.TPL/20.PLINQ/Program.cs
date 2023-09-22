using System;
using System.Linq;
using System.Threading.Tasks;

// PLINQ sorğuları. AsOrdered() metodundan istifadə.

namespace PLINQ
{
    class Program
    {
        static void Main()
        {
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

            // AsOrdered() metodundan istifadə edərək mənfi ədədlərin tapılması
            // (ardıcıllığın qorunması üçün).
            var negatives = array.AsParallel().AsOrdered().Where(element => element < 0);

            foreach (var element in negatives)
                Console.Write(element + " ");

            // Delay
            Console.ReadKey();
        }
    }
}
