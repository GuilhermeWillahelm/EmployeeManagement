using EmployeeManagement.Dtos;
using EmployeeManagement.Services;
using EmployeeManagement.Repositories;
using NSubstitute;

namespace EmployeeManagement.Test
{
    public class EmployeeTest
    {
        private readonly IEmployeeService _targetTest;
        private readonly IEmployeeRepository _repository = Substitute.For<IEmployeeRepository>();

        public EmployeeTest() 
        {
            _targetTest = new EmployeeService(_repository);
        }

        [Fact]
        public async Task GetEmployee_ReturnEmployee_WhenExist()
        {
            //Context
            var employeeId = 1;
            var employeeDto = new EmployeeDto() { Id = employeeId };
            _repository.GetEmployee(employeeId).Returns(employeeDto);

            //Action
            var costumerUser = await _targetTest.GetEmployee(employeeId);

            //Verification
            Assert.NotNull(costumerUser);
            Assert.Equal(employeeId, costumerUser.Id);
        }

        [Theory]
        [MemberData(nameof(DatasForTest))]
        public async Task CreateEmployeeTest(int id, string name, DateTime date, int offId, float salary)
        {
            //Context
            var employeeDto = new EmployeeDto() { Id = id, FullName = name, CreatedDate = date, OfficeId = offId, Salary = salary };
            _repository.CreateEmploye(employeeDto).Returns(employeeDto);

            //Action
            var costumerEmployee = await _targetTest.CreateEmploye(employeeDto);

            //Verification
            Assert.NotNull(costumerEmployee);
            Assert.Equal(employeeDto, costumerEmployee);
        }

        [Theory]
        [MemberData(nameof(DatasForTest))]
        public async Task UpdateEmployeeTest(int id, string name, DateTime date, int offId, float salary)
        {
            //Context
            var employeeDto = new EmployeeDto() { FullName = name, CreatedDate = date, OfficeId = offId, Salary = salary };
            _repository.UpdateEmploye(id, employeeDto).Returns(employeeDto);

            //Action
            var costumerEmployee = await _targetTest.UpdateEmploye(id, employeeDto);

            //Verification
            Assert.NotNull(costumerEmployee);
            Assert.Equal(employeeDto, costumerEmployee);
        }
        [Fact]
        public async Task DeleteEmployeeTest()
        {
            int id = 1;
            //Context
            var employeeDto = new EmployeeDto() { Id = id};
            _repository.DeleteEmploye(employeeDto.Id).Returns(employeeDto);

            //Action
            var costumerEmployee = await _targetTest.DeleteEmploye(id);

            //Verification
            Assert.NotNull(costumerEmployee);
        }

        public static IEnumerable<object[]> DatasForTest()
        {
            yield return new object[] { 1, "Guilherme", DateTime.Now, 1, 1.555f };
            yield return new object[] { 2, "Raquel", DateTime.Now, 3, 2.555f };
            yield return new object[] { 3, "Gabriel", DateTime.Now, 2, 3.555f };

        }
    }
}
