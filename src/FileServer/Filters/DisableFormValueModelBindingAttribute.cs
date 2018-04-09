using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FileServer.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableFormValueModelBindingAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var factories = context.ValueProviderFactories;

            foreach (var providerFactory in factories)
            {
                var type = providerFactory.GetType();
                if (type == typeof(FormValueProviderFactory)
                    || type == typeof(JQueryFormValueProviderFactory))
                    factories.Remove(providerFactory);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }
    }
}
