using ErpDashboard.Application.Models;
using ErpDashboard.Shared.CustomAttribute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ErpDashboard.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        static readonly MethodInfo SetQueryFilterMethod = typeof(ModelBuilderExtensions)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                        .Single(t => t.IsGenericMethod && t.Name == nameof(SetQueryFilter));

        //public static void SetQueryFilterOnAllEntities<TEntityInterface>(
        //    this ModelBuilder builder,
        //    Expression<Func<TEntityInterface, bool>> filterExpression)
        //{
        //    foreach (var type in builder.Model.GetEntityTypes()
        //        .Where(t => t.BaseType == null)
        //        .Select(t => t.ClrType)
        //        .Where(t => typeof(TEntityInterface).IsAssignableFrom(t)))
        //    {
        //        builder.SetEntityQueryFilter(
        //            type,
        //            filterExpression);
        //    }
        //}

        public static void SetQueryFilterOnAllEntities<TEntityInterface>(
    this ModelBuilder builder,
    int? CompanyId)
        {
            if (CompanyId.HasValue)
            {
                var AllCls = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().SelectMany(a => a.GetProperties()).Where(t => t.IsDefined(typeof(CompanyIdAttribute)))).Select(x => new { propName = x.Name, EntityName = x.DeclaringType.AssemblyQualifiedName,propType = x.PropertyType }).ToList();

                foreach (var cls in AllCls)
                {
                    Type t = Type.GetType(cls.EntityName);
                    var parameter = Expression.Parameter(t, "t");
                    var propertyExpression = Expression.PropertyOrField(parameter, cls.propName);
                    var constant = Expression.Constant(CompanyId.Value, cls.propType);
                    var equalExpression = Expression.Equal(propertyExpression, constant);
                    var lambda = Expression.Lambda(Expression.GetFuncType(t, typeof(bool)),equalExpression, parameter);
                    builder.Entity(t).HasQueryFilter(lambda);
                    //builder.SetEntityQueryFilter<CompanyIdAttribute>(
                    //t,
                    //    lambda);
                }
            }

        }

        static void SetEntityQueryFilter<TEntityInterface>(
            this ModelBuilder builder,
            Type entityType,
            Expression filterExpression)
        {
            SetQueryFilterMethod
                .MakeGenericMethod(entityType, entityType)
               .Invoke(null, new object[] { builder, filterExpression });
        }

        static void SetQueryFilter<TEntity, TEntityInterface>(
            this ModelBuilder builder,
            Expression<Func<TEntityInterface, bool>> filterExpression)
                where TEntityInterface : class
                where TEntity : class, TEntityInterface
        {
            var concreteExpression = filterExpression
                .Convert<TEntityInterface, TEntity>();
            //builder.Entity<TEntity>()
            //    .AppendQueryFilter(concreteExpression);
            builder.Entity<TEntity>().HasQueryFilter(filterExpression);
        }

        // CREDIT: This comment by magiak on GitHub https://github.com/dotnet/efcore/issues/10275#issuecomment-785916356
        static void AppendQueryFilter<T>(this EntityTypeBuilder entityTypeBuilder, Expression<Func<T, bool>> expression)
            where T : class
        {
            var parameterType = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);

            var expressionFilter = ReplacingExpressionVisitor.Replace(
                expression.Parameters.Single(), parameterType, expression.Body);

            if (entityTypeBuilder.Metadata.GetQueryFilter() != null)
            {
                var currentQueryFilter = entityTypeBuilder.Metadata.GetQueryFilter();
                var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                    currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
                expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
            }
            
            var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
            entityTypeBuilder.HasQueryFilter(lambdaExpression);
        }
    }

    public static class ExpressionExtensions
    {
        // This magic is courtesy of this StackOverflow post.
        // https://stackoverflow.com/questions/38316519/replace-parameter-type-in-lambda-expression
        // I made some tweaks to adapt it to our needs - @haacked
        public static Expression<Func<TTarget, bool>> Convert<TSource, TTarget>(
            this Expression<Func<TSource, bool>> root)
        {
            var visitor = new ParameterTypeVisitor<TSource, TTarget>();
            return (Expression<Func<TTarget, bool>>)visitor.Visit(root);
        }

        class ParameterTypeVisitor<TSource, TTarget> : ExpressionVisitor
        {
            private ReadOnlyCollection<ParameterExpression> _parameters;

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return _parameters?.FirstOrDefault(p => p.Name == node.Name)
                       ?? (node.Type == typeof(TSource) ? Expression.Parameter(typeof(TTarget), node.Name) : node);
            }

            protected override Expression VisitLambda<T>(Expression<T> node)
            {
               
                _parameters = VisitAndConvert(node.Parameters, "VisitLambda");
                return Expression.Lambda(Visit(node.Body), _parameters);
            }
        }
    }
}
