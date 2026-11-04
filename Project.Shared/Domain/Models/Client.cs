using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models
{
    public class Client
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
    }
}
