using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;

namespace webapp.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
    }
}
