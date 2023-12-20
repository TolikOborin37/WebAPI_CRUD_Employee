using Design_InternetApps.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;

namespace Design_InternetApps.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;
        
        public EmployeeController(EmployeeContext context)
        {
            
            _context = context;
            if (_context.Employees.Count() == 0)
            {
                _context.Employees.AddRange(new Employee
                {
                    Id = 1,
                    FirstName = "Анатолий",
                    LastName = "Оборин",
                    PhoneNumber = "1234567890",
                }, new Employee
                {
                    Id = 2,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    PhoneNumber = "1234567890",
                }, new Employee
                {
                    Id = 3,
                    FirstName = "Федор",
                    LastName = "Федоров",
                    PhoneNumber = "1234567890",
                });

                _context.SaveChanges();
            }
        }

        //метот Get для извлечения всех сотрудников из БД
        [HttpGet]
        public ActionResult<List<Employee>> GetEmployeesItems()
        {
            return _context.Employees.ToList();
        }

        // метод Get для извлечения сотрудника по Id
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeItem(int id)
        {
            var empItem = _context.Employees.FirstOrDefault(emp => emp.Id == id);
            if (empItem == null)
            {
                return NotFound();
            }
            return empItem;
        }

        //метод Post для создания новой записи
        [HttpPost]
        public ActionResult<Employee> CreateEmployeeItem(Employee emp)
        {
            
            _context.Employees.Add(emp);
            _context.SaveChanges();
            return NoContent();
        }

        //метод Delete для удаление записи по id
        [HttpDelete("{id}")]
        public ActionResult<Employee> DeleteEmployeeItem(int id)
        {
            var empItem = _context.Employees.FirstOrDefault(emp => emp.Id == id);

            if (empItem == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(empItem);
            _context.SaveChanges();
            return NoContent();
        }

        //метод Put для обновления существующей записи по id
        [HttpPut("{id}")]
        public ActionResult<Employee> UpdateEmployeeItem(int id, Employee emp)
        {
            if (id != emp.Id)
            {
                return BadRequest();
            }
            var empItem = _context.Employees.FirstOrDefault(emp => emp.Id == id);

            empItem.FirstName = emp.FirstName;
            empItem.LastName = emp.LastName;
            empItem.PhoneNumber = emp.PhoneNumber;

            _context.SaveChanges();
            return NoContent();
        }
    }
}
