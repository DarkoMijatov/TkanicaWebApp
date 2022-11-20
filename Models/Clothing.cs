using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TkanicaWebApp.Interfaces;

namespace TkanicaWebApp.Models
{
    public class Clothing : ITrackable
    {
        public int Id { get; set; }
        public int ClothingTypeId { get; set; }
        public ClothingType ClothingType { get; set; }
        public int ClothingRegionId { get; set; }
        public ClothingRegion ClothingRegion { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string Size { get; set; }
        public byte[] Image { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
