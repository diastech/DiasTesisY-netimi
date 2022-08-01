using DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.Models;
using System.Linq;

namespace DiasDataAccessLayer.DataAccessLayers.EF_Layers.IdentityManagement_DFMTemplate.SqlServer.Development.StaticDatabaseContext
{
    public static class IdentityManagement_DFMTemplateSqlServerInitializer
    {
        public static void InitializeAndSeedData()
        {
            using (var context = new IdentityManagement_DFMTemplateSqlServer())
            {
                context.Database.EnsureCreated();

                #region idnSchema
                InitializeCompanyRoles(context);

                //..
                //TODO :Diğer idn sabit tabloları içinde aynı method yazılacak

                #endregion idnSchema

            }
        }

        private static void InitializeCompanyRoles(IdentityManagement_DFMTemplateSqlServer context)
        {
            CompanyRole testRow = context.CompanyRoles.FirstOrDefault(b => b.Name == "Administrator");

            if (testRow == null)
            {
                context.CompanyRoles.Add(new CompanyRole { Id = 1, Name = "Administrator", NormalizedName = "ADMINISTRATOR" });
            }

            //testRow = context.CompanyRoles.FirstOrDefault(b => b.Name == "Atandı");

            //if (testRow == null)
            //{
            //    context.TicketStates.Add(new TicketState { Id = 2, StateDescription = "Atandı" });
            //}

            //testRow = context.TicketStates.FirstOrDefault(b => b.StateDescription == "Çalışılıyor");

            //if (testRow == null)
            //{
            //    context.TicketStates.Add(new TicketState { Id = 3, StateDescription = "Çalışılıyor" });
            //}

            //testRow = context.TicketStates.FirstOrDefault(b => b.StateDescription == "Beklet");

            //if (testRow == null)
            //{
            //    context.TicketStates.Add(new TicketState { Id = 4, StateDescription = "Beklet" });
            //}

            //testRow = context.TicketStates.FirstOrDefault(b => b.StateDescription == "Reddet");

            //if (testRow == null)
            //{
            //    context.TicketStates.Add(new TicketState { Id = 5, StateDescription = "Reddet" });
            //}

            //testRow = context.TicketStates.FirstOrDefault(b => b.StateDescription == "Çözümlendi");

            //if (testRow == null)
            //{
            //    context.TicketStates.Add(new TicketState { Id = 6, StateDescription = "Çözümlendi" });
            //}

            //testRow = context.TicketStates.FirstOrDefault(b => b.StateDescription == "Kapalı");

            //if (testRow == null)
            //{
            //    context.TicketStates.Add(new TicketState { Id = 7, StateDescription = "Kapalı" });
            //}

            //testRow = context.TicketStates.FirstOrDefault(b => b.StateDescription == "Yeniden Açıldı");

            //if (testRow == null)
            //{
            //    context.TicketStates.Add(new TicketState { Id = 8, StateDescription = "Yeniden Açıldı" });
            //}

            context.SaveChanges();
        }


    }

}

