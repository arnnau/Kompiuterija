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
***REMOVED***
    [Authorize(Roles = "user,employee,admin")]
    [ApiController]
    [Route("shops")]
    public class ShopController : ControllerBase
    ***REMOVED***
        private readonly kompiuterijaContext DBContext;

        public ShopController(kompiuterijaContext DBContext)
        ***REMOVED***
            this.DBContext = DBContext;
***REMOVED***
        [HttpGet("")]
        public async Task<ActionResult<List<ShopDTO>>> Get()
        ***REMOVED***
            var List = await DBContext.Shop.Select(
                s => new ShopDTO
                ***REMOVED***
                    Id = s.Id,
                    Address = s.Address,
                    City = s.City
        ***REMOVED***
            ).ToListAsync();

            if (List.Count < 0)
            ***REMOVED***
                return NotFound();
    ***REMOVED***
            else
            ***REMOVED***
                return List;
    ***REMOVED***
***REMOVED***
        [HttpGet("***REMOVED***Id***REMOVED***")]
        public async Task<ActionResult<ShopDTO>> GetShopById(int Id)
        ***REMOVED***
            ShopDTO Shop = await DBContext.Shop.Select(s => new ShopDTO
            ***REMOVED***
                Id = s.Id,
                Address = s.Address,
                City = s.City
    ***REMOVED***).FirstOrDefaultAsync(s => s.Id == Id);
            if (Shop == null)
            ***REMOVED***
                return NotFound();
    ***REMOVED***
            else
            ***REMOVED***
                return Shop;
    ***REMOVED***
***REMOVED***
        [HttpGet("***REMOVED***Id***REMOVED***/computers")]
        public async Task<ActionResult<IEnumerable<ComputerDTO>>> GetUserComputersByShop(int Id)
        ***REMOVED***
            List<string> ident = GetIdentity();
            if (ident[1] == "user")
            ***REMOVED***
                //System.Diagnostics.Debug.WriteLine(user);
                var List = await DBContext.Computer.Select(
                    s => new ComputerDTO
                    ***REMOVED***
                        Id = s.Id,
                        User = s.User,
                        Name = s.Name,
                        ShopId = s.ShopId,
                        Registered = s.Registered
            ***REMOVED***
                ).Where(s => s.ShopId == Id && string.Equals(s.User.Trim(), ident[0].Trim(), StringComparison.OrdinalIgnoreCase)).ToListAsync();

                if (List.Count <= 0)
                ***REMOVED***
                    return NotFound();
        ***REMOVED***
                else
                ***REMOVED***
                    return List;
        ***REMOVED***
    ***REMOVED***
            else if (ident[1] == "employee" || ident[1] == "admin")
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
            ).Where(s => s.ShopId == Id).ToListAsync();

                if (List.Count <= 0)
                ***REMOVED***
                    return NotFound();
        ***REMOVED***
                else
                ***REMOVED***
                    return List;
        ***REMOVED***
    ***REMOVED***
            else return NotFound();
***REMOVED***
        [Authorize(Roles = "admin")]
        [HttpPost("")]
        public async Task<ActionResult<ShopDTO>> InsertShop(ShopDTO Shop)
        ***REMOVED***
            var entity = new Shop()
            ***REMOVED***
                Address = Shop.Address,
                City = Shop.City
    ***REMOVED***;
            DBContext.Shop.Add(entity);
            await DBContext.SaveChangesAsync();
            return Created(new Uri(Request.Path, UriKind.Relative), entity);
***REMOVED***
        [Authorize(Roles = "admin")]
        [HttpPut("")]
        public async Task<ActionResult<ShopDTO>> UpdateShop(ShopDTO Shop)
        ***REMOVED***
            var entity = await DBContext.Shop.FirstOrDefaultAsync(s => s.Id == Shop.Id);
            if(entity == null)
            ***REMOVED***
                var newEntity = new Shop()
                ***REMOVED***
                    Address = Shop.Address,
                    City = Shop.City
        ***REMOVED***;
                DBContext.Shop.Add(newEntity);
                await DBContext.SaveChangesAsync();
                return Created(new Uri(Request.Path, UriKind.Relative), newEntity);
    ***REMOVED***
            else
            ***REMOVED***
                entity.Address = Shop.Address;
                entity.City = Shop.City;
                await DBContext.SaveChangesAsync();
                return Ok();
    ***REMOVED***
***REMOVED***
        [Authorize(Roles = "admin")]
        [HttpDelete("***REMOVED***Id***REMOVED***")]
        public async Task<IActionResult> DeleteShop(int Id)
        ***REMOVED***
            var entity = new Shop()
            ***REMOVED***
                Id = Id
    ***REMOVED***;
            DBContext.Shop.Attach(entity);
            DBContext.Shop.Remove(entity);
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
