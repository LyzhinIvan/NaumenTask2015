using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Naumen
{
    public class TreeSearcher : ISearcher
    {
        private readonly int _resultsInGuess;
        private SearchTree _searchTree;

        public TreeSearcher(int resultsInGuess = 12)
        {
            _resultsInGuess = resultsInGuess;
            _searchTree = new SearchTree();
        }

        public void Refresh(List<string> classNames, List<long> modificationDates)
        {
            if(classNames.Count!=modificationDates.Count)
                throw new Exception("Размеры списков не равны!");

            LinkedList<ProjectClass> classes = new LinkedList<ProjectClass>();
            for (int i = 0; i < classNames.Count; ++i)
                classes.AddLast(new ProjectClass(classNames[i], modificationDates[i]));
            List<ProjectClass> sortedClasses = classes.ToList();
            sortedClasses.Sort();
            foreach (var projectClass in sortedClasses)
                _searchTree.Insert(projectClass.Name);
        }

        public List<string> Guess(string start)
        {
            return _searchTree.GetResultsList(start);
        }

        protected class SearchTree
        {
            private readonly int _storedWordsCount;
            private TreeNode _treeRoot;
            
            public SearchTree(int storedWordsCount = 12)
            {
                _storedWordsCount = storedWordsCount;
                _treeRoot = new TreeNode("");
            }

            public void Insert(string word)
            {
                TreeNode curNode = _treeRoot;
                int pos = 0;
                while (pos < word.Length)
                {
                    if (curNode.Results.Count < _storedWordsCount)
                        curNode.Results.Add(word);
                    char curLetter = word[pos];
                    if (!curNode.ChildNodes.ContainsKey(curLetter))
                        curNode.ChildNodes.Add(curLetter, new TreeNode(word.Substring(0, pos + 1)));
                    curNode = curNode.ChildNodes[curLetter];
                    pos++;
                }
            }

            public List<string> GetResultsList(string start)
            {
                TreeNode curNode = _treeRoot;
                int pos = 0;
                while (pos < start.Length)
                {
                    char curLetter = start[pos];
                    if (curNode.ChildNodes.ContainsKey(curLetter))
                        curNode = curNode.ChildNodes[curLetter];
                    else return new List<string>();
                    pos++;
                }
                return curNode.Results;
            }

            protected class TreeNode
            {
                public string Prefix { get; private set; }
                public Dictionary<char, TreeNode> ChildNodes;
                public List<string> Results;

                public TreeNode(string prefix)
                {
                    Prefix = prefix;
                    ChildNodes = new Dictionary<char, TreeNode>();
                    Results = new List<string>();
                }
            }
        }
    }

    
}
