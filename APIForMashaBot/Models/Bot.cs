using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot;
using TelegramBotApp.Models.Commands;

namespace TelegramBotApp.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;
        private static bool isOnline;
        private static ParserWorker<string> parserBonga;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> Get()
        {
            if(client != null)
            {
                return client;
            }

            commandsList = new List<Command>();
            commandsList.Add(new HelloCommand());
            //TODO: Add more commands

            
            client = new TelegramBotClient(AppSettings.Key);

            parserBonga = new ParserWorker<string>(new BongaParser(), new BongaSettings());
            parserBonga.OnFinish += DataReview;
            isOnline = false;
            Thread myThread = new Thread(new ThreadStart(StartParsing));
            myThread.Start();


            return client;
        }
        private static void DataReview(object arg1, string resalt)
        {
            bool status;
            if (resalt == "онлайн") status = true;
            else status = false;
            if(isOnline != status)
            {
                isOnline = status;
                EchoModelStatus(status);
            }
        }

        private static void EchoModelStatus(bool status)
        {
            string massage;
            if (status) massage = "Маша сейчас онлайн";
            else massage = "Маша сейчас оффлайн";
            int myChatId = 381062098;
            int MashaId = 293527495;
            client.SendTextMessageAsync(myChatId, massage);
            client.SendTextMessageAsync(MashaId, massage);

        }

        public static void StartParsing()
        {
            while(true)
            {
                parserBonga.Start();
                Thread.Sleep(60000);
            }           
        }
    }
}