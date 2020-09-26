using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class StudentProject
    {
        public String StudentId { get; set; }
        public User Student { get; set; }

        public String ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
