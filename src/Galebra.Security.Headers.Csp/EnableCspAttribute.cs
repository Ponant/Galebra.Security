using Galebra.Security.Headers.Csp.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Galebra.Security.Headers.Csp;

/// <summary>
/// <inheritdoc cref="IEnableCspFilterAttribute"/><br/>
/// Defaults to the default <see cref="CspPolicyGroup"/>
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class EnableCspAttribute : ResultFilterAttribute, IEnableCspFilterAttribute
{
    /// <summary>
    /// <inheritdoc cref="EnableCspAttribute"/>
    /// </summary>
    public EnableCspAttribute()
    {
    }
    public string? PolicyGroupName { get; init; }

    public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        return base.OnResultExecutionAsync(context, next);
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        /// Override filter with an if statment, however this is not proper, especially that
        /// filter execute differently if they are requested globally or per folder.
        /// A better way is to provide the <see cref="EnableCspPageFilter"/>
        /// to configure with RazorPAges in startup
        //if (context.HttpContext.Items[CspConstants.EnableCspResultFilterAttributeKey] is not null)
        context.HttpContext.Items[CspConstants.EnableCspResultFilterAttributeKey] = PolicyGroupName;
        base.OnResultExecuting(context);
    }
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        base.OnResultExecuted(context);
    }
}
