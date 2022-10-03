using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kompiuterija.DTO;
using Kompiuterija.Entities;

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

        [HttpGet("GetShops")]
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
***REMOVED***
***REMOVED***
