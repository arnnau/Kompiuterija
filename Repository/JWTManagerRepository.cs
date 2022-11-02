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
***REMOVED***
	public class JWTManagerRepository : IJWTManagerRepository
	***REMOVED***

		private readonly IConfiguration iconfiguration;
		public JWTManagerRepository(IConfiguration iconfiguration)
		***REMOVED***
			this.iconfiguration = iconfiguration;
		***REMOVED***
		public Tokens Authenticate(Users users)
		***REMOVED***
			Kompiuterija_dbContext context = new Kompiuterija_dbContext();
			User user = context.User.Find(users.Email);
			if (user == null || !BCrypt.Net.BCrypt.Verify(users.Password, user.Password))
			***REMOVED***
				return null;
			***REMOVED***

			// Else we generate JSON Web Token
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			***REMOVED***

				Subject = new ClaimsIdentity(new Claim[]
				***REMOVED***
					new Claim("email", users.Email),
					new Claim("role", user.Role)
				***REMOVED***),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			***REMOVED***;
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens ***REMOVED*** Token = tokenHandler.WriteToken(token) ***REMOVED***;

		***REMOVED***
	***REMOVED***
***REMOVED***
