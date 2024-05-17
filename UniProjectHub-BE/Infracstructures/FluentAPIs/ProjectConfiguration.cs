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
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project");
            builder.HasKey(x => x.Id);

            
            builder.HasMany(x => x.Members).WithOne(x => x.Project).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.GroupChats).WithOne(x => x.Project).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Tasks).WithOne(x => x.Project).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
