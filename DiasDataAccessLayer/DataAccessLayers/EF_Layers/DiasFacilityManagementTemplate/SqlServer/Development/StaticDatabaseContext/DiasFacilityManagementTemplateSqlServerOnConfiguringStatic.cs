using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagementTemplate.SqlServer.Development.Models
{
    public partial class DiasFacilityManagementTemplateSqlServer : IdentityManagement_DFMTemplateSqlServer
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //IConfiguration settings = ConfigurationHelper.GetConfig();

                string conString = String.Empty;

                //if (ConfigurationHelper.GetDatabaseType(settings) == ApplicationDatabaseType.SqlServer)
                //{
                //    switch (ConfigurationHelper.GetDatabaseEnvironment(settings))
                //    {
                //        case DatabaseEnvironment.Development:
                //            {
                //                conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer").GetSection("DevelopmentConnection").Value;
                //                break;
                //            }

                //        case DatabaseEnvironment.Test:
                //            {
                //                conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer").GetSection("TestConnection").Value;
                //                break;
                //            }

                //        case DatabaseEnvironment.Live:
                //            {
                //                conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer").GetSection("LiveConnection").Value;
                //                break;
                //            }

                //        default:
                //            {
                //                conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer").GetSection("DevelopmentConnection").Value;
                //                break;
                //            }
                //    }
                //}
                //else
                //{
                //    conString = String.Empty;
                //}

                //TODO : konfigürasyonda hata var, çözülünce bu kalkacak.
                conString = "Server=10.6.11.77,5432; Database=diasFacilityManagementV2Template; User Id=sa; Password=Dias*753951?;";

                optionsBuilder.UseSqlServer(conString, conf =>
                {
                    conf.UseHierarchyId();
                    conf.UseNetTopologySuite();
                });
            }
        }

    }
}
