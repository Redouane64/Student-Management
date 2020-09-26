using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class ProjectNote
    {
        public String Id { get; set; }

        public String ProjectId { get; set; }

        public Project Project { get; set; }
        
        public String TeacherId { get; set; }

        public User Teacher { get; set; }

        public DateTime Created { get; set; }

        public String Text { get; set; }
    }
}
