using Microsoft.AspNetCore.Mvc;
using PLC_Control.Context;
using PLC_Control.Entitity;

namespace PLC_Control.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RequestController : Controller
	{
		private readonly APIDbContext dbContext;

		public RequestController(APIDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		
		[HttpGet("getValue")]
		public IActionResult UpdateValue()
		{
			try
			{
				var request = dbContext.Requests.FirstOrDefault(r => r.Id==1);
				var requests = dbContext.Requests.ToList();

				if (request.Value == false)
                {
					request.Value = true;
					dbContext.SaveChanges();
					return Ok(requests);
                }
				else if(request.Value == true)
				{
					request.Value = false;
					dbContext.SaveChanges();
					return Ok(requests);
				}
				else
				{
					return NotFound("Request not found with Id: 1");
				}
            }
			catch (Exception ex)
			{
				return BadRequest("An error occurred: " + ex.Message);
			}
		}
		
	}
}
