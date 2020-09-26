using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class Project
    {
        public String Id { get; set; }

        public String TeacherId { get; set; }

        public User Teacher { get; set; }

        public ICollection<StudentProject> Students { get; set; }

        public ICollection<ProjectNote> Notes { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }
    }
}
