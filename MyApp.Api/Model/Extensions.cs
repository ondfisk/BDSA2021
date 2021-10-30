using System;
using MyApp.Core;
using Microsoft.AspNetCore.Mvc;
using static MyApp.Core.Status;

namespace MyApp.Api.Model
{
    public static class Extensions
    {
        public static IActionResult ToActionResult(this Status status) => status switch
        {
            Updated => new NoContentResult(),
            Deleted => new NoContentResult(),
            NotFound => new NotFoundResult(),
            Conflict => new ConflictResult(),
            _ => throw new NotSupportedException($"{status} not supported")
        };

        public static IActionResult ToActionResult(this object obj)
            => obj == null ? new NotFoundResult() : new OkObjectResult(obj);
    }
}
