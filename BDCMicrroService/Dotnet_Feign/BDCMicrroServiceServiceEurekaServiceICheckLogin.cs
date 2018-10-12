﻿namespace DotnetEureka.Wrapper
{
    // Generated by DotnetEureka
    public sealed class BDCMicrroServiceServiceEurekaServiceICheckLogin : DotnetEureka.HttpSupport.DiscoveryAwareBase, BDCMicrroService.Service.EurekaService.ICheckLogin
    {
        public BDCMicrroServiceServiceEurekaServiceICheckLogin()
            : base("ACE-AUTH", "/api/auth/oauth") { }
        public System.String GetToken(System.String username, System.String password, System.String grant_type)
        {
            var variables = new System.Collections.Generic.Dictionary<string,object>();
            var reqParams = new System.Collections.Generic.Dictionary<string,string>();
            reqParams.Add("username", username?.ToString());
            reqParams.Add("password", password?.ToString());
            reqParams.Add("grant_type", grant_type?.ToString());
            var request = CreateRequest(System.Net.Http.HttpMethod.Get, "token", variables, reqParams);
            var response = Invoke(request);
            return ConvertToObject<System.String>(response);
        }
        public System.Boolean CheckToken(System.String token)
        {
            var variables = new System.Collections.Generic.Dictionary<string,object>();
            var reqParams = new System.Collections.Generic.Dictionary<string,string>();
            reqParams.Add("token", token?.ToString());
            var request = CreateRequest(System.Net.Http.HttpMethod.Get, "验证token地址", variables, reqParams);
            var response = Invoke(request);
            return ConvertToObject<System.Boolean>(response);
        }
    }
}