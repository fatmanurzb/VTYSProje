using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace VTYSProje.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            using (Db db = new Db())
            {
                List<Admin> allAdmin = db.Admin.ToList();
                return PartialView(allAdmin);
            }
        }
    }
}
