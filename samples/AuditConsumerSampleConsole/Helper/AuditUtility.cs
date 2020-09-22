using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Force.DeepCloner;
using IAS.Audit;

namespace AuditConsumerSampleConsole.Helper
{
    public static class AuditUtility
    {


        public static AuditEvent GenerateFakeMasterDealerAuditEvent()
        {
            var randomMdId = new Random().Next(10, 20);
            var userId = new Random().Next(100, 1000);

            var address=new Faker<Address>()
                .RuleFor(a => a.City, faker => faker.Address.City())
                .RuleFor(a => a.Street, faker => faker.Address.StreetName())
                .RuleFor(a => a.Zip, faker => faker.Address.ZipCode())
                .RuleFor(a => a.Country, _ => "US")
                .Generate(); ;

            var masterDealer = new Faker<MasterDealer>()
                .RuleFor(dealer => dealer.MasterDealerName, faker => faker.Company.CompanyName())
                .RuleFor(dealer => dealer.MasterDealerId, _ => randomMdId)
                .RuleFor(dealer => dealer.ContractId, faker => faker.Random.Int())
                .RuleFor(dealer => dealer.Email, faker => faker.Internet.Email())
                .RuleFor(dealer => dealer.PhoneNumber, faker => faker.Phone.PhoneNumber())
                .RuleFor(dealer => dealer.Address, _ => address)
                .Generate();
                


           
            var initialState = masterDealer.DeepClone();

            var faker= new Faker("en");


            masterDealer.Email = faker.Internet.Email();
            masterDealer.PhoneNumber= faker.Phone.PhoneNumber();


            var auditEvent = new AuditEvent()
            {
                ApplicationName = "Insight - MasterDealer Module",
                EntityId = randomMdId,
                AuditText = $"MD with id {randomMdId} has been mutated",
                EventType = AuditEventType.EntityMutation,
                UserId = userId.ToString(),
                TimeStamp = DateTime.Now,
                Target = new AuditEntity
                {
                    InitialState = initialState,
                    FinalState = masterDealer,
                    Name = masterDealer.GetType().Name,
                    FullyQualifiedName = masterDealer.GetType().FullName
                }

            };

            return auditEvent;

        }
        
    }
}
