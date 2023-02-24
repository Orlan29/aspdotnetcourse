namespace UnivAssurance.DataAccess.Models;

public class InitData
{
    public static void Begin(){

        DataBase.TPComercials.Add(
            new Comercial {
                ComercialID = 1,
                Name = "Lou",
                SerialNumber = "n20"
            }
        );
        
        DataBase.TPComercials.Add(
            new Comercial {
                ComercialID = 2,
                Name = "Michele",
                SerialNumber = "n20"
            }
        );
    
        DataBase.TPersons.Add(
            new Person {
                PersonID = 1,
                TypePart = "cni",
                NumberTypePart = "n02",
                Name = "John",
                FirstName = "Issac",
                Phone = "074856712",
                Sex = "M",
                MaritalStatus = "Marié",
                NumberChildren = 1,
                Employer = "Spvie"
            }
        );

        DataBase.TPersons.Add(
            new Person {
                PersonID = 2,
                TypePart = "cni",
                NumberTypePart = "n03",
                Name = "Mozart",
                FirstName = "Kiang",
                Phone = "074856712",
                Sex = "M",
                MaritalStatus = "Marié",
                NumberChildren = 3,
                Employer = "Spvie"
            }
        );

        DataBase.TPersons.Add(
            new Person {
                PersonID = 3,
                TypePart = "Password",
                NumberTypePart = "n04",
                Name = "MBALI",
                FirstName = "Aube",
                Phone = "074856712",
                Sex = "M",
                MaritalStatus = "Celibataire",
                NumberChildren = 0,
                Employer = "Spvie"
            }
        );

        DataBase.TProducts.Add(
            new Product {
                ProductID = 1,
                ProductName = "Assurance vie",
                Price = 1000,
                CampaignStartDate = new DateTime().ToLocalTime(),
                CampaignEndDate = new DateTime().ToLocalTime()
            }
        );

        DataBase.TProducts.Add(
            new Product {
                ProductID = 2,
                ProductName = "Assurance décès",
                Price = 2000,
                CampaignStartDate = new DateTime().ToLocalTime(),
                CampaignEndDate = new DateTime().ToLocalTime()
            }
        );

        DataBase.TProducts.Add(
            new Product {
                ProductID = 3,
                ProductName = "Assurance mixte (vie et décès)",
                Price = 5000,
                CampaignStartDate = new DateTime().ToLocalTime(),
                CampaignEndDate = new DateTime().ToLocalTime()
            }
        );
    
        DataBase.TSubscriptions.Add(
            new Subscription {
                SubscriptionID = 1,
                PersonID = 1,
                ProductID = 1,
                SubscriptionDate = new DateTime().ToLocalTime(),
                SubscriptionState = 30,
                ComercialID = 1
            }
        );

        DataBase.TSubscriptions.Add(
            new Subscription {
                SubscriptionID = 1,
                PersonID = 2,
                ProductID = 1,
                SubscriptionDate = new DateTime().ToLocalTime(),
                SubscriptionState = 20,
                ComercialID = 1
            }
        );

        DataBase.TSubscriptions.Add(
            new Subscription {
                SubscriptionID = 1,
                PersonID = 3,
                ProductID = 1,
                SubscriptionDate = new DateTime().ToLocalTime(),
                SubscriptionState = 14,
                ComercialID = 1
            }
        );

        DataBase.TSubscriptions.Add(
            new Subscription {
                SubscriptionID = 1,
                PersonID = 2,
                ProductID = 2,
                SubscriptionDate = new DateTime().ToLocalTime(),
                SubscriptionState = 10,
                ComercialID = 1
            }
        );
        
        DataBase.TSubscriptions.Add(
            new Subscription {
                SubscriptionID = 1,
                PersonID = 3,
                ProductID = 1,
                SubscriptionDate = new DateTime().ToLocalTime(),
                SubscriptionState = 40,
                ComercialID = 1
            }
        );
    }
}