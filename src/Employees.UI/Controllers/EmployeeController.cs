using Employees.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Employees.UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _context;

        public EmployeeController(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var employees = await _context.Employees.ToListAsync();

            return View(employees);
        }
    }
}
