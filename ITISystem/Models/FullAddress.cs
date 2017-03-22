using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITISystem.Models
{
    [ComplexType]
    public class FullAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}