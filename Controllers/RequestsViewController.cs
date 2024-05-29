using Backend_ServiceDesk.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_ServiceDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsViewController : Controller
    {
        public static ServiceDeskDbContext context = new ServiceDeskDbContext();

        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<RequestsView>> Get()
        {
            try
            {
                var data = context.RequestsViews.Where(x => x.StatusId != 3).ToList();
                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
