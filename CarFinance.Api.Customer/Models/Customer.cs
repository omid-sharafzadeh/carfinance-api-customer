using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarFinance.Api.Customer.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Email { get; private set; }

        [BsonConstructor]
        public Customer(string email)
        {
            Email = email;
        }
    }
}