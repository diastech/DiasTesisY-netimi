using System.Collections.Generic;
using System.Linq.Expressions;

namespace DiasBusinessLogic.Shared.Classes.EF_Operations
{
    internal class SubstExpressionVisitor : ExpressionVisitor
    {
        public Dictionary<Expression, Expression> subst = new Dictionary<Expression, Expression>();

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Expression newValue;

            if (subst.TryGetValue(node, out newValue))
            {
                return newValue;
            }

            return node;
        }
    }
}
