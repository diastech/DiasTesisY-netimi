using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork
{
    public interface IUnitOfWork_EF
    {
        public Task CompleteAsync(string contextTypeName, DbContext context);
    }
}
