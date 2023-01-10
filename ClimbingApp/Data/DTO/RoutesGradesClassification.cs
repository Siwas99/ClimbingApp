namespace ClimbingApp.Data.DTO
{
    public class RoutesGradesClassification
    {
        public List<string> VeryEasyGrades = new List<string> { "I", "II", "III", "IV-", "IV", "IV+", "V-" };
        public List<string> EasyGrades = new List<string> { "V", "V+", "VI-", "VI", "VI+" };
        public List<string> MediumGrades = new List<string> { "VI.1", "VI.1+", "VI.2", "VI.2+" };
        public List<string> HardGrades = new List<string> { "VI.3", "VI.3+", "VI.4", "VI.4+" };
        public List<string> VeryHardGrades = new List<string> { "VI.5", "VI.5+", "VI.6", "VI.6+", "VI.7", "VI.7+", "VI.8", "VI.8+" };
    }
}
