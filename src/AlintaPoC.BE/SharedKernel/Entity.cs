using SharedKernel.Interfaces;
using System;

namespace SharedKernel
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
