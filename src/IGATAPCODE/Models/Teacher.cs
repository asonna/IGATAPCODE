using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGATAPCODE.Models
{
    [Table("Teachers")]
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public byte[] Picture { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Teacher(string firstname, string lastname, string bio, byte[] picture, ApplicationUser user)
        {
            FirstName = firstname;
            LastName = lastname;
            Bio = bio;
            Picture = picture;
            User = user;
        }
        public Teacher() { }
    }
}
