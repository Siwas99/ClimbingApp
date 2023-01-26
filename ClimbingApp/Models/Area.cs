using System.ComponentModel.DataAnnotations;

namespace ClimbingApp.Models
{
    public class Area
    {
        public int AreaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RegionId { get; set; }    

        public virtual Region Region { get; set; }    
    }
}
