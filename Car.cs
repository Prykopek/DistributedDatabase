using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DistributedDatabase
{
    public class Car
    {
        [BsonElement("vin")]
        public string Vin { get; set; }
        [BsonElement("brand")]
        public string Brand { get; set; }
        [BsonElement("model")]
        public string Model { get; set; }
        [BsonElement("productionYear")]
        public DateTime ProductionYear { get; set; }

        public Car() { }

        public Car(string vin, string brand, string model, DateTime productionYear)
        {
            Vin = vin;
            Brand = brand;
            Model = model;
            ProductionYear = productionYear;
        }
    }
}
