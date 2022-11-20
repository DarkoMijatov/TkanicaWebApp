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
                new ClothingType { Id = 1, Name = "anterija", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 2, Name = "cipele", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 3, Name = "čakšire", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 4, Name = "čarape", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 5, Name = "čizme", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 6, Name = "dolama", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 7, Name = "džuba", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 8, Name = "futa", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 9, Name = "gaće", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 10, Name = "gunj", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 11, Name = "haljina", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 12, Name = "jelek", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 13, Name = "kecelja", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 14, Name = "košulja", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 15, Name = "marama", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 16, Name = "opanci", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 17, Name = "pantalone", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 18, Name = "pargar", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 19, Name = "pojas", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 20, Name = "prsluk", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 21, Name = "suknja", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 22, Name = "šajkača", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 23, Name = "šalvare", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 24, Name = "šubara", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 25, Name = "torba", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ClothingType { Id = 26, Name = "zubun", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );
        }
    }
}
