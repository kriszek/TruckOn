namespace TruckOn.Trucks.Models
{
    /// <summary>
    /// Truck
    /// </summary>
    public class Truck
    {
        public Truck(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
