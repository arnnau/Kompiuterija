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
{
	[Route("")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IJWTManagerRepository _jWTManager;
		private Kompiuterija_dbContext DBcontext;

		public UsersController(IJWTManagerRepository jWTManager, Kompiuterija_dbContext DBcontext)
		{
			this._jWTManager = jWTManager;
			this.DBcontext = DBcontext;
		}

		[HttpPost]
		[Route("login")]
		public IActionResult Authenticate(Users usersdata)
		{
			var token = _jWTManager.Authenticate(usersdata);

			if (token == null)
			{
				return Unauthorized();
			}

			return Ok(token);
		}
		[HttpPost]
		[Route("register")]
		public async Task<ActionResult<User>> Register(Users usersdata)
		{
			var entity = new User()
			{
				Email = usersdata.Email,
				Password = BCrypt.Net.BCrypt.HashPassword(usersdata.Password),
				Role = "user"
			};
			DBcontext.User.Add(entity);
			await DBcontext.SaveChangesAsync();

			return Created(new Uri(Request.Path, UriKind.Relative), entity.Role);
		}
	}
}
