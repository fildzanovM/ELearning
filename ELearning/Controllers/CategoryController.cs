using AutoMapper;
using ELearning.Data;
using ELearning.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELearning.Controllers
{

    [Route("api")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IELearningRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IELearningRepository repository, IMapper mapper, ILogger<CategoryController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <response code="200">Succesfully returns the catetories</response>
        /// <response code="400">If the item is null</response> 
        //Get all categories
        [HttpGet("categories")]
        public async Task<ActionResult<CategoryDTO[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllCategoriesAsync();
                IMapper mapper = ELearningProfile.CategoryMapper();

                return mapper.Map<CategoryDTO[]>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all categories: {ex}");
                return BadRequest("Failed to get all categories");
            }
        }
        /// <summary>
        /// Get SubCategories by Category Id.
        /// </summary>
        /// <param name="categoryId ">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returns the SubCate</response>
        /// <response code="400">If the item is null</response> 
        //Get all SubCategories
        [HttpGet("subcategories/{categoryId}")]
        public async Task<ActionResult<SubCategoryDTO[]>> Get(int categoryId)
        {
            try
            {
                var results = await _repository.GetSubCategoriesById(categoryId);
                IMapper mapper = ELearningProfile.SubCategoryMapper();

                return mapper.Map<SubCategoryDTO[]>(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all SubCategories: {ex}");
                return BadRequest("Failed to get all SubCategories");
            }         
        }

        /// <summary>
        /// Get single category with Category Id.
        /// </summary>
        /// <response code="200">Succesfully returns the Category</response>
        /// <response code="400">If the item is null</response> 
        //Get Category
        [HttpGet("category/{categoryID}")]
        public ActionResult<CategoryDTO>GetCategory(int categoryID)
        {
            try
            {
                var result = _repository.GetCategoryById(categoryID);
                IMapper mapper = ELearningProfile.CategoryMapper();

                return mapper.Map<CategoryDTO>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Category: {ex}");
                return BadRequest("Failed to get Category");
            }
        }

        /// <summary>
        /// Get single SubCategory by SybCategory ID.
        /// </summary>
        /// <param name="subCategoryID ">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returns the SubCategory</response>
        /// <response code="400">If the item is null</response> 
        //Get SubCategory
        [HttpGet("subcategory/{subCategoryID}")]
        public ActionResult<SubCategoryDTO> GetSubCategory(int subCategoryID)
        {
            try
            {
                var result = _repository.GetSubCategoryById(subCategoryID);
                IMapper mapper = ELearningProfile.SubCategoryMapper();

                return mapper.Map<SubCategoryDTO>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get SubCategory: {ex}");
                return BadRequest("Failed to get SubCategory");
            }
        }


        /// <summary>
        /// Get category with array of courses.
        /// </summary>
        /// <response code="200">Succesfully returns the catetories</response>
        /// <response code="400">If the item is null</response> 
        //Get Category with array of Courses
        [HttpGet("category/get")]
        public ActionResult<CategoryWithCourses[]> CategoryWithArrayOfCourses()
        {
            try
            {
                var result = _repository.CategoryWithArrayOfCourses();
                IMapper mapper = ELearningProfile.CoursesByCategories();

                return mapper.Map<CategoryWithCourses[]>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Category with the array of courses: {ex}");
                return BadRequest("Failed to get Category with the array of courses ");
            }
        }
    }
}
