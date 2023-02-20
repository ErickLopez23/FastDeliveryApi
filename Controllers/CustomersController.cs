using Microsoft.AspNetCore.Mvc;

using FastDeliveryApi.Entity;
using FastDeliveryApi.Repositories.Interfaces;
using FastDeliveryApi.Models;

namespace FastDeliveryApi.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomersController(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    private readonly IUnitOfWork _unitOfWork;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> Get()
    {
        var customers = await _customerRepository.GetAll();
        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(
            request.Name,
            request.PhoneNumber,
            request.Email,
            request.Address
        );

        _customerRepository.Add(customer);

        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCustomerById),
            new { id = customer.Id },
            customer
        );
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        if (request.Id != id)
        {
            return BadRequest("Body Id is not equal than Url Id");
        }

        var customer = await _customerRepository.GetCustomerById(id);
        if (customer is null)
        {
            return NotFound($"Customer Not Found With th Id {id}");
        }

        customer.ChangeName(request.Name);
        customer.ChangePhoneNumber(request.PhoneNumber);
        customer.ChangeEmail(request.Email);
        customer.ChangeAddress(request.Address);
        customer.ChangeStatus(request.Status);

        _customerRepository.Update(customer);

        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCustomerById(int id, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerById(id);
        if (customer is null)
        {
            return NotFound($"Customer Not Found With th Id {id}");
        }
        return Ok(customer);
    }

    // [HttpGet("{id}")]
    // public ActionResult<IEnumerable<Customer>>  GetById(int id)
    // {
    //     var customers = _context.Customers.Find(id);
    //     return Ok(customers);
    // }

    // [HttpPost]
    // public ActionResult<IEnumerable<Customer>>  Post(Customer customers)
    // {
    //     _context.Customers.Add(customers);
    //     _context.SaveChanges();
    //     return Created($"/id?id={customers.Id}", customers);
    // }

    // [HttpPut]
    // public ActionResult<IEnumerable<Customer>>  Put(Customer customersToUpdate)
    // {
    //     _context.Customers.Update(customersToUpdate);
    //     _context.SaveChanges();
    //     return NoContent();
    // }

    // [HttpDelete("{id}")]
    // public ActionResult<IEnumerable<Customer>>  Delete(int id)
    // {
    //     var customersToDelete = _context.Customers.Find(id);
    //     if (customersToDelete == null)
    //     {
    //         return NotFound();
    //     }
    //     _context.Customers.Remove(customersToDelete);
    //     _context.SaveChanges();
    //     return NoContent();
    // }

}
