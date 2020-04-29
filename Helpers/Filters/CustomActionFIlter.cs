using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorsAPI.Helpers.Filters
{
    public class CustomActionFilter : IActionFilter
    {

        private readonly ILogger<CustomActionFilter> _logger;

        public CustomActionFilter(ILogger<CustomActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //From filters you can access to certain context information from our action
            _logger.LogError("OnActionExecuting...");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogError("OnActionExecuted...");

        }

    }
}
