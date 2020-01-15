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
        ActionResult<common.response<T>> Get();
        [HttpPost]
        ActionResult<common.response> Post(T t);
        [HttpPut]
        ActionResult<common.response> Put(T t);
        [HttpDelete]
        ActionResult<common.response> Delete(int id);
    }
}
