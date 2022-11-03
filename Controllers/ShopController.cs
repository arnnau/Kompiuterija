using Microsoft.AspNetCore.Http;
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
    [Route("shops")]
    public class ShopController : ControllerBase
    {
        private readonly Kompiuterija_dbContext DBContext;

        public ShopController(Kompiuterija_dbContext DBContext)
        {
            this.DBContext = DBContext;
        }
        [HttpGet("")]
        public async Task<ActionResult<List<ShopDTO>>> Get()
        {
            var List = await DBContext.Shop.Select(
                s => new ShopDTO
                {
                    Id = s.Id,
                    Address = s.Address,
                    City = s.City
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ShopDTO>> GetShopById(int Id)
        {
            ShopDTO Shop = await DBContext.Shop.Select(s => new ShopDTO
            {
                Id = s.Id,
                Address = s.Address,
                City = s.City
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (Shop == null)
            {
                return NotFound();
            }
            else
            {
                return Shop;
            }
        }
        [HttpGet("***REMOVED***Id***REMOVED***/computers")]
        public async Task<ActionResult<IEnumerable<ComputerDTO>>> GetUserComputersByShop(int Id)
        {
            List<string> ident = GetIdentity();
            if (ident[1] == "user")
            {
                //System.Diagnostics.Debug.WriteLine(user);
                var List = await DBContext.Computer.Select(
                    s => new ComputerDTO
                    {
                        Id = s.Id,
                        User = s.User,
                        Name = s.Name,
                        ShopId = s.ShopId,
                        Registered = s.Registered
                    }
                ).Where(s => s.ShopId == Id && string.Equals(s.User.Trim(), ident[0].Trim(), StringComparison.OrdinalIgnoreCase)).ToListAsync();

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
                var List = await DBContext.Computer.Select(
            s => new ComputerDTO
            {
                Id = s.Id,
                User = s.User,
                Name = s.Name,
                ShopId = s.ShopId,
                Registered = s.Registered
            }
        ).Where(s => s.ShopId == Id).ToListAsync();

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
        [Authorize(Roles = "admin")]
        [HttpPost("")]
        public async Task<ActionResult<ShopDTO>> InsertShop(ShopDTO Shop)
        {
            var entity = new Shop()
            {
                Address = Shop.Address,
                City = Shop.City
            };
            DBContext.Shop.Add(entity);
            await DBContext.SaveChangesAsync();
            return Created(new Uri(Request.Path, UriKind.Relative), entity);
        }
        [Authorize(Roles = "admin")]
        [HttpPut("")]
        public async Task<ActionResult<ShopDTO>> UpdateShop(ShopDTO Shop)
        {
            var entity = await DBContext.Shop.FirstOrDefaultAsync(s => s.Id == Shop.Id);
            if (entity == null)
            {
                var newEntity = new Shop()
                {
                    Address = Shop.Address,
                    City = Shop.City
                };
                DBContext.Shop.Add(newEntity);
                await DBContext.SaveChangesAsync();
                return Created(new Uri(Request.Path, UriKind.Relative), newEntity);
            }
            else
            {
                entity.Address = Shop.Address;
                entity.City = Shop.City;
                await DBContext.SaveChangesAsync();
                return Ok();
            }
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("***REMOVED***Id***REMOVED***")]
        public async Task<IActionResult> DeleteShop(int Id)
        {
            var entity = new Shop()
            {
                Id = Id
            };
            DBContext.Shop.Attach(entity);
            DBContext.Shop.Remove(entity);
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
