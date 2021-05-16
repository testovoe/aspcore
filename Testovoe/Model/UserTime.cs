using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Testovoe.Model
{
    public class UserTime
    {
        [Key]
        public int UserId { get; set; }
        public DateTime DateRegistration { get; set; }
        public DateTime? DateLastActivity { get; set; }
    }
}
