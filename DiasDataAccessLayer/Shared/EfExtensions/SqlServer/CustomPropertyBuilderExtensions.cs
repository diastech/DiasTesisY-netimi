using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiasDataAccessLayer.Shared.EfExtensions.SqlServer
{
    public static class CustomPropertyBuilderExtensions
    {
        public static PropertyBuilder<TProperty> HasColumnOrder<TProperty>(this PropertyBuilder<TProperty> propertyBuilder, int order)
        {
            propertyBuilder.HasAnnotation(CustomAnnotationNames.ColumnOrder, order);
            return propertyBuilder;
        }
    }
}
