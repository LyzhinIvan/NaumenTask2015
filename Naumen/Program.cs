using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Naumen
{
    class Program
    {
        private static Random _random = new Random();

        static void Main(string[] args)
        {
            int classesCount = Int32.Parse(Console.ReadLine());
            var classNames = GetRandomClassNames(classesCount);
        }

        static List<string> GetRandomClassNames(int quantity)
        {
            //LinkedList<string> classNames = new LinkedList<string>();
            HashSet<string> classNames = new HashSet<string>();
            while (classNames.Count < quantity)
                classNames.Add(GetNextClassName());
            return classNames.ToList();
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
