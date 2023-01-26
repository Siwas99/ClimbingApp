using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ClimbingApp.Data.DTO
{
    public class RockDTO
    {
        public int RockId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Height { get; set; }
        public int Distance { get; set; }
        public int Popularity { get; set; }
        public string RockFaceExposure { get; set; }
        public bool IsShadedFromTrees { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsLoose { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public bool Slabs { get; set; }
        public bool Vertical { get; set; }
        public bool Overhang { get; set; }
        public bool Roof { get; set; }
        public int AreaId { get; set; }
        public bool changes { get; set; }
        public IFormFile Image { get; set; }
    }
}
