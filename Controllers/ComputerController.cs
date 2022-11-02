using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kompiuterija.DTO;
using Kompiuterija.Entities;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Kompiuterija.Controllers
***REMOVED***
    [Authorize(Roles = "user,employee,admin")]
    [ApiController]
    [Route("computers")]
    public class ComputerController : ControllerBase
    ***REMOVED***
        private readonly Kompiuterija_dbContext DBContext;

        public ComputerController(Kompiuterija_dbContext DBContext)
        ***REMOVED***
            this.DBContext = DBContext;
***REMOVED***

        [HttpGet("")]
        public async Task<ActionResult<List<ComputerDTO>>> Get()
        ***REMOVED***
            List<string> ident = GetIdentity();
            if (ident[1] == "employee" || ident[1] == "admin")
            ***REMOVED***
                var List = await DBContext.Computer.Select(
                s => new ComputerDTO
                ***REMOVED***
                    Id = s.Id,
                    User = s.User,
                    Name = s.Name,
                    ShopId = s.ShopId,
                    Registered = s.Registered
        ***REMOVED***
            ).ToListAsync();

                if (List.Count <= 0)
                ***REMOVED***
                    return NotFound();
        ***REMOVED***
                else
                ***REMOVED***
                    return List;
        ***REMOVED***
    ***REMOVED***
            else
            ***REMOVED***
                var List = await DBContext.Computer.Select(
                s => new ComputerDTO
                ***REMOVED***
                    Id = s.Id,
                    User = s.User,
                    Name = s.Name,
                    ShopId = s.ShopId,
                    Registered = s.Registered
        ***REMOVED***
                ).Where(s => string.Equals(s.User.Trim(), ident[0].Trim())).ToListAsync();

                if (List.Count <= 0)
                ***REMOVED***
                    return NotFound();
        ***REMOVED***
                else
                ***REMOVED***
                    return List;
        ***REMOVED***
    ***REMOVED***
***REMOVED***
        [HttpGet("***REMOVED***Id***REMOVED***")]
        public async Task<ActionResult<ComputerDTO>> GetComputerById(int Id)
        ***REMOVED***
            List<string> ident = GetIdentity();
            if (ident[1] == "employee" || ident[1] == "admin") ***REMOVED***
                ComputerDTO Computer = await DBContext.Computer.Select(s => new ComputerDTO
                ***REMOVED***
                    Id = s.Id,
                    User = s.User,
                    Name = s.Name,
                    ShopId = s.ShopId,
                    Registered = s.Registered
        ***REMOVED***).FirstOrDefaultAsync(s => s.Id == Id);
                if (Computer == null)
                ***REMOVED***
                    return NotFound();
        ***REMOVED***
                else
                ***REMOVED***
                    return Computer;
        ***REMOVED***
    ***REMOVED***
            else
            ***REMOVED***
                ComputerDTO Computer = await DBContext.Computer.Select(s => new ComputerDTO
                ***REMOVED***
                    Id = s.Id,
                    User = s.User,
                    Name = s.Name,
                    ShopId = s.ShopId,
                    Registered = s.Registered
        ***REMOVED***).FirstOrDefaultAsync(s => s.Id == Id && string.Equals(s.User.Trim(), ident[0].Trim()));
                if (User == null)
                ***REMOVED***
                    return NotFound();
        ***REMOVED***
                else
                ***REMOVED***
                    return Computer;
        ***REMOVED***
    ***REMOVED***
***REMOVED***
        [HttpPost("")]
        public async Task<ActionResult<ComputerDTO>> InsertComputer(ComputerDTO Computer)
        ***REMOVED***
            List<string> ident = GetIdentity();
            var entity = new Computer()
            ***REMOVED***
                User = ident[0],
                Name = Computer.Name,
                ShopId = Computer.ShopId,
                Registered = Computer.Registered
    ***REMOVED***;
            DBContext.Computer.Add(entity);
            await DBContext.SaveChangesAsync();
            return Created(new Uri(Request.Path, UriKind.Relative), entity);
***REMOVED***
        [HttpPut("")]
        public async Task<ActionResult<ComputerDTO>> UpdateComputer(ComputerDTO Computer)
        ***REMOVED***
            List<string> ident = GetIdentity();
            if (ident[1] == "employee" || ident[1] == "admin")
            ***REMOVED***
                var entity = await DBContext.Computer.FirstOrDefaultAsync(s => s.Id == Computer.Id);
                if (entity == null)
                ***REMOVED***
                    var newEntity = new Computer()
                    ***REMOVED***
                        User = Computer.User,
                        Name = Computer.Name,
                        ShopId = Computer.ShopId,
                        Registered = Computer.Registered
            ***REMOVED***;
                    DBContext.Computer.Add(newEntity);
                    await DBContext.SaveChangesAsync();
                    return Created(new Uri(Request.Path, UriKind.Relative), newEntity);
        ***REMOVED***
                else
                ***REMOVED***
                    entity.User = Computer.User;
                    entity.Name = Computer.Name;
                    entity.ShopId = Computer.ShopId;
                    entity.Registered = Computer.Registered;
                    await DBContext.SaveChangesAsync();
                    return Ok();
        ***REMOVED***
    ***REMOVED***
            else
            ***REMOVED***
                var entity = await DBContext.Computer.FirstOrDefaultAsync(s => s.Id == Computer.Id);
                if(entity == null)
                ***REMOVED***
                    var newEntity = new Computer()
                    ***REMOVED***
                        User = ident[0],
                        Name = Computer.Name,
                        ShopId = Computer.ShopId,
                        Registered = Computer.Registered
            ***REMOVED***;
                    DBContext.Computer.Add(newEntity);
                    await DBContext.SaveChangesAsync();
                    return Created(new Uri(Request.Path, UriKind.Relative), newEntity);
        ***REMOVED***
                else
                ***REMOVED***
                    if (entity.User != ident[0])
                    ***REMOVED***
                        return Unauthorized();
            ***REMOVED***
                    entity.User = ident[0];
                    entity.Name = Computer.Name;
                    entity.ShopId = Computer.ShopId;
                    entity.Registered = Computer.Registered;
                    await DBContext.SaveChangesAsync();
                    return Ok();
        ***REMOVED***
    ***REMOVED***
***REMOVED***
        [Authorize(Roles = "admin")]
        [HttpDelete("***REMOVED***Id***REMOVED***")]
        public async Task<IActionResult> DeleteComputer(int Id)
        ***REMOVED***
            var entity = new Computer()
            ***REMOVED***
                Id = Id
    ***REMOVED***;
            DBContext.Computer.Attach(entity);
            DBContext.Computer.Remove(entity);
            await DBContext.SaveChangesAsync();
            return NoContent();
***REMOVED***
        private List<string> GetIdentity()
        ***REMOVED***
            List<string> list = new List<string>();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            string user = claim
                .Where(x => x.Type == ClaimTypes.Email)
                .FirstOrDefault().Value.ToString();
            string role = claim
                .Where(x => x.Type == ClaimTypes.Role)
                .FirstOrDefault().Value.ToString();
            list.Add(user);
            list.Add(role);
            return list;
***REMOVED***
***REMOVED***
***REMOVED***
