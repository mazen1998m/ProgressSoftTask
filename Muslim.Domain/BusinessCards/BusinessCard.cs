using Microsoft.EntityFrameworkCore;
using Muslim.ConfigureTable;
using Muslim.Domain.Enums;

namespace Muslim.Domain.BusinessCards;
public class BusinessCard : Entity
{


    public string Name { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Photo { get; set; }
    public string Address { get; set; }

    internal class Configuration : ConfigureTable<BusinessCard>
    {
        protected override void ConfigureCustomizations()
        {
            Builder.ToTable(nameof(BusinessCard) + "s");
        }
    }

}
