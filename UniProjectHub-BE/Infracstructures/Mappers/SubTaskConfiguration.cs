using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.FluentAPIs
{

    public class SubTaskConfiguration : IEntityTypeConfiguration<SubTask>
    {
        public void Configure(EntityTypeBuilder<SubTask> builder)
        {
            builder.ToTable(nameof(SubTask));
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Task).WithMany(x => x.subTasks).HasForeignKey(x => x.TaskId);
        }
    }

}
