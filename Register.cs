using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DistributedDatabase
{
    public class Register
    {
        [BsonElement("plate")]
        public string PlateNumber { get; set; }
        [BsonElement("registerYear")]
        public DateTime RegisterYear { get; set; }
        [BsonElement("car")]
        public Car Car { get; set; }
        [BsonElement("person")]
        public Person Person { get; set; }

        public Register() { }

        public Register(string plateNumber, DateTime registerYear, Car car, Person person)
        {
            PlateNumber = plateNumber;
            RegisterYear = registerYear;
            Car = car;
            Person = person;
        }
    }
}
