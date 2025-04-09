using Ehsan.CSMS.Dtos.CustomerDto;
using Ehsan.CSMS.Entities;
using Ehsan.CSMS.Helper;
using Ehsan.CSMS.IRepositories;
using Ehsan.CSMS.IService;
using Ehsan.CSMS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace Ehsan.CSMS.Services;

public class CustomerService : ApplicationService, ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    public CustomerService(ICustomerRepository costumerRepository)
    {
        _customerRepository = costumerRepository;
    }

    public async Task<CustomerResponse> AddAsync(CustomerAddRequest? customer)
    {
        if (customer is null)
        { throw new ArgumentNullException(nameof(customer)); }
        ValidationHelper.ValidateModel(customer);
        if (await _customerRepository.GetByMobileNumberAsync(customer.MobileNumber!) != null)
        { throw new ArgumentException($"{customer.MobileNumber} already exit"); }


        var customerEntity = ObjectMapper.Map<CustomerAddRequest, Customer>(customer);
        var addedCustomer = await _customerRepository.AddAsync(customerEntity);
        return ObjectMapper.Map<Customer, CustomerResponse>(addedCustomer);
    }

    public async Task<bool> DeleteByIdAsync(Guid? id)
    {
        if (id is null)
        { throw new ArgumentNullException(nameof(id)); }
        return await _customerRepository.DeleteByIdAsync(id.Value);
    }

    public async Task<IEnumerable<CustomerResponse>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return ObjectMapper.Map<List<Customer>, List<CustomerResponse>>(customers);
    }

    public async Task<CustomerResponse?> GetByIdAsync(Guid? id)
    {
        if (id is null)
        { throw new ArgumentNullException(nameof(id)); }
        var customer = await _customerRepository.GetByIdAsync(id.Value);
        return ObjectMapper.Map<Customer?, CustomerResponse>(customer);
    }

    public async Task<CustomerResponse?> GetByMobile(string? mobile)
    {
        if (mobile is null)
        { throw new ArgumentNullException(nameof(mobile)); }
        var customer = await _customerRepository.GetByMobileNumberAsync(mobile);
        return ObjectMapper.Map<Customer?, CustomerResponse>(customer);
    }

    public Task<int> GetLoyaltyPointByIdAsync(Guid? id)
    {
        if (id is null)
        { throw new ArgumentNullException(nameof(id)); }
        return _customerRepository.GetLoyaltyPointByIdAsync(id.Value);
    }

    public async Task<IEnumerable<CustomerResponse>> SearchAsync(CustumerSearchCriteria searchCriteria)
    {
        var customers = await _customerRepository.SearchAsync(searchCriteria);
        return ObjectMapper.Map<List<Customer>, List<CustomerResponse>>(customers);
    }

    public async Task<CustomerResponse> UpdateAsync(CustomerUpdateRequest? customer)
    {
        if (customer is null)
        { throw new ArgumentNullException(nameof(customer)); }
        ValidationHelper.ValidateModel(customer);
        if(customer.Id == Guid.Empty)
        { throw new ArgumentNullException(nameof(customer.Id)); }

        // unique check
        var customerToCheck = await _customerRepository.GetByMobileNumberAsync(customer.MobileNumber!);
        if ( customerToCheck != null && customerToCheck.Id != customer.Id)
        { throw new ArgumentException($"{customer.MobileNumber} already exit"); }

        // valid id check 
        var customerToUpdate = await _customerRepository.GetByIdAsync(customer.Id);
        if (customerToUpdate == null)
        {
            { throw new ArgumentException($"Invalid Customer {0}", nameof(customer.Id)); }
        }

        customerToUpdate.Name = customer.Name;
        customerToUpdate.MobileNumber = customer.MobileNumber;
        customerToUpdate.LoyaltyPoint = customer.LoyaltyPoint;
        var updatedCustomer = await _customerRepository.UpdateAsync(customerToUpdate);
        return ObjectMapper.Map<Customer, CustomerResponse>(updatedCustomer);
    }
}
