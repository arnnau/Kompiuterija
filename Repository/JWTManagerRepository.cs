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

namespace Kompiuterija.Repository
***REMOVED***
	public class JWTManagerRepository : IJWTManagerRepository
	***REMOVED***
		Dictionary<string, string> UsersRecords = new Dictionary<string, string>
	***REMOVED***
		***REMOVED*** "user1","password1"***REMOVED***,
		***REMOVED*** "user2","password2"***REMOVED***,
		***REMOVED*** "user3","password3"***REMOVED***,
	***REMOVED***;

		private readonly IConfiguration iconfiguration;
		public JWTManagerRepository(IConfiguration iconfiguration)
		***REMOVED***
			this.iconfiguration = iconfiguration;
		***REMOVED***
		public Tokens Authenticate(Users users)
		***REMOVED***
			if (!UsersRecords.Any(x => x.Key == users.Name && x.Value == users.Password))
			***REMOVED***
				return null;
			***REMOVED***

			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			***REMOVED***
				Subject = new ClaimsIdentity(new Claim[]
			  ***REMOVED***
			 new Claim(ClaimTypes.Name, users.Name),
			 new Claim(ClaimTypes.Role, users.Role)
			  ***REMOVED***),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			***REMOVED***;
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens ***REMOVED*** Token = tokenHandler.WriteToken(token) ***REMOVED***;

		***REMOVED***
	***REMOVED***
***REMOVED***
