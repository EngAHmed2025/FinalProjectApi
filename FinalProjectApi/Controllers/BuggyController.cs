using FinalProject.Repository.Data;
using FinalProjectApi.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
           _dbContext = dbContext;
        }

        [HttpGet("notFound")]

        public ActionResult GetNotFoundRequest() 
        {
            var product = _dbContext.Products.Find(100);
            if (product == null) 
            {
                return NotFound( new ApiResponse(404));
            }
            return Ok(product); 
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(100);
            var productDTo = product.ToString();

            return Ok(productDTo);
        }
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("unauthorized")]
        public ActionResult GetAuthorized() 
        { 
            return Unauthorized(new ApiResponse(401));        
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }

 
    }
}
