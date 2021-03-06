﻿using System.Linq;
using System.Threading.Tasks;
using Loggifyio.Api.Models;
using Loggifyio.AutoMapperSetup;
using Loggifyio.Data.Model;
using Loggifyio.Filters;
using Loggifyio.Queries.Processors;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Loggifyio.Controllers
{
    [EnableCors("loggifyio-client")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserQueryProcessor _query;
        private readonly IAutoMapper _mapper;

        public UserController(IUserQueryProcessor query, IAutoMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet]
        [QueryableResult]
        public IQueryable<UserModel> Get()
        {
            var result = _query.Get();
            var models = _mapper.Map<User, UserModel>(result);
            return models;
        }

        [HttpGet("{id}")]
        [ValidateModel]
        public UserModel Get(int id)
        {
            var item = _query.Get(id);
            var model = _mapper.Map<UserModel>(item);
            return model;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<UserModel> Post([FromBody]CreateUserModel requestModel)
        {
            var item = await _query.Create(requestModel);
            var model = _mapper.Map<UserModel>(item);
            return model;
        }

        [HttpPost("{id}/password")]
        [ValidateModel]
        public async Task ChangePassword(int id, [FromBody]ChangeUserPasswordModel requestModel)
        {
            await _query.ChangePassword(id, requestModel);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<UserModel> Put(int id, [FromBody]UpdateUserModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<UserModel>(item);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _query.Delete(id);
        }
    }
}
