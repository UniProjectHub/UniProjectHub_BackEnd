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
    public class FileConfiguration : IEntityTypeConfiguration<Domain.Models.File>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.File> builder)
        {
            builder.ToTable("File");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Users).WithMany(x => x.files).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Task).WithMany(x => x.files).HasForeignKey(x => x.TaskId);
        }
    }
}
