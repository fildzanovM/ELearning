using ELearning.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELearning.Data
{
    public interface IELearningRepository
    {
        //General
        //void Add<T>(T entity) where T : class;
        //void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        bool SaveChanges();

        //Author
        Task<Author> GetAuthorAsync(int authorID);
        Author GetAuthorById(int authorId);
        Author[] GetAllAuthors();
        void AddAuthor(Author author);

        //Category
        Task<Category[]> GetAllCategoriesAsync();
        Task<Category> GetCategoryAsync(int speakerID);
        Category GetCategoryById(int categoryId);
        Category[] CategoryWithArrayOfCourses();

        //SubCategory
        Task<SubCategory[]> GetSubCategoriesById(int categoryID);
        Task<SubCategory> GetSubCategoryAsync(int subCategoryID);
        SubCategory GetSubCategoryById(int subCategoryId);

        //Course
        Task<Course[]> GetAllCourses();
        Task<Course[]> GetCoursesBySubCategories(int subCategoryID);
        Task<Course> GetCourseAsync(int courseID);
        Course[] AllCourses(string courseName);
        Course[] GetCourseByCategoryID(int categoryID);
        Course GetCourseById(int courseId);
        Course SearchCourseByCourseName(string courseName);
        List<Course> SearchCourse(string courseName);
        void AddCourse(Course course);
        void DeleteCourse(Course course);

        //CourseInfo
        Task<CourseInfo[]> GetCourseInfoByCourseID(int courseID);
        //CourseInfo GetCourseInfoById(int courseInfoId);

        //CourseModule
        Task<CourseModule[]> GetCourseModuleByCourseID(int courseID);

        //CourseLevel
        CourseLevel GetCourseLevelById(int courseLevelId);
        CourseLevel[] GetAllCourseLevels();

        //Purchases
        void AddPurchase(Purchases purchases);
        Purchases[] GetAllPurchases();

    }
}