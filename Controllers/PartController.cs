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
    public class PartController : ControllerBase
    ***REMOVED***
        private readonly kompiuterijaContext DBContext;

        public PartController(kompiuterijaContext DBContext)
        ***REMOVED***
            this.DBContext = DBContext;
***REMOVED***

        [HttpGet("all")]
        public async Task<ActionResult<List<PartDTO>>> Get()
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
        [HttpGet("get/***REMOVED***Id***REMOVED***")]
        public async Task<ActionResult<PartDTO>> GetPartById(int Id)
        ***REMOVED***
            PartDTO Part = await DBContext.Part.Select(s => new PartDTO
            ***REMOVED***
                Id = s.Id,
                ComputerId = s.ComputerId,
                Name = s.Name,
                Type = s.Type
    ***REMOVED***).FirstOrDefaultAsync(s => s.Id == Id);
            if (User == null)
            ***REMOVED***
                return NotFound();
    ***REMOVED***
            else
            ***REMOVED***
                return Part;
    ***REMOVED***
***REMOVED***
        [HttpPost("insert")]
        public async Task<HttpStatusCode> InsertPart(PartDTO Part)
        ***REMOVED***
            var entity = new Part()
            ***REMOVED***
                ComputerId = Part.ComputerId,
                Name = Part.Name,
                Type = Part.Type
    ***REMOVED***;
            DBContext.Part.Add(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.Created;
***REMOVED***
        [HttpPut("update")]
        public async Task<HttpStatusCode> UpdatePart(PartDTO Part)
        ***REMOVED***
            var entity = await DBContext.Part.FirstOrDefaultAsync(s => s.Id == Part.Id);
            entity.ComputerId = Part.ComputerId;
            entity.Name = Part.Name;
            entity.Type = Part.Type;
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
***REMOVED***
        [HttpDelete("delete/***REMOVED***Id***REMOVED***")]
        public async Task<HttpStatusCode> DeletePart(int Id)
        ***REMOVED***
            var entity = new Part()
            ***REMOVED***
                Id = Id
    ***REMOVED***;
            DBContext.Part.Attach(entity);
            DBContext.Part.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
***REMOVED***
***REMOVED***
***REMOVED***
