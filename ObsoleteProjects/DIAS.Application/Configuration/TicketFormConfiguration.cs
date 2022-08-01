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
    public class TicketFormConfiguration : IEntityTypeConfiguration<TicketForm>
    {
        public void Configure(EntityTypeBuilder<TicketForm> builder)
        {
            builder.ToTable("Ticket_Forms");
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(x => x.reasonId)
                .HasColumnName("reasonId");

            builder.Property(e => e.reasonCategoryId)
                .HasColumnName("reasonCategoryId");

            builder.Property(e => e.formDescription)
                .HasColumnName("formDescription");

            builder.Property(e => e.ticketStateId)
                .HasColumnName("ticketStateId");

            builder.Property(x => x.mandatory)
                .HasColumnName("mandatory");

            builder.Property(x => x.formQuestionType)
                .HasColumnName("formQuestionType");
        }
    }
}