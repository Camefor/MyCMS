﻿using Camefor.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Camefor.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase {

        private readonly IAdvertisementServices _advertisementServices;
        private readonly ILogger<TestController> _logger;
        public TestController(IAdvertisementServices advertisementServices, ILogger<TestController> logger) {
            _advertisementServices = advertisementServices;
            _logger = logger;
        }
        // GET: api/Test
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }



        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id) {
            return "value";
        }

        // POST: api/Test
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT: api/Test/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}