using Lab8.Models;

namespace Lab8.Services
{
    internal class CafedraService : ICafedraService
    {
        private readonly CafedraDataContext _context;

        public CafedraService(CafedraDataContext context)
        {
            _context = context;
        }

        public void Create(Cafedra cafedra)
        {
            try
            {
                _context.Cafedras.Add(cafedra);
                _context.SaveChanges();
            }
            catch { }
        }

        public void Delete(int id)
        {
            try
            {
                var cafedra = _context.Cafedras.FirstOrDefault(c => c.Id == id);
                _context.Cafedras.Remove(cafedra);
                _context.SaveChanges();
            }
            catch { }

        }

        public IEnumerable<Cafedra> GetAll()
        {
            return _context.Cafedras.ToList();
        }

        public void Update(Cafedra cafedra)
        {
            try
            {
                var entity = _context.Cafedras.FirstOrDefault(c => c.Id == cafedra.Id);
                entity.Name = cafedra.Name;
                entity.Facult = cafedra.Facult;
                entity.Count = cafedra.Count;
                entity.Fio = cafedra.Fio;
                _context.Cafedras.Update(entity);
                _context.SaveChanges();
            }
            catch { }
        }
    }
}
