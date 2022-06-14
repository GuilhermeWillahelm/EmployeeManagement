using EmployeeManagement.Dtos;

namespace EmployeeManagement.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUser(int id);
        Task<UserDto> RegisterUser(UserDto user);
        Task<UserDto> UpdateUser(int id, UserDto user);
        Task<UserDto> DeleteUser(int id);
        Task<UserLoginDto> LoginUser(UserLoginDto userLoginDto);
    }
}
