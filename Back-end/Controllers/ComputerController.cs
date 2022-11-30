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
{
    [Authorize(Roles = "user,employee,admin")]
    [ApiController]
    [Route("computers")]
    public class ComputerController : ControllerBase
    {
        private readonly Kompiuterija_dbContext DBContext;

        public ComputerController(Kompiuterija_dbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<ComputerDTO>>> Get()
        {
            List<string> ident = GetIdentity();
            if (ident[1] == "employee" || ident[1] == "admin")
            {
                var List = await DBContext.Computer.Select(
                s => new ComputerDTO
                {
                    Id = s.Id,
                    User = s.User,
                    Name = s.Name,
                    ShopId = s.ShopId,
                    Registered = s.Registered
                }
            ).ToListAsync();

                if (List.Count <= 0)
                {
                    return NotFound();
                }
                else
                {
                    return List;
                }
            }
            else
            {
                var List = await DBContext.Computer.Select(
            s => new ComputerDTO
            {
                Id = s.Id,
                User = s.User,
                Name = s.Name,
                ShopId = s.ShopId,
                Registered = s.Registered
            }
            ).Where(s => string.Equals(s.User.Trim(), ident[0].Trim())).ToListAsync();

                if (List.Count <= 0)
                {
                    return NotFound();
                }
                else
                {
                    return List;
                }
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ComputerDTO>> GetComputerById(int Id)
        {
            List<string> ident = GetIdentity();
            if (ident[1] == "employee" || ident[1] == "admin")
            {
                ComputerDTO Computer = await DBContext.Computer.Select(s => new ComputerDTO
                {
                    Id = s.Id,
                    User = s.User,
                    Name = s.Name,
                    ShopId = s.ShopId,
                    Registered = s.Registered
                }).FirstOrDefaultAsync(s => s.Id == Id);
                if (Computer == null)
                {
                    return NotFound();
                }
                else
                {
                    return Computer;
                }
            }
            else
            {
                ComputerDTO Computer = await DBContext.Computer.Select(s => new ComputerDTO
                {
                    Id = s.Id,
                    User = s.User,
                    Name = s.Name,
                    ShopId = s.ShopId,
                    Registered = s.Registered
                }).FirstOrDefaultAsync(s => s.Id == Id && string.Equals(s.User.Trim(), ident[0].Trim()));
                if (User == null)
                {
                    return NotFound();
                }
                else
                {
                    return Computer;
                }
            }
        }
        [HttpGet("{Id}/parts")]
        public async Task<ActionResult<IEnumerable<PartDTO>>> GetUserComputerPartsByComputer(int Id)
        {
            List<string> ident = GetIdentity();
            if (ident[1] == "user")
            {
                //System.Diagnostics.Debug.WriteLine(user);
                ComputerDTO computer = await DBContext.Computer.Select(
                    s => new ComputerDTO
                    {
                        Id = s.Id,
                        User = s.User,
                        Name = s.Name,
                        ShopId = s.ShopId,
                        Registered = s.Registered
                    }
                ).FirstOrDefaultAsync(s => s.Id == Id && string.Equals(s.User.Trim(), ident[0].Trim()));
                if (computer == null)
                {
                    return NotFound();
                }
                var List = await DBContext.Part.Select(
                    s => new PartDTO
                    {
                        Id = s.Id,
                        ComputerId = s.ComputerId,
                        Name = s.Name,
                        Type = s.Type
                    }
                ).Where(s => s.ComputerId == computer.Id).ToListAsync();
                if (List.Count <= 0)
                {
                    return NotFound();
                }
                else
                {
                    return List;
                }
            }
            else if (ident[1] == "employee" || ident[1] == "admin")
            {
                ComputerDTO computer = await DBContext.Computer.Select(
                    s => new ComputerDTO
                    {
                        Id = s.Id,
                        User = s.User,
                        Name = s.Name,
                        ShopId = s.ShopId,
                        Registered = s.Registered
                    }
                ).FirstOrDefaultAsync(s => s.Id == Id);
                if (computer == null)
                {
                    return NotFound();
                }
                var List = await DBContext.Part.Select(
                    s => new PartDTO
                    {
                        Id = s.Id,
                        ComputerId = s.ComputerId,
                        Name = s.Name,
                        Type = s.Type
                    }
                ).Where(s => s.ComputerId == computer.Id).ToListAsync();
                if (List.Count <= 0)
                {
                    return NotFound();
                }
                else
                {
                    return List;
                }
            }
            else return NotFound();
        }
        [HttpPost("")]
        public async Task<ActionResult<ComputerDTO>> InsertComputer(ComputerDTO Computer)
        {
            List<string> ident = GetIdentity();
            var entity = new Computer()
            {
                User = ident[0],
                Name = Computer.Name,
                ShopId = Computer.ShopId,
                Registered = Computer.Registered
            };
            DBContext.Computer.Add(entity);
            await DBContext.SaveChangesAsync();
            return Created(new Uri(Request.Path, UriKind.Relative), entity);
        }
        [HttpPut("")]
        public async Task<ActionResult<ComputerDTO>> UpdateComputer(ComputerDTO Computer)
        {
            List<string> ident = GetIdentity();
            if (ident[1] == "employee" || ident[1] == "admin")
            {
                var entity = await DBContext.Computer.FirstOrDefaultAsync(s => s.Id == Computer.Id);
                if (entity == null)
                {
                    var newEntity = new Computer()
                    {
                        User = Computer.User,
                        Name = Computer.Name,
                        ShopId = Computer.ShopId,
                        Registered = Computer.Registered
                    };
                    DBContext.Computer.Add(newEntity);
                    await DBContext.SaveChangesAsync();
                    return Created(new Uri(Request.Path, UriKind.Relative), newEntity);
                }
                else
                {
                    entity.User = Computer.User;
                    entity.Name = Computer.Name;
                    entity.ShopId = Computer.ShopId;
                    entity.Registered = Computer.Registered;
                    await DBContext.SaveChangesAsync();
                    return Ok();
                }
            }
            else
            {
                var entity = await DBContext.Computer.FirstOrDefaultAsync(s => s.Id == Computer.Id);
                if (entity == null)
                {
                    var newEntity = new Computer()
                    {
                        User = ident[0],
                        Name = Computer.Name,
                        ShopId = Computer.ShopId,
                        Registered = Computer.Registered
                    };
                    DBContext.Computer.Add(newEntity);
                    await DBContext.SaveChangesAsync();
                    return Created(new Uri(Request.Path, UriKind.Relative), newEntity);
                }
                else
                {
                    if (entity.User != ident[0])
                    {
                        return Unauthorized();
                    }
                    entity.User = ident[0];
                    entity.Name = Computer.Name;
                    entity.ShopId = Computer.ShopId;
                    entity.Registered = Computer.Registered;
                    await DBContext.SaveChangesAsync();
                    return Ok();
                }
            }
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteComputer(int Id)
        {
            var existing = await DBContext.Computer.FirstOrDefaultAsync(s => s.Id == Id);
            if (existing == null)
            {
                return NoContent();
            }
            var entity = new Computer()
            {
                Id = Id
            };
            DBContext.Computer.Attach(entity);
            DBContext.Computer.Remove(entity);
            await DBContext.SaveChangesAsync();
            return NoContent();
        }
        private List<string> GetIdentity()
        {
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
        }
    }
}
