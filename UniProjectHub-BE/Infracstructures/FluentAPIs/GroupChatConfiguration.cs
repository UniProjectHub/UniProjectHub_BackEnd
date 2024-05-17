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
    public class GroupChatConfiguration : IEntityTypeConfiguration<GroupChat>
    {
        public void Configure(EntityTypeBuilder<GroupChat> builder)
        {
            builder.ToTable("GroupChat");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Project).WithMany(x => x.GroupChats).HasForeignKey(x => x.ProjectId);
            builder.HasOne(x => x.User).WithMany(x => x.GroupChats).HasForeignKey(x => x.MemberId);
        }
    }
}
