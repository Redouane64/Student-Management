using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace StudentManagement.Models
{
    public class User : IdentityUser
    {
        public ICollection<Project> Projects { get; set; }
        public ICollection<ProjectNote> Notes { get; set; }

        public ICollection<StudentProject> StudentProjects { get; set; }
        public ICollection<StudentNote> StudentNotes { get; set; }
    }

}
