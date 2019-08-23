using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.ViewModel
{
    //Category Data Transfer Object
    public class CategoryDTO
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

    }

    public class CategoryWithCourses
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<CoursesForCategory> Courses { get; set; }
    

    }


    public class CoursesForCategory
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseImage { get; set; }
        public string CourseAuthor { get; set; }
        public string SubCategoryName { get; set; }
        public ICollection<CourseInfoForAllCourses> CourseInfo { get; set; }

    }
}
