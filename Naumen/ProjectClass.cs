using System;

namespace Naumen
{
    public class ProjectClass : IComparable
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
            if (obj.GetType() != this.GetType())
                throw new Exception("Нельзя сравнивать ProjectClass с другим типом!");
            ProjectClass otherClass = obj as ProjectClass;
            if (ModificationDate > otherClass.ModificationDate) return 1;
            if (ModificationDate < otherClass.ModificationDate) return -1;
            return Name.CompareTo(otherClass.Name);
        }
    }
}
