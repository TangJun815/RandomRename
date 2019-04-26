using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var stopwatch = new Stopwatch();

            try
            {
                Console.WriteLine(@"
指令列表：
1、RandomRename,c:\Source,c:\Target（参数1：重命名；参数2：原文件夹路径；参数3：目标文件夹路径；）
请输入指令：
");

                var input = string.Empty;
                if (args.Length == 0)
                    input = Console.ReadLine();
                else
                {
                    input = args[0];
                    Console.WriteLine(input);
                }


                stopwatch.Start();

                if (input.ToLower().Contains("RandomRename".ToLower()))
                {
                    var temps = input.Split(',');
                    string source = temps[1];
                    string target = temps[2];
                    RandomRename(source, target);
                }
                else
                {
                    Console.WriteLine("输入指令错误，请重新运行程序。");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            stopwatch.Stop();
            ShowRunTime(stopwatch);

            Console.WriteLine("ok");
            Console.ReadKey();
        }

        private static void ShowRunTime(Stopwatch stopwatch)
        {
            var ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        private static void RandomRename(string source, string target)
        {
            EnsureExistsDirectory(target);

            var list = new List<string>();
            list.AddRange(Directory.GetFiles(source));

            while (list.Count>0)
            {
                Console.WriteLine($"待处理文件总数：{list.Count}");
                var randomIndex = new Random().Next(list.Count);
                var sourceFileName = list[randomIndex];
                Console.WriteLine($"随机索引：{randomIndex}，文件名：{sourceFileName}");

                Console.WriteLine($"copying...");
                File.Copy(sourceFileName, Path.Combine(target, IndexFileName.GetNexFileName(sourceFileName)), true);
                
                Console.WriteLine($"copied...");
                list.RemoveAt(randomIndex);
                Console.WriteLine(string.Empty.PadLeft(50,'-'));
            }
            
        }

        private static string GetNewFileName(string sourceFileName, string newFileName)
        {
            return $"{newFileName}{Path.GetExtension(sourceFileName)}";
        }

        private static void EnsureExistsDirectory(string directory)
        {
            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
