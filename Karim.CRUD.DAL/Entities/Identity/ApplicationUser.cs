using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FisrtName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsAgreeOnTerms { get; set; }

    }
}
