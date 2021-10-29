using System;
using Lecture08.Core;
using Microsoft.AspNetCore.Mvc;
using static Lecture08.Core.Status;

namespace Lecture08.Api.Model
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
