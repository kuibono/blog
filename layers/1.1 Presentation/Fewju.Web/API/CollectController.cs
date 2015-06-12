using Fewju.Application.Service;
using Fewju.Domain.CollectEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fewju.Web.API
{
    public class CollectController : ApiController
    {

        CollectService CollectService = new CollectService();
        // GET api/collect
        public IEnumerable<SiteSetting> Get()
        {
            return CollectService.GetAll();
        }

        // GET api/collect/5
        public List<SiteSetting> Get(int id)
        {
            return CollectService.GetAll();
        }

        // POST api/collect
        public void Post([FromBody]SiteSetting item)
        {
            CollectService.Create(item);
        }

        // PUT api/collect/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/collect/5
        public void Delete(int id)
        {
        }
    }
}
