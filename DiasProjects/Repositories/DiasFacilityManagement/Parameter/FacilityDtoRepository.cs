using DiasBusinessLogic.AutoMapper.EF_Automapper.DiasFacilityManagementSqlServer.Profiles.StandartProfiles.Development;
using DiasBusinessLogic.InterfacesAbstracts.Interfaces.BusinessRules.Generic.DiasFacilityManagementSqlServer.Development;
using DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;
using DiasWebApi.Shared.Operations.BusinessLogicOperations.SimpleInjectorOperations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DiasWebApi.Shared.Enums.WebApiApplicationEnums;
using DevelopmentRepositoryInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DiasShared.Services.Communication.BusinessLogicMessage;
using Newtonsoft.Json;
using DiasShared.Errors;
using DiasBusinessLogic.Shared.Error;
using DiasShared.Operations.StringOperation;

namespace DiasWebApi.Repositories.DiasFacilityManagement
{
    public class FacilityDtoRepository : DevelopmentRepositoryInterface.IFacilityDtoRepository
    {
        private ApplicationBusinessLogicEnvironment _applicationBusinessLogicEnvironment;
        private IGenericStandartBusinessRules<FacilityDto, FacilityProfile> _genericStandartBusinessRules;
        private bool businessLogicContainerStatus { get; set; }
        public FacilityDtoRepository(
            ApplicationBusinessLogicEnvironment applicationBusinessLogicEnvironment,
            IGenericStandartBusinessRules<FacilityDto, FacilityProfile> genericStandartBusinessRules
            )
        {
            businessLogicContainerStatus = SimpleInjectorContainerOperations.VerifyContainer();

            if (businessLogicContainerStatus)
            {
                _applicationBusinessLogicEnvironment = applicationBusinessLogicEnvironment;
            }

            _genericStandartBusinessRules = genericStandartBusinessRules;
        }


        #region WebClient

            #region Development

            //TODO : Hata kodları özelleştirilecek
            public async Task<Tuple<Error, IEnumerable<FacilityDto>>> GetAllAsync()
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return await _genericStandartBusinessRules.GetAllV2();
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                return new Tuple<Error, IEnumerable<FacilityDto>>(Errors.General.GeneralServerError(), null);
                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<Error, IEnumerable<FacilityDto>>(Errors.General.GeneralServerError(), null);
                            }
                        default:
                            {
                                return new Tuple<Error, IEnumerable<FacilityDto>>(Errors.General.GeneralServerError(), null);
                            }
                    }
                }
                else
                {
                    return new Tuple<Error, IEnumerable<FacilityDto>>(Errors.General.GeneralServerError(), null);
                }
            }

            //TODO : Hata kodları özelleştirilecek
            public async Task<Tuple<Error, FacilityDto>> DeleteAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(FacilityDto))))
                                {
                                    return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                                }

                                FacilityDto castedDto = JsonConvert.DeserializeObject<FacilityDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                                }


                                return await _genericStandartBusinessRules.DeleteFromIntV2(castedDto.Id);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                        default:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                    }
                }
                else
                {
                    return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                }
            }

            //TODO : Hata kodları özelleştirilecek
            public async Task<Tuple<Error, FacilityDto>> GetByIdFromIntAsync(int id)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                return await _genericStandartBusinessRules.GetByIdFromIntV2(id);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                        default:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                    }
                }
                else
                {
                    return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                }
            }

            //TODO : Hata kodları özelleştirilecek
            public async Task<Tuple<Error, FacilityDto>> InsertAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(FacilityDto))))
                                {
                                    return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                                }
                                FacilityDto castedDto = JsonConvert.DeserializeObject<FacilityDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                                }

                                List<string> uniqueColumns = new List<string>()
                                            {
                                                "Name"
                                            };

                                List<object> uniqueValues = new List<object>()
                                            {
                                                castedDto.Name,
                                            };

                                //Normalized Name'i ayarla
                                castedDto.NormalizedName = StringOperations.NormalizeString(castedDto.Name);

                                return await _genericStandartBusinessRules.InsertV2(castedDto, uniqueColumns, uniqueValues, true);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                        default:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                    }
                }
                else
                {
                    return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                }
            }

            //TODO : Hata kodları özelleştirilecek
            public async Task<Tuple<Error, FacilityDto>> UpdateAsync(BusinessLogicRequest request)
            {
                if (businessLogicContainerStatus)
                {
                    switch (_applicationBusinessLogicEnvironment)
                    {
                        case ApplicationBusinessLogicEnvironment.Development:
                            {
                                if ((request == null) || (request.RequestDtosTypes == null) || (request.RequestDtosJsons == null) ||
                                              (request.RequestDtosTypes.Count < 1) || (request.RequestDtosJsons.Count < 1) ||
                                                    (!Type.Equals(request.RequestDtosTypes[0], typeof(FacilityDto))))
                                {
                                    return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                                }

                                FacilityDto castedDto = JsonConvert.DeserializeObject<FacilityDto>(request.RequestDtosJsons[0]);

                                if (castedDto == null)
                                {
                                    return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                                }

                                List<string> uniqueColumns = new List<string>() { "Id" };
                                List<object> uniqueValues = new List<object>() { castedDto.Id.ToString() };

                                //Normalized Name'i ayarla
                                castedDto.NormalizedName = StringOperations.NormalizeString(castedDto.Name);

                                return await _genericStandartBusinessRules.UpdateV2(castedDto, uniqueColumns, uniqueValues);
                            }
                        case ApplicationBusinessLogicEnvironment.Test:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                        case ApplicationBusinessLogicEnvironment.Live:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                        default:
                            {
                                return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                            }
                    }
                }
                else
                {
                    return new Tuple<Error, FacilityDto>(Errors.General.GeneralServerError(), null);
                }
            }

            #endregion

        #endregion

    }
}
