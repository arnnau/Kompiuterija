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
    [Route("computers")]
    public class ComputerController : ControllerBase
    ***REMOVED***
        private readonly kompiuterijaContext DBContext;

        public ComputerController(kompiuterijaContext DBContext)
        ***REMOVED***
            this.DBContext = DBContext;
***REMOVED***

        [HttpGet("")]
        public async Task<ActionResult<List<ComputerDTO>>> Get()
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
        [HttpGet("***REMOVED***Id***REMOVED***")]
        public async Task<ActionResult<ComputerDTO>> GetComputerById(int Id)
        ***REMOVED***
            ComputerDTO Computer = await DBContext.Computer.Select(s => new ComputerDTO
            ***REMOVED***
                Id = s.Id,
                UserId = s.UserId,
                Name = s.Name,
                ShopId = s.ShopId,
                Registered = s.Registered
    ***REMOVED***).FirstOrDefaultAsync(s => s.Id == Id);
            if (User == null)
            ***REMOVED***
                return NotFound();
    ***REMOVED***
            else
            ***REMOVED***
                return Computer;
    ***REMOVED***
***REMOVED***
        [HttpPost("")]
        public async Task<ActionResult<ComputerDTO>> InsertComputer(ComputerDTO Computer)
        ***REMOVED***
            var entity = new Computer()
            ***REMOVED***
                UserId = Computer.UserId,
                Name = Computer.Name,
                ShopId = Computer.ShopId,
                Registered = Computer.Registered
    ***REMOVED***;
            DBContext.Computer.Add(entity);
            await DBContext.SaveChangesAsync();
            return Created(new Uri(Request.Path, UriKind.Relative), entity);
***REMOVED***
        [HttpPut("")]
        public async Task<HttpStatusCode> UpdateComputer(ComputerDTO Computer)
        ***REMOVED***
            var entity = await DBContext.Computer.FirstOrDefaultAsync(s => s.Id == Computer.Id);
            entity.UserId = Computer.UserId;
            entity.Name = Computer.Name;
            entity.ShopId = Computer.ShopId;
            entity.Registered = Computer.Registered;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
***REMOVED***
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
***REMOVED***
***REMOVED***
