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
        await admin.createTopics(["topic1"]);

    }catch(e){
        console.error(`Error: ${e}`);
    }
}