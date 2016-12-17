using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DesignPatternsProject.Models;

namespace DesignPatternsProject.ViewModel
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}