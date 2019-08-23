using AutoMapper;
using ELearning.Data;
using ELearning.Models;
using ELearning.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ELearning.Controllers
{
    [Route("api")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly IELearningRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseController> _logger;
       // private readonly LinkGenerator _linkGenerator;

        public CourseController(IELearningRepository repository, IMapper mapper, ILogger<CourseController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get courses by SubCategory Id.
        /// </summary>
        /// <param name="subCategoryID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returns the courses</response>
        /// <response code="400">If the item is null</response> 
        //Get Courses by SubCategories
        [HttpGet("courses/{subCategoryID}")]
        public async Task<ActionResult<CourseDTO[]>> Get(int subCategoryID)
        {
            try
            {
                var result = await _repository.GetCoursesBySubCategories(subCategoryID);
                IMapper mapper = ELearningProfile.CourseMapper();
                return mapper.Map<CourseDTO[]>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all courses{ex}");
                return BadRequest("Failed to get all courses");

            }
        }

        /// <summary>
        /// Get all courses.
        /// </summary>
        /// <response code="200">Succesfully returns the catetories</response>
        /// <response code="400">If the item is null</response> 
        // Get All Courses
        [HttpGet("courses")]
        public async Task<ActionResult<AllCourses[]>> Get()
        {
            try
            {
                var result = await _repository.GetAllCourses();
                IMapper mapper = ELearningProfile.AllCoursesMapper();
                return mapper.Map<AllCourses[]>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all courses{ex}");
                return BadRequest("Failed to get all courses");
            }
        }

        /// <summary>
        /// Get CourseInfo by Course Id.
        /// </summary>
        /// <param name="courseID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returns the CourseInfo</response>
        /// <response code="400">If the item is null</response> 
        //Get Course Info by Course ID
        [HttpGet("courseinfo/{courseID}")]
        public async Task<ActionResult<CourseInfoDTO[]>> GetCourseInfo(int courseID)
        {
            try
            {
                var result = await _repository.GetCourseInfoByCourseID(courseID);
                IMapper mapper = ELearningProfile.CourseInfo();
                return mapper.Map<CourseInfoDTO[]>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get course info{ex}");
                return BadRequest("Failed to get course info");
            }
        }

        /// <summary>
        /// Get CourseModule by Course Id.
        /// </summary>
        /// <param name="courseID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returns the CourseModules</response>
        /// <response code="400">If the item is null</response> 
        //Get Course Module by Course ID
        [HttpGet("coursemodule/{courseID}")]
        public async Task<ActionResult<CourseModuleDTO[]>> GetCourseModule(int courseID)
        {
            try
            {
                var result = await _repository.GetCourseModuleByCourseID(courseID);
                IMapper mapper = ELearningProfile.CourseModule();
                return mapper.Map<CourseModuleDTO[]>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get course modules{ex}");
                return BadRequest("Failed to get course modules");
            }
        }

        /// <summary>
        /// Get courses by Category Id.
        /// </summary>
        /// <param name="categoryID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returns the courses</response>
        /// <response code="400">If the item is null</response> 
        //Get Course By CategoryID
        [HttpGet("course/{categoryID}")]
        public ActionResult<CourseByCategoryDTO[]> GetCourseByCategory(int categoryID)
        {
            try
            {
                var result = _repository.GetCourseByCategoryID(categoryID);
                IMapper mapper = ELearningProfile.CourseByCategoryID();
                return mapper.Map<CourseByCategoryDTO[]>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get courses by categories{ex}");
                return BadRequest("Failed to get courses by categories");
            }

        }

        /// <summary>
        /// Get single course by Course Id.
        /// </summary>
        /// <param name="courseId">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returns the catetories</response>
        /// <response code="400">If the item is null</response> 
        //Get Course by ID
        [HttpGet("course/get")]
        public ActionResult<CourseDTO> GetCourseById(int courseId)
        {
            try
            {
                var result = _repository.GetCourseById(courseId);
                IMapper mapper = ELearningProfile.CourseMapper();

                return mapper.Map<CourseDTO>(result);
                    

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the course:{ex}");
                return BadRequest("Failed to get the course");
            }
        }

        /// <summary>
        /// Get single Course level by CourseLevel Id.
        /// </summary>
        /// <param name="courseLevelId">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returns course level</response>
        /// <response code="400">If the item is null</response> 
        //Get CourseLevel by ID
        [HttpGet("courselevel/get")]
        public ActionResult<CourseLevelDTO> GetCourseLevelById(int courseLevelId)
        {
            try
            {
                var result = _repository.GetCourseLevelById(courseLevelId);
                IMapper mapper = ELearningProfile.CourseLevelMapper();

                return mapper.Map<CourseLevelDTO>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get course levels:{ex}");
                return BadRequest("Failed to get course levels");
            }
        }

        /// <summary>
        /// Post course and array of modules.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="201">Succesfully created course</response>
        /// <response code="400">If the item is null</response> 
        //Post Course with modules
        [HttpPost("save/courses")]
        public ActionResult<PostCourseDTO> PostCourse([FromBody] PostCourseDTO model)
        {
            try
            {              
                var category =  _repository.GetCategoryById(model.CategoryID);
                if (category == null) return BadRequest("Category could not be find");
      
                var subCategory = _repository.GetSubCategoryById(model.SubCategoryID);
                if (subCategory == null) return BadRequest("SubCategory could not be find");
        
                var author =  _repository.GetAuthorById(model.AuthorID);
                if (author == null) return BadRequest("Author could not be find");
     
                foreach(var courseInfo in model.CourseInfo)
                {
                    var courseLevel = _repository.GetCourseLevelById(courseInfo.CourseLevel.CourseLevelId);
                    if (courseLevel == null) return BadRequest("Course level could not be find");

                }

                IMapper mapper = ELearningProfile.PostCourse();
                var course = mapper.Map<Course>(model);

                _repository.AddCourse(course);
                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created course" });

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create new course {ex}");
            }

            return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created course" });
        }

        /// <summary>
        /// Delete course.
        /// </summary>
        /// <param name="courseID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully deleted course</response>
        /// <response code="400">If the item is null</response>  
        //  Delete Course
        [HttpDelete("delete/{courseID}")]
        public async Task<ActionResult<CourseDTO>> DeleteCourse(int courseID)
        {
            try
            {
                var oldCourse = await _repository.GetCourseAsync(courseID);
                if (oldCourse == null) return NotFound($"Could not find the course with id: {courseID}");

                _repository.DeleteCourse(oldCourse);
                    return Ok();
              
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to delete the course: {ex}");
                return BadRequest();
            }
        }

        /// <summary>
        /// Search course by CourseName.
        /// </summary>
        /// <param name="courseName">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returns the course</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("search")]
        public ActionResult<SearchCourseDTO[]> SearchCourse(string courseName)
        {
            try
            {
                var searchCourse = _repository.SearchCourse(courseName);
                if (!searchCourse.Any()) return NotFound();

                IMapper mapper = ELearningProfile.SearchCourse();
                return mapper.Map<SearchCourseDTO[]>(searchCourse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to find the course with the given name: {ex}");
                return BadRequest();
            }
        }
    }
}
