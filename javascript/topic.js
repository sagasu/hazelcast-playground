const {Kafka} = require("kafkajs");

const run = async () => {
    try{
        const kafka = new Kafka({
            "clientId":"myapp",
            "brokers":["localhost:9092"]
        });

        const admin = kafka.admin();
        await admin.connect();
        console.log("Connected to Kafka");
        await admin.createTopics({
            topics: [{
                topic: "my-topic",
                numPartitions: 1,
                replicationFactor: 1,
            }]
        });
        await admin.disconnect();

    }catch(e){
        console.error(`Error: ${e}`);
    }finally {
        process.exit();
    }
}

run();