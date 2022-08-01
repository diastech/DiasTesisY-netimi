using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;
using System.Collections.Generic;
using System.Linq;

namespace DiasDataAccessLayer.Shared.EfExtensions.SqlServer
{
    public class CustomAnnotationProvider : SqlServerMigrationsAnnotationProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
        public CustomAnnotationProvider(MigrationsAnnotationProviderDependencies dependencies)
            : base(dependencies)
        {
        }

        //TODO: EF 5.0 için ne olacak
        //public override IEnumerable<IAnnotation> For(IProperty property)
        //{
        //    var baseAnnotations = base.For(property);

        //    var orderAnnotation = property.FindAnnotation(CustomAnnotationNames.ColumnOrder);

        //    return orderAnnotation == null
        //        ? baseAnnotations
        //        : baseAnnotations.Concat(new[] { orderAnnotation });
        //}
    }
}
