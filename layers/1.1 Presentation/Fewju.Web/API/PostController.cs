using AutoMapper;
using Fewju.Application.Service;
using Fewju.Domain.Context;
using Fewju.Domain.Entity;
using Fewju.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fewju.Web.API
{
    public class PostController : ApiController
    {

        PostService PostService = new PostService();

        // GET api/post
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/post/5
        public string Get(int id)
        {
            return "value";
        }


        private string replaceAll(string str, string array)
        {
            foreach (char chars in array)
            {
                str = str.Replace(chars.ToString(), "");
            }
            return str;
        }

        // POST api/post
        public long Post([FromBody]PostDTO dto)
        {

            dto.UrlName = replaceAll(dto.UrlName, "!@#$%^&*()+{}[]|\\:;\"'<>?,./").ToLower();
            Mapper.CreateMap<PostDTO, Post>();

            Post post = Mapper.Map<Post>(dto);
            post.Content.Content = dto.ContentContent;
            return PostService.Create(post, dto.Domain);
        }

        // PUT api/post/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/post/5
        public void Delete(int id)
        {
        }
    }
}
