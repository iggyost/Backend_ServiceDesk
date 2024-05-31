using Backend_ServiceDesk.ApplicationData;
using Microsoft.AspNetCore.Mvc;

namespace Backend_ServiceDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        public static ServiceDeskDbContext context = new ServiceDeskDbContext();

        [HttpGet]
        [Route("login/{email}/{password}")]
        public ActionResult<IEnumerable<User>> Login(string email, string password)
        {
            try
            {
                var user = context.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {

                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
        [HttpGet]
        [Route("reg/{email}/{password}")]
        public ActionResult<IEnumerable<User>> RegUser(string email, string password)
        {
            try
            {
                var checkAvail = context.Users.Where(x => x.Email == email).FirstOrDefault();
                if (checkAvail == null)
                {
                    User user = new User()
                    {
                        Password = password,
                        Email = email,
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                    return Ok(user);
                }
                else
                {
                    return BadRequest("Пользователь с таким E-mail уже есть");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ошибка сервера");
            }
        }
    }
}
