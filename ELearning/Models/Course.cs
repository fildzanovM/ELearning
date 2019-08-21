using System;
using System.Collections.Generic;

namespace ELearning.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseInfo = new HashSet<CourseInfo>();
            CourseModule = new HashSet<CourseModule>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string CourseImage { get; set; }

        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<CourseInfo> CourseInfo { get; set; }
        public virtual ICollection<CourseModule> CourseModule { get; set; }
    }
}
