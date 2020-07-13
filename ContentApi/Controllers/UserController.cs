﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLLayer.Interface;
using DataAccessLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserBL _userBL;
        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
        }
        // GET: api/User

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_userBL.GetAll().ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            var item = _userBL.GetById(id);

            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);

        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] User userDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _userBL.InsertUser(userDetails);

            return CreatedAtAction("Get", new { id = userDetails.Id }, userDetails);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromBody] User userDetails)
        {
            _userBL.UpdateUser(userDetails);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existingItem = _userBL.GetById(id);

            if (existingItem == null)
            {
                return NotFound();
            }
            _userBL.DeleteUser(id);

            return Ok();
        }
    }
}