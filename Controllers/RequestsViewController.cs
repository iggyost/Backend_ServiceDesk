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
                var data = context.RequestsViews.Where(x => x.StatusId == 1).ToList();
                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("get/user/{userId}")]
        public ActionResult<IEnumerable<RequestsView>> GetUser(int userId)
        {
            try
            {
                var data = context.RequestsViews.Where(x => x.UserId == userId).ToList();
                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("send/{name}/{description}/{categoryId}/{userId}")]
        public ActionResult<IEnumerable<Request>> Send(string name, string description, int categoryId, int userId)
        {
            try
            {
                Request request = new Request()
                {
                    Name = name,
                    Description = description,
                    CategoryId = categoryId,
                    Date = DateTime.Now,
                    Time = DateTime.Now.TimeOfDay,
                    StatusId = 1,
                    UserId = userId
                };
                context.Requests.Add(request);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
