using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiasShared.Enums.Standart
{
    public class ControllerAction
    {
        public enum LocationWrapperContollerActionEnum
        {
            [Description("GetAll")]
            GetAll = 0,
            [Description("GetById")]
            GetById = 1,
        }
        public enum TicketWrapperContollerActionEnum
        {
            [Description("GetAll")]
            GetAll = 0,
            [Description("GetById")]
            GetById = 1,
            [Description("Delete")]
            Delete = 2,
            [Description("Insert")]
            Insert = 3,
            [Description("Update")]
            Update = 4,
            [Description("InsertWithFastTicket")]
            InsertWithFastTicket = 5,
            [Description("GetAllTicketsByBasicTicketId")]
            GetAllTicketsByBasicTicketId = 6,
            [Description("UpdateState")]
            UpdateState =7,
        }
        public enum TicketReasonCategoryWrapperContollerActionEnum
        {
            [Description("GetAll")]
            GetAll = 0,
            [Description("GetById")]
            GetById = 1,
        }
        public enum UserContollerActionEnum
        {
            [Description("GetAllUsers")]
            GetAllUsers = 0,
            [Description("GetById")]
            GetById = 1,
            [Description("Delete")]
            Delete = 2,
            [Description("Insert")]
            Insert = 3,
            [Description("Update")]
            Update = 4,
        }
        public enum TicketStateFlowWrapperContollerActionEnum
        {
            [Description("GetAll")]
            GetAll = 0,            
        }
        public enum TicketPriorityContollerActionEnum
        {
            [Description("GetAll")]
            GetAll = 0,
            [Description("GetById")]
            GetById = 1,
            [Description("Delete")]
            Delete = 2,
            [Description("Insert")]
            Insert = 3,
            [Description("Update")]
            Update = 4,
        }
        public enum TicketNoteWrapperContollerActionEnum
        {
            [Description("GetById")]
            GetById = 0,
            [Description("Insert")]
            Insert = 1,
            [Description("Delete")]
            Delete = 2,            
        }
        public enum AttachmentWrapperContollerActionEnum
        {
            [Description("GetById")]
            GetById = 0,
            [Description("Insert")]
            Insert = 1,            
        }

        public enum BasicTicketStateContollerActionEnum
        {
            [Description("GetAll")]
            GetAll = 0,
            [Description("GetById")]
            GetById = 1,
            [Description("Delete")]
            Delete = 2,
            [Description("Insert")]
            Insert = 3,
            [Description("Update")]
            Update = 4,
        }
        public enum TicketStateContollerActionEnum
        {
            [Description("GetAll")]
            GetAll = 0,
            [Description("GetById")]
            GetById = 1,
            [Description("Delete")]
            Delete = 2,
            [Description("Insert")]
            Insert = 3,
            [Description("Update")]
            Update = 4,
        }
    }
}
