﻿using System;
using System.Threading.Tasks;

// Nümunəni CTRL+F5 işə salmaq.

namespace TPL_WPF_Context
{
    static class Program
    {
        static int MyTask()
        {
            byte result = 255;

            checked // Şərhdən çıxarmaq.
            {
                result += 1;
            }

            return result;
        }

        static void Main()
        {
            Task<int> task = new Task<int>(MyTask);
            Action<Task<int>> continuation;

            continuation = t => Console.WriteLine("Result : " + task.Result);
            task.ContinueWith(continuation, TaskContinuationOptions.OnlyOnRanToCompletion);

            continuation = t => Console.WriteLine("Inner Exception : " + task.Exception.InnerException.Message);
            task.ContinueWith(continuation, TaskContinuationOptions.OnlyOnFaulted);

            task.Start();

            // Delay
            Console.ReadKey();
        }
    }
}
