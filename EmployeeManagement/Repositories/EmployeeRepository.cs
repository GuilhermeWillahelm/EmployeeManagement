using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataBaseDBContext _context;
        private readonly ILogger _logger;

        public EmployeeRepository(DataBaseDBContext context)
        {
            _context = context;
        }

        public async Task<EmployeeDto> CreateEmploye(EmployeeDto employee)
        {
            var offId = _context.Offices.Find(employee.OfficeId);
            var employeeItem = new Employee
            {
                FullName = employee.FullName,
                CreatedDate = employee.CreatedDate,
                Salary = employee.Salary,
                OfficeId = offId.Id
            };

            

            _context.Employees.Add(employeeItem);
            await _context.SaveChangesAsync();

            return EmployeeToDto(employeeItem);
        }

        public async Task<EmployeeDto> DeleteEmploye(int id)
        {
            var employeeItem = await _context.Employees.FindAsync(id);

            if (employeeItem == null)
            {
                return null;
            }

            _context.Employees.Remove(employeeItem);
            await _context.SaveChangesAsync();

            return EmployeeToDto(employeeItem);
        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            var employee = await _context.Employees.Include(o => o.Office).Where(e => e.Id == id).FirstOrDefaultAsync();

            return EmployeeToDto(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            return await _context.Employees.Include(o => o.Office).Select(c => EmployeeToDto(c)).ToListAsync();
        }

        public async Task<EmployeeDto> UpdateEmploye(int id, EmployeeDto employee)
        {
            if(id != employee.Id)
            {
                return null;
            }

            var employeeItem = await _context.Employees.FindAsync(id);

            if (employeeItem == null)
            {
                return null;
            }

            employeeItem.Id = employee.Id;
            employeeItem.FullName = employee.FullName;
            employeeItem.Salary = employee.Salary;
            employeeItem.OfficeId = employee.OfficeId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new EmployeeDto();   
            }

            return new EmployeeDto();

        }

        private static EmployeeDto EmployeeToDto(Employee employee) =>
                new EmployeeDto
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    CreatedDate = employee.CreatedDate,
                    Salary = employee.Salary,
                    OfficeId = employee.OfficeId,
                    OfficeDto = new OfficeDto
                    {
                        Id = employee.Office.Id,
                        NameOffice = employee.Office.NameOffice
                    }
                    
                }; 
    }
}
