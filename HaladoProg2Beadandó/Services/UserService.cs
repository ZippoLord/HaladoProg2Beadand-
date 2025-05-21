using HaladoProg2Beadandó.Controllers;
using HaladoProg2Beadandó.Data;
using AutoMapper;
using HaladoProg2Beadandó.Models;
using HaladoProg2Beadandó.Models.DTOs.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace HaladoProg2Beadandó.Services
{

    public interface IUserService
    {
        Task CreateUserAsync(UserRegisterDTO userDTO);
        Task<UserDTO> GetUserByAsync(int userId);
        Task<UserDTO> EditUserByAsync(int userId, EditUserDTO userDTO);

        Task DeleteUserByAsync(int userId);
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(UserRegisterDTO userDTO)
        {

                var existedEmail = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email);
                if(existedEmail != null)
                {
                 throw new InvalidOperationException("Ez az email cím már foglalt");
                }
                var user = _mapper.Map<User>(userDTO);
                user.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
        }

        public async Task<UserDTO> GetUserByAsync(int userId)
        {


            var existedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if(existedUser == null)
            {
                throw new InvalidOperationException("Nincs ilyen id-jű user");
            }

            var user = _mapper.Map<UserDTO>(existedUser);
            return user;
        }

        public async Task<UserDTO> EditUserByAsync(int userId, EditUserDTO userDTO)
        {
            var existedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (existedUser == null)
            {
                throw new InvalidOperationException("Nincs ilyen id-jű user");
            }
            else
            {
                existedUser.Email = userDTO.Email;
                existedUser.Name = userDTO.Name;
                if (!string.IsNullOrWhiteSpace(userDTO.Password))
                {
                    existedUser.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
                }

                _context.Users.Update(existedUser);
                await _context.SaveChangesAsync();
                return _mapper.Map<UserDTO>(existedUser);
            }
        }

            public async Task DeleteUserByAsync(int userId)
            {
                var existedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (existedUser == null)
                {
                    throw new InvalidOperationException("Nincs ilyen id-jű user");
                }
                _context.Users.Remove(existedUser);
                await _context.SaveChangesAsync();
            }
    }
}
