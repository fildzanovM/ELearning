using ELearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.ViewModel
{
    //Course By SubCategory Data Transfer Object
    public class CourseDTO
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseImage { get; set; }

        public string CourseCategory { get; set; }
        public string CourseSubCategory { get; set; }
        public string CourseAuthor { get; set; }
        public ICollection<PostCourseModule> CourseModule { get; set; }
    }

    //All Courses Data Transfer Object
    public class AllCourses
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseImage { get; set; }

        public string CourseAuthor { get; set; }
        public string SubCategoryName { get; set; }
        public string CourseCategory { get; set; }

        public ICollection<PostCourseModule> CourseModule { get; set; }
    }

    //Course Info Data Transfer Object
    public class CourseInfoDTO
    {
        public string CourseName { get; set; }
        public string CourseDuration { get; set; }
        public int CourseRating { get; set; }
        public int CoursePrice { get; set; }
        public string CourseLevel { get; set; }
    }

    //Course Module by Course ID Data Transfer Object
    public class CourseModuleDTO
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string ModuleName { get; set; }
        public string ModuleDuration { get; set; }
        public string ModuleVideo { get; set; }
    }

    //Post Course with Modules Data Transfer Object
    public class PostCourseModule
    {
        public string ModuleName { get; set; }
        public string ModuleDuration { get; set; }
        public string ModuleVideo { get; set; }
    }

    //Post Course Data Transfer Object
    public class PostCourseDTO
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int AuthorID { get; set; }

        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }

        public string CourseImage { get; set; }

        public ICollection<PostCourseModule> CourseModule { get; set; }
            = new List<PostCourseModule>();

    }

    //Author Data Transfer Object
    public class AuthorDTO
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImage { get; set; }
        public ICollection<AuthorCourseDTO> Course {get; set; }


    }

    //Get Author Course Data Transfer Object
    public class AuthorCourseDTO
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
    }

    //Course Info for Slider
    public class CourseInfoForCategories
    {
        public  string CoursePrice { get; set; }
        public string  CourseDuration { get; set; }
    }
    //Get Course By Category Data Transfer Object
    public class CourseByCategoryDTO
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string CourseImage { get; set; }
        public string CourseAuthor { get; set; }

        public ICollection<CourseInfoForCategories> CourseInfo { get; set; }
    }

    public class PostAuthorDTO
    {
        public string AuthorFirstName{ get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorTelephone { get; set; }
        public string AuthorImage { get; set; }
    }

    public class SearchCourseDTO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }

    public class AllAuthorsDTO
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

    }
}
