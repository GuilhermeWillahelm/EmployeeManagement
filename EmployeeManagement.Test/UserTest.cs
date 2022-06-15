using EmployeeManagement.Dtos;
using EmployeeManagement.Services;
using EmployeeManagement.Repositories;
using NSubstitute;

namespace EmployeeManagement.Test
{
    public class UserTest
    {
        private readonly IUserService _targetTest;
        private readonly IUserRepository _repository = Substitute.For<IUserRepository>();

        public UserTest()
        {
            _targetTest = new UserService(_repository);
        }

        [Fact]
        public async Task GetUser_ReturnUser_WhenExists()
        {
            //Context
            int id = 1;
            var user = new UserDto() { Id = id };
            _repository.GetUser(id).Returns(user);

            //Action
            var result = await _targetTest.GetUser(id);

            //Verification
            Assert.NotNull(result);
            Assert.Equal(user, result);
        }

        [Theory]
        [MemberData(nameof(DatasForTest))]
        public async Task CreateUser_WhenDiferent(int id, string fullName, string nickName, string email, string pass)
        {
            //Context
            var user = new UserDto() { Id = id, FullName = fullName, UserName = nickName, Email = email, Password = pass };
            _repository.RegisterUser(user).Returns(user);

            //Action
            var resultTest = await _targetTest.RegisterUser(user);

            //Verification
            Assert.NotNull(resultTest);
            Assert.Equal(user, resultTest);
        }

        [Theory]
        [MemberData(nameof(DataForLogin))]
        public async Task LoginUserTest_WhenExist(int id, string userName, string pass)
        {
            //Context
            var user = new UserLoginDto() { Id = id, UserName = userName, Password = pass };
            _repository.LoginUser(user).Returns(user);

            //Action
            var resultTest = await _targetTest.LoginUser(user);

            //Verification
            Assert.NotNull(resultTest);
            Assert.Equal(user, resultTest);
        }

        public static IEnumerable<object[]> DatasForTest()
        {
            yield return new object[] { 1, "Guilherme Lima", "GuilhermeLm", "guilherme@gmail.com", "teste@159" };
            yield return new object[] { 2, "Gabriel Lima", "GabrielGl", "gabriel@gmail.com", "teste@259" };
            yield return new object[] { 3, "Rodrigo Baltar", "RodrigoBT", "rodrigobt@gmail.com", "teste@259" };
        }

        public static IEnumerable<object[]> DataForLogin()
        {
            yield return new object[] { 1, "GuilhermeLm", "teste@159" };
            yield return new object[] { 2, "GabrielGl", "teste@259" };
            yield return new object[] { 3, "RodrigoBT", "teste@259" };
        }
    }
}
