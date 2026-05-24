using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DormitoryManagement.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DormitoryManagement.Application.Services.Implements
{
    /// <summary>
    /// Lớp triển khai dịch vụ sinh mã JWT Token xác thực (JwtTokenGenerator).
    /// </summary>
    public class JwtTokenGenerator(IConfiguration config) : IJwtTokenGenerator
    {
        private readonly IConfiguration _config = config;

        /// <summary>
        /// Sinh mã JWT Token chứa thông tin Id, Username, và Vai trò (Role) của người dùng.
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <param name="userName">Tên tài khoản</param>
        /// <param name="role">Tên vai trò phân quyền</param>
        /// <returns>Chuỗi JWT Token đã ký số</returns>
        public string GenerateToken(Guid userId, string userName, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId", userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_config["JwtSettings:ExpiryMinutes"]!)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
