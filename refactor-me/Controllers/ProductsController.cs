using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using AutoMapper;
using refactor_me.Models;
using refactor_me.Services;
using refactor_me.DTO;

namespace refactor_me.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Version 2 of the ProductsController 
    /// with improved functionalities
    /// </summary>
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        //TODO implement swagger

        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Products
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(ItemLists<ProductDto>))]
        public IHttpActionResult Get()
        {
            var result = new ItemLists<ProductDto>()
            {
                Items = _mapper.Map<IEnumerable<ProductDto>>(_service.GetAll())
            };
            return Ok(result);
        }

        // GET: api/Products?name=
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(ProductDto))]
        public async Task<IHttpActionResult> Get(string name)
        {
            return Ok(_mapper.Map<Product,ProductDto>(await _service.GetByName(name)));
        }
               
        [Route("{id}")]
        [HttpGet]
        [ResponseType(typeof(ProductDto))]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var product = await _service.GetById(id);

            if (product != null)
            {
                return Ok(_mapper.Map<ProductDto>(product));
            }
            return NotFound();
        }

        // POST: api/Products
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(OkResult))]
        public async Task<IHttpActionResult> Post([FromBody]ProductDto productDto)
        {
            await _service.Create(_mapper.Map<Product>(productDto));

            return Ok();
        }

        // PUT: api/Products/5
        // /products/{id} is the Id needed? surely we can get the id from the payload
        [Route("{id}")]
        [HttpPut]
        [ResponseType(typeof(OkResult))]
        public async Task<IHttpActionResult> Put(Guid id, [FromBody] ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _service.Update(id, product);

            return Ok();
        }

        // DELETE: api/Products/5
        [Route("{id}")]
        [HttpDelete]
        [ResponseType(typeof(OkResult))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
