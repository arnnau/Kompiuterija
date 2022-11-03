using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Kompiuterija.Entities
{
    public partial class Computer
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int ShopId { get; set; }
        public string Name { get; set; }
        public DateTime Registered { get; set; }
    }
}
