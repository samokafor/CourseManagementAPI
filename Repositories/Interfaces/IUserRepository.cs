﻿using CourseManagementAPI.Database.Models;
using CourseManagementAPI.DTOs;

namespace CourseManagementAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddRegularUserAsync(UserDto userDto);
        Task<User> AddAdminUserAsync(UserDto userDto);
        public User GetUserByEmail(string email);
        public UserLogin GetUserLogin(string email);
        Task<User> GetUserByUserName(string userName);
        Task<IEnumerable<User>> GetAllUsers();
        Task<IEnumerable<User>> SearchUsers(string searchTerm);
        Task<User> GetUserById(int id);
        Task<User> UpdateUserRole(int Id, UpdateUserRoleDto roleDto);
        Task DeleteUser(int Id);
        User GetCurentUser();
        bool CheckIfLoggedIn();
    }
}
