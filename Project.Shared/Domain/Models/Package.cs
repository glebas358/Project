using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models
{
    public class Package
    {

        public long Id { get; set; }
        
        public string Number { get; set; } = string.Empty;
        
        public long ClientId { get; set; }              
       
        public Client? Client { get; set; }

        public string Seller { get; set; } = string.Empty;

        public string Contents { get; set; } = string.Empty;
        
        public DateTime? ArrivalDate { get; set; }
        
        public DateTime? StorageDeadline { get; set; }
        
        public PackageStatus Status { get; set; }
        
        public long? StorageLocationId { get; set; }    
        
        public StorageLocation? StorageLocation { get; set; }
    }
}
