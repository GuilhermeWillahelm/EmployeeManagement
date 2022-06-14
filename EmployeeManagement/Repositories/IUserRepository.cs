using EmployeeManagement.Dtos;

namespace EmployeeManagement.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetUsers();
        Task<UserDto> GetUser(int id);
        Task<UserDto> RegisterUser(UserDto user);
        Task<UserDto> UpdateUser(int id, UserDto user);
        Task<UserDto> DeleteUser(int id);
        Task<UserLoginDto> LognUser(UserLoginDto userLoginDto);
    }
}
