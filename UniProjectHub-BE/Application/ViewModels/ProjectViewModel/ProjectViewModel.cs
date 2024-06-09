using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ProjectViewModel
{
    public class ProjectViewModel
    {

        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public bool IsGroup { get; set; }
        public DateTime CreatedAt { get; set; }

        public class CreateProjectRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Status { get; set; }
            public bool IsGroup { get; set; }
        }

        public class UpdateProjectRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Status { get; set; }
            public bool IsGroup { get; set; }
        }
    }
}
