using EmployeeManagement.Dtos;
using EmployeeManagement.Services;
using EmployeeManagement.Repositories;
using NSubstitute;

namespace EmployeeManagement.Test
{
    public class OfficeTest
    {
        private readonly IOfficeService _targetTest;
        private readonly IOfficeRepository _repository =  Substitute.For<IOfficeRepository>();

        public OfficeTest()
        {
            _targetTest = new OfficeService(_repository);
        }

        [Fact] 
        public async Task GetOffice_WhenExists()
        {
            //Context 
            int id = 1;
            var office = new OfficeDto() { Id = id };
            _repository.GetOffice(id).Returns(office);

            //Action
            var resultTest = await _targetTest.GetOffice(id);

            //Verification
            Assert.NotNull(office);
            Assert.Equal(office, resultTest);
        }

        [Theory]
        [MemberData(nameof(DatasForTest))]
        public async Task CreateOffice_Test(int id, string officeName)
        {
            //Context
            var office = new OfficeDto() { Id = id, NameOffice = officeName};
            _repository.CreateOffice(office).Returns(office);

            //Action
            var resultTest = await _targetTest.CreateOffice(office);

            //Verification
            Assert.NotNull(office);
            Assert.Equal(office, resultTest);
        }

        [Theory]
        [MemberData(nameof(DatasForTest))]
        public async Task UpdateOfficeTest(int id, string officeName)
        {
            //Context
            var office = new OfficeDto() { NameOffice = officeName };
            _repository.UpdateOffice(id, office).Returns(office);

            //Action
            var resultTest = await _targetTest.UpdateOffice(id, office);

            //Verification
            Assert.NotNull(office);
            Assert.Equal(office, resultTest);
        }

        public static IEnumerable<object[]> DatasForTest()
        {
            yield return new object[] { 1, "Gerente" };
            yield return new object[] { 2, "Vendedor" };
            yield return new object[] { 3, "Administrador" };

        }
    }
}
