using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using JwtAuthDemo.DTOs;


namespace JwtAuthDemo.Repository
{
    public interface IJwt
    {
        string GenerateToken(int userId, string userName, string userEmail, string pass, string userRole);
        JwtSecurityToken VertifyToken(string token);
        string GetTokenFromHeader(string authorizationHeader);
        Task<UserClaims> ReadTokenData(string token);
    }
}