#region Copyright 2017 D-Haven.org

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using DotnetEureka.Compiler;
using MicroServiceHelper.DotnetEureka;

namespace DotnetEureka
{
    public class DotnetEureka<TService>
        where TService : class // Really an interface
    {
        private readonly string className;
        private TService service;

        public DotnetEureka()
        {
            className = TypeFactory.Compiler.RegisterInterface(typeof(TService).GetTypeInfo());
        }
        public TService Service => service ?? (service = TypeFactory.CreateInstance<TService>(className));
    }

    //public class DotnetEurekaInterface
    //{
    //    private readonly string className;
    //    private object service;
    //    public DotnetEurekaInterface(string srvName, string route, string requestMethod, string returnType, List<ParameterModel> paramsList)
    //    {
    //        var interfaceName = TypeFactory.Compiler.CreateInterface(srvName, route, requestMethod,returnType , paramsList);
    //        var face = TypeFactory.CreateInstance(interfaceName);

    //        var className = TypeFactory.Compiler.RegisterInterface(face.GetType().GetTypeInfo());
    //    }
    //    public object Service =>  service ?? (service = TypeFactory.CreateInstance(className));
    //}




}