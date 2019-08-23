using ELearning.Models;
using ELearning.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Data
{
    public class ELearningRepository : IELearningRepository
    {
        private readonly ELearningDBContext _dBContext;
        private readonly ILogger<ELearningRepository> _logger;

        public ELearningRepository(ELearningDBContext dBContext, ILogger<ELearningRepository> logger)
        {
            _dBContext = dBContext;
            _logger = logger;
        }

        //Add entity
        //public void Add<T>(T entity) where T : class
        //{
        //    _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
        //    _dBContext.Add(entity);
        //}

        ////Delete entity
        //public void Delete<T>(T entity) where T : class
        //{
        //    _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
        //    _dBContext.Remove(entity);
        //}

        //Save changes
        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempting to save the changes in the context");
            return (await _dBContext.SaveChangesAsync()) > 0;
        }

        public bool SaveChanges()
        {
            _logger.LogInformation($"Attempting to save the changes in the context");
            return (_dBContext.SaveChanges()) > 0;
        }

        //Get All Categories
        public async Task<Category[]> GetAllCategoriesAsync()
        {
            _logger.LogInformation($"Getting all categories");

            IQueryable<Category> query = _dBContext.Category;

            query.OrderBy(c => c.CategoryId);

            return await query.ToArrayAsync();
        
        }
        
        //Get SubCategories by Categories
        public async Task<SubCategory[]> GetSubCategoriesById(int categoryID)
        {
            _logger.LogInformation($"Getting all SubCategories");
             IQueryable<SubCategory> query = _dBContext.SubCategory;

            query = query.Where(t => t.Category.CategoryId == categoryID)
                .OrderBy(t => t.SubCategoryId);

            return await query.ToArrayAsync();
        }

        public Course[] GetCourseByCategoryID(int categoryID)
        {
            _logger.LogInformation($"Getting course by CategoryID");
            var result = _dBContext.Course.Include(c => c.CourseInfo)
                .Include(o => o.Author)
                .Where(t => t.CategoryId == categoryID);

            return result.ToArray();
        }

        //Get Courses by SubCategories
        public async Task<Course[]> GetCoursesBySubCategories(int subCategoryID)
        {
            _logger.LogInformation($"Getting all courses");
            IQueryable<Course> query = _dBContext.Course.Include(c => c.Author);

            query = query.Where(t => t.SubCategory.SubCategoryId == subCategoryID)
                .OrderBy(t => t.CourseId);

            return await query.ToArrayAsync();
        }

        //Get All Courses 
        public async Task<Course[]> GetAllCourses()
        {
            _logger.LogInformation($"Getting all courses");
            IQueryable<Course> query = _dBContext.Course.Include(c => c.Author);

            query = query.Include(o => o.SubCategory)
                .Include(o => o.Category)
                .Include(o => o.CourseModule)
                .Include(o => o.CourseInfo)
                .ThenInclude(o => o.CourseLevel)
                .OrderBy(t => t.CourseId);

            return await query.ToArrayAsync();
        }

        public Course[] AllCourses( string courseName)
        {
            _logger.LogInformation($"Getting all courses");
            var result = _dBContext.Course.Include(c => c.Author)
                .Include(o => o.SubCategory)
                .Include(o => o.CourseModule)
                .OrderBy(t => t.CourseId);

            return result.ToArray();
        }

        //Get Courses Info by Course Id
        public async Task<CourseInfo[]> GetCourseInfoByCourseID(int courseID)
        {
            _logger.LogInformation($"Getting the course info");
            IQueryable<CourseInfo> query = _dBContext.CourseInfo.Include(c => c.CourseLevel);

            query = query.Include(o => o.Course)
                .Where(t => t.Course.CourseId == courseID)
                .OrderBy(t => t.CourseLevelId);

            return await query.ToArrayAsync();
        }   

        //Get Course Modules by Course Id
        public async Task<CourseModule[]> GetCourseModuleByCourseID(int courseID)
        {
            _logger.LogInformation($"Getting course modules");
            IQueryable<CourseModule> query = _dBContext.CourseModule.Include(c => c.Course);

            query = query.Where(t => t.Course.CourseId == courseID)
                .OrderBy(t => t.CourseModuleId);

            return await query.ToArrayAsync();
        }

        //Get Category by ID
        public async Task<Category> GetCategoryAsync(int categoryID)
        {
            _logger.LogInformation($"Getting Category");

            var query = _dBContext.Category
                .Where(o => o.CategoryId == categoryID);

            return await query.FirstOrDefaultAsync();
        }

        public Category GetCategoryById(int categoryId)
        {
            var category = _dBContext.Category
                .Where(o => o.CategoryId == categoryId);

            return category.FirstOrDefault();
        }

        public CourseLevel GetCourseLevelById(int courseLevelId)
        {
            _logger.LogInformation($"Getting CourseLevel");

            var courseLevel = _dBContext.CourseLevel
                .Where(o => o.CourseLevelId == courseLevelId).FirstOrDefault();

            return courseLevel;
        }


        // Get SubCategory by ID
        public async Task<SubCategory> GetSubCategoryAsync(int subCategoryID)
        {
            _logger.LogInformation($"Getting SubCategory");

            var query = _dBContext.SubCategory
                .Where(o => o.SubCategoryId == subCategoryID);

            return await query.FirstOrDefaultAsync();
        }

        public SubCategory GetSubCategoryById(int subCategoryId)
        {
            var subCategory = _dBContext.SubCategory
                .Where(o => o.SubCategoryId == subCategoryId);

            return subCategory.FirstOrDefault();
        }

        //Get Author by ID
        public async Task<Author> GetAuthorAsync(int authorID)
        {
            _logger.LogInformation($"Getting Author");
            var query = _dBContext.Author
                .Include(o => o.Course)
                .Where(o => o.AuthorId == authorID);

            return await query.FirstOrDefaultAsync();
        }

        public Author GetAuthorById(int authorID)
        {
            var author = _dBContext.Author.Include(o => o.Course)
                    .Where(o => o.AuthorId == authorID);

            return author.FirstOrDefault();
        }

        public Author[] GetAllAuthors()
        {
            _logger.LogInformation("Getting all Authors");
            var author = _dBContext.Author.OrderBy(o => o.AuthorId).ToArray();

            return author;
        }

       // Get Course by ID
        public async Task<Course> GetCourseAsync(int courseID)
        {
            IQueryable<Course> query = _dBContext.Course.Include(c => c.Author);

            _logger.LogInformation($"Getting Course");
            query = query.Include(o => o.Category)
               .Include(o => o.SubCategory)
               .Include(o => o.CourseModule)
               .Where(o => o.CourseId == courseID);


            return await query.FirstOrDefaultAsync();
        }

        public Course GetCourseById(int courseId)
        {
            var course = _dBContext.Course.Where(o => o.CourseId == courseId)
                .Include(o => o.Author)
                .Include(o => o.Category)
                .Include(o => o.SubCategory)
                .Include(o => o.CourseModule)
                .Include(o => o.CourseInfo)
                .ThenInclude(o => o.CourseLevel)
                .FirstOrDefault();


            return course;
        }
        // Search Course By CourseName
        public Course SearchCourseByCourseName(string courseName)
        {
            var course = _dBContext.Course.Where(o => o.CourseName == courseName)
                .Include(o => o.Author)
                .Include(o => o.Category)
                .Include(o => o.SubCategory)
                .Include(o => o.CourseModule).FirstOrDefault();

            return course;

        }

        //public CourseInfo GetCourseInfoById(int courseInfoId)
        //{
        //    _logger.LogInformation($"Getting Counse Info");
        //    var result = _dBContext.CourseInfo.Include(o => o.CourseLevel)
        //        .Where(o => o.CourseInfoId == courseInfoId).FirstOrDefault();

        //    return result;

        //}

        //SearchCourse
        public List<Course> SearchCourse(string courseName)
        {
            List<Course> courses = _dBContext.Course.Where(o => o.CourseName.Contains(courseName))
                .OrderBy(o => o.CourseName).ToList();

            return courses;
        }

        //Categories with array of courses
        public Category[] CategoryWithArrayOfCourses()
        {
            _logger.LogInformation("Getting category with array of courses");
            var categories = _dBContext.Category
                .Include(o => o.Course)
                .ThenInclude(o => o.Author)
                .Include(o => o.SubCategory)
                .Include(o => o.Course)
                .ThenInclude(o => o.CourseInfo)
                .ThenInclude(o => o.CourseLevel)
                .ToArray();
             

            return categories;

        }
        

        // Create Course with Modules
        public void AddCourse(Course course)
        {
            _dBContext.Course.Add(course);

            if (course.CourseModule.Any())
            { 
                foreach (var courseModule in course.CourseModule)
                {
                    PostCourseModule postCourseModule = new PostCourseModule
                    {
                        ModuleName = courseModule.CourseModuleName,
                        ModuleDuration = courseModule.ModuleDuration
                    };

                    _dBContext.SaveChanges();
                }
            }

            if (course.CourseInfo.Any())
            {
                foreach (var courseInfo in course.CourseInfo)
                {
                    PostCourseInfoDTO postCourseInfo = new PostCourseInfoDTO
                    {
                        CoursePrice = courseInfo.CoursePrice,
                        CourseDuration = courseInfo.CourseDuration,
                    
                    };
                 
                }
            }
        
        }

        public void AddAuthor(Author author)
        {
            _dBContext.Author.Add(author);
            _dBContext.SaveChanges();
        }

     //   Delete Course
        public void DeleteCourse(Course course)
        {
            if (course.CourseModule.Any())
            {
                _dBContext.CourseModule.RemoveRange(course.CourseModule);
                _dBContext.SaveChanges();
            }

            _dBContext.Course.Remove(course);
            _dBContext.SaveChanges();
        }


    }
}
