# run  
Notice that we are not using here docker-compose, we are running all the docker images in different terminals, you can always add `-d` and run it in one, but I just like it that way.  

To see all docker containers running on your machine, notice that `-a` is important. Because after first time you run the `run` command you need to stop the image and remove it if it is problematic.  
`docker ps -a`  
`docker stop kafka`  
`docker rm kafka`  
  
If you see error: `docker: Error response from daemon: Conflict. The container name "/kafka" is already in use by container`, make sure you `stop` the image and then remove it. Or you can just connect to it if everything is fine like:
`docker start -a kafka`  
`docker start -a zookeeper`  
  

Please be aware that when you run `docker run` you can not stop it by running `ctrl+c` you need to run `docker stop kafka` or other image name in other terminal to stop it.  
  
In one terminal run zookeeper  
`docker run --name zookeeper -p 2181:2181 zookeeper`  

In second run kafka, please use your ip address instead of `192.168.1.157` (change in 2 places)  
`docker run --name kafka -p 9092:9092 -e KAFKA_ZOOKEEPER_CONNECT=192.168.1.157:2181 -e KAFKA_ADVERTISED_LISTENERS=PLAINTEXT://192.168.1.157:9092 -e KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR=1 confluentinc/cp-kafka`  
  