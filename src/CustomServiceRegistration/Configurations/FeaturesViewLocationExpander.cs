using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CustomServiceRegistration.Configurations
{
    public class AngularAppViewLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
            context.Values["customviewlocation"] = nameof(AngularAppViewLocationExpander);
        }

        public IEnumerable<string> ExpandViewLocations(
                  ViewLocationExpanderContext context,
                  IEnumerable<string> viewLocations)
        {
            var viewLocationFormats = new[]
            {
                "~/wwwroot/app/components/{1}/{0}.html",
                "~/wwwroot/app/{1}/{0}.html",
                "~/wwwroot/app/{0}.html"
            };
            return viewLocationFormats;
        }
    }
}
