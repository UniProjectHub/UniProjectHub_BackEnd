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
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Models.Task>
    {

        public void Configure(EntityTypeBuilder<Domain.Models.Task> builder)
        {
            builder.ToTable("Task");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Project).WithMany(x => x.Tasks).HasForeignKey(x => x.ProjectId);
            builder.HasMany(x => x.Members).WithOne(x => x.Task).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.subTasks).WithOne(x => x.Task).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.files).WithOne(x => x.Task).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
