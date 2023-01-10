namespace ClimbingApp.Data.DTO
{
    public class NumberOfRoutes
    {
        
        public int VeryEasyRoutes { get; set; }
        public int EasyRoutes { get; set; }
        public int MediumRoutes { get; set; }
        public int HardRoutes { get; set; }
        public int VeryHardRoutes { get; set; }
        public int Projects { get; set; }

        public NumberOfRoutes()
        {
            VeryEasyRoutes = 0;
            EasyRoutes = 0;
            MediumRoutes = 0;
            HardRoutes = 0;
            VeryHardRoutes = 0;
            Projects = 0;

        }
    }
}
