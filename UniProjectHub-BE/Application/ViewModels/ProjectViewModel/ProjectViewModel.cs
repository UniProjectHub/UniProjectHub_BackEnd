using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProjectViewModel
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? NameLeader { get; set; }
        public string? TypeOfSpace { get; set; }
        public string? Img { get; set; }
        public int Status { get; set; }
        public bool IsGroup { get; set; }
        public DateTime CreatedAt { get; set; }

        public class CreateProjectRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string? NameLeader { get; set; }
            public string? TypeOfSpace { get; set; }
            public string? Img { get; set; }
            public int Status { get; set; }
            public bool IsGroup { get; set; }
            public ICollection<MemberViewModelInProject>? Members { get; set; }
        }

        public class UpdateProjectRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string? NameLeader { get; set; }
            public string? TypeOfSpace { get; set; }
            public string? Img { get; set; }
            public int Status { get; set; }
            public bool IsGroup { get; set; }
        }
    }
    public class MemberViewModelInProject
    {
        public string? UserId { get; set; }
    }
}
