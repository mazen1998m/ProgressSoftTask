using Muslim.Filter;

namespace Muslim.Domain.BusinessCards;

//using Lampda = Expression<Func<BusinessCard, bool>>;
public class BusinessCardFilter : Filter<BusinessCard>
{

    //public string? Name { get; set; }



    //public Lampda _Name() => x => x.Name == Name;

    protected override void ApplyFilter()
    {
        //AddFilter(!Name!.IsNullOrEmpty(), _Name());

    }
}


