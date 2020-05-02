## How run project

```
sudo docker network create distributed-cluster
sudo docker run --name cluster1 -d -p 30001:27017 --net distributed-cluster mongo mongod --replSet "replica"
sudo docker run --name cluster2 -d -p 30002:27017 --net distributed-cluster mongo mongod --replSet "replica"
sudo docker run --name cluster3 -d -p 30003:27017 --net distributed-cluster mongo mongod --replSet "replica"
sudo docker exec -it cluster1 mongo

sett = {
      "_id" : "replica",
      "members" : [
          {
              "_id" : 0,
              "host" : "cluster1:27017"
          },
          {
              "_id" : 1,
              "host" : "cluster2:27017"
          },
          {
              "_id" : 2,
              "host" : "cluster3:27017"
          }
      ]
  }

rs.initiate(sett)

sudo docker build -t distributed-database .
sudo docker run --net distributed-cluster distributed-database
```
