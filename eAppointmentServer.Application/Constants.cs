using eAppointmentServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAppointmentServer.Application
{
    public static class Constants
    {
        public static List<AppRole> GetRoles()
        {
            List<string> roles = new List<string>()
            {
                "Admin",
                "Doctor",
                "Personal",
                "Patient"
            };
            return roles.Select(s => new AppRole() { Name = s }).ToList();
        }
        //public static List<AppRole> Roles = new List<AppRole>()
        //{
        //    new AppRole()
        //    {
        //        Name="Admin"
        //    },
        //    new AppRole()
        //    {
        //        Name="Doctor"
        //    },
        //    new AppRole()
        //    {
        //        Name="Personal"
        //    },
        //};
    }
}

