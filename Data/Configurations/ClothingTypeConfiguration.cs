using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TkanicaWebApp.Models;

namespace TkanicaWebApp.Data.Configurations
{
    public class ClothingTypeConfiguration : IEntityTypeConfiguration<ClothingType>
    {
        public void Configure(EntityTypeBuilder<ClothingType> builder)
        {
            builder.HasData(
                new ClothingType { Id = 1, Name = "anterija" },
                new ClothingType { Id = 2, Name = "cipele" },
                new ClothingType { Id = 3, Name = "čakšire" },
                new ClothingType { Id = 4, Name = "čarape" },
                new ClothingType { Id = 5, Name = "čizme" },
                new ClothingType { Id = 6, Name = "dolama" },
                new ClothingType { Id = 7, Name = "džuba" },
                new ClothingType { Id = 8, Name = "futa" },
                new ClothingType { Id = 9, Name = "gaće" },
                new ClothingType { Id = 10, Name = "gunj" },
                new ClothingType { Id = 11, Name = "haljina" },
                new ClothingType { Id = 12, Name = "jelek" },
                new ClothingType { Id = 13, Name = "kecelja" },
                new ClothingType { Id = 14, Name = "košulja" },
                new ClothingType { Id = 15, Name = "marama" },
                new ClothingType { Id = 16, Name = "opanci" },
                new ClothingType { Id = 17, Name = "pantalone" },
                new ClothingType { Id = 18, Name = "pargar" },
                new ClothingType { Id = 19, Name = "pojas" },
                new ClothingType { Id = 20, Name = "prsluk" },
                new ClothingType { Id = 21, Name = "suknja" },
                new ClothingType { Id = 22, Name = "šajkača" },
                new ClothingType { Id = 23, Name = "šalvare" },
                new ClothingType { Id = 24, Name = "šubara" },
                new ClothingType { Id = 25, Name = "torba" },
                new ClothingType { Id = 26, Name = "zubun" }
            );
        }
    }
}
