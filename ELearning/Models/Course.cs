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
            Purchases = new HashSet<Purchases>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string CourseImage { get; set; }

        public Author Author { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public ICollection<CourseInfo> CourseInfo { get; set; }
        public ICollection<CourseModule> CourseModule { get; set; }
        public ICollection<Purchases> Purchases { get; set; }
    }
}
