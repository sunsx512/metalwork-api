using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.Common
{
    public interface IController<T> where T : class, new()
    {
        [HttpGet]
        ActionResult<string> Get();
        [HttpPost]
        ActionResult<string> Post(T t);
        [HttpPut]
        ActionResult<string> Put(T t);
        [HttpDelete]
        ActionResult<string> Delete(int id);
    }
}
