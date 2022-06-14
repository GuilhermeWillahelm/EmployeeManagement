using EmployeeManagement.Repositories;
using EmployeeManagement.Dtos;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) 
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> CreateEmploye(EmployeeDto employee)
        {
            return await _employeeRepository.CreateEmploye(employee);
        }

        public async Task<EmployeeDto> DeleteEmploye(int id)
        {
            return await _employeeRepository.DeleteEmploye(id);
        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            return await _employeeRepository.GetEmployee(id);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            return await _employeeRepository.GetEmployees();
        }

        public async Task<EmployeeDto> UpdateEmploye(int id, EmployeeDto employee)
        {
            return await _employeeRepository.UpdateEmploye(id, employee);
        }
    }
}
