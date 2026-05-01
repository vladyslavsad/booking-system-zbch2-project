using bookingSystemZBC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookingSystemZBC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolymorfismController(IPolymorfismService polymorfismService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<string>>> PolymorfismGetActivityDisc() 
        {
            var activities = await polymorfismService.GetAllActivities();
            var result = new List<string>();
            foreach (var activity in activities) 
            {
                result.Add("Description : " + activity.GetDescription() + " Brutto : " + activity.CalculateBrutto());
            }
            return Ok(result);
        }
    }
}
