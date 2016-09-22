HOLMS.Messaging
===============

This module implements the messaging layer of HOLMS. It's basically syntactic
sugar over the RabbitMQ library, with a few nice benefits:

(1) Users don't have to learn another complicated library (RabbitMQ) with its
    idiosyncratic arguments and options
(2) The RabbitMQ components are hidden from the interface, removing the need
    for direct inclusion of the RabbitMQ library
(3) The component enforces a constrained style, including setup and publication
	to an (internally) named exchange, queue setup/teardown for listeners,
	use of pub/sub style, and automatic message encoding/decoding using the .NET
	binary serializer
(4) Simplified interfaces for testing

Use
===

Start by creating a connection factory, the source of connections to the messaging
system:

var cf = new MessageConnectionFactory("localhost");

Next, open a connection:

var cn = cf.OpenConnection();

Connections are freely shareable between threads, and are appropriate objects
for inclusion in static DI/dependency injection container. However, dependency
management systems should still maintain a reference to the connection factory,
to re-establish the connection in case it is interrupted.

Next, create a channel -- this must be used by only one thing at once, it is
not "thread safe":

var ch = cn.GetChannel();

Channels are the object on which publication and subscription occurs.

To publish an event:

var dto = new DTOForPublication();
ch.Publish("context.subject", dto);

Be sure to include messages here: https://shortbar.atlassian.net/wiki/display/PMC/Messaging+Design

To subscribe to all messages on a topic:

void d(object msg) {
  if (d is OneTypeOfMessage) {
    ...
  } else if (d is SecondTypeOfMessage) {
    ...
  }
}

var listener = ch.CreateListenerForTopics(d,
  new string[] {"operations.checkins", "operations.checkouts"});

listener.Start();

Listeners can be started and stopped at any time. Retain a reference to them
in a component with an ongoing subscription.

I hope this helps. - DA
