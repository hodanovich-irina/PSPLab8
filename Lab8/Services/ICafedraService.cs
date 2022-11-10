using Lab8.Models;

namespace Lab8.Services
{
    public interface ICafedraService
    {
        IEnumerable<Cafedra> GetAll();

        void Create(Cafedra cafedra);

        void Update(Cafedra cafedra);

        void Delete(int id);
    }
}
