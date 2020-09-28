using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Service
{
    public interface IUserPermission
    {
        Boolean CanEditProject(Models.Project project);

    }
}