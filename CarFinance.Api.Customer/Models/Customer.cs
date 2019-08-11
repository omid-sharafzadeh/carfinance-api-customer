using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarFinance.Api.Customer.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        [BsonConstructor]
        public Customer(string email, string firstName, string surname, string password)
        {
            Email = email;
            FirstName = firstName;
            Surname = surname;
            Password = password;
        }
    }
}