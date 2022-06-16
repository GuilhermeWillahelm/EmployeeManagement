using EmployeeManagement.Data;
using EmployeeManagement.Dtos;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using EmployeeManagement.Identity;
using AutoMapper;
using System.Text;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseDBContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserRepository(DataBaseDBContext context, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IConfiguration confg)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = confg;
        }

        public async Task<UserDto> RegisterUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password);
            var userToReturn = _mapper.Map<UserDto>(user);
            try
            {
                if (result.Succeeded)
                {
                    return userToReturn;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return userToReturn;
        }

        public async Task<UserDto> DeleteUser(int id)
        {
            var userItem = await _context.Users.FindAsync(id);

            if (userItem == null)
            {
                return null;
            }

            _context.Users.Remove(userItem);
            await _context.SaveChangesAsync();

            return UserToDto(userItem);
        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return UserToDto(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return await _context.Users.Select(u => UserToDto(u)).ToListAsync();
        }

        public async Task<UserLoginDto> LoginUser(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.UserName);

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

            var userToReturn = userLoginDto;

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == userLoginDto.UserName.ToUpper());

                userToReturn = _mapper.Map<UserLoginDto>(appUser);
                userToReturn.Token = GenerateJWToken(appUser).Result;
            }

            return userToReturn;
        }

        public async Task<UserDto> UpdateUser(int id, UserDto user)
        {
            var userItem = await _context.Users.FindAsync(id);

            if (userItem == null)
            {
                return null;
            }

            userItem.FullName = user.FullName;
            userItem.UserName = user.UserName;
            userItem.Email = user.Email;
            userItem.PasswordHash = user.Password;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UserDto();
            }

            return new UserDto();
        }

        private async Task<string> GenerateJWToken(User user)
        {
            var claims = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(5),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static UserDto UserToDto(User user) =>
            new UserDto
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Password = user.PasswordHash,    
            };
    }
}
