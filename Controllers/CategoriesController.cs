using Backend_ServiceDesk.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_ServiceDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesViewController : Controller
    {
        public static ServiceDeskDbContext context = new ServiceDeskDbContext();

        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                var data = context.Categories.ToList();
                return data;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
