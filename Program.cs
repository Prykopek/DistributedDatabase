using System;
using System.Diagnostics;
using MongoDB.Driver;
using System.Linq;

namespace DistributedDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var sett = new MongoClientSettings
            {
                Servers = new[]
                {
                    new MongoServerAddress("cluster1", 27017),
                    new MongoServerAddress("cluster2", 27017),
                    new MongoServerAddress("cluster3", 27017)
                },
                ConnectionMode = ConnectionMode.ReplicaSet,
                ReplicaSetName = "replica"
            };

	    
            var mongoClient = new MongoClient(sett);
            var db = mongoClient.GetDatabase("register");
            var registers = db.GetCollection<Register>("registers").AsQueryable(
                new AggregateOptions
                    {
                        AllowDiskUse = true
                    }
                );
            var firstTimer = Stopwatch.StartNew();
            var first = registers.Select(x => new
                {
                    Plate = x.PlateNumber,
                    Registration = x.RegisterYear,
                    Vin = x.Car.Vin,
                    Car = x.Car.Brand + " " + x.Car.Model,
                    Production = x.Car.ProductionYear,
                    Owner = x.Person.FirstName + " " + x.Person.LastName
                }
            ).OrderByDescending(x => x.Registration).ThenByDescending(x => x.Production)
            .ThenByDescending(x => x.Plate).ToList();
            
            firstTimer.Stop();

           foreach (var item in first)
           {
               Console.WriteLine($"Plate: {item.Plate} Registration: {item.Registration:yyyy-MM-dd} Vin: {item.Vin} Car: {item.Car} Production: {item.Production:yyyy-MM-dd} Owner: {item.Owner}");
           }
           
           Console.WriteLine("Execute time: " + firstTimer.ElapsedMilliseconds);
           
           try
           {
               var secondTimer = Stopwatch.StartNew();
               var second = registers.GroupBy(x => new
                        {
                           x.Person.FirstName,
                           x.Person.LastName,
                       }
                   ).Select(x => new
                       {
                           Person = x.Key.FirstName + " " + x.Key.LastName,
                           Register = x.Count()
                       }
                   ).OrderByDescending(x => x.Register).ToList();

               second.RemoveAll(x => x.Person == null);
               
               foreach (var item in second)
               {
                   Console.WriteLine($"Person: {item.Person} Register: {item.Register}");
               }
               
               secondTimer.Stop();
           
               Console.WriteLine($"Execute time: {secondTimer.ElapsedMilliseconds}");
           }
           catch (NullReferenceException e)
           {
               Console.WriteLine(e);
           }
           
           var thirdTimer = Stopwatch.StartNew();
           var third = registers.GroupBy(x => x.Car.Brand).Select(x => new
                   {
                       Car = x.Key,
                       Register = x.Count()
                   }
               ).OrderByDescending(x => x.Register).ToList();
           
           thirdTimer.Stop();
           
           foreach (var item in third)
           {
               Console.WriteLine($"Car: {item.Car} Register: {item.Register}");
           }
           
           Console.WriteLine($"Execute time: {thirdTimer.ElapsedMilliseconds}");
        }
    }
}

