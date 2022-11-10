using Lab8.Models;
using Lab8.Services;

namespace Lab8.Controllers
{
    public class CafedraController
    {
        private readonly ICafedraService _cafedraService;

        public CafedraController(ICafedraService cafedraService)
        {
            _cafedraService = cafedraService;
        }

        public Response GetAll()
        {
            var cafedras = _cafedraService.GetAll();
            return new Response(cafedras);
        }
        public Response GetById(int id)
        {
            var cafedras = _cafedraService.GetAll();
            var ans = cafedras.Where(x => x.Id == id).FirstOrDefault();
            return new Response(ans);
        }

        public Response Create(Cafedra cafedra)
        {
            _cafedraService.Create(cafedra);
            return new Response();
        }

        public Response Update(Cafedra cafedra)
        {
            _cafedraService.Update(cafedra);
            return new Response();
        }

        public Response Delete(int id)
        {
            _cafedraService.Delete(id);
            return new Response();
        }
    }
}
