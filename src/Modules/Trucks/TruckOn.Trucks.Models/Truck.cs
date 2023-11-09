using System.ComponentModel.DataAnnotations;

namespace TruckOn.Trucks.Models
{
    /// <summary>
    /// Truck
    /// </summary>
    public class Truck
    {
        [Key]
        [Required]
        [MaxLength(ModelRestrictions.TruckCodeMaxLength)]
        public string Code { get; set; } = default!;

        [Required]
        [MaxLength(ModelRestrictions.TruckNameMaxLength)]
        public string Name { get; set; } = default!;

        [Required]
        public TruckStatus Status { get; set; } = default!;

        [MaxLength(ModelRestrictions.TruckDescriptionMaxLength)]
        public string? Description { get; set; } = default!;
    }
}
