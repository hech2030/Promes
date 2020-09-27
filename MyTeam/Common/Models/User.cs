using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MyTeam.Common.Models
{
    public class User : IdentityUser
    {
        [Column(TypeName = "int")]
        public int role { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string roleLabel { get; set; }

    }
}
