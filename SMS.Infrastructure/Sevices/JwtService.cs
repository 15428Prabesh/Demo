using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using SMS.Core.Entities;
using SMS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Infrastructure.Sevices
{
	public class JwtService : IJwtService
	{
		private readonly string _secret;
		private readonly string _issuer;
		private readonly string _audience;

		public JwtService(string secret, string issuer, string audience)
		{
			_secret = secret ?? throw new ArgumentNullException(nameof(secret));
			_issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
			_audience = audience ?? throw new ArgumentNullException(nameof(audience));
		}

		public string GenerateJwtToken(User user)
		{
			if (user == null || string.IsNullOrEmpty(user.Email))
			{
				throw new ArgumentException("Invalid user information.");
			}
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim("userId", user.Id.ToString())
			};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _issuer,
				audience: _audience,
				claims: claims,
				expires: DateTime.Now.AddHours(3),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
