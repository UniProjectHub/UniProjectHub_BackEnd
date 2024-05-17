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
    public class MemberInTaskConfiguration : IEntityTypeConfiguration<MemberInTask>
    {
        public void Configure(EntityTypeBuilder<MemberInTask> builder)
        {
            builder.ToTable("MemberInTask");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User).WithMany(x => x.MemberInTasks).HasForeignKey(x => x.MemberId);
            builder.HasOne(x => x.Task).WithMany(x => x.Members).HasForeignKey(x => x.TaskId);
        }
    }
}
