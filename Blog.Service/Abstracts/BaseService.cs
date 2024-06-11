using System.Net;
using Blog.Common.Const;
using Blog.Common.Dtos;
using Blog.Common.Helpers;
using Microsoft.AspNetCore.Http;

namespace Blog.Service.Abstracts;

public abstract class BaseService
{
  protected BaseService()
  {
    res = new ResponseDto
    {
      StatusCode = HttpStatusCode.BadRequest,
      Message = "Error"
    };
  }

  protected ResponseDto res { get; set; }

  public int? UserId
  {
    get
    {
      var userId = default(int?);

      var accessor = HttpHelper.GetService<IHttpContextAccessor>();

      if (accessor.HttpContext.User.Identity != null && accessor.HttpContext.User.Identity.IsAuthenticated)
      {
        var userIdClaim = accessor.HttpContext.User.FindFirst(Consts.UserIdClaim);

        if (userIdClaim != null)
        {
          var claimUserId = userIdClaim.Value;

          if (!string.IsNullOrWhiteSpace(claimUserId) && int.TryParse(claimUserId, out int tempUserId))
          {
            userId = tempUserId;
          }
        }
      }

      return userId;
    }
  }
}