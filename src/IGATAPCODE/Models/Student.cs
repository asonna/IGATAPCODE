using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IGATAPCODE.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Picture { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Student(string firstname, string lastname, byte[] picture, ApplicationUser user)
        {
            FirstName = firstname;
            LastName = lastname;
            Picture = picture;
            User = user;
        }
        public Student() { }
    }
}