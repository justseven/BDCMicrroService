using BDCMicrroService.Comman.Attribute;
using BDCMicrroService.Service;
using BDCMicrroService.Service.Contract;
using BDCMicrroService.Service.EurekaService;
using DotnetEureka;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BDCMicrroService.Controllers
{
    /// <summary>
    /// API Controller
    /// </summary>
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        ///  GET api/values
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var service = ServiceFactory.GetService<ITest>();
            int name = service.GetSum(1 , 2);
            return new string[] { "value1", $"{name}" };
        }

        /// <summary>
        ///  GET api/values/5
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [Encrypt]
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public ActionResult<string> Get(string name,string pwd)
        {
            string responseStr = string.Empty;
            try
            {
                responseStr = CheckLogin(name,pwd).ToString();

                //if(CheckLogin(name,pwd))
                //{
                //    responseStr = "成功";
                //}
            }
            catch(Exception ex)
            {
                responseStr = ex.Message;
            }
            return responseStr;
        }

        private bool CheckLogin(string username, string password)
        {
            var wrapper = new DotnetEureka<ICheckLogin>();
            string token = wrapper.Service.GetToken(username, password);
            return wrapper.Service.CheckToken(token);
        }

        private string GetAuth()
        {
            try
            {
                var Checkwappar = new DotnetEureka<ICheckClient>();

                return Checkwappar.Service.GetToken("ace-zh02", "123456").ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        ///  POST api/values
        /// </summary>
        /// <param name="value"></param>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        ///  PUT api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// DELETE api/values/5
        /// </summary>
        /// <param name="id"></param>
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
