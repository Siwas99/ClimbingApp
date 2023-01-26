namespace ClimbingApp.Models
{
    public class ExpeditionLog
    {
        public int ExpeditionLogId { get; set; }
        public int UserId{ get; set; }
        public int RouteId { get; set; }
        public DateTime Date{ get; set; }
        public int Valuation { get; set; }
        public int ClimbStyleId { get; set; }
        public string? Comment { get; set; }
        
        public virtual User User{ get; set; }
        public virtual ClimbStyle ClimbStyle { get; set; }
        public virtual Route Route { get; set; }
    }
}
