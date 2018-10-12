using DotnetEureka;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDCMicrroService.Service.EurekaService
{
    [DotnetEurekaClient("ACE-AUTH")]
    [Route("/api/auth/oauth")]
    public interface ICheckLogin
    {
        [HttpGet("token")]
        string GetToken([RequestParameter("username")]string username, [RequestParameter("password")]string password, [RequestParameter("grant_type")]string grant_type = "password");
        [HttpGet("验证token地址")]
        bool CheckToken([RequestParameter("token")]string token);
    }
}
