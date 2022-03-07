const {Kafka} = require("kafkajs");
const msg = process.argv[2];

const run = async () => {
    try{
        const kafka = new Kafka({
            "clientId":"myapp",
            "brokers":["localhost:9092"]
        });

        const producer = kafka.producer();
        await producer.connect();
        console.log("Connected to Kafka");

        // This is not necessary it is for fun to control the partition according to rule A-M=0, N-Z=1
        const partition = msg[0] < "N" ? 0 : 1;
        // notice no key here, but we play with partitions we don't have to, it is for fun :)
        await producer.send({
            "topic": "Users",
            "messages":[
                {
                    "value": "Hello World",
                    partition: partition
                }
            ]
        });
        await producer.disconnect();

    }catch(e){
        console.error(`Error: ${e}`);
    }finally {
        process.exit();
    }
}

run();