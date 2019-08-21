using AutoMapper;
using ELearning.Data;
using ELearning.Models;
using ELearning.ViewModel;
using Microsoft.AspNetCore.Mvc;
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
    public class AuthorController : Controller
    {
        private readonly IELearningRepository _repository;
        private readonly ILogger<AuthorController> _logger;
        private readonly IMapper _mapper;

        public AuthorController(IELearningRepository repository, ILogger<AuthorController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get single Author by AuthorId.
        /// </summary>
        /// <param name="authorID">Data to create the houshold from.</param>
        /// <response code="200">Succesfully returns the author</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("author/{authorID}")]
        public ActionResult<AuthorDTO> GetAuthor(int authorID)
        {
            try
            {
                var result = _repository.GetAuthorById(authorID);
                IMapper mapper = ELearningProfile.AuthorModule();

                return mapper.Map<AuthorDTO>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get the Author: {ex}");
                return BadRequest("Failed to get the Author");
               
            }
        }

        /// <summary>
        /// Get all Authors.
        /// </summary>
        /// <response code="200">Succesfully returns all authors</response>
        /// <response code="400">If the item is null</response> 
        [HttpGet("authors")]
        public ActionResult<AllAuthorsDTO[]> GetAllAuthors()
        {
            try
            {
                var authors = _repository.GetAllAuthors();
                IMapper mapper = ELearningProfile.AllAuthors();

                return mapper.Map<AllAuthorsDTO[]>(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Authors: {ex}");
                return BadRequest("Failed to get all Authors");
            }
        }

        /// <summary>
        /// Post Author.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully created Author</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("author/save")]
        public ActionResult<PostAuthorDTO> PostAuthor([FromBody] PostAuthorDTO model)
        {
            try
            {
                IMapper mapper = ELearningProfile.CreateAuthorMapper();
                var author = mapper.Map<Author>(model);

                _repository.AddAuthor(author);

                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created course" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add new Author:{ex}");
                return BadRequest("Failed to add new Author");
            }
        }
           
    }
}
