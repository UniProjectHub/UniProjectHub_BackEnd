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
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable(nameof(Schedule));
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CourseName).IsRequired();
  
            builder.HasOne(x => x.User)
                   .WithMany(x => x.Schedules)
                   .HasForeignKey(x => x.UserId);

            
        }
    }
}
