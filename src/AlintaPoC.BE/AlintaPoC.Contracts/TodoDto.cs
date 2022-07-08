using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Contracts
{
    public class TodoDto
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }

        public string Content { get; set; }
        public bool Completed { get; set; }
    }
}
