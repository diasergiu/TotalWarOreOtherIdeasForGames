using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TotalWarDLA.Models.Enum;

namespace TotalWarDLA.Models
{
    public class User
    {
        public User(){}
        public User(int idUser, String userName, string email, byte[] password, UserType userType)
        {
            this.IdUser = idUser;
            this.UserName = userName;
            this.Email = email;
            this.Password = password;
            this.UserType = userType;
        }
        [Key]
        public int IdUser { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public UserType UserType { get; set; }
        [NotMapped]
        public string UiPassword { get; set; }
        [NotMapped]
        public string UiPasswordCheck { get; set; }
    }
}
