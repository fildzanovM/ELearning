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

        //Get array of Courses by Category Id
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

        //Get All Courses async
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
  
        // Get All Courses by CourseName
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

        //Get Category by ID async
        public async Task<Category> GetCategoryAsync(int categoryID)
        {
            _logger.LogInformation($"Getting Category");

            var query = _dBContext.Category
                .Where(o => o.CategoryId == categoryID);

            return await query.FirstOrDefaultAsync();
        }

        //Get single Category by Category Id
        public Category GetCategoryById(int categoryId)
        {
            var category = _dBContext.Category
                .Where(o => o.CategoryId == categoryId);

            return category.FirstOrDefault();
        }

        //Get single CourseLevel by CourseLevel Id
        public CourseLevel GetCourseLevelById(int courseLevelId)
        {
            _logger.LogInformation($"Getting CourseLevel");

            var courseLevel = _dBContext.CourseLevel
                .Where(o => o.CourseLevelId == courseLevelId).FirstOrDefault();

            return courseLevel;
        }

        //Get All CourseLevels 
        public CourseLevel[] GetAllCourseLevels()
        {
            _logger.LogInformation($"Getting all CourseLevels");
            var courseLevels = _dBContext.CourseLevel.OrderBy(o => o.CourseLevelId).ToArray();

            return courseLevels;
        }


        // Get SubCategory by ID async
        public async Task<SubCategory> GetSubCategoryAsync(int subCategoryID)
        {
            _logger.LogInformation($"Getting SubCategory");

            var query = _dBContext.SubCategory
                .Where(o => o.SubCategoryId == subCategoryID);

            return await query.FirstOrDefaultAsync();
        }

        //Get single SubCategory by SubCategoryId
        public SubCategory GetSubCategoryById(int subCategoryId)
        {
            var subCategory = _dBContext.SubCategory
                .Where(o => o.SubCategoryId == subCategoryId);

            return subCategory.FirstOrDefault();
        }

        //Get Author by ID async
        public async Task<Author> GetAuthorAsync(int authorID)
        {
            _logger.LogInformation($"Getting Author");
            var query = _dBContext.Author
                .Include(o => o.Course)
                .Where(o => o.AuthorId == authorID);

            return await query.FirstOrDefaultAsync();
        }

        //Get single Author by Id
        public Author GetAuthorById(int authorID)
        {
            var author = _dBContext.Author.Include(o => o.Course)
                    .Where(o => o.AuthorId == authorID);

            return author.FirstOrDefault();
        }

        //Get All Authors
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

        //Get single Course by CourseId
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

        //Add Author
        public void AddAuthor(Author author)
        {
            _dBContext.Author.Add(author);
            _dBContext.SaveChanges();
        }

        //Add Purchase 
        public void AddPurchase(Purchases purchases)
        {
            DateTime dateTime = DateTime.Now;
            purchases.PurchaseDate = dateTime;
            _dBContext.Purchases.Add(purchases);
            _dBContext.SaveChanges();
        }

        //Get All Purchases
        public Purchases[] GetAllPurchases()
        {
            _logger.LogInformation($"Getting all purchases");

            var result = _dBContext.Purchases.Include(c => c.Course)
                .ThenInclude(o => o.Category)
                .OrderBy(p => p.Course.Category.CategoryName).ToArray();

            return result;
        }

        //Delete Course
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


        //#region SERVICE DATATABLE
        //public PagedList<_company_service_table_item> getServices(_company_service_filter filter)
        //{
        //    var db = new ETrustContext();
        //    int decryptedCompanyId = int.Parse(AESCryptography.AES256_Decrypt(filter.company_id));
        //    var services = db.Service.Where(temp => temp.CompanyId == decryptedCompanyId).Include(s => s.ServiceVariant).ToList();
        //    var dtomodel = new List<_company_service_table_item>();
        //    if (services != null)
        //    {
        //        IMapper mapper = MapperFn.companyservicetableItemmapper();
        //        if (services != null)
        //        {
        //            dtomodel = mapper.Map<List<_company_service_table_item>>(services);
        //        }
        //        foreach (var model in dtomodel)
        //        {
        //            var marketCategory = db.MarketCategory.Where(temp => temp.CategoryId == int.Parse(AESCryptography.AES256_Decrypt(model.market_category.ToString()))).FirstOrDefault();

        //            if (marketCategory != null)
        //            {
        //                model.market_category = marketCategory.CategoryName;
        //            }
        //        }
        //    }

        //    #region Filter

        //    if (!string.IsNullOrEmpty(filter.sale_type))
        //    {
        //        var typeName = CommonFn.getconfigurationname(int.Parse(AESCryptography.AES256_Decrypt(filter.sale_type)));
        //        dtomodel = dtomodel.Where(temp => temp.sale_type == typeName).ToList();
        //    }

        //    if (!string.IsNullOrEmpty(filter.category))
        //    {
        //        var marketCategory = db.MarketCategory.Where(temp =>
        //        temp.CategoryId == int.Parse(AESCryptography.AES256_Decrypt(filter.category))).FirstOrDefault();

        //        dtomodel = dtomodel.Where(temp => temp.market_category == marketCategory.CategoryName).ToList();
        //    }
        //    if (filter.price_from != null)
        //    {
        //        dtomodel = dtomodel.Where(temp => temp.price >= filter.price_from).ToList();
        //    }
        //    if (filter.price_to != null)
        //    {
        //        dtomodel = dtomodel.Where(temp => temp.price <= filter.price_to).ToList();
        //    }

        //    if (!string.IsNullOrEmpty(filter.service_name))
        //    {
        //        dtomodel = dtomodel.Where(temp => temp.service_name.Contains(filter.service_name)).ToList();
        //    }
        //    if (filter.with_booking != null)
        //    {
        //        dtomodel = dtomodel.Where(temp => temp.is_booking_purchase == filter.with_booking).ToList();
        //    }
        //    if (filter.service_status != null)
        //    {
        //        dtomodel = dtomodel.Where(temp => temp.is_active == filter.service_status).ToList();
        //    }
        //    #endregion

        //    #region sorting
        //    if (filter.sort_direction.ToUpper() == "ASC")
        //    {
        //        switch (filter.sort_column.ToLower())
        //        {
        //            case "name":
        //                dtomodel = dtomodel.OrderBy(p => p.service_name).ToList();
        //                break;
        //            case "sale_type":
        //                dtomodel = dtomodel.OrderBy(p => p.sale_type).ToList();
        //                break;
        //            case "variants":
        //                dtomodel = dtomodel.OrderBy(p => p.variants).ToList();
        //                break;
        //            case "category":
        //                dtomodel = dtomodel.OrderBy(d => d.variants).ToList();
        //                break;
        //            case "with_booking":
        //                dtomodel = dtomodel.OrderBy(d => d.is_booking_purchase).ToList();
        //                break;
        //            case "price":
        //                dtomodel = dtomodel.OrderBy(d => d.price).ToList();
        //                break;
        //            default:
        //                dtomodel = dtomodel.OrderBy(p => p.is_active).ToList();
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        switch (filter.sort_column.ToLower())
        //        {
        //            case "name":
        //                dtomodel = dtomodel.OrderByDescending(p => p.service_name).ToList();
        //                break;
        //            case "sale_type":
        //                dtomodel = dtomodel.OrderByDescending(p => p.sale_type).ToList();
        //                break;
        //            case "variants":
        //                dtomodel = dtomodel.OrderByDescending(p => p.variants).ToList();
        //                break;
        //            case "category":
        //                dtomodel = dtomodel.OrderByDescending(d => d.variants).ToList();
        //                break;
        //            case "with_booking":
        //                dtomodel = dtomodel.OrderByDescending(d => d.is_booking_purchase).ToList();
        //                break;
        //            case "price":
        //                dtomodel = dtomodel.OrderByDescending(d => d.price).ToList();
        //                break;
        //            default:
        //                dtomodel = dtomodel.OrderByDescending(p => p.is_active).ToList();
        //                break;
        //        }
        //    }
        //    #endregion

        //    return new PagedList<_company_service_table_item>
        //    {
        //        Items = dtomodel.Skip((filter.page_index - 1) * filter.page_size).Take(filter.page_size),
        //        Page = filter.page_index,
        //        PageSize = filter.page_size,
        //        TotalCount = dtomodel.Count()
        //    };
        //}
        //#endregion


    }
}
