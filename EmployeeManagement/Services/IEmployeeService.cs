using EmployeeManagement.Dtos;

namespace EmployeeManagement.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployee(int id);
        Task<EmployeeDto> CreateEmploye(EmployeeDto employee);
        Task<EmployeeDto> UpdateEmploye(int id, EmployeeDto employee);
        Task<EmployeeDto> DeleteEmploye(int id);
    }
}
