using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class StudentNote
    {
        public String Id { get; set; }

        public String StudentId { get; set; }

        public User Student { get; set; }

        public String TeacherId { get; set; }

        public User Teacher { get; set; }

        public DateTime CreatedAt { get; set; }

        public String Text { get; set; }
    }
}
