using SharedKernel;
using System;

namespace AlintaPoC.Domain
{
    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
    }
}
