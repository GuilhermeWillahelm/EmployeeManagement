using EmployeeManagement.Dtos;
using EmployeeManagement.Repositories;

namespace EmployeeManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public async Task<UserDto> GetUser(int id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<UserLoginDto> LoginUser(UserLoginDto userLoginDto)
        {
            return await _userRepository.LoginUser(userLoginDto);
        }

        public async Task<UserDto> RegisterUser(UserDto user)
        {
            return await _userRepository.RegisterUser(user);
        }

        public async Task<UserDto> UpdateUser(int id, UserDto user)
        {
            return await _userRepository.UpdateUser(id, user);
        }
    }
}
