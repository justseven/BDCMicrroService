﻿namespace DotnetEureka.Wrapper
{
    // Generated by DotnetEureka
    public sealed class BDCMicrroServiceServiceEurekaServiceICheckClient : DotnetEureka.HttpSupport.DiscoveryAwareBase, BDCMicrroService.Service.EurekaService.ICheckClient
    {
        public BDCMicrroServiceServiceEurekaServiceICheckClient()
            : base("ACE-AUTH", "/client/token") { }
        public System.Object GetToken(System.String clientId, System.String secret)
        {
            var variables = new System.Collections.Generic.Dictionary<string,object>();
            var reqParams = new System.Collections.Generic.Dictionary<string,string>();
            reqParams.Add("clientId", clientId?.ToString());
            reqParams.Add("secret", secret?.ToString());
            var request = CreateRequest(System.Net.Http.HttpMethod.Post, "", variables, reqParams);
            var response = Invoke(request);
            return ConvertToObject<System.Object>(response);
        }
    }
}