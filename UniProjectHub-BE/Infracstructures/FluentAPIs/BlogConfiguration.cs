using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.FluentAPIs
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ToTable(nameof(Blog));
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User).WithMany(x => x.Blogs).HasForeignKey(x => x.OwnerId);
            builder.HasMany(x => x.Comments).WithOne(x => x.Blog).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Category).WithMany(x => x.Blogs).HasForeignKey(x => x.CategoryID);

        }
    }
}
