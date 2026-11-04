using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Domain.Models
{
    public class Employee
    {
  
        public long Id { get; set; }

      
        public string Name { get; set; } = string.Empty;

    
        public string Login { get; set; } = string.Empty;

       
        public string PasswordHash { get; set; } = string.Empty;

     
        public UserRole Role { get; set; }
    }
}
