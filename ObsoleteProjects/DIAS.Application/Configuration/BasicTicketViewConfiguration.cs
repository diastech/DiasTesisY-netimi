using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIAS.Domain.Models;
using DIAS.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DIAS.Domain.Configuration.Base
{
    public class BasicTicketViewConfiguration : IEntityTypeConfiguration<BasicTicketView>
    {
        public void Configure(EntityTypeBuilder<BasicTicketView> builder)
        {
            builder.HasNoKey();
            builder.ToView("vw_BasicTicket");
        }
    }
}