using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using loggifyio.AutoMapperSetup;
using loggifyio.Data.Model;
using loggifyio.Filters;
using Loggifyio.Api.Models;
using Loggifyio.Api.Models.traininglog;
using Loggifyio.Queries;
using Loggifyio.Queries.Processors;
using Microsoft.AspNetCore.Mvc;

namespace loggifyio.Controllers
{
    [Route("api/[controller]")]
    public class TraininglogController : Controller
    {
        private readonly ITraininglogQueryProcessor _query;
        private readonly IAutoMapper _mapper;

        public TraininglogController(ITraininglogQueryProcessor query, IAutoMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet]
        [QueryableResult]
        public IQueryable<TrainingLogModel> Get()
        {
            var result = _query.Get();
            var models = _mapper.Map<Traininglog, TrainingLogModel>(result);
            return models;
        }

        [HttpGet("{id}")]
        [ValidateModel]
        public TrainingLogModel Get(int id)
        {
            var item = _query.Get(id);
            var model = _mapper.Map<TrainingLogModel>(item);
            return model;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<TrainingLogModel> Post([FromBody]CreateTrainingLogModel requestModel)
        {
            var item = await _query.Create(requestModel);
            var model = _mapper.Map<TrainingLogModel>(item);
            return model;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<TrainingLogModel> Put(int id, [FromBody]UpdateTrainingLogModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<TrainingLogModel>(item);
            return model;
        }

        [HttpDelete("{id}")]
        [ValidateModel]
        public async Task Delete(int id)
        {
            await _query.Delete(id);
        }
    }
}
