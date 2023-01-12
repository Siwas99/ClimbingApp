namespace ClimbingApp.Data.Dictionaries
{
    public class GradesDictionary
    {
        public IDictionary<string, int> Grades { get; set; }

        public GradesDictionary() {
            var grades = new Dictionary<string, int>()
            {
                {"I", 1 },
                {"II", 2 },
                {"III", 3 },
                {"IV-", 4 },
                {"IV", 5 },
                {"IV+", 6 },
                {"V-", 7 },
                {"V", 8 },
                {"V+", 9 },
                {"VI-", 10 },
                {"VI", 11 },
                {"VI+", 12 },
                {"VI.1", 13 },
                {"VI.1+", 14 },
                {"VI.2", 15 },
                {"VI.2+", 16 },
                {"VI.3", 17 },
                {"VI.3+", 18 },
                {"VI.4", 19 },
                {"VI.4+", 20 },
                {"VI.5", 21 },
                {"VI.5+", 22 },
                {"VI.6", 23 },
                {"VI.6+", 24 },
                {"VI.7", 25 },
                {"VI.7+", 26 },
                {"VI.8", 27 },
                {"VI.8+", 28 }
            };
            Grades = grades;
        }

        public string GetKeyFromValue(int valueVar)
        {
            foreach (string keyVar in Grades.Keys)
            {
                if (Grades[keyVar] == valueVar)
                {
                    return keyVar;
                }
            }
            return null;
        }
    }
}
