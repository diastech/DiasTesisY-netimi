using DiasShared.Enums.Standart;
using DiasShared.Operations.EnumOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static DiasShared.Enums.Standart.AuthorizationEnums;

namespace DiasShared.AuthorizationOperation
{
    public static class AuthorizationDeserialize
    {
        public static List<Tuple<ApiControllerDescription, List<string>>> AuthorizationDeserializeFunc(IEnumerable<Claim> claims)
        {
            List<Tuple<ApiControllerDescription, List<string>>> listControllerClaims = new();

            foreach (var item in claims)
            {
                if (item.Type == "CompanyRoleAuthorizationCode")

                {
                    List<string> listString = new();
                    //item.Value.Split("",);
                    var str = item.Value;
                    int numberAuthorization = Convert.ToInt32(str.Split(':')[0]);
                    char[] binaryString = str.Split(':')[1].ToCharArray();
                    Array.Reverse(binaryString);
                    ApiControllerDescription controllerDescription = numberAuthorization.GetEnumValue<ApiControllerDescription>();


                    for (int i = 0; i < binaryString.Length; i++)
                    {
                        if (binaryString[i] == '1')
                        {
                            if (controllerDescription.ToString() == "LocationWrapper")
                            {
                                ControllerAction.LocationWrapperContollerActionEnum actions = i.GetEnumValue<ControllerAction.LocationWrapperContollerActionEnum>();
                                listString.Add(actions.ToString());
                            }
                            if (controllerDescription.ToString() == "TicketWrapper")
                            {
                                ControllerAction.TicketWrapperContollerActionEnum actions = i.GetEnumValue<ControllerAction.TicketWrapperContollerActionEnum>();
                                listString.Add(actions.ToString());
                            }
                            if (controllerDescription.ToString() == "TicketReasonCategoryWrapper")
                            {
                                ControllerAction.TicketReasonCategoryWrapperContollerActionEnum actions = i.GetEnumValue<ControllerAction.TicketReasonCategoryWrapperContollerActionEnum>();
                                listString.Add(actions.ToString());
                            }
                            if (controllerDescription.ToString() == "Users")
                            {
                                ControllerAction.UserContollerActionEnum actions = i.GetEnumValue<ControllerAction.UserContollerActionEnum>();
                                listString.Add(actions.ToString());
                            }
                            if (controllerDescription.ToString() == "TicketStateFlowWrapper")
                            {
                                ControllerAction.TicketStateFlowWrapperContollerActionEnum actions = i.GetEnumValue<ControllerAction.TicketStateFlowWrapperContollerActionEnum>();
                                listString.Add(actions.ToString());
                            }
                            if (controllerDescription.ToString() == "TicketPriority")
                            {
                                ControllerAction.TicketPriorityContollerActionEnum actions = i.GetEnumValue<ControllerAction.TicketPriorityContollerActionEnum>();
                                listString.Add(actions.ToString());
                            }
                            if (controllerDescription.ToString() == "TicketNoteWrapper")
                            {
                                ControllerAction.TicketNoteWrapperContollerActionEnum actions = i.GetEnumValue<ControllerAction.TicketNoteWrapperContollerActionEnum>();
                                listString.Add(actions.ToString());
                            }
                            if (controllerDescription.ToString() == "AttachmentWrapper")
                            {
                                ControllerAction.AttachmentWrapperContollerActionEnum actions = i.GetEnumValue<ControllerAction.AttachmentWrapperContollerActionEnum>();
                                listString.Add(actions.ToString());
                            }
                            if (controllerDescription.ToString() == "BasicTicketState")
                            {
                                ControllerAction.BasicTicketStateContollerActionEnum actions = i.GetEnumValue<ControllerAction.BasicTicketStateContollerActionEnum>();
                                listString.Add(actions.ToString());
                            }
                            if (controllerDescription.ToString() == "TicketState")
                            {
                                ControllerAction.TicketStateContollerActionEnum actions = i.GetEnumValue<ControllerAction.TicketStateContollerActionEnum>();
                                listString.Add(actions.ToString());
                            }
                        }
                    }
                    listControllerClaims.Add(new Tuple<ApiControllerDescription, List<string>>(controllerDescription, listString));
                }
            }
            return listControllerClaims;
        }
        public static List<Tuple<List<WebMenuHierarchicalNode>, List<string>>> AuthorizationMenuDeserializeFunc(Claim claim)
        {
            List<Tuple<List<WebMenuHierarchicalNode>, List<string>>> listControllerClaims = new();


            if (claim.Type == "CompanyRoleMenuAuthorizationCode")

            {
                List<string> listString = new();
                string str = claim.Value;
                int numberAuthorization = Convert.ToInt32(str.Split(':')[0]);
                char[] binaryString = str.Split(':')[1].ToCharArray();
                ulong deger;
                List<WebMenuHierarchicalNode> menuName = new();

                Array.Reverse(binaryString);              
                
                for (int i = 0; i < binaryString.Length; i++)
                {
                    deger = (Convert.ToUInt32(Char.GetNumericValue(binaryString[i]))) * Convert.ToUInt64((Math.Pow(2, i)));

                    if (deger > 0)
                    {
                        AuthorizationEnums.WebMenuHierarchicalNode name = deger.GetEnumValue<AuthorizationEnums.WebMenuHierarchicalNode>();
                        listString.Add(binaryString[i].ToString());
                        menuName.Add(name);
                    }                   
                }

                listControllerClaims.Add(new Tuple<List<WebMenuHierarchicalNode>, List<string>>(menuName, listString));
            }

            return listControllerClaims;
        }
        
        public static List<Tuple<List<Tuple<WebTicketPageDefinitions, string>>,int>> TicketRoleTicketPageReadOnlyAttribute(IEnumerable<Claim> claims)
        {
            
            List<Tuple<List<Tuple<WebTicketPageDefinitions, string>>,int>> listTicketRoleTicketPageReadOnlyAttribute = new();
            int numberAuthorization;
            foreach (var item in claims)
            {
                List<Tuple<WebTicketPageDefinitions, string>> listTicketRoleTicketPage = new();
                if (item.Type == "TicketRoleTicketPageReadOnlyAttribute")
                {
                    WebTicketPageDefinitions disabledItem = new();
                    string stringBinary = "";
                    string str = item.Value;
                    numberAuthorization = Convert.ToInt32(str.Split(':')[0]);
                    char[] binaryString = str.Split(':')[1].ToCharArray();
                    ulong deger;
                    
                    Array.Reverse(binaryString);

                    for (int i = 0; i < binaryString.Length; i++)
                    {
                        deger = (Convert.ToUInt32(Char.GetNumericValue(binaryString[i]))) * Convert.ToUInt64((Math.Pow(2, i)));

                        if (deger > 0)
                        {
                            AuthorizationEnums.WebTicketPageDefinitions name = deger.GetEnumValue<AuthorizationEnums.WebTicketPageDefinitions>();
                            stringBinary = binaryString[i].ToString();
                            disabledItem = name;
                            listTicketRoleTicketPage.Add(new Tuple<WebTicketPageDefinitions, string>(disabledItem, stringBinary));                            
                        }
                    }
                    if (listTicketRoleTicketPage.Count != 0)
                    {
                        listTicketRoleTicketPageReadOnlyAttribute.Add(new Tuple<List<Tuple<WebTicketPageDefinitions, string>>, int>(listTicketRoleTicketPage, numberAuthorization));
                    }
                }
            }
            return listTicketRoleTicketPageReadOnlyAttribute;            
        }
    }
}
