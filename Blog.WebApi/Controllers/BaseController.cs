using System.Net;
using Blog.Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers;

public class BaseController : ControllerBase
{
  protected IActionResult HandleResponse(ResponseDto response, bool displayData = false)
  {
    switch (response.StatusCode)
    {
      case HttpStatusCode.NotFound:
        return NotFound(response.Message);
      case HttpStatusCode.BadRequest:
        return BadRequest(response.Message);
      case HttpStatusCode.Found:
        return Conflict(response.Message);
      case HttpStatusCode.Unauthorized:
        return Unauthorized(response.Message);
      default:
        return displayData ? Ok(new { response.Message, response.Data }) : Ok(response.Message);
    }
  }
}