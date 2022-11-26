using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kompiuterija.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Kompiuterija.Entities;

namespace Kompiuterija.Repository
{
	public class JWTManagerRepository : IJWTManagerRepository
	{

		private readonly IConfiguration iconfiguration;
		public JWTManagerRepository(IConfiguration iconfiguration)
		{
			this.iconfiguration = iconfiguration;
		}
		public Tokens Authenticate(Users users)
		{
			Kompiuterija_dbContext context = new Kompiuterija_dbContext();
			User user = context.User.Find(users.Email);
			if (user == null || !BCrypt.Net.BCrypt.Verify(users.Password, user.Password))
			{
				return null;
			}

			// Else we generate JSON Web Token
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{

				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim("email", users.Email),
					new Claim("role", user.Role)
				}),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };

		}
	}
}
