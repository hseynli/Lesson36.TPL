using System;
using System.Threading.Tasks;

// Tasklardan xətaların yaranması. (Nümunəni CTRL+F5 ilə salın)

namespace TPL
{
    class Program
    {
        // Xətanın yaranacağı metod.
        static void MyTask()
        {
            Console.WriteLine("Task işə düşdü.");

            throw new Exception();

            Console.WriteLine("Task işini yekunlaşdırdı.");
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Əsas thread işə düşdü.");

            Task task = new Task(MyTask);

            try
            {
                task.Start();
                task.Wait(); // Xətanı emal etmək üçün mütləü Wait metodunu çağırmaq lazımdır!
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception       : " + ex.GetType());
                Console.WriteLine("Message         : " + ex.Message);

                if (ex.InnerException != null)
                    Console.WriteLine("Inner Exception : " + ex.InnerException.GetType());
            }
            finally
            {
                Console.WriteLine("Taskın statusu   : " + task.Status);
            }

            Console.WriteLine("Əsas thread işini yekunlaşdırdı.");

            // Delay
            Console.ReadKey();
        }
    }
}
