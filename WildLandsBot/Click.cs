using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace WildLandsBot
{
    class Click
    {
        /// <summary>
        /// Экземпляр окна
        /// </summary>
        private MainWindow w;

        /// <summary>
        /// telegram бот клиент
        /// </summary>
        TelegramBotClient clickBot;

        string[] newsList;

        private void newsListPool()
        {
            newsList[0] = "Новость Боливии 1";
            newsList[1] = "Новость Боливии 2";
            newsList[2] = "Новость Боливии 3";
            newsList[3] = "Новость Боливии 4";
            newsList[4] = "Новость Боливии 5";
            newsList[5] = "Новость Боливии 6";
            newsList[6] = "Новость Боливии 7";
            newsList[7] = "Новость Боливии 8";
            newsList[8] = "Новость Боливии 9";
            newsList[9] = "Новость Боливии 10";
            newsList[10] = "Новость Боливии 11";
            newsList[11] = "Новость Боливии 12";
            newsList[12] = "Новость Боливии 13";
            newsList[13] = "Новость Боливии 14";
            newsList[14] = "Новость Боливии 15";
            newsList[15] = "Новость Боливии 16";
            newsList[16] = "Новость Боливии 17";
            newsList[17] = "Новость Боливии 18";
            newsList[18] = "Новость Боливии 19";
            newsList[19] = "Новость Боливии 20";
            newsList[20] = "Новость Боливии 21";
            newsList[21] = "Новость Боливии 22";
            newsList[22] = "Новость Боливии 23";
            newsList[23] = "Новость Боливии 24";
            newsList[24] = "Новость Боливии 25";
            newsList[25] = "Новость Боливии 26";
            newsList[26] = "Новость Боливии 27";
            newsList[27] = "Новость Боливии 28";
            newsList[28] = "Новость Боливии 29";
            newsList[29] = "Новость Боливии 30";
            newsList[30] = "Новость Боливии 31";
            newsList[31] = "Новость Боливии 32";
            newsList[32] = "Новость Боливии 33";
            newsList[33] = "Новость Боливии 34";
            newsList[34] = "Новость Боливии 35";
            newsList[35] = "Новость Боливии 36";
            newsList[36] = "Новость Боливии 37";
            newsList[37] = "Новость Боливии 38";
            newsList[38] = "Новость Боливии 39";
            newsList[39] = "Новость Боливии 40";
        }

        /// <summary>
        /// Метод для ответа на сообщения от пользователей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            //var i = 0;
            //string news;
            //newsListPool();
            //while (true)
            //{
            //    Thread.Sleep(15000);
            //    //clickBot.SendTextMessageAsync("-549065909", $"Хаюшки");
            //    if (i < 40)
            //    {
            //        news = newsList[i];
            //        await clickBot.SendTextMessageAsync(e.Message.Chat.Id, news);
            //        i++;
            //    }
            //    else
            //    {
            //        await clickBot.SendTextMessageAsync(e.Message.Chat.Id, $"Хаюшки");
            //    }

            //}
            

            //clickBot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);



        }

        /// <summary>
        /// Запуск чат бота
        /// </summary>
        public void ClickStart()
        {
            string tokenClick = "Click.txt";

            clickBot = new TelegramBotClient(NewDoc(tokenClick));
            newsList = new string[40];

            clickBot.OnMessage += MessageListener;
            clickBot.StartReceiving();


        }

        /// <summary>
        /// Создание документа и вывод токена
        /// </summary>
        /// <param name="tokenText"></param>
        /// <returns></returns>
        public string NewDoc(string tokenText)
        {
            string token;
            if (File.Exists(tokenText) == true)
            {
                token = File.ReadAllText(tokenText);
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(tokenText, true, Encoding.Unicode))
                {
                    sw.WriteLine($"");
                }
                token = File.ReadAllText(tokenText);
            }
            return token;
        }

        /// <summary>
        /// конструктро
        /// </summary>
        /// <param name="w"></param>
        public Click(MainWindow w)
        {
            this.w = w;

            Thread clickStartTask = new Thread(ClickStart);
            clickStartTask.Start();
        }

    }
}
