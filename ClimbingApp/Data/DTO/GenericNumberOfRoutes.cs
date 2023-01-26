using ClimbingApp.Models;

namespace ClimbingApp.Data.DTO
{
    public class GenericNumberOfRoutes<T>
    {
        public T Obj { get; set; }
        public NumberOfRoutes NumberOfRoutes { get; set; }

        public GenericNumberOfRoutes(T obj, NumberOfRoutes numberOfRoutes)
        {
            Obj = obj;
            NumberOfRoutes = numberOfRoutes;
        }
    }
}
