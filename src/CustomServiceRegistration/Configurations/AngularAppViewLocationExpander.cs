using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

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
                "~/wwwroot/app/{0}.html",
                "~/wwwroot/{0}.html"
            };
            return viewLocationFormats;
        }
    }
}
