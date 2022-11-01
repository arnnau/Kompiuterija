using Kompiuterija.Models;
using Kompiuterija.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuterija.Controllers
***REMOVED***
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	***REMOVED***
		private readonly IJWTManagerRepository _jWTManager;

		public UsersController(IJWTManagerRepository jWTManager)
		***REMOVED***
			this._jWTManager = jWTManager;
		***REMOVED***

		[HttpGet]
		public List<string> Get()
		***REMOVED***
			var users = new List<string>
		***REMOVED***
			"Satinder Singh",
			"Amit Sarna",
			"Davin Jon"
		***REMOVED***;

			return users;
		***REMOVED***

		[AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate(Users usersdata)
		***REMOVED***
			var token = _jWTManager.Authenticate(usersdata);

			if (token == null)
			***REMOVED***
				return Unauthorized();
			***REMOVED***

			return Ok(token);
		***REMOVED***
	***REMOVED***
***REMOVED***
