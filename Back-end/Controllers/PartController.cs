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
    [Route("parts")]
    public class PartController : ControllerBase
    {
        private readonly Kompiuterija_dbContext DBContext;

        public PartController(Kompiuterija_dbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<PartDTO>>> Get()
        {
            List<string> ident = GetIdentity();
            if (ident[1] == "employee" || ident[1] == "admin")
            {
                var List = await DBContext.Part.Select(
                s => new PartDTO
                {
                    Id = s.Id,
                    ComputerId = s.ComputerId,
                    Name = s.Name,
                    Type = s.Type
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
                var ListComputer = await DBContext.Computer.Select(
            s => new ComputerDTO
            {
                Id = s.Id,
                User = s.User,
                Name = s.Name,
                ShopId = s.ShopId,
                Registered = s.Registered
            }
            ).Where(s => string.Equals(s.User.Trim(), ident[0].Trim())).ToListAsync();
                var PartList = await DBContext.Part.Select(
                s => new PartDTO
                {
                    Id = s.Id,
                    ComputerId = s.ComputerId,
                    Name = s.Name,
                    Type = s.Type
                }
                ).ToListAsync();
                var List = PartList.Where(s => ListComputer.FirstOrDefault(d => d.Id == s.ComputerId) != null).ToList();
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
        public async Task<ActionResult<PartDTO>> GetPartById(int Id)
        {
            List<string> ident = GetIdentity();
            if (ident[1] == "employee" || ident[1] == "admin")
            {
                PartDTO Part = await DBContext.Part.Select(s => new PartDTO
                {
                    Id = s.Id,
                    ComputerId = s.ComputerId,
                    Name = s.Name,
                    Type = s.Type
                }).FirstOrDefaultAsync(s => s.Id == Id);
                if (Part == null)
                {
                    return NotFound();
                }
                else
                {
                    return Part;
                }
            }
            else
            {
                var ListComputer = await DBContext.Computer.Select(
            s => new ComputerDTO
            {
                Id = s.Id,
                User = s.User,
                Name = s.Name,
                ShopId = s.ShopId,
                Registered = s.Registered
            }
        ).Where(s => string.Equals(s.User.Trim(), ident[0].Trim())).ToListAsync();
                PartDTO Part = await DBContext.Part.Select(s => new PartDTO
                {
                    Id = s.Id,
                    ComputerId = s.ComputerId,
                    Name = s.Name,
                    Type = s.Type
                }).FirstOrDefaultAsync(s => s.Id == Id);
                ComputerDTO Computer = await DBContext.Computer.Select(s => new ComputerDTO
                {
                    Id = s.Id,
                    User = s.User,
                    Name = s.Name,
                    ShopId = s.ShopId,
                    Registered = s.Registered
                }).Where(s => s.Id == Part.ComputerId).FirstOrDefaultAsync();
                if (Part == null || Computer == null || Computer.User != ident[0])
                {
                    return NotFound();
                }
                else
                {
                    return Part;
                }
            }

        }
        [Authorize(Roles = "employee,admin")]
        [HttpPost("")]
        public async Task<ActionResult<PartDTO>> InsertPart(PartDTO Part)
        {
            var entity = new Part()
            {
                ComputerId = Part.ComputerId,
                Name = Part.Name,
                Type = Part.Type
            };
            DBContext.Part.Add(entity);
            await DBContext.SaveChangesAsync();
            return Created(new Uri(Request.Path, UriKind.Relative), entity);
        }
        [Authorize(Roles = "employee,admin")]
        [HttpPut("")]
        public async Task<ActionResult<PartDTO>> UpdatePart(PartDTO Part)
        {
            var entity = await DBContext.Part.FirstOrDefaultAsync(s => s.Id == Part.Id);
            if (entity == null)
            {
                var newEntity = new Part()
                {
                    ComputerId = Part.ComputerId,
                    Name = Part.Name,
                    Type = Part.Type
                };
                DBContext.Part.Add(newEntity);
                await DBContext.SaveChangesAsync();
                return Created(new Uri(Request.Path, UriKind.Relative), newEntity);
            }
            else
            {
                entity.ComputerId = Part.ComputerId;
                entity.Name = Part.Name;
                entity.Type = Part.Type;
                await DBContext.SaveChangesAsync();
                return Ok();
            }
        }
        [Authorize(Roles = "employee,admin")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePart(int Id)
        {
            var existing = await DBContext.Part.FirstOrDefaultAsync(s => s.Id == Id);
            if(existing == null)
            {
                return NoContent();
            }
            var entity = new Part()
            {
                Id = Id
            };
            DBContext.Part.Attach(entity);
            DBContext.Part.Remove(entity);
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
