using System.Collections.Generic;

namespace Net_Lab6
{
    public class StudentYearComparer : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.Year.CompareTo(y.Year);
        }
    }
}
