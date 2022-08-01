using DevelopmentDiasFacilityManagement = DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models;
using DevelopmentIdentityManagement = DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFM.SqlServer.Development.Models;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.UnitOfWork;
using System;

namespace DiasBusinessLogic.Data.EF_Data.UnitOfWork
{
    public class UnitOfWork_EF : IUnitOfWork_EF
    {
        public UnitOfWork_EF()
        {
        }

        public async Task CompleteAsync(string contextTypeName, DbContext context) 
        {
            //Contextin tipini öğrenip onun üzerinde işlem yapalım
            switch (contextTypeName)
            {
                case "DiasFacilityManagementSqlServer":
                    {
                        context = (DevelopmentDiasFacilityManagement.DiasFacilityManagementSqlServer)context;

                        using (context)
                        {
                            await context.SaveChangesAsync();
                        }

                        break;
                    }               

                default:
                    {
                        throw new ArgumentException();
                    }
            }
        }
    }
}
