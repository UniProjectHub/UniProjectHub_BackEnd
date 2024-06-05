using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructures.FluentAPIs
{
    public class UserConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {

            builder.ToTable("Users");

            //Id
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(50);
            //Schedules
            builder.HasMany(x => x.Schedules).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);
            //Blog
            builder.HasMany(x => x.Blogs).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);
            //Notification
            builder.HasMany(x => x.Notifications).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);
            //GroupChats
            builder.HasMany(x => x.GroupChats).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);
            //Members
            builder.HasMany(x => x.Members).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);
            //MemberInTasks
            builder.HasMany(x => x.MemberInTasks).WithOne(x => x.User).OnDelete(DeleteBehavior.Cascade);
            //File
            builder.HasMany(x => x.files).WithOne(x => x.Users).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
