using Blog.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastracture.Connections
{
    public static class DataModelConfiguration
    {
        public static void ConfigureModels(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.Password).IsRequired();

                entity.HasMany(u => u.Publications)
                      .WithOne(p => p.Users)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PublicationEntity>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Title)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(p => p.Content)
                      .IsRequired()
                      .HasColumnType("text");

                entity.Property(p => p.UserId)
                      .IsRequired();
            });
        }
    }
}