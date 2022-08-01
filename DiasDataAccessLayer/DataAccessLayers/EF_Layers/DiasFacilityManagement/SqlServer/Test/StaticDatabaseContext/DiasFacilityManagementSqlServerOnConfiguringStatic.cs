using DiasDataAccessLayer.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Runtime.CompilerServices;
using static DiasShared.Enums.ApplicationEnums;

[assembly: InternalsVisibleTo("DiasBusinessLogic")]
namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Test.Models
{
    public partial class DiasFacilityManagementSqlServer
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
                conString = "Server=10.6.11.77,5432; Database=diasWorkOrderV2; User Id=sa; Password=Dias*753951?;";

                optionsBuilder.UseSqlServer(conString, conf =>
                {
                    conf.UseHierarchyId();
                    conf.UseNetTopologySuite();
                });
            }
        }

    }
}
