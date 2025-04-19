using Muslim.Data.Repository;
using Muslim.Domain.BusinessCards;
using Muslim.GenericService;

namespace Muslim.Application.BusinessCards;

internal class BusinessCardService(IBusinessCardRepository repository) : Service<BusinessCard>(repository), IBusinessCardService
{

}

public interface IBusinessCardService : IService<BusinessCard>
{
}


