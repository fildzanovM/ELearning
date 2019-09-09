using AutoMapper;
using ELearning.Models;
using ELearning.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Data
{
    public class ELearningRepository : IELearningRepository
    {
        private readonly ELearningDBContext _dBContext;
        private readonly ILogger<ELearningRepository> _logger;
        private int weekNum;

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

        //Sorting Purchases
        public PagedList<Sorted_Purchases_DTO> GetSortedPurchases(Purchase_Filter filter)
        {
            _logger.LogInformation("Getting sorted purchases");
            var purchases = _dBContext.Purchases
                    .Include(o => o.Course)
                    .ThenInclude(o => o.Category).ToList();

            var dtomodel = new List<Sorted_Purchases_DTO>();


            if (purchases != null)
            {
                IMapper mapper = ELearningProfile.PurchaseFilterMapper();
                dtomodel = mapper.Map<List<Sorted_Purchases_DTO>>(purchases);
            }

            if (!string.IsNullOrEmpty(filter.CourseName))
            {
                dtomodel = dtomodel.Where(temp => temp.CourseName == filter.CourseName).ToList();
            }

            if (!string.IsNullOrEmpty(filter.CategoryName))
            {
                dtomodel = dtomodel.Where(temp => temp.CategoryName == filter.CategoryName).ToList();
            }

            
            if(filter.Sort_direction.ToUpper() == "ASC")
            {
                switch (filter.Sort_column.ToLower())
                {
                    case "purchaseid":
                        dtomodel = dtomodel.OrderBy(o => o.PurchaseId).ToList();
                        break;

                    case "courseid":
                        dtomodel = dtomodel.OrderBy(o => o.CourseId).ToList();
                        break;

                    case "categoryid":
                        dtomodel = dtomodel.OrderBy(o => o.CategoryId).ToList();
                        break;

                    case "categoryname":
                        dtomodel = dtomodel.OrderBy(o => o.CategoryName).ToList();
                        break;

                    case "purchasedate":
                        dtomodel = dtomodel.OrderBy(o => o.PurchaseDate).ToList();
                        break;
                }
            }

            else
            {
                switch (filter.Sort_column.ToLower())
                {
                    case "purchaseid":
                        dtomodel = dtomodel.OrderByDescending(o => o.PurchaseId).ToList();
                        break;

                    case "courseid":
                        dtomodel = dtomodel.OrderByDescending(o => o.CourseId).ToList();
                        break;

                    case "categoryid":
                        dtomodel = dtomodel.OrderByDescending(o => o.CategoryId).ToList();
                        break;

                    case "categoryname":
                        dtomodel = dtomodel.OrderByDescending(o => o.CategoryName).ToList();
                        break;

                    case "purchasedate":
                        dtomodel = dtomodel.OrderByDescending(o => o.PurchaseDate).ToList();
                        break;
                }

            }

            return new PagedList<Sorted_Purchases_DTO>
            {
                Items = dtomodel.Skip((filter.Page_index - 1) * filter.Page_size).Take(filter.Page_size),
                Page = filter.Page_index,
                PageSize = filter.Page_size,
                TotalCount = dtomodel.Count()
            };
        }

        public List<WeekDTO> PurchasesByWeeks()
        {
            var weeksInMonth = GetWeeks();
            int index = 0;

            var weeks = new List<WeekDTO>();
            while (index != weeksInMonth.Count)
            {
                var results = _dBContext.Purchases
                .Where(x => x.PurchaseDate.Date >= weeksInMonth[index].Item1 || x.PurchaseDate.Date <= weeksInMonth[index].Item2)
                .Include(x => x.Course)
                    .ThenInclude(x => x.Category)
                .Select(x => new CoursePurchasedNumberByCategoryDTO
                {
                    CategoryName = x.Course.Category.CategoryName,
                    PurchasedCount = x.Course.Purchases.Count
                })
                .ToList();

                weeks.Add(new WeekDTO { Id = index, Categories = results, StartDate = weeksInMonth[index].Item1, EndDate = weeksInMonth[index].Item2 } );
                ++index;
            }

            return weeks;
        }

        private static List<Tuple<DateTime,DateTime>> GetWeeks()
        {
            DateTime reference = DateTime.Now;
            Calendar calendar = CultureInfo.CurrentCulture.Calendar;

            IEnumerable<int> daysInMonth = Enumerable.Range(1, calendar.GetDaysInMonth(reference.Year, reference.Month));

            List<Tuple<DateTime, DateTime>> weeks = daysInMonth.Select(day => new DateTime(reference.Year, reference.Month, day))
                .GroupBy(d => calendar.GetWeekOfYear(d, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday))
                .Select(g => new Tuple<DateTime, DateTime>(g.First(), g.Last()))
                .ToList();

            weeks.ForEach(x => Console.WriteLine("{0:MM/dd/yyyy} - {1:MM/dd/yyyy}", x.Item1, x.Item2));

            return weeks;
        }

        //var categoryNames = new List<string>();

        //var myList = _dBContext.Category.Select(o => o.CategoryName).ToList();

        //categoryNames.AddRange(myList);

        //To calculate last four weeks purchases
        public Dictionary<string, int> CalculateWeeklyPurchases()
        {
            DayOfWeek weekStart = DayOfWeek.Monday;
            DateTime startingDate = DateTime.Today;

            while (startingDate.DayOfWeek != weekStart)
                startingDate = startingDate.AddDays(-1);

            DateTime previousWeekStart =  startingDate.AddDays(-7);
            DateTime previousWeekEnd = startingDate.AddDays(-1);

            List<Purchases> listAllPurchases = new List<Purchases>();
            Dictionary<string, int> dictionaryWeeklySum = new Dictionary<string, int>();


            var webDevelopmentSum = _dBContext.Purchases
                .Include(o => o.Course.Category.CategoryName)
                .Where(o => o.PurchaseDate > startingDate)
                .Select(cat => cat.Course.Purchases).Count();

            var mobileAppsSum = _dBContext.Purchases
                .Where(cat => cat.Course.Category.CategoryName == "Mobile Apps" && (cat.PurchaseDate > startingDate))
                .Select(cat => cat.Course.Purchases).Count();

            var databasesSum = _dBContext.Purchases.Where
                (cat => cat.Course.Category.CategoryName == "Databases" && (cat.PurchaseDate > startingDate))
                .Select(cat => cat.Course.Purchases).Count();

            var eCommerceSum = _dBContext.Purchases.Where
                (cat => cat.Course.Category.CategoryName == "E-Commerce" && (cat.PurchaseDate > startingDate))
                .Select(cat => cat.Course.Purchases).Count();

            dictionaryWeeklySum.Add("Web Development", webDevelopmentSum);
            dictionaryWeeklySum.Add("Mobile Apps", mobileAppsSum);
            dictionaryWeeklySum.Add("Databases", databasesSum);
            dictionaryWeeklySum.Add("E-Commerce", eCommerceSum);
           
            return dictionaryWeeklySum;
        }
    }
}
