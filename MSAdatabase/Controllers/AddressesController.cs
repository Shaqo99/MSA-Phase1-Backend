using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSAdatabase.Data;
using MSAdatabase.Models;

namespace MSAdatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly StudentContext _context;

        public AddressesController(StudentContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            return await _context.Address.Include(c => c.Student).ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.addressID)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses

        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.addressID }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return address;
        }

        // Adding address based off his/her studentId.
        [HttpPost("{id}")]
        public async Task<ActionResult<Address>> PostStudentAddress(Address address, int id)
        {
            // checks if student has an entry in the db.

            var student = await _context.Student.FindAsync(id);

            // If no student entry then a notfound is thrown

            if (student == null)
            {
                return NotFound();
            }

            // Creates a new entry into the Address table. 

            Student newStudent = _context.Student.Single(c => c.studentId == id);
            Address newAddress = new Address
            {
                streetNumber = address.streetNumber,
                street = address.street,
                suburb = address.suburb,
                city = address.city,
                postcode = address.postcode,
                studentId = id,
                Student = newStudent
            };
            _context.Address.Add(newAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = newAddress.addressID }, newAddress);


        }


        // PUT: api/StudentAddress/{studentid}
        // Allows students to change their own addresses using the address ID and their student ID.

        [HttpPut("/api/StudentAddress/{id}")]
        public async Task<IActionResult> PutStudentAddress(int id, Address address)
        {
            if (_context.Address.Any(c => c.studentId == id))
            {
                
                Address existingAddress = _context.Address.Single(c => c.studentId == id && c.addressID == address.addressID);
                
                if (existingAddress != null)
                {
                    existingAddress.streetNumber = address.streetNumber;
                    existingAddress.street = address.street;
                    existingAddress.suburb = address.suburb;
                    existingAddress.city = address.city;
                    existingAddress.postcode = address.postcode;
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AddressExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                };
            }
            else
            {
                return BadRequest();
            }


            return NoContent();
        }






        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.addressID == id);
        }
    }
}