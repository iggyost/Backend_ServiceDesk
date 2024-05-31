using Backend_ServiceDesk.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_ServiceDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsViewController : Controller
    {
        public static ServiceDeskDbContext context = new ServiceDeskDbContext();

        [HttpGet]
        [Route("get")]
        public ActionResult<IEnumerable<ReportsView>> Get()
        {
            try
            {
                var data = context.ReportsViews.Where(x => x.StatusId == 3).ToList();
                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }

        [HttpGet]
        [Route("get/count/{adminId}")]
        public ActionResult<IEnumerable<int>> GetCount(int adminId)
        {
            try
            {
                var countRequests = context.AdminsRequests.Where(x => x.AdminId == adminId && x.IsReady == false).Count();
                return Ok(countRequests);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("set/work/{adminId}/{requestId}")]
        public ActionResult<IEnumerable<int>> SetInWork(int adminId, int requestId)
        {
            try
            {
                AdminsRequest adminsRequest = new AdminsRequest()
                {
                    AdminId = adminId,
                    RequestId = requestId,
                    AcceptedDate = DateTime.Now,
                    AcceptedTime = DateTime.Now.TimeOfDay,
                    LastChangeDate = DateTime.Now,
                    LastChangeTime = DateTime.Now.TimeOfDay,
                    IsReady = false,
                };
                context.AdminsRequests.Add(adminsRequest);
                context.SaveChanges();
                var currentRequest = context.Requests.Where(x => x.RequestId == requestId).FirstOrDefault();
                currentRequest.StatusId = 2;
                context.Requests.Attach(currentRequest);
                context.Entry(currentRequest).Property(x => x.StatusId).IsModified = true;
                context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("get/admin/{adminId}")]
        public ActionResult<IEnumerable<ReportsView>> GetAdminRequests(int adminId)
        {
            try
            {
                var data = context.ReportsViews.Where(x => x.AdminId == adminId && x.StatusId == 2).ToList();
                return Ok(data);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }

    }
}