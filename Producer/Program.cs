using MassTransit;
using Shared;
using System;
using System.Threading.Tasks;
 
namespace Producer
{
    public class Message : IMessage
    {
        public string Text { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            string rabbitMqUri = "amqps://ziavcpxm:RAzXXKKajEqU25FjnbQJ24Lsx8qcDuEw@hawk.rmq.cloudamqp.com/ziavcpxm";
            string queue = "test-queue";
            string userName = "ziavcpxm";
            string password = "RAzXXKKajEqU25FjnbQJ24Lsx8qcDuEw";
 
            var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
            {
                factory.Host(rabbitMqUri, configurator =>
                {
                    configurator.Username(userName);
                    configurator.Password(password);
                });
            });
 
            await Task.Run(async () =>
            {
                while (true)
                {
                    Console.Write("Mesaj yaz : ");
                    Message message = new Message
                    {
                        Text = Console.ReadLine()
                    };
                    if (message.Text.ToUpper() == "C")
                        break;
                    await bus.Publish<IMessage>(message);
                    Console.WriteLine("");
                }
            });
        }
    }
}