using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Domain
{
    public class Role : Entity
    {
        public string Name { get; set; }

        public List<Person> People { get; set; }
    }
}
