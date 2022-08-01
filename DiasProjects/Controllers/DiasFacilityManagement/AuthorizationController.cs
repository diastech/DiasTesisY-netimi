using DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using Microsoft.AspNetCore.Mvc;

namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly IUserDtoRepository _usersRepository;
        public AuthorizationController(IUserDtoRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
    }
}
