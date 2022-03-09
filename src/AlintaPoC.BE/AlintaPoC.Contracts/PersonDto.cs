using System;

namespace AlintaPoC.Contracts
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DoB { get; set; }
        public int RoleId { get; set; }
        public RoleDto Role { get; set; }
    }
}
