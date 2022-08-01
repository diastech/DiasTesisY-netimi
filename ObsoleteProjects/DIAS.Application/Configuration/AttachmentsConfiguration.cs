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
    public class AttachmentsConfiguration : IEntityTypeConfiguration<Attachments>
    {
        public void Configure(EntityTypeBuilder<Attachments> builder)
        {
            builder.ToTable("Attachments");
            builder.HasKey(x => x.id);
            builder.HasIndex(x => x.id);

            builder.Property(x => x.id).ValueGeneratedOnAdd()
                .HasColumnName("id");

            builder.Property(e => e.ticketId)
                .HasColumnName("ticketId");

            builder.Property(e => e.description)
                .HasColumnName("description");

            builder.Property(e => e.folder)
                .HasColumnName("folder");

            builder.Property(x => x.ticketNotesId)
                .HasColumnName("ticketNotesId");

            builder.Property(x => x.basicTicketId)
              .HasColumnName("basicTicketId");
        }
    }
}