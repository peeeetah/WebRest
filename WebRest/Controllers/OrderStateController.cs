﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;
using WebRest.Interfaces;
namespace WebRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStateController : ControllerBase, iController<OrderState>
    {
        private readonly WebRestOracleContext _context;

        public OrderStateController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderState
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderState>>> Get()
        {
            return await _context.OrderStates.ToListAsync();
        }

        // GET: api/OrderState/5
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrderState>> Get(string id)
        {
            var orderstate = await _context.OrderStates.FindAsync(id);

            if (orderstate == null)
            {
                return NotFound();
            }

            return orderstate;
        }

        // PUT: api/OrderState/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrderState orderstate)
        {
            if (id != orderstate.OrderStateId)
            {
                return BadRequest();
            }
            _context.OrderStates.Update(orderstate);



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderState
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderState>> Post(OrderState orderstate)
        {
            _context.OrderStates.Add(orderstate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderState", new { id = orderstate.OrderStateId }, orderstate);
        }

        // DELETE: api/OrderState/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var orderstate = await _context.OrderStates.FindAsync(id);
            if (orderstate == null)
            {
                return NotFound();
            }

            _context.OrderStates.Remove(orderstate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderStateExists(string id)
        {
            return _context.OrderStates.Any(e => e.OrderStateId == id);
        }
    }
}
