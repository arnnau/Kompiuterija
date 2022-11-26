using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuterija.DTO
{
    public class ComputerDTO
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int ShopId { get; set; }
        public string Name { get; set; }
        public DateTime Registered { get; set; }
    }
   }
