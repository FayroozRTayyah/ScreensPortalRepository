using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screens.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(10)]
        public string EmployeeNo { get; set; }

        [StringLength(300)]
        public string FullName { get; set; }

        public int RelatedEntityID { get; set; }


        //user type
        // 1 => Admin
        // 2 => Employee
        // 3 => Company
        //public int UserTypeId { get; set; }


    }

}
