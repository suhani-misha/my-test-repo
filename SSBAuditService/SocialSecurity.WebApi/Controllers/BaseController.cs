using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialSecurity.API.ActionFilters;

namespace SocialSecurity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModelFilter]
    public class BaseController : ControllerBase
    {
    }
}
