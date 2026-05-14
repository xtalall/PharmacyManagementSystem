using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWinForms
{
    internal class User
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string DateAdded { get; set; }
        public string LastLogin { get; set; }
        public string Status { get; set; }
    }
    
}
