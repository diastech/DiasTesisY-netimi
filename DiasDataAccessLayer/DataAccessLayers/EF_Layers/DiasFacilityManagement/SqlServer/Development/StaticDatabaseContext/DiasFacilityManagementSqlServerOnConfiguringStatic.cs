using DiasDataAccessLayer.Shared.Configuration;
using DiasShared.Operations.EnumOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Runtime.CompilerServices;
using static DiasShared.Enums.ApplicationEnums;

[assembly: InternalsVisibleTo("DiasBusinessLogic")]
namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.DiasFacilityManagement.SqlServer.Development.Models
{
    public partial class DiasFacilityManagementSqlServer
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration settings = ConfigurationHelper.GetConfig();

                string conString = String.Empty;
               
                if (ConfigurationHelper.GetDatabaseType(settings) == ApplicationDatabaseType.SqlServer)
                {
                    switch (ConfigurationHelper.GetDatabaseEnvironment(settings))
                    {
                        case DatabaseEnvironment.Development:
                            {
                                conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer")
                                                        .GetSection(DatabaseEnvironment.Development.DescriptionAttr()).Value;
                                break;
                            }

                        case DatabaseEnvironment.Test:
                            {
                                conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer")
                                                        .GetSection(DatabaseEnvironment.Test.DescriptionAttr()).Value;
                                break;
                            }

                        case DatabaseEnvironment.Live:
                            {
                                conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer")
                                                        .GetSection(DatabaseEnvironment.Live.DescriptionAttr()).Value;
                                break;
                            }

                        case DatabaseEnvironment.DevelopmentMobile:
                            {
                                conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer")
                                                        .GetSection(DatabaseEnvironment.DevelopmentMobile.DescriptionAttr()).Value;
                                break;
                            }

                        case DatabaseEnvironment.TestMobile:
                            {
                                conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer")
                                                        .GetSection(DatabaseEnvironment.TestMobile.DescriptionAttr()).Value;
                                break;
                            }
                            
                        case DatabaseEnvironment.LiveMobile:
                            {
                                conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer")
                                                        .GetSection(DatabaseEnvironment.LiveMobile.DescriptionAttr()).Value;
                                break;
                            }

                        default:
                            {
                                conString = settings.GetSection("ConnectionStrings").GetSection("DiasFacilityManagementSqlServer")
                                                        .GetSection(DatabaseEnvironment.Development.DescriptionAttr()).Value;
                                break;
                            }
                    }
                }
                else
                {
                    conString = String.Empty;
                }

                //TODO : konfigürasyonda hata var, çözülünce bu kalkacak.
                //conString = "Server=10.6.11.77,5432; Database=diasWorkOrderV2; User Id=sa; Password=Dias*753951?;";


                //Azure storage'ın connection stringini al
                //switch (ConfigurationHelper.GetDatabaseEnvironment(settings))
                //{
                //    case DatabaseEnvironment.Development:
                //        {
                //            conString = settings.GetSection("ConnectionStrings").
                //                GetSection(AzureStorageServiceName.DiasAzureStorage.DescriptionAttr()).GetSection("DevelopmentConnection").Value;
                //            break;
                //        }

                //    case DatabaseEnvironment.Test:
                //        {
                //            conString = settings.GetSection("ConnectionStrings").
                //                 GetSection(AzureStorageServiceName.DiasAzureStorage.DescriptionAttr()).GetSection("TestConnection").Value;
                //            break;
                //        }

                //    case DatabaseEnvironment.Live:
                //        {
                //            conString = settings.GetSection("ConnectionStrings").
                //                 GetSection(AzureStorageServiceName.DiasAzureStorage.DescriptionAttr()).GetSection("LiveConnection").Value;
                //            break;
                //        }

                //    case DatabaseEnvironment.DevelopmentMobile:
                //        {
                //            conString = settings.GetSection("ConnectionStrings").
                //                 GetSection(AzureStorageServiceName.DiasAzureStorage.DescriptionAttr()).GetSection("LiveConnection").Value;
                //            break;
                //        }

                //    case DatabaseEnvironment.TestMobile:
                //        {
                //            conString = settings.GetSection("ConnectionStrings").
                //                 GetSection(AzureStorageServiceName.DiasAzureStorage.DescriptionAttr()).GetSection("LiveConnection").Value;
                //            break;
                //        }

                //    case DatabaseEnvironment.LiveMobile:
                //        {
                //            conString = settings.GetSection("ConnectionStrings").
                //                 GetSection(AzureStorageServiceName.DiasAzureStorage.DescriptionAttr()).GetSection("LiveConnection").Value;
                //            break;
                //        }

                //    default:
                //        {
                //            conString = settings.GetSection("ConnectionStrings").
                //                 GetSection(AzureStorageServiceName.DiasAzureStorage.DescriptionAttr()).GetSection("DevelopmentConnection").Value;
                //            break;
                //        }
                //}

                optionsBuilder.UseSqlServer(conString, conf =>
                {
                    conf.UseHierarchyId();
                    conf.UseNetTopologySuite();
                });
            }
        }

    }
}
