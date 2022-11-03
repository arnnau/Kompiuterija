using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuterija.DTO
{
    public class PartDTO
    {
        public int Id { get; set; }
        public int ComputerId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
