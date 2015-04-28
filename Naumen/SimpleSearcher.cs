using System;
using System.Collections.Generic;
using System.Linq;

namespace Naumen
{
    public class SimpleSearcher : ISearcher
    {
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
            _classes.Sort();
        }

        public List<string> Guess(string start)
        {
            IEnumerable<ProjectClass> suitableClasses = _classes.Where(c => IsPrefix(start, c.Name)).Take(_resultsInGuess);
            return suitableClasses.Select(c => c.Name).ToList();
        }

        private bool IsPrefix(string prefix, string full)
        {
            if(prefix.Length>full.Length) return false;
            return full.Substring(0, prefix.Length) == prefix;
        }
    }
}
