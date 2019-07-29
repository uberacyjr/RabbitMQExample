# RabbitMQExample

Notes from pluralsight course: https://app.pluralsight.com/library/courses/rabbitmq-by-example
### RabbitMQ AMQP Protocol

Advanced Message Queue Protocol (0-9-1)
Network Protocol
Enable applications to communicate with the compatible messaging system

### Exchanges
AMQP Entities where message are sent to message broker.
Exchanges take a message and then route it to one or more queues.
Type of routing depends on the type of exchange used in different exchange rules called bindings

### RabbitMQ Exchange Bindings
#### Most important attributes
#### Name
Name of the exchange
#### Durability
Persisting the messages to disk
#### Auto-Delete
Delete message when not needed
#### Arguments
These are message broker-dependent

### Bindings Type
Direct
Ideal for publish a message onto just one queue. A queue bind to a exchange with  a routing key. Example of routing key:

### Fan-out
Routes messages to all queues that are bound to it. Unlike direct exchange, the route key is ignored. Ideal for broadcast routing of messages.

### Topic
Topic exchange route messages to one or many(1, N) based on matches between the message routing key and the patterns that was used to bind the queue to an exchange.

### Headers
Headers exchange is for routing of multiple attributes that are expressed in headers. This means that the routing key is ignored in this type of exchange due to the fact that they can only express one piece of information.

### Queue
Durable: Where the queue and the messages will survive a broker or a server re-start.
Name: Max of 255 caracters.
Work in a first in first out basis.

### Bindings
The purpose of the routing key is to select certain messages published to an exchange to be rooted to hat bound queue. Routing keys acts like a filter.

### Consumers
Applications will register as consumers, or subscribers to a queue.

You can have one or more applications registered as a subscriber to a queue, or a set of queues. And this is a usage scenario when you want to balance a load of applications feeding from the queues in a high volume scenario.


### Code Example
```
ConnectionFactory factory = new ConnectionFactory
{
    // "guest"/"guest" by default, limited to localhost connections
    UserName = "guest",
    Password = "guest",
    VirtualHost = "/",
    HostName = "localhost"
};

IConnection conn = factory.CreateConnection();
//AMQP data channel and provides the AMQP operations
IModel model = conn.CreateModel();
model.ExchangeDeclare("VSExchange", ExchangeType.Direct);

model.QueueDeclare("VSQueue", false, false, false, null);
model.QueueBind("VSQueue", "VSExchange", "", null);
```


