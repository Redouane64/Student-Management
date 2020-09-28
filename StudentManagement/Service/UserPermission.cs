using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Service
{
    public class UserPermission : IUserPermission
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public UserPermission(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        private HttpContext HttpContext => this.httpContextAccessor.HttpContext;
        public Boolean CanEditProject(Models.Project project)
        {
            if (!this.HttpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            if ((this.HttpContext.User.IsInRole(UserRoles.Administrators)) || (this.HttpContext.User.IsInRole(UserRoles.Teachers)))
            {
                return true;
            }

            return this.userManager.GetUserId(this.httpContextAccessor.HttpContext.User) == project.TeacherId;
        }
    }
}