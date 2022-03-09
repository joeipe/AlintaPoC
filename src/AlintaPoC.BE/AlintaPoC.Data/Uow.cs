using AlintaPoC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlintaPoC.Data
{
    public class Uow : IDisposable
    {
        private DataContext _context;

        public Uow(DataContext context)
        {
            _context = context;
        }

        public GenericRepository<Person> PersonRepo { get { return new GenericRepository<Person>(_context); } }
        public GenericRepository<Role> RoleRepo { get { return new GenericRepository<Role>(_context); } }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
