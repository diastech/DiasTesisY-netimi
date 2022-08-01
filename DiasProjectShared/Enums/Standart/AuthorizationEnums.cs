using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DiasShared.Enums.Standart
{
    public class AuthorizationEnums
    {
        /// <summary>
        /// int karşılıkları veritabanı Id ile aynı olmalıdır
        /// Descriptionlar aynı zamanda veritabanında tutulan claimlerdir
        /// </summary>
        public enum ApiControllerDescription
        {
            [Description("Assignment Group")]
            AssignmentGroup = 1,

            [Description("Attachment")]
            Attachment = 2,

            [Description("Basic Ticket")]
            BasicTicket = 3,

            [Description("Basic Ticket State")]
            BasicTicketState = 4,

            [Description("Basic Ticket Wrapper")]
            BasicTicketWrapper = 5,

            [Description("Location")]
            Location = 6,

            [Description("Location Wrapper")]
            LocationWrapper = 7,

            [Description("Menu Page")]
            MenuPage = 8,

            [Description("Periodic Ticket")]
            PeriodicTicket = 9,

            [Description("Periodic Ticket Wrapper")]
            PeriodicTicketWrapper = 10,

            [Description("Resolution Form")]
            ResolutionForm = 11,

            [Description("Ticket Audit History")]
            TicketAuditHistory = 12,

            [Description("Ticket")]
            Ticket = 13,

            [Description("Ticket Note")]
            TicketNote = 14,

            [Description("Ticket Priority")]
            TicketPriority = 15,

            [Description("Ticket Reason Category")]
            TicketReasonCategory = 16,

            [Description("Ticket Reason Category Wrapper")]
            TicketReasonCategoryWrapper = 17,

            [Description("Ticket Reason")]
            TicketReason = 18,

            [Description("Ticket Related Location")]
            TicketRelatedLocation = 19,

            [Description("Ticket State")]
            TicketState = 20,

            [Description("Ticket Wrapper")]
            TicketWrapper = 21,

            [Description("User Menu Page")]
            UserMenuPage = 22,

            //Test amaçlıdır, canlıda kullanılmayacak
            [Description("Weather Forecast")]
            WeatherForecast = 23,

            [Description("Authentication")]
            Authentication = 24,

            [Description("Authorization")]
            Authorization = 25,

            [Description("Users")]
            Users = 27,

            [Description("Api Action Description")]
            ApiActionDescription = 30,

            [Description("Api Controller Description")]
            ApiControllerDescription = 31,

            [Description("Attachment Wrapper")]
            AttachmentWrapper = 32,

            //Test amaçlıdır, canlıda kullanılmayacak
            [Description("Authentication Test")]
            AuthenticationTest = 33,

            [Description("Company Role Claim")]
            CompanyRoleClaim = 34,

            [Description("Company Role")]
            CompanyRole = 35,

            [Description("Company Role User")]
            CompanyRoleUser = 36,

            [Description("MenuPage V2")]
            MenuPageV2 = 37,

            [Description("Ticket Note Wrapper")]
            TicketNoteWrapper = 38,

            [Description("Ticket State Flow")]
            TicketStateFlow = 39,

            [Description("Ticket State Flow Role")]
            TicketStateFlowRole = 40,

            [Description("Ticke tState Flow Wrapper")]
            TicketStateFlowWrapper = 41,

            [Description("Ticket State Role")]
            TicketStateRole = 42,

            [Description("User Claim")]
            UserClaim = 43,

            [Description("User Login")]
            UserLogin = 44,

            [Description("User Token")]
            UserToken = 45
        }

        /// <summary>
        /// int karşılıkları veritabanı Id ile aynı olmalıdır
        /// EnumAdları -> Format :  ControllerDescriptionEnum + "_" + kendi enum adı
        /// Descriptionlar aynı zamanda veritabanında tutulan claimlerdir ->
        /// Format : ControllerDescriptionEnumdaki Description + ":" + kendi descriptionu
        ///  Display attribute -> Bağlı olduğu ControllerDescriptionEnum adı
        ///  Bunu çekmek için string enumDesc = ApiActionDescription.AssignmentGroup_GetAll.GetDisplayOrValueFromEnum<ApiActionDescription>()
        /// </summary>
        public enum ApiActionDescription
        {            
            [Description("Assignment Group:Get All")]
            [Display(Name = "AssignmentGroup")]
            AssignmentGroup_GetAll = 1,

            [Description("Assignment Group:Get By Id")]
            [Display(Name = "AssignmentGroup")]
            AssignmentGroup_GetById = 2,

            [Description("Assignment Group:Delete")]
            [Display(Name = "AssignmentGroup")]
            AssignmentGroup_Delete = 3,

            [Description("Assignment Group:Insert")]
            [Display(Name = "AssignmentGroup")]
            AssignmentGroup_Insert = 4,

            [Description("Assignment Group:Update")]
            [Display(Name = "AssignmentGroup")]
            AssignmentGroup_Update = 5,


            [Description("Attachment:Get All")]
            [Display(Name = "Attachment")]
            Attachment_GetAll = 6,

            [Description("Attachment:Get By Id")]
            [Display(Name = "Attachment")]
            Attachment_GetById = 7,

            [Description("Attachment:Delete")]
            [Display(Name = "Attachment")]
            Attachment_Delete = 8,

            [Description("Attachment:Insert")]
            [Display(Name = "Attachment")]
            Attachment_Insert = 9,

            [Description("Attachment:Update")]
            [Display(Name = "Attachment")]
            Attachment_Update = 10,

            [Description("Attachment:Get All Ticket Attachments By Ticket Id")]
            [Display(Name = "Attachment")]
            Attachment_GetAllTicketAttachmentsByTicketId = 11,

            [Description("Attachment:Get All Note Attachments By Note Id")]
            [Display(Name = "Attachment")]
            Attachment_GetAllNoteAttachmentsByNoteId = 12,

            [Description("Attachment:Get All Ticket Attachments By Basic Ticket Id")]
            [Display(Name = "Attachment")]
            Attachment_GetAllTicketAttachmentsByBasicTicketId = 13,


            [Description("Basic Ticket:Get All")]
            [Display(Name = "BasicTicket")]
            BasicTicket_GetAll = 14,

            [Description("Basic Ticket:Get By Id")]
            [Display(Name = "BasicTicket")]
            BasicTicket_GetById = 15,

            [Description("Basic Ticket:Delete")]
            [Display(Name = "BasicTicket")]
            BasicTicket_Delete = 16,

            [Description("Basic Ticket:Insert")]
            [Display(Name = "BasicTicket")]
            BasicTicket_Insert = 17,

            [Description("Basic Ticket:Update")]
            [Display(Name = "BasicTicket")]
            BasicTicket_Update = 18,

            [Description("Basic Ticket:Get All Basic Tickets By User Id")]
            [Display(Name = "BasicTicket")]
            BasicTicket_GetAllBasicTicketsByUserId = 19,


            [Description("Basic Ticket State:Get All")]
            [Display(Name = "BasicTicketState")]
            BasicTicketState_GetAll = 20,

            [Description("Basic Ticket State:Get By Id")]
            [Display(Name = "BasicTicketState")]
            BasicTicketState_GetById = 21,

            [Description("Basic Ticket State:Delete")]
            [Display(Name = "BasicTicketState")]
            BasicTicketState_Delete = 22,

            [Description("Basic Ticket State:Insert")]
            [Display(Name = "BasicTicketState")]
            BasicTicketState_Insert = 23,

            [Description("Basic Ticket State:Update")]
            [Display(Name = "BasicTicketState")]
            BasicTicketState_Update = 24,


            [Description("Basic Ticket Wrapper:Get All")]
            [Display(Name = "BasicTicketWrapper")]
            BasicTicketWrapper_GetAll = 25,

            [Description("Basic Ticket Wrapper:Get By Id")]
            [Display(Name = "BasicTicketState")]
            BasicTicketWrapper_GetById = 26,

            [Description("Basic Ticket Wrapper:Insert")]
            [Display(Name = "BasicTicketState")]
            BasicTicketWrapper_Insert = 27,


            [Description("Location:Get All")]
            [Display(Name = "Location")]
            Location_GetAll = 28,

            [Description("Location:Get By Id")]
            [Display(Name = "Location")]
            Location_GetById = 29,

            [Description("Location:Delete")]
            [Display(Name = "Location")]
            Location_Delete = 30,

            [Description("Location:Insert")]
            [Display(Name = "Location")]
            Location_Insert = 31,

            [Description("Location:Update")]
            [Display(Name = "Location")]
            Location_Update = 32,


            [Description("Location Wrapper:Get All")]
            [Display(Name = "LocationWrapper")]
            LocationWrapper_GetAll = 33,

            [Description("Location Wrapper:Get By Id")]
            [Display(Name = "LocationWrapper")]
            LocationWrapper_GetById = 34,


            [Description("Menu Page:Get All")]
            [Display(Name = "MenuPage")]
            MenuPage_GetAll = 35,

            [Description("Menu Page:Get By Id")]
            [Display(Name = "MenuPage")]
            MenuPage_GetById = 36,

            [Description("Menu Page:Delete")]
            [Display(Name = "MenuPage")]
            MenuPage_Delete = 37,

            [Description("Menu Page:Insert")]
            [Display(Name = "MenuPage")]
            MenuPage_Insert = 38,

            [Description("Menu Page:Update")]
            [Display(Name = "MenuPage")]
            MenuPage_Update = 39,


            [Description("Periodic Ticket:Get All")]
            [Display(Name = "PeriodicTicket")]
            PeriodicTicket_GetAll = 40,

            [Description("Periodic Ticket:Get By Id")]
            [Display(Name = "PeriodicTicket")]
            PeriodicTicket_GetById = 41,

            [Description("Periodic Ticket:Delete")]
            [Display(Name = "PeriodicTicket")]
            PeriodicTicket_Delete = 42,

            [Description("Periodic Ticket:Insert")]
            [Display(Name = "PeriodicTicket")]
            PeriodicTicket_Insert = 43,

            [Description("Periodic Ticket:Update")]
            [Display(Name = "PeriodicTicket")]
            PeriodicTicket_Update = 44,


            [Description("Periodic Ticket Wrapper:Get All")]
            [Display(Name = "PeriodicTicketWrapper")]
            PeriodicTicketWrapper_GetAll = 45,

            [Description("Periodic Ticket Wrapper:Get By Id")]
            [Display(Name = "PeriodicTicketWrapper")]
            PeriodicTicketWrapper_GetById = 46,

            [Description("Periodic Ticket Wrapper:Delete")]
            [Display(Name = "PeriodicTicketWrapper")]
            PeriodicTicketWrapper_Delete = 47,

            [Description("Periodic Ticket Wrapper:Insert")]
            [Display(Name = "PeriodicTicketWrapper")]
            PeriodicTicketWrapper_Insert = 48,

            [Description("Periodic Ticket Wrapper:Update")]
            [Display(Name = "PeriodicTicketWrapper")]
            PeriodicTicketWrapper_Update = 49,

            //ResolutionForm actionları yok


            [Description("Ticket Audit History:Get All")]
            [Display(Name = "TicketAuditHistory")]
            TicketAuditHistory_GetAll = 50,

            [Description("Ticket Audit History:Get By Id")]
            [Display(Name = "TicketAuditHistory")]
            TicketAuditHistory_GetById = 51,

            [Description("Ticket Audit History:Delete")]
            [Display(Name = "TicketAuditHistory")]
            TicketAuditHistory_Delete = 52,

            [Description("Ticket Audit History:Insert")]
            [Display(Name = "TicketAuditHistory")]
            TicketAuditHistory_Insert = 53,

            [Description("Ticket Audit History:Update")]
            [Display(Name = "TicketAuditHistory")]
            TicketAuditHistory_Update = 54,

            [Description("Ticket Audit History:Get All Ticket Audit History By Ticket Id")]
            [Display(Name = "TicketAuditHistory")]
            TicketAuditHistory_GetAllTicketAuditHistoryByTicketId = 55,


            [Description("Ticket:Get All")]
            [Display(Name = "Ticket")]
            Ticket_GetAll = 56,

            [Description("Ticket:Get By Id")]
            [Display(Name = "Ticket")]
            Ticket_GetById = 57,

            [Description("Ticket:Delete")]
            [Display(Name = "Ticket")]
            Ticket_Delete = 58,

            [Description("Ticket:Insert")]
            [Display(Name = "Ticket")]
            Ticket_Insert = 59,

            [Description("Ticket:Update")]
            [Display(Name = "Ticket")]
            Ticket_Update = 60,

            [Description("Ticket:Get All Tickets By Basic Ticket Id")]
            [Display(Name = "Ticket")]
            Ticket_GetAllTicketsByBasicTicketId = 61,

            [Description("Ticket:Update Ticket State")]
            [Display(Name = "Ticket")]
            Ticket_UpdateTicketState = 62,


            [Description("Ticket Note:Get All")]
            [Display(Name = "TicketNote")]
            TicketNote_GetAll = 63,

            [Description("Ticket Note:Get By Id")]
            [Display(Name = "TicketNote")]
            TicketNote_GetById = 64,

            [Description("Ticket Note:Delete")]
            [Display(Name = "TicketNote")]
            TicketNote_Delete = 65,

            [Description("Ticket Note:Insert")]
            [Display(Name = "TicketNote")]
            TicketNote_Insert = 66,

            [Description("Ticket Note:Update")]
            [Display(Name = "TicketNote")]
            TicketNote_Update = 67,

            [Description("Ticket Note:Get Ticket Note By Ticket Id")]
            [Display(Name = "TicketNote")]
            TicketNote_GetTicketNoteByTicketId = 68,


            [Description("Ticket Priority:Get All")]
            [Display(Name = "TicketPriority")]
            TicketPriority_GetAll = 69,

            [Description("Ticket Priority:Get By Id")]
            [Display(Name = "TicketPriority")]
            TicketPriority_GetById = 70,

            [Description("Ticket Priority:Delete")]
            [Display(Name = "TicketPriority")]
            TicketPriority_Delete = 71,

            [Description("Ticket Priority:Insert")]
            [Display(Name = "TicketPriority")]
            TicketPriority_Insert = 72,

            [Description("Ticket Priority:Update")]
            [Display(Name = "TicketPriority")]
            TicketPriority_Update = 73,


            [Description("Ticket Reason Category:Get All")]
            [Display(Name = "TicketReasonCategory")]
            TicketReasonCategory_GetAll = 74,

            [Description("Ticket Reason Category:Get By Id")]
            [Display(Name = "TicketReasonCategory")]
            TicketReasonCategory_GetById = 75,

            [Description("Ticket Reason Category:Delete")]
            [Display(Name = "TicketReasonCategory")]
            TicketReasonCategory_Delete = 76,

            [Description("Ticket Reason Category:Insert")]
            [Display(Name = "TicketReasonCategory")]
            TicketReasonCategory_Insert = 77,

            [Description("Ticket Reason Category:Update")]
            [Display(Name = "TicketReasonCategory")]
            TicketReasonCategory_Update = 78,


            [Description("Ticket Reason Category Wrapper:Get All")]
            [Display(Name = "TicketReasonCategoryWrapper")]
            TicketReasonCategoryWrapper_GetAll = 79,

            [Description("Ticket Reason Category Wrapper:Get By Id")]
            [Display(Name = "TicketReasonCategoryWrapper")]
            TicketReasonCategoryWrapper_GetById = 80,


            [Description("Ticket Reason:Get All")]
            [Display(Name = "TicketReason")]
            TicketReason_GetAll = 81,

            [Description("Ticket Reason:Get By Id")]
            [Display(Name = "TicketReason")]
            TicketReason_GetById = 82,

            [Description("Ticket Reason:Delete")]
            [Display(Name = "TicketReason")]
            TicketReason_Delete = 83,

            [Description("Ticket Reason:Insert")]
            [Display(Name = "TicketReason")]
            TicketReason_Insert = 84,

            [Description("Ticket Reason:Update")]
            [Display(Name = "TicketReason")]
            TicketReason_Update = 85,


            [Description("Ticket Related Location:Get All")]
            [Display(Name = "TicketRelatedLocation")]
            TicketRelatedLocation_GetAll = 86,

            [Description("Ticket Related Location:Get By Id")]
            [Display(Name = "TicketRelatedLocation")]
            TicketRelatedLocation_GetById = 87,

            [Description("Ticket Related Location:Delete")]
            [Display(Name = "TicketRelatedLocation")]
            TicketRelatedLocation_Delete = 88,

            [Description("Ticket Related Location:Insert")]
            [Display(Name = "TicketRelatedLocation")]
            TicketRelatedLocation_Insert = 89,

            [Description("Ticket Related Location:Update")]
            [Display(Name = "TicketRelatedLocation")]
            TicketRelatedLocation_Update = 90,


            [Description("Ticket State:Get All")]
            [Display(Name = "TicketState")]
            TicketState_GetAll = 91,

            [Description("Ticket State:Get By Id")]
            [Display(Name = "TicketState")]
            TicketState_GetById = 92,

            [Description("Ticket State:Delete")]
            [Display(Name = "TicketState")]
            TicketState_Delete = 93,

            [Description("Ticket State:Insert")]
            [Display(Name = "TicketState")]
            TicketState_Insert = 94,

            [Description("Ticket State:Update")]
            [Display(Name = "TicketState")]
            TicketState_Update = 95,


            [Description("Ticket Wrapper:Get All")]
            [Display(Name = "TicketWrapper")]
            TicketWrapper_GetAll = 96,

            [Description("Ticket Wrapper:Get By Id")]
            [Display(Name = "TicketWrapper")]
            TicketWrapper_GetById = 97,

            [Description("Ticket Wrapper:Delete")]
            [Display(Name = "TicketWrapper")]
            TicketWrapper_Delete = 98,

            [Description("Ticket Wrapper:Insert")]
            [Display(Name = "TicketWrapper")]
            TicketWrapper_Insert = 99,

            [Description("Ticket Wrapper:Update")]
            [Display(Name = "TicketWrapper")]
            TicketWrapper_Update = 100,

            [Description("Ticket Wrapper:Insert With Fast Ticket")]
            [Display(Name = "TicketWrapper")]
            TicketWrapper_InsertWithFastTicket = 101,

            [Description("Ticket Wrapper:Get All Tickets By Basic Ticket Id")]
            [Display(Name = "TicketWrapper")]
            TicketWrapper_GetAllTicketsByBasicTicketId = 102,

            [Description("Ticket Wrapper:Update State")]
            [Display(Name = "TicketWrapper")]
            TicketWrapper_UpdateState = 104,


            [Description("User Menu Page:Get All")]
            [Display(Name = "UserMenuPage")]
            UserMenuPage_GetAll = 105,

            [Description("User Menu Page:Get By Id")]
            [Display(Name = "UserMenuPage")]
            UserMenuPage_GetById = 106,

            [Description("User Menu Page:Delete")]
            [Display(Name = "UserMenuPage")]
            UserMenuPage_Delete = 107,

            [Description("User Menu Page:Insert")]
            [Display(Name = "UserMenuPage")]
            UserMenuPage_Insert = 108,

            [Description("User Menu Page:Update")]
            [Display(Name = "UserMenuPage")]
            UserMenuPage_Update = 109,

            //WeatherForecast actionları yok


            [Description("Authentication:Login")]
            [Display(Name = "Authentication")]
            Authentication_Login = 110,


            //Authorization actionları yok


            [Description("Users:Get All Users")]
            [Display(Name = "Users")]
            Users_GetAllUsers = 111,

            [Description("Users:Get By Id")]
            [Display(Name = "Users")]
            Users_GetById = 112,

            [Description("Users:Delete")]
            [Display(Name = "Users")]
            Users_Delete = 113,

            [Description("Users:Insert")]
            [Display(Name = "Users")]
            Users_Insert = 114,

            [Description("Users:Update")]
            [Display(Name = "Users")]
            Users_Update = 115,


            [Description("Api Action Description:Get All")]
            [Display(Name = "ApiActionDescription")]
            ApiActionDescription_GetAll = 116,

            [Description("Api Action Description:Get By Id")]
            [Display(Name = "ApiActionDescription")]
            ApiActionDescription_GetById = 117,

            [Description("Api Action Description:Delete")]
            [Display(Name = "ApiActionDescription")]
            ApiActionDescription_Delete = 118,

            [Description("Api Action Description:Insert")]
            [Display(Name = "ApiActionDescription")]
            ApiActionDescription_Insert = 119,

            [Description("Api Action Description:Update")]
            [Display(Name = "ApiActionDescription")]
            ApiActionDescription_Update = 120,


            [Description("Api Controller Description:Get All")]
            [Display(Name = "ApiControllerDescription")]
            ApiControllerDescription_GetAll = 122,

            [Description("Api Controller Description:Get By Id")]
            [Display(Name = "ApiControllerDescription")]
            ApiControllerDescription_GetById = 123,

            [Description("Api Controller Description:Delete")]
            [Display(Name = "ApiControllerDescription")]
            ApiControllerDescription_Delete = 124,

            [Description("Api Controller Description:Insert")]
            [Display(Name = "ApiControllerDescription")]
            ApiControllerDescription_Insert = 125,

            [Description("Api Controller Description:Update")]
            [Display(Name = "ApiControllerDescription")]
            ApiControllerDescription_Update = 126,



            [Description("Attachment Wrapper:Get By Id")]
            [Display(Name = "AttachmentWrapper")]
            AttachmentWrapper_GetById = 127,

            [Description("Attachment Wrapper:Insert")]
            [Display(Name = "AttachmentWrapper")]
            AttachmentWrapper_Insert = 128,



            //Test amaçlıdır, canlıda olmayacak
            [Description("Authentication Test:Test")]
            [Display(Name = "AuthenticationTest")]
            AuthenticationTest_Test = 129,

            [Description("Authentication Test:TestAnonymous")]
            [Display(Name = "AuthenticationTest")]
            AuthenticationTest_TestAnonymous = 130,



            [Description("Company Role Claim:Get All")]
            [Display(Name = "CompanyRoleClaim")]
            CompanyRoleClaim_GetAll = 131,

            [Description("Company Role Claim:Get By Id")]
            [Display(Name = "CompanyRoleClaim")]
            CompanyRoleClaim_GetById = 132,

            [Description("Company Role Claim:Delete")]
            [Display(Name = "CompanyRoleClaim")]
            CompanyRoleClaim_Delete = 133,

            [Description("Company Role Claim:Insert")]
            [Display(Name = "CompanyRoleClaim")]
            CompanyRoleClaim_Insert = 134,

            [Description("Company Role Claim:Update")]
            [Display(Name = "CompanyRoleClaim")]
            CompanyRoleClaim_Update = 135,

            [Description("Company Role Claim:Get By User Id")]
            [Display(Name = "CompanyRoleClaim")]
            CompanyRoleClaim_GetByUserId = 136,



            [Description("Company Role:Get All")]
            [Display(Name = "CompanyRole")]
            CompanyRole_GetAll = 137,

            [Description("Company Role:Get By Id")]
            [Display(Name = "CompanyRole")]
            CompanyRole_GetById = 138,

            [Description("Company Role:Delete")]
            [Display(Name = "CompanyRole")]
            CompanyRole_Delete = 139,

            [Description("Company Role:Insert")]
            [Display(Name = "CompanyRole")]
            CompanyRole_Insert = 140,

            [Description("Company Role:Update")]
            [Display(Name = "CompanyRole")]
            CompanyRole_Update = 141,


            [Description("Company Role User:Get All")]
            [Display(Name = "CompanyRoleUser")]
            CompanyRoleUser_GetAll = 142,

            [Description("Company Role User:Get By Id")]
            [Display(Name = "CompanyRoleUser")]
            CompanyRoleUser_GetById = 143,

            [Description("Company Role User:Delete")]
            [Display(Name = "CompanyRoleUser")]
            CompanyRoleUser_Delete = 144,

            [Description("Company Role User:Insert")]
            [Display(Name = "CompanyRoleUser")]
            CompanyRoleUser_Insert = 145,

            [Description("Company Role User:Update")]
            [Display(Name = "CompanyRoleUser")]
            CompanyRoleUser_Update = 146,

            [Description("Company Role User:Get By User Id")]
            [Display(Name = "CompanyRoleUser")]
            CompanyRoleUser_GetByUserId = 147,



            [Description("Menu Page V2:Get All")]
            [Display(Name = "MenuPageV2")]
            MenuPageV2_GetAll = 148,

            [Description("Menu Page V2:Get By Id")]
            [Display(Name = "MenuPageV2")]
            MenuPageV2_GetById = 149,

            [Description("Menu Page V2:Delete")]
            [Display(Name = "MenuPageV2")]
            MenuPageV2_Delete = 150,

            [Description("Menu Page V2:Insert")]
            [Display(Name = "MenuPageV2")]
            MenuPageV2_Insert = 151,

            [Description("Menu Page V2:Update")]
            [Display(Name = "MenuPageV2")]
            MenuPageV2_Update = 152,



            [Description("Ticket Note Wrapper:Get By Id")]
            [Display(Name = "TicketNoteWrapper")]
            TicketNoteWrapper_GetById = 153,

            [Description("Ticket Note Wrapper:Insert")]
            [Display(Name = "TicketNoteWrapper")]
            TicketNoteWrapper_Insert = 154,

            [Description("Ticket Note Wrapper:Delete")]
            [Display(Name = "TicketNoteWrapper")]
            TicketNoteWrapper_Delete = 155,


            [Description("Ticket State Flow:Get All")]
            [Display(Name = "TicketStateFlow")]
            TicketStateFlow_GetAll = 156,

            [Description("Ticket State Flow:Get By Id")]
            [Display(Name = "TicketStateFlow")]
            TicketStateFlow_GetById = 157,

            [Description("Ticket State Flow:Delete")]
            [Display(Name = "TicketStateFlow")]
            TicketStateFlow_Delete = 158,

            [Description("Ticket State Flow:Insert")]
            [Display(Name = "TicketStateFlow")]
            TicketStateFlow_Insert = 159,

            [Description("Ticket State Flow:Update")]
            [Display(Name = "TicketStateFlow")]
            TicketStateFlow_Update = 160,



            [Description("Ticket State Flow Role:Get All")]
            [Display(Name = "TicketStateFlowRole")]
            TicketStateFlowRole_GetAll = 161,

            [Description("Ticket State Flow Role:Get By Id")]
            [Display(Name = "TicketStateFlowRole")]
            TicketStateFlowRole_GetById = 162,

            [Description("Ticket State Flow Role:Delete")]
            [Display(Name = "TicketStateFlowRole")]
            TicketStateFlowRole_Delete = 163,

            [Description("Ticket State Flow Role:Insert")]
            [Display(Name = "TicketStateFlowRole")]
            TicketStateFlowRole_Insert = 164,

            [Description("Ticket State Flow Role:Update")]
            [Display(Name = "TicketStateFlowRole")]
            TicketStateFlowRole_Update = 165,


            [Description("Ticket State Flow Wrapper:Get All")]
            [Display(Name = "TicketStateFlowWrapper")]
            TicketStateFlowWrapper_GetAll = 166,



            [Description("Ticket State Role:Get All")]
            [Display(Name = "TicketStateRole")]
            TicketStateRole_GetAll = 167,

            [Description("Ticket State Role:Get By Id")]
            [Display(Name = "TicketStateRole")]
            TicketStateRole_GetById = 168,

            [Description("Ticket State Role:Delete")]
            [Display(Name = "TicketStateRole")]
            TicketStateRole_Delete = 169,

            [Description("Ticket State Role:Insert")]
            [Display(Name = "TicketStateRole")]
            TicketStateRole_Insert = 170,

            [Description("Ticket State Role:Update")]
            [Display(Name = "TicketStateRole")]
            TicketStateRole_Update = 171,



            [Description("User Claim:Get All")]
            [Display(Name = "UserClaim")]
            UserClaim_GetAll = 172,

            [Description("User Claim:Get By Id")]
            [Display(Name = "UserClaim")]
            UserClaim_GetById = 173,

            [Description("User Claim:Delete")]
            [Display(Name = "UserClaim")]
            UserClaim_Delete = 174,

            [Description("User Claim:Insert")]
            [Display(Name = "UserClaim")]
            UserClaim_Insert = 175,

            [Description("User Claim:Update")]
            [Display(Name = "UserClaim")]
            UserClaim_Update = 176,


            [Description("User Login:Get All")]
            [Display(Name = "UserLogin")]
            UserLogin_GetAll = 177,

            [Description("User Login:Get By Id")]
            [Display(Name = "UserLogin")]
            UserLogin_GetById = 178,

            [Description("User Login:Delete")]
            [Display(Name = "UserLogin")]
            UserLogin_Delete = 179,

            [Description("User Login:Insert")]
            [Display(Name = "UserLogin")]
            UserLogin_Insert = 180,

            [Description("User Login:Update")]
            [Display(Name = "UserLogin")]
            UserLogin_Update = 181,

            [Description("User Login:Get All User Logins By User Id")]
            [Display(Name = "UserLogin")]
            UserLogin_GetAllUserLoginsByUserId = 182,



            [Description("User Token:Get All")]
            [Display(Name = "UserToken")]
            UserToken_GetAll = 183,

            [Description("User Token:Get By Id")]
            [Display(Name = "UserToken")]
            UserToken_GetById = 184,

            [Description("User Token:Delete")]
            [Display(Name = "UserToken")]
            UserToken_Delete = 185,

            [Description("User Token:Insert")]
            [Display(Name = "UserToken")]
            UserToken_Insert = 186,

            [Description("User Token:Update")]
            [Display(Name = "UserToken")]
            UserToken_Update = 187,

            [Description("User Token:Get All User Tokens By User Id")]
            [Display(Name = "UserToken")]
            UserToken_GetAllUserTokensByUserId = 188
        }

        public enum MenuHierarchicalLevel : long
        {
            [Description("Menu Hierarchical Level 1")]
            MenuHierarchicalLevel1 = 1,

            [Description("Menu Hierarchical Level 2")]
            MenuHierarchicalLevel2 = 2,

            [Description("Menu Hierarchical Level 3")]
            MenuHierarchicalLevel3 = 3,

            [Description("Menu Hierarchical Level 4")]
            MenuHierarchicalLevel4 = 4
        }

        [Flags]
        public enum WebMenuHierarchicalNode : ulong
        {
            [Description("Root")]
            [Display(Name = "MenuHierarchicalLevel1")]
            Root = 0,

            [Description("System Management")]
            [Display(Name = "MenuHierarchicalLevel1")]
            SystemManagement = 1,

            [Description("Description Operations")]
            [Display(Name = "MenuHierarchicalLevel1")]
            DescriptionOperations = 2,

            [Description("Work Order Descriptions")]
            [Display(Name = "MenuHierarchicalLevel1")]
            WorkOrderDescriptions = 4,

            [Description("Work Order Module")]
            [Display(Name = "MenuHierarchicalLevel1")]
            WorkOrderModule = 8,

            [Description("Work Order")]
            [Display(Name = "MenuHierarchicalLevel1")]
            WorkOrder = 16,

            [Description("Simple Work Order")]
            [Display(Name = "MenuHierarchicalLevel1")]
            SimpleWorkOrder = 32,

            [Description("Simple Work Order Assignment")]
            [Display(Name = "MenuHierarchicalLevel1")]
            SimpleWorkOrderAssignment = 64,

            [Description("Periodic Work Order")]
            [Display(Name = "MenuHierarchicalLevel1")]
            PeriodicWorkOrder = 128
        }

        [Flags]
        public enum MobilDashboardHierarchicalNode : long
        {
            [Description("Root")]
            [Display(Name = "MenuHierarchicalLevel1")]
            Root = 0,

            [Description("Work Order")]
            [Display(Name = "MenuHierarchicalLevel1")]
            WorkOrder = 1,

            [Description("Assets Management")]
            [Display(Name = "MenuHierarchicalLevel1")]
            AssetsManagement = 2,

            [Description("My Pay Checks")]
            [Display(Name = "MenuHierarchicalLevel1")]
            MyPayChecks = 4,

            [Description("My Leaves")]
            [Display(Name = "MenuHierarchicalLevel1")]
            MyLeaves = 8,

            [Description("PDKS")]
            [Display(Name = "MenuHierarchicalLevel1")]
            PDKS = 16,

            [Description("Announcements")]
            [Display(Name = "MenuHierarchicalLevel1")]
            Announcements = 32,

            /// <summary>
            /// Değişebilir
            /// </summary>
            [Description("Dashboard")]
            [Display(Name = "MenuHierarchicalLevel1")]
            Dashboard = 64,

            [Description("Reports")]
            [Display(Name = "MenuHierarchicalLevel1")]
            Reports = 128
        }
    }
}
