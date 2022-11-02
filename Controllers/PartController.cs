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
    [Route("parts")]
    public class PartController : ControllerBase
    ***REMOVED***
        private readonly kompiuterijaContext DBContext;

        public PartController(kompiuterijaContext DBContext)
        ***REMOVED***
            this.DBContext = DBContext;
***REMOVED***

        [HttpGet("")]
        public async Task<ActionResult<List<PartDTO>>> Get()
        ***REMOVED***
            List<string> ident = GetIdentity();
            if (ident[1] == "employee" || ident[1] == "admin")
            ***REMOVED***
                var List = await DBContext.Part.Select(
                s => new PartDTO
                ***REMOVED***
                    Id = s.Id,
                    ComputerId = s.ComputerId,
                    Name = s.Name,
                    Type = s.Type
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
                var ListComputer = await DBContext.Computer.Select(
                s => new ComputerDTO
                ***REMOVED***
                    Id = s.Id,
                    User = s.User,
                    Name = s.Name,
                    ShopId = s.ShopId,
                    Registered = s.Registered
        ***REMOVED***
            ).Where(s => string.Equals(s.User.Trim(), ident[0].Trim())).ToListAsync();
                var PartList = await DBContext.Part.Select(
                s => new PartDTO
                ***REMOVED***
                    Id = s.Id,
                    ComputerId = s.ComputerId,
                    Name = s.Name,
                    Type = s.Type
        ***REMOVED***
                ).ToListAsync();
                var List = PartList.Where(s => ListComputer.FirstOrDefault(d => d.Id == s.ComputerId) != null).ToList();
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
        public async Task<ActionResult<PartDTO>> GetPartById(int Id)
        ***REMOVED***
            List<string> ident = GetIdentity();
            if (ident[1] == "employee" || ident[1] == "admin")
            ***REMOVED***
                PartDTO Part = await DBContext.Part.Select(s => new PartDTO
                ***REMOVED***
                    Id = s.Id,
                    ComputerId = s.ComputerId,
                    Name = s.Name,
                    Type = s.Type
        ***REMOVED***).FirstOrDefaultAsync(s => s.Id == Id);
                if (Part == null)
                ***REMOVED***
                    return NotFound();
        ***REMOVED***
                else
                ***REMOVED***
                    return Part;
        ***REMOVED***
    ***REMOVED***
            else
            ***REMOVED***
                var ListComputer = await DBContext.Computer.Select(
                s => new ComputerDTO
                ***REMOVED***
                    Id = s.Id,
                    User = s.User,
                    Name = s.Name,
                    ShopId = s.ShopId,
                    Registered = s.Registered
        ***REMOVED***
            ).Where(s => string.Equals(s.User.Trim(), ident[0].Trim())).ToListAsync();
                PartDTO Part = await DBContext.Part.Select(s => new PartDTO
                ***REMOVED***
                    Id = s.Id,
                    ComputerId = s.ComputerId,
                    Name = s.Name,
                    Type = s.Type
        ***REMOVED***).FirstOrDefaultAsync(s => s.Id == Id);
                ComputerDTO Computer = await DBContext.Computer.Select(s => new ComputerDTO
                ***REMOVED***
                    Id = s.Id,
                    User = s.User,
                    Name = s.Name,
                    ShopId = s.ShopId,
                    Registered = s.Registered
        ***REMOVED***).Where(s => s.Id == Part.ComputerId).FirstOrDefaultAsync();
                if (Part == null || Computer == null || Computer.User != ident[0])
                ***REMOVED***
                    return NotFound();
        ***REMOVED***
                else
                ***REMOVED***
                    return Part;
        ***REMOVED***
    ***REMOVED***
            
***REMOVED***
        [Authorize(Roles = "employee,admin")]
        [HttpPost("")]
        public async Task<ActionResult<PartDTO>> InsertPart(PartDTO Part)
        ***REMOVED***
            var entity = new Part()
            ***REMOVED***
                ComputerId = Part.ComputerId,
                Name = Part.Name,
                Type = Part.Type
    ***REMOVED***;
            DBContext.Part.Add(entity);
            await DBContext.SaveChangesAsync();
            return Created(new Uri(Request.Path, UriKind.Relative), entity);
***REMOVED***
        [Authorize(Roles = "employee,admin")]
        [HttpPut("")]
        public async Task<ActionResult<PartDTO>> UpdatePart(PartDTO Part)
        ***REMOVED***
            var entity = await DBContext.Part.FirstOrDefaultAsync(s => s.Id == Part.Id);
            if(entity == null)
            ***REMOVED***
                var newEntity = new Part()
                ***REMOVED***
                    ComputerId = Part.ComputerId,
                    Name = Part.Name,
                    Type = Part.Type
        ***REMOVED***;
                DBContext.Part.Add(newEntity);
                await DBContext.SaveChangesAsync();
                return Created(new Uri(Request.Path, UriKind.Relative), newEntity);
    ***REMOVED***
            else
            ***REMOVED***
                entity.ComputerId = Part.ComputerId;
                entity.Name = Part.Name;
                entity.Type = Part.Type;
                await DBContext.SaveChangesAsync();
                return Ok();
    ***REMOVED***
***REMOVED***
        [Authorize(Roles = "employee,admin")]
        [HttpDelete("***REMOVED***Id***REMOVED***")]
        public async Task<IActionResult> DeletePart(int Id)
        ***REMOVED***
            var entity = new Part()
            ***REMOVED***
                Id = Id
    ***REMOVED***;
            DBContext.Part.Attach(entity);
            DBContext.Part.Remove(entity);
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
