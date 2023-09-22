﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
    class Program
    {
        static void MyTask()
        {
            for (int i = 0; i < 80; i++)
            {
                Thread.Sleep(25);
                Console.Write(".");
            }
        }

        static void Main()
        {
            Task task = new Task(MyTask);
            task.Start();

            Thread.Sleep(500);

            Console.WriteLine("\ntask.IsCompleted = " + task.IsCompleted);

            // Asinxron taskın işini bitirməsini gözləmək.

            // Varinat 1:
            task.Wait();

            // Varinat 2:
            //while (!task.IsCompleted)
            //    Thread.Sleep(100);

            // Varinat 3: 
            //IAsyncResult asynkResult = task as IAsyncResult;
            //ManualResetEvent waitHandle = asynkResult.AsyncWaitHandle as ManualResetEvent;
            //waitHandle.WaitOne();

            Console.WriteLine("\ntask.IsCompleted = " + task.IsCompleted);

            // Delay
            Console.ReadKey();
        }
    }
}
