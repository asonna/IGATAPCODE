using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IGATAPCODE.Models
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string TeacherId { get; set; }
        public byte[] Attendance { get; set; }
        public byte[] CodeReviews { get; set; }
        public virtual ApplicationUser User { get; set; }

        //public Course(string description, string teacherId, int attendance, string codereviews, ApplicationUser user)
        //{
        //    Description = description;
        //    TeacherId = teacherId;
        //    Attendance = attendance;
        //    CodeReviews = codereviews;
        //    User = user;
        //}
        public Course() { }
    }
}
