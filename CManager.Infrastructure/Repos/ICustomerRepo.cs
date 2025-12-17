using CManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CManager.Infrastructure.Repos;

public interface ICustomerRepo
{
    List<CustomerModel> GetAllCustomers();
    bool SaveCustomers(List<CustomerModel> customers);
}

