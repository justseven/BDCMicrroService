using BDCMicrroService.Entity.CheckModel;
using DotnetEureka;

namespace BDCMicrroService.Service.EurekaService
{
    [DotnetEurekaClient("ACE-AUTH")]
    [Route("/client/token")]
    public interface ICheckClient
    {
        [HttpPost]
        object GetToken([RequestParameter("clientId")]string clientId, [RequestParameter("secret")]string secret);
    }
}
