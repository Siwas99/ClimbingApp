namespace ClimbingApp.Models
{
    public class ExpeditionLog
    {
        public int ExpeditionLogId { get; set; }
        public User User{ get; set; }
        public Route Route { get; set; }
        public DateTime Date{ get; set; }
        public int Valuation { get; set; }
        public string Comment { get; set; }
    }
}
