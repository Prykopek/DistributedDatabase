using MongoDB.Bson.Serialization.Attributes;

namespace DistributedDatabase
{
    public class Person
    {
        [BsonElement("personalId")]
        public string PersonalId { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        public Person() { }

        public Person(string personalId, string firstName, string lastName)
        {
            PersonalId = personalId;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
