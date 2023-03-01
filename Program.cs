using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;

namespace botserver_pico
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var client = new TelegramBotClient("5969998133:AAF3nNDlYNfryOulNHKtsxlhuGo_roxXYXI");
            client.StartReceiving(Update, Error);
            var me = await client.GetMeAsync();
            Console.WriteLine($"Start listening bot @{me.Username} named as {me.FirstName}. Timestamp: {DateTime.Now}\n");

            Console.Read();
            
            static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
            {
                var message = update.Message;
                if (message.Text is not null)
                {
                    botClient.SendTextMessageAsync(message.Chat.Id, $"I received some text:\n{message.Text}");
                    Console.WriteLine($"{message.Text}");
                }            
                return Task.CompletedTask;
            }

            static Task Error(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}\nTimestamp: {DateTime.Now}",
                    _ => exception.ToString()
                };

                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }
        }
    }
}