
using Muslim.Domain.BusinessCards;
using Muslim.Domain.BusinessCards.Dtos;

namespace Muslim.Application.BusinessCards.MapperProfile;

public class BusinessCardMapperProfile : Profile
{
    public BusinessCardMapperProfile()
    {
        #region List
        CreateMap<BusinessCard, ListBusinessCardDto>().ReverseMap();
        #endregion

        #region Details
        CreateMap<BusinessCard, DetailsBusinessCardDto>().ReverseMap();
        #endregion

        #region Create
        CreateMap<CreateBusinessCardDto, BusinessCard>().ReverseMap();
        #endregion


        CreateMap<BusinessCard, IdNameDto>().ReverseMap();

        CreateMap<BusinessCard, BusinessCard>().ReverseMap();
    }
}

