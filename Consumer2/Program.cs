using MassTransit;
using Shared;
using System;
using System.Threading.Tasks;
 
namespace Consumer2
{
    public class Message : IMessage
    {
        public string Text { get; set; }
    }
    public class MessageConsumer : IConsumer<IMessage>
    {
        public async Task Consume(ConsumeContext<IMessage> context)
            => Console.WriteLine($"test-queue-2 Gelen mesaj : {context.Message.Text}");
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            string rabbitMqUri = "amqps://ziavcpxm:RAzXXKKajEqU25FjnbQJ24Lsx8qcDuEw@hawk.rmq.cloudamqp.com/ziavcpxm";
            string queue = "test-queue-2";
 
            string userName = "ziavcpxm";
            string password = "RAzXXKKajEqU25FjnbQJ24Lsx8qcDuEw";
 
            var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
            {
                factory.Host(rabbitMqUri, configurator =>
                {
                    configurator.Username(userName);
                    configurator.Password(password);
                });
 
                factory.ReceiveEndpoint(queue, endpoint => endpoint.Consumer<MessageConsumer>());
            });
            await bus.StartAsync();
            Console.ReadLine();
            await bus.StopAsync();
        }
    }
}