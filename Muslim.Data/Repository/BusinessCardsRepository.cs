using Muslim.Domain.BusinessCards;
namespace Muslim.Data.Repository;

internal class BusinessCardRepository : Repository<BusinessCard>, IBusinessCardRepository
{

}

public interface IBusinessCardRepository : IRepository<BusinessCard>
{
}

