using System.Net;

namespace Blog.Common.Dtos;

public class ResponseDto
{
  public HttpStatusCode StatusCode { get; set; }
  public string Message { get; set; }
  public Object Data { get; set; }
}