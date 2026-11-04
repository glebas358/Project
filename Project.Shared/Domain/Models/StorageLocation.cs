using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models
{
    public class StorageLocation
    {

        public long Id { get; set; }


        public string Zone { get; set; } = string.Empty;

        public string Cell { get; set; } = string.Empty;

        public bool IsOccupied { get; set; } = false;
    }
}
