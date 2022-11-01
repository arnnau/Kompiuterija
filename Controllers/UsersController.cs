using Kompiuterija.Models;
using Kompiuterija.Repository;
using Kompiuterija.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuterija.Controllers
***REMOVED***
	[Route("")]
	[ApiController]
	public class UsersController : ControllerBase
	***REMOVED***
		private readonly IJWTManagerRepository _jWTManager;
		private kompiuterijaContext DBcontext;

		public UsersController(IJWTManagerRepository jWTManager, kompiuterijaContext DBcontext)
		***REMOVED***
			this._jWTManager = jWTManager;
			this.DBcontext = DBcontext;
		***REMOVED***

		[HttpPost]
		[Route("login")]
		public IActionResult Authenticate(Users usersdata)
		***REMOVED***
			var token = _jWTManager.Authenticate(usersdata);

			if (token == null)
			***REMOVED***
				return Unauthorized();
			***REMOVED***

			return Ok(token);
		***REMOVED***
		[HttpPost]
		[Route("register")]
		public async Task<ActionResult<User>> Register(Users usersdata)
		***REMOVED***
			var entity = new User()
			***REMOVED***
				Email = usersdata.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(usersdata.Password),
				Role = "user"
			***REMOVED***;
			DBcontext.User.Add(entity);
			await DBcontext.SaveChangesAsync();

			return Created(new Uri(Request.Path, UriKind.Relative), entity.Email);
		***REMOVED***
	***REMOVED***
***REMOVED***
