using System.ComponentModel.DataAnnotations;
using PlatformService.Domain.Exceptions;

namespace PlatformService.Domain.Models;

public class PlatformAggregate : Entity
{
    public PlatformAggregate(int id, string name, string publisher, string cost)
    {
        Id = id;
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Platform name is empty");
        Name = name;
        Publisher = publisher;
        Cost = cost;
    }
    
    #region props
    public string Name { get; private set; }
    public string Publisher { get;private set; }
    public string Cost { get;private set; }

    #endregion
    
}
