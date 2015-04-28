using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Naumen
{
    class Program
    {
        private static Random _random = new Random();

        static void Main(string[] args)
        {
            int classesCount = Int32.Parse(Console.ReadLine());
            var classNames = GetRandomClassNames(classesCount);
            var modificationDates = GetRandomDates(classesCount);
            Stopwatch watch = new Stopwatch();
            SimpleSearcher ss = new SimpleSearcher();
            TreeSearcher ts = new TreeSearcher();
            //Refresh searchers
            watch.Start();
            ss.Refresh(classNames, modificationDates);
            watch.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Refresh <ss>: {0}мс", watch.ElapsedMilliseconds);
            Console.ForegroundColor = ConsoleColor.White;
            watch.Restart();
            ts.Refresh(classNames, modificationDates);
            watch.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Refresh <ts>: {0}мс", watch.ElapsedMilliseconds);
            Console.ForegroundColor = ConsoleColor.White;
            //Queries
            while (true)
            {
                string start = Console.ReadLine();
                watch.Restart();
                List<string> ssVariants = ss.Guess(start);
                watch.Stop();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Guess <ss>: {0}мс", watch.ElapsedMilliseconds);
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var variant in ssVariants)
                {
                    Console.WriteLine(variant);
                }
                watch.Restart();
                List<string> tsVariants = ts.Guess(start);
                watch.Stop();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Guess <ts>: {0}мс", watch.ElapsedMilliseconds);
                Console.ForegroundColor = ConsoleColor.White;
                foreach (var variant in tsVariants)
                {
                    Console.WriteLine(variant);
                }
            }
        }

        static List<string> GetRandomClassNames(int quantity)
        {
            //LinkedList<string> classNames = new LinkedList<string>();
            HashSet<string> classNames = new HashSet<string>();
            while (classNames.Count < quantity)
                classNames.Add(GetNextClassName());
            return classNames.ToList();
        }

        static List<long> GetRandomDates(int quantity)
        {
            LinkedList<long> dates = new LinkedList<long>();
            for (int i = 0; i < quantity; ++i)
                dates.AddLast(_random.Next(0, int.MaxValue));
            return dates.ToList();

        }

        static string GetNextClassName()
        {
            string name = "";
            string characters = "abcdefghijklmnopqrstuvwxyz0123456789";
            int len = _random.Next(1, 32);
            for (int i = 0; i < len; ++i)
                name += characters[_random.Next(characters.Length)];
            return name;
        }
    }
}