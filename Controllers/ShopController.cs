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

namespace Kompiuterija.Controllers
***REMOVED***
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
        public async Task<ActionResult<IEnumerable<ComputerDTO>>> GetComputersByShop(int Id)
        ***REMOVED***
            var List = await DBContext.Computer.Select(
                s => new ComputerDTO
                ***REMOVED***
                    Id = s.Id,
                    UserId = s.UserId,
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
        [HttpPut("")]
        public async Task<HttpStatusCode> UpdateShop(ShopDTO Shop)
        ***REMOVED***
            var entity = await DBContext.Shop.FirstOrDefaultAsync(s => s.Id == Shop.Id);
            entity.Address = Shop.Address;
            entity.City = Shop.City;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
***REMOVED***
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
***REMOVED***
***REMOVED***
