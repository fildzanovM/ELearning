using AutoMapper;
using ELearning.Models;
using ELearning.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ELearning.Data
{
    [Route("api")]
    [ApiController]
    public class PurchaseController : Controller
    {
        private readonly IELearningRepository _repository;
        private readonly ILogger<PurchaseController> _logger;
        private readonly IMapper _mapper;

        public PurchaseController(IELearningRepository repository, ILogger<PurchaseController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Create purchase.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="201">Succesfully created purchase</response>
        /// <response code="400">If the item is null</response> 
        //Post Purchase
        [HttpPost("purchase/save")]
        public ActionResult<PurchasesDTO> PostPurchase([FromBody] PurchasesDTO model)
        {
            try
            {
                var course = _repository.GetCourseById(model.CourseId);
                if (course == null) return BadRequest("Course could not be find");

                IMapper mapper = ELearningProfile.CoursePourchaseMapper();
                var purchase = mapper.Map<Purchases>(model);

                _repository.AddPurchase(purchase);
                return new ObjectResult(new { message = "success", statusCode = HttpStatusCode.OK, response = "Created course" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to add new Purchase: {ex}");
                return BadRequest("Failed to add new purchase");
            }
        }

        /// <summary>
        /// Get all purchases.
        /// </summary>
        /// <response code="200">Succesfully created course</response>
        /// <response code="400">If the item is null</response> 
        //Get all Purchases
        [HttpGet("purchases")]
        public ActionResult<GetAllPurchases[]> GetAllPurchases()
        {
            try
            {
                var purchases = _repository.GetAllPurchases();
                IMapper mapper = ELearningProfile.AllPurchasesMapper();

                return mapper.Map<GetAllPurchases[]>(purchases);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all Purchases: {ex}");
                return BadRequest("Failed to get all Purchases");
            }
        }

    }
}
