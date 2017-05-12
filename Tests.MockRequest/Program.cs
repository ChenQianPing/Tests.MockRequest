using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.MockRequest
{
    class Program
    {
        static void Main(string[] args)
        {
            const int maxThread = 20;
            BeginTest(maxThread);
        }

        public static void BeginTest(int maxThread)
        {
            for (var i = 0; i < maxThread; i++)
            {
                var task = new ConcurrentHelper(i);
                new Thread(ThreadFun) { IsBackground = true }.Start(task);
            }
            Console.ReadLine();
        }

        private static void ThreadFun(object o)
        {
            var t = o as ConcurrentHelper;
            var count = 0;
            while (true)
            {
                count++;
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                if (t != null)
                {
                    var time = t.ReceiveData();
                    stopwatch.Stop();
                    if (time == -1)
                    {
                        Console.WriteLine($"线程ID：{t.TaskId}第{count}次接收失败！");
                    }
                }
                if (t != null)
                    Console.WriteLine($"线程ID：{t.TaskId}第{count}次接收成功！耗时：{stopwatch.Elapsed.TotalMilliseconds}毫秒!");
                Thread.Sleep(500);
            }
        }
    }
}
