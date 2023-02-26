using Microsoft.Extensions.Caching.Memory;

using FastDeliveryApi.Entity;
using FastDeliveryApi.Repositories.Interfaces;

namespace FastDeliveryApi.Repositories;

public class CachedCustomerRepository : ICustomerRepository
{
    private readonly ICustomerRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public CachedCustomerRepository(ICustomerRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public void Add(Customer customer)
    {
        _memoryCache.Remove("getAllCustomers");
        _decorated.Add(customer);
    }


    // public async Task<IReadOnlyCollection<Customer>> GetAll()
    // {
    //     string key = "getAllCustomers";
    //     return _memoryCache.GetOrCreateAsync(
    //         key,
    //         entry =>
    //         {
    //             _decorated.GetAll();
    //         }
    //     );
    // }
    public async Task<IReadOnlyCollection<Customer>> GetAll()
    {
         string key = $"customerGetAll";
        return _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                var getting =  _decorated.GetAll();
                return getting;
            }
        );
    }


    public Task<Customer?> GetCustomerById(int id, CancellationToken cancellationToken = default)
    {
        string key = $"customer-{id}";
        return _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                return _decorated.GetCustomerById(id, cancellationToken);
            }
        );
    }

    public void Update(Customer customer) =>
        _decorated.Update(customer);
}