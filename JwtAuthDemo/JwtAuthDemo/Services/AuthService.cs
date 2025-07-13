using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JwtAuthDemo.Data;
using JwtAuthDemo.DTOs;
using JwtAuthDemo.Models;
using JwtAuthDemo.Repository;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthDemo.Services
{
    public class AuthService : IAuth
    {
        private readonly ApplicationDbContext _db;
        private readonly IJwt _jwt;
        private readonly IConfiguration _config;

        public AuthService(ApplicationDbContext _db, IJwt _jwt, IConfiguration config)
        {
            this._db = _db;
            this._jwt = _jwt;
            _config = config;
        }

        public async Task<AuthResponse> GetUserData(string email, string pass)
        {

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass))
            {
                return new AuthResponse("Email and Password are required!");
            }

            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.userEmail == email && u.userPass == pass);

            if (user == null)
            {
                return new AuthResponse("Invalid email or password!");
            }

            return new AuthResponse("Login successful!", user);
        }


        public async Task<AuthResponse> Authentication(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return new AuthResponse("Email and Password are required!");
            }

            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.userEmail == email && u.userPass == password);

            if (user == null)
            {
                return new AuthResponse("Invalid email or password!");
            }

            var data = new AuthUserDTO
            {
                userId = user.userId,
                userName = user.userName,
                userEmail = user.userEmail,
                Pass = user.userPass,
                userRole = user.userRole
            };

            return new AuthResponse("Login successful!", data);
        }

        public List<User> GetUsers()
        {
            var userData = _db.Users.ToList();
            return userData;
        }

    }
}