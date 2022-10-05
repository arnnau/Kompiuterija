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
    [Route("[controller]")]
    public class ShopController : ControllerBase
    ***REMOVED***
        private readonly kompiuterijaContext DBContext;

        public ShopController(kompiuterijaContext DBContext)
        ***REMOVED***
            this.DBContext = DBContext;
***REMOVED***

        [HttpGet("all")]
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
        [HttpGet("get/***REMOVED***Id***REMOVED***")]
        public async Task<ActionResult<ShopDTO>> GetShopById(int Id)
        ***REMOVED***
            ShopDTO Shop = await DBContext.Shop.Select(s => new ShopDTO
            ***REMOVED***
                Id = s.Id,
                Address = s.Address,
                City = s.City
    ***REMOVED***).FirstOrDefaultAsync(s => s.Id == Id);
            if (User == null)
            ***REMOVED***
                return NotFound();
    ***REMOVED***
            else
            ***REMOVED***
                return Shop;
    ***REMOVED***
***REMOVED***
        [HttpGet("get/***REMOVED***Id***REMOVED***/computers")]
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
        [HttpPost("insert")]
        public async Task<HttpStatusCode> InsertShop(ShopDTO Shop)
        ***REMOVED***
            var entity = new Shop()
            ***REMOVED***
                Address = Shop.Address,
                City = Shop.City
    ***REMOVED***;
            DBContext.Shop.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
***REMOVED***
        [HttpPut("update")]
        public async Task<HttpStatusCode> UpdateShop(ShopDTO Shop)
        ***REMOVED***
            var entity = await DBContext.Shop.FirstOrDefaultAsync(s => s.Id == Shop.Id);
            entity.Address = Shop.Address;
            entity.City = Shop.City;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
***REMOVED***
        [HttpDelete("delete/***REMOVED***Id***REMOVED***")]
        public async Task<HttpStatusCode> DeleteShop(int Id)
        ***REMOVED***
            var entity = new Shop()
            ***REMOVED***
                Id = Id
    ***REMOVED***;
            DBContext.Shop.Attach(entity);
            DBContext.Shop.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
***REMOVED***
***REMOVED***
***REMOVED***
