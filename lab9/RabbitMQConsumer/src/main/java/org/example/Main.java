package org.example;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;
import com.rabbitmq.client.DeliverCallback;
import java.nio.charset.StandardCharsets;
import java.text.SimpleDateFormat;
import java.util.Date;

public class Main {
    private static int endMsgs = 0;

    public static void main(String[] args) throws Exception{

        MyData.info();

        ConnectionFactory factory = new ConnectionFactory();
        factory.setHost("localhost");
        SimpleDateFormat sdf = new SimpleDateFormat("hh:mm:ss");
//        factory.setPort(5672);
//        factory.setUsername("user");
//        factory.setPassword("123");
        Connection connection = factory.newConnection();
        Channel channel = connection.createChannel();
        channel.queueDeclare("hello", false, false, false, null);

        DeliverCallback deliverCallback = (consumerTag, delivery) -> {
            String message = new String(delivery.getBody(), StandardCharsets.UTF_8);
//            System.out.println(" [x] Received '" + message + "'");

            if (message.contains("ended producing")) {
                System.out.println(" [x] Received '" + message + "'");
                endMsgs++;
                if (endMsgs >= 2) {
                    System.out.println("Received 2 end messages.");
                    channel.basicCancel(consumerTag);
                    try {
                        channel.close();
                        connection.close();
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
            else {

                Gson json = new GsonBuilder().setDateFormat("hh:mm:ss").create();
                Message msg = json.fromJson(message, Message.class);

                System.out.print("New msg: \t\tName: "+ msg.text);
                System.out.print("\tTime: " + sdf.format(msg.time));
                System.out.println("\tNumber: " + msg.number);
            }
        };
        channel.basicConsume("hello", true, deliverCallback, consumerTag -> { });
    }
}

class Message{
    public Message(String text, Date time, int number){
        this.text = text;
        this.time = time;
        this.number = number;
    }
    String text;
    Date time;
    int number;
}