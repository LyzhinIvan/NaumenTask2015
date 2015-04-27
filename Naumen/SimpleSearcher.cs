using System;
using System.Collections.Generic;
using System.Linq;

namespace Naumen
{
    public class SimpleSearcher : ISearcher
    {
        protected class ProjectClass : IComparable
        {
            public string Name { get; private set; }
            public long ModificationDate { get; private set; }

            public ProjectClass(string name, long modificationDate)
            {
                Name = name;
                ModificationDate = modificationDate;
            }

            public int CompareTo(object obj)
            {
                if(obj.GetType()!=this.GetType())
                    throw new Exception("Нельзя сравнивать ProjectClass с другим типом!");
                ProjectClass otherClass = obj as ProjectClass;
                if (ModificationDate > otherClass.ModificationDate) return 1;
                if (ModificationDate < otherClass.ModificationDate) return -1;
                return Name.CompareTo(otherClass.Name);
            }
        }

        private readonly int _resultsInGuess;
        private List<ProjectClass> _classes;

        public SimpleSearcher(int resultsInGuess = 12)
        {
            _resultsInGuess = resultsInGuess;
            _classes = new List<ProjectClass>();
        }

        public void Refresh(List<string> classNames, List<long> modificationDates)
        {
            if(classNames.Count()!=modificationDates.Count())
                throw new Exception("Размеры массивов не равны!");
            for (int i = 0; i < classNames.Count(); ++i)
                _classes.Add(new ProjectClass(classNames[i], modificationDates[i]));
        }

        public List<string> Guess(string start)
        {
            List<ProjectClass> suitableClasses = _classes.Where(c => IsPrefix(start, c.Name)).ToList();
            suitableClasses.Sort();
            return suitableClasses.Take(_resultsInGuess).Select(c => c.Name).ToList();
        }

        private bool IsPrefix(string prefix, string full)
        {
            if(prefix.Length>full.Length) return false;
            return full.Substring(0, prefix.Length) == prefix;
        }
    }
}
