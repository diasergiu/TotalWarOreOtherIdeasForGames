using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TotalWarDLA.Models.Enum;

namespace TotalWarDLA.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
