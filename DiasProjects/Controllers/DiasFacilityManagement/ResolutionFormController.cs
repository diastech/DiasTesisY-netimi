using Microsoft.AspNetCore.Mvc;
using DFManagementStdDtoInterface = DiasWebApi.InterfacesAbstracts.Interfaces.Repositories.DiasFacilityManagement;
using DFManagementStdDto = DiasShared.Data.EF_Data.DiasFacilityManagement.SqlServer.DataTransferObjects.Development.Shared.Standard;


namespace DiasWebApi.DiasFacilityManagement.Controllers
{
    [ApiController]
    public class ResolutionFormController : Controller
    {
        private readonly DFManagementStdDtoInterface.IResolutionFormDtoRepository _resolutionFormsRepository;
        private readonly DFManagementStdDtoInterface.IResolutionFormAnswerDtoRepository _resolutionFormAnswersRepository;
        private readonly DFManagementStdDtoInterface.IResolutionFormChoiceOptionDtoRepository _resolutionFormChoiceOptionsRepository;
        private readonly DFManagementStdDtoInterface.IResolutionFormMultipleChoiceDtoRepository _resolutionFormMultipleChoicesRepository;
        private readonly DFManagementStdDtoInterface.IResolutionFormQuestionDtoRepository _resolutionFormQuestionsRepository;
        private readonly DFManagementStdDtoInterface.IResolutionFormQuestionTypeDtoRepository _resolutionFormQuestionTypesRepository;
        private readonly DFManagementStdDtoInterface.IResolutionFormSingleQuestionDtoRepository _resolutionFormSingleQuestionsRepository;
        private readonly DFManagementStdDtoInterface.IResolutionFormYesNoDtoRepository _resolutionFormYesNosRepository;



        public ResolutionFormController(DFManagementStdDtoInterface.IResolutionFormDtoRepository resolutionFormsRepository,
            DFManagementStdDtoInterface.IResolutionFormAnswerDtoRepository resolutionFormAnswersRepository,
            DFManagementStdDtoInterface.IResolutionFormChoiceOptionDtoRepository resolutionFormChoiceOptionsRepository,
            DFManagementStdDtoInterface.IResolutionFormMultipleChoiceDtoRepository resolutionFormMultipleChoicesRepository,
            DFManagementStdDtoInterface.IResolutionFormQuestionDtoRepository resolutionFormQuestionsRepository,
            DFManagementStdDtoInterface.IResolutionFormQuestionTypeDtoRepository resolutionFormQuestionTypesRepository,
            DFManagementStdDtoInterface.IResolutionFormSingleQuestionDtoRepository resolutionFormSingleQuestionsRepository,
            DFManagementStdDtoInterface.IResolutionFormYesNoDtoRepository resolutionFormYesNosRepository)
        {
            _resolutionFormsRepository = resolutionFormsRepository;
            _resolutionFormAnswersRepository = resolutionFormAnswersRepository;
            _resolutionFormChoiceOptionsRepository = resolutionFormChoiceOptionsRepository;
            _resolutionFormMultipleChoicesRepository = resolutionFormMultipleChoicesRepository;
            _resolutionFormQuestionsRepository = resolutionFormQuestionsRepository;
            _resolutionFormQuestionTypesRepository = resolutionFormQuestionTypesRepository;
            _resolutionFormSingleQuestionsRepository = resolutionFormSingleQuestionsRepository;
            _resolutionFormYesNosRepository = resolutionFormYesNosRepository;
        }

        #region ControllerRoute

        #endregion

        #region Controller_ActionRoute

        #endregion

    }

    
}
