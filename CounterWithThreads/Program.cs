using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CounterWithThreads
{
    class Program
    {
        static object locker = new object();
        static int count;

        static void ThreadMain()
        {
            WriteCount();
        }

        static void WriteCount()
        {
            string threadName = Thread.CurrentThread.Name;

            while (true)
            {
                Monitor.Enter(locker);
                if (threadName == "firstThread")
                    count += 2;
                else
                    count -= 1;

                Console.WriteLine(count);
                Monitor.Exit(locker);
                Thread.Sleep(500);
            }
        }
        static void Main(string[] args)
        {
            Thread firstThread = new Thread(WriteCount);
            Thread secondThread = new Thread(WriteCount);
            firstThread.Name = "firstThread";
            secondThread.Name = "secondThread";
            firstThread.Start();
            secondThread.Start();

            Console.Read();
        }
    }
}
