using AutoMapper;
using ELearning.Models;
using ELearning.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Data
{
    public class ELearningProfile
    {
        //Category Mapper
        public static IMapper CategoryMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>()
                    .ForMember(o => o.CategoryName, ex => ex.MapFrom(o => o.CategoryName));               
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //SubCategory Mapper
        public static IMapper SubCategoryMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubCategory, SubCategoryDTO>()
                    .ForMember(o => o.SubCategoryID, ex => ex.MapFrom(o => o.SubCategoryId))
                    .ForMember(o => o.SubCategoryName, ex => ex.MapFrom(o => o.SubCategoryName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Course Mapper
        public static IMapper CourseMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, CourseDTO>()
                 .ForMember(o => o.CourseID, ex => ex.MapFrom(o => o.CourseId))
                 .ForMember(o => o.CourseName, ex => ex.MapFrom(o => o.CourseName))
                 .ForMember(o => o.CourseDescription, ex => ex.MapFrom(o => o.CourseDescription))
                 .ForMember(o => o.CourseImage, ex => ex.MapFrom(o => o.CourseImage))
                 .ForMember(o => o.CourseCategory, ex => ex.MapFrom(o => o.Category.CategoryName))
                 .ForMember(o => o.CourseSubCategory, ex => ex.MapFrom(o => o.SubCategory.SubCategoryName))
                 .ForMember(o => o.CourseAuthor, ex => ex.MapFrom(o => o.Author.AuthorFirstName + " " + (o.Author.AuthorLastName)));

                cfg.CreateMap<CourseModule, PostCourseModule>()
                    .ForMember(o => o.ModuleName, ex => ex.MapFrom(o => o.CourseModuleName));

                cfg.CreateMap<CourseInfo, CourseInfoForAllCourses>()
                    .ForMember(o => o.CoursePrice, ex => ex.MapFrom(o => o.CoursePrice))
                    .ForMember(o => o.CourseDuration, ex => ex.MapFrom(o => o.CourseDuration))
                    .ForMember(o => o.CourseLevel, ex => ex.MapFrom(o => o.CourseLevel.CourseLevelName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        // All Courses Mapper
        public static IMapper AllCoursesMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, AllCourses>()
               .ForMember(o => o.CourseID, ex => ex.MapFrom(o => o.CourseId))
               .ForMember(o => o.CourseName, ex => ex.MapFrom(o => o.CourseName))
               .ForMember(o => o.CourseDescription, ex => ex.MapFrom(o => o.CourseDescription))
               .ForMember(o => o.CourseImage, ex => ex.MapFrom(o => o.CourseImage))
               .ForMember(o => o.SubCategoryName, ex => ex.MapFrom(o => o.SubCategory.SubCategoryName))
               .ForMember(o => o.CourseCategory, ex => ex.MapFrom(o => o.Category.CategoryName))
               .ForMember(o => o.CourseAuthor, ex => ex.MapFrom(o => o.Author.AuthorFirstName + " " + (o.Author.AuthorLastName)))
               .ForMember(o => o.CourseModule, ex => ex.MapFrom(o => o.CourseModule));

                cfg.CreateMap<CourseModule, PostCourseModule>()
                    .ForMember(o => o.ModuleName, ex => ex.MapFrom(o => o.CourseModuleName))
                .ForMember(o => o.ModuleDuration, ex => ex.MapFrom(o => o.ModuleDuration));

                cfg.CreateMap<CourseInfo, CourseInfoForAllCourses>()
                    .ForMember(o => o.CoursePrice, ex => ex.MapFrom(o => o.CoursePrice))
                    .ForMember(o => o.CourseDuration, ex => ex.MapFrom(o => o.CourseDuration))
                    .ForMember(o => o.CourseLevel, ex => ex.MapFrom(o => o.CourseLevel.CourseLevelName));
                 
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        // Course Info mapper
        public static IMapper CourseInfo()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CourseInfo, CourseInfoDTO>()
                    .ForMember(o => o.CourseName, ex => ex.MapFrom(o => o.Course.CourseName))
                    .ForMember(o => o.CourseDuration, ex => ex.MapFrom(o => o.CourseDuration))
                    .ForMember(o => o.CourseRating, ex => ex.MapFrom(o => o.CourseRating))
                    .ForMember(o => o.CoursePrice, ex => ex.MapFrom(o => o.CoursePrice))
                    .ForMember(o => o.CourseLevel, ex => ex.MapFrom(o => o.CourseLevel.CourseLevelName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        // Course Module mapper
        public static IMapper CourseModule()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CourseModule, CourseModuleDTO>()
                    .ForMember(o => o.CourseID, ex => ex.MapFrom(o => o.Course.CourseId))
                    .ForMember(o => o.CourseName, ex => ex.MapFrom(o => o.Course.CourseName))
                    .ForMember(o => o.ModuleName, ex => ex.MapFrom(o => o.CourseModuleName))
                    .ForMember(o => o.ModuleDuration, ex => ex.MapFrom(o => o.ModuleDuration))
                    .ForMember(o => o.ModuleVideo, ex => ex.MapFrom(o => o.ModuleVideo));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        // Post Course mapper
        public static IMapper PostCourse()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, PostCourseDTO>()
                   .ForMember(o => o.CourseName, ex => ex.MapFrom(o => o.CourseName))
                   .ForMember(o => o.CourseDescription, ex => ex.MapFrom(o => o.CourseDescription))
                   .ForMember(o => o.AuthorID, ex => ex.MapFrom(o => o.AuthorId))
                   .ForMember(o => o.CategoryID, ex => ex.MapFrom(o => o.CategoryId))
                   .ForMember(o => o.SubCategoryID, ex => ex.MapFrom(o => o.SubCategoryId))
                   .ForMember(o => o.CourseImage, ex => ex.MapFrom(o => o.CourseImage))
                   .ReverseMap();

                cfg.CreateMap<CourseModule, PostCourseModule>()
                .ForMember(o => o.ModuleName, ex => ex.MapFrom(o => o.CourseModuleName))
                .ForMember(o => o.ModuleDuration, ex => ex.MapFrom(o => o.ModuleDuration))
                .ForMember(o => o.ModuleVideo, ex => ex.MapFrom(o => o.ModuleVideo))
                .ReverseMap();

                cfg.CreateMap<CourseInfo, PostCourseInfoDTO>()
                    .ForMember(o => o.CoursePrice, ex => ex.MapFrom(o => o.CoursePrice))
                    .ForMember(o => o.CourseDuration, ex => ex.MapFrom(o => o.CourseDuration))
                    .ForMember(o => o.CourseLevel, ex => ex.MapFrom(o => o.CourseLevel))
                    .ReverseMap();

                cfg.CreateMap<CourseLevel, CourseLevelDTO>()
                    .ForMember(o => o.CourseLevelId, ex => ex.MapFrom(o => o.CourseLevelId))
                   // .ForMember(o => o.CourseLevelName, ex => ex.MapFrom(o => o.CourseLevelName))
                   .ReverseMap();

            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Author Module
        public static IMapper AuthorModule()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, AuthorCourseDTO>();

                cfg.CreateMap<Author, AuthorDTO>()
                    .ForMember(o => o.AuthorID, ex => ex.MapFrom(o => o.AuthorId))
                    .ForMember(o => o.AuthorName, ex => ex.MapFrom(o => o.AuthorFirstName + " " + (o.AuthorLastName)))
                    .ForMember(o => o.AuthorImage, ex => ex.MapFrom(o => o.AuthorImage))
                    .ForMember(o => o.Course, ex => ex.MapFrom(o => o.Course));
               
            });

           
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Courses by Category ID
        public static IMapper CourseByCategoryID()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CourseInfo, CourseInfoForCategories>()
                    .ForMember(o => o.CoursePrice, ex => ex.MapFrom(o => o.CoursePrice))
                    .ForMember(o => o.CourseDuration, ex => ex.MapFrom(o => o.CourseDuration));

                cfg.CreateMap<Course, CourseByCategoryDTO>()
                    .ForMember(o => o.CourseName, ex => ex.MapFrom(o => o.CourseName))
                    .ForMember(o => o.CourseDescription, ex => ex.MapFrom(o => o.CourseDescription))
                    .ForMember(o => o.CourseImage, ex => ex.MapFrom(o => o.CourseImage))
                    .ForMember(o => o.CourseAuthor, ex => ex.MapFrom(o => o.Author.AuthorFirstName + " " + (o.Author.AuthorLastName)));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        
        //Create Author
        public static IMapper CreateAuthorMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, PostAuthorDTO>()
                    .ForMember(o => o.AuthorFirstName, ex => ex.MapFrom(o => o.AuthorFirstName))
                    .ForMember(o => o.AuthorLastName, ex => ex.MapFrom(o => o.AuthorLastName))
                    .ForMember(o => o.AuthorEmail, ex => ex.MapFrom(o => o.AuthorEmail))
                    .ForMember(o => o.AuthorTelephone, ex => ex.MapFrom(o => o.AuthorTelephone))
                    .ForMember(o => o.AuthorImage, ex => ex.MapFrom(o => o.AuthorImage))
                    .ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            return mapper;

        }

        //Search Course
        public static IMapper SearchCourse()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Course, SearchCourseDTO>()
                    .ForMember(o => o.CourseId, ex => ex.MapFrom(o => o.CourseId))
                    .ForMember(o => o.CourseName, ex => ex.MapFrom(o => o.CourseName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }
        
        
        //Categories with array of courses
        public static IMapper CoursesByCategories()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Course, CoursesForCategory>()
                      .ForMember(o => o.CourseID, ex => ex.MapFrom(o => o.CourseId))
                      .ForMember(o => o.CourseName, ex => ex.MapFrom(o => o.CourseName))
                      .ForMember(o => o.CourseDescription, ex => ex.MapFrom(o => o.CourseDescription))
                      .ForMember(o => o.CourseAuthor, ex => ex.MapFrom(o => o.Author.AuthorFirstName + " " + (o.Author.AuthorLastName)))
                      .ForMember(o => o.SubCategoryName, ex => ex.MapFrom(o => o.SubCategory.SubCategoryName));

                cfg.CreateMap<Category, CategoryWithCourses>()
                    .ForMember(o => o.CategoryId, ex => ex.MapFrom(o => o.CategoryId))
                    .ForMember(o => o.CategoryName, ex => ex.MapFrom(o => o.CategoryName))
                    .ForMember(o => o.Courses, ex => ex.MapFrom(o => o.Course));

                cfg.CreateMap<CourseInfo, CourseInfoForAllCourses>()
                    .ForMember(o => o.CoursePrice, ex => ex.MapFrom(o => o.CoursePrice))
                    .ForMember(o => o.CourseDuration, ex => ex.MapFrom(o => o.CourseDuration))
                    .ForMember(o => o.CourseLevel, ex => ex.MapFrom(o => o.CourseLevel.CourseLevelName));

            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        // All Authors Mapper
        public static IMapper AllAuthors()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AllAuthorsDTO>()
                    .ForMember(o => o.AuthorId, ex => ex.MapFrom(o => o.AuthorId))
                    .ForMember(o => o.AuthorName, ex => ex.MapFrom(o => o.AuthorFirstName + " " + (o.AuthorLastName)));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        //Course Level Mapper
        public static IMapper CourseLevelMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CourseLevel, CourseLevelDTO>()
                    .ForMember(o => o.CourseLevelId, ex => ex.MapFrom(o => o.CourseLevelId));
                 //   .ForMember(o => o.CourseLevelName, ex => ex.MapFrom(o => o.CourseLevelName));
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

    }
}
