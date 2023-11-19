using Microsoft.AspNetCore.Mvc;

namespace EKO.ToDoApp.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class StatusController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Json(new
        {
            Status = "OK",
            Message = "Hello from EKO.ToDoApp.Api!"
        });
    }
}
