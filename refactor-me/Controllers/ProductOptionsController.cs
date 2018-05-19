using System.Linq;
using System.Web.Http;
using refactor_me.Models;
using refactor_me.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Web.Http.Results;
using AutoMapper;
using refactor_me.DTO;

namespace refactor_me.Controllers
{
    [RoutePrefix("api/products/{productId}")]
    public class ProductOptionsController : ApiController
    {
        private readonly IProductOptionsService _service;
        private readonly IMapper _mapper;
        public ProductOptionsController(IProductOptionsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        // find all options for a specified product
        [Route("options")]
        [HttpGet]
        [ResponseType(typeof(ItemLists<ProductOptionsDto>))]
        public IHttpActionResult Get(Guid productId)
        {
            var result = new ItemLists<ProductOptionsDto>()
            {
                Items = _mapper.Map<IEnumerable<ProductOptionsDto>>(_service.GetAll(productId))
            };
            return Ok(result);
        }

        [Route("options/{optionId}")]
        [HttpGet]
        [ResponseType(typeof(ProductOptionsDto))]
        public async  Task<IHttpActionResult> Get(Guid productId, Guid optionId)
        {
            var result = await _service.GetBy(productId, optionId);
            return Ok(_mapper.Map<ProductOptionsDto>(result));
        }

        // POST: api/ProductOptions
        [Route("options")]
        [HttpPost]
        [ResponseType(typeof(OkResult))]
        public async Task<IHttpActionResult> Post(Guid productId, [FromBody]ProductOptionsDto prodOptDto)
        {
            await _service.Create(productId, _mapper.Map<ProductOption>(prodOptDto));

            return Ok();
        }

        // PUT: api/ProductOptions/5
        [Route("options/{optionId}")]
        [HttpPut]
        [ResponseType(typeof(OkResult))]
        public async Task<IHttpActionResult> Put(Guid productId, Guid optionId, [FromBody]ProductOptionsDto prodOpts)
        {
            await _service.Update(productId, optionId, _mapper.Map<ProductOption>(prodOpts));

            return Ok();
        }

        // DELETE: api/ProductOptions/5
        [Route("options/{optionId}")]
        [HttpDelete]
        [ResponseType(typeof(OkResult))]
        public async Task<IHttpActionResult> Delete(Guid productId, Guid optionId)
        {
            await _service.Delete(productId, optionId);

            return Ok();
        }
    }
}
