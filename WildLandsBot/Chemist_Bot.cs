using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace WildLandsBot
{
    class Chemist_Bot
    {
        /// <summary>
        /// Дата и время начала игры
        /// </summary>
        private DateTime chemistDateStart;

        /// <summary>
        /// Дата и время для сравнения с датой и временем начала игры
        /// </summary>
        private DateTime chemistComparisonDateFromStart;

        /// <summary>
        /// Переменная для хранения введенного текста
        /// </summary>
        private string chemistMessageText;

        /// <summary>
        /// Денежный баланс химика
        /// </summary>
        public static double chemistCashBalance;

        /// <summary>
        /// Денежный баланс химика для временного хранения для переводов
        /// </summary>
        public static double chemistCashBalanceIntermediateStorage;

        /// <summary>
        /// Денежный баланс химика для сравнения
        /// </summary>
        private double chemistCashBalanceCheck;

        /// <summary>
        /// Денежный баланс химика разница
        /// </summary>
        private double chemistCashBalanceDifference;

        /// <summary>
        /// Экземпляр окна
        /// </summary>
        private MainWindow w;

        /// <summary>
        /// telegram бот клиент
        /// </summary>
        TelegramBotClient chemistBot;

        /// <summary>
        /// Определяем статическую встроенную клавиатуру химика
        /// </summary>
        private ReplyKeyboardMarkup chemistOperationsMenu;

        /// <summary>
        /// Метод для ответа на сообщения от пользователей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ChemistMessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            chemistComparisonDateFromStart = DateTime.Now;

            //Выводим меню
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && e.Message.Text == "/меню")
            {
                //Выводим основную клавиатуру картеля
                ChemistOperationsMenuOperation();
            }

            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && e.Message.Text == "/start")
            {
                //Выводим основную клавиатуру повстанцев
                ChemistOperationsMenuOperation();
                chemistBot.SendTextMessageAsync(e.Message.Chat.Id, "Добро пожаловать лабораторию!", replyMarkup: chemistOperationsMenu);
            }

            //Thread.Sleep(500);
            //Удаляем все введенные запросы от пользователя
            //chemistBot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

            //если сообщение текстовое, то записываем в переменную текст сообщения и проводим операции КОШЕЛЕК
            chemistMessageText = e.Message.Text;
            switch (chemistMessageText)
            {
                //Проверяем баланс кошелька
                case "Кошелек":
                    await chemistBot.SendTextMessageAsync(e.Message.Chat.Id, $"Сумма на кошельке: {chemistCashBalance}");
                    break;
            }

            //Проверяем изменения баланса
            if (chemistCashBalanceIntermediateStorage != chemistCashBalanceCheck)
            {
                chemistCashBalanceDifference = chemistCashBalanceIntermediateStorage - chemistCashBalanceCheck;
                chemistCashBalance = chemistCashBalanceIntermediateStorage;
                chemistCashBalanceCheck = chemistCashBalance;
                await chemistBot.SendTextMessageAsync(e.Message.Chat.Id, $"Ваш счет пополнился на {chemistCashBalanceDifference}\nСумма на кошельке: {chemistCashBalance}");
            }

            //ChemistSerialization();
            Thread.Sleep(200);

            //Если сообщение не текстовое, то выходим из метода
            if (e.Message.Text == null)
            {
                return;
            }
        }

        /// <summary>
        /// Статическая клавиатура вопросов для химика
        /// </summary>
        private void ChemistOperationsMenuOperation()
        {
            chemistOperationsMenu = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("Кошелек"),
            })
            {
                OneTimeKeyboard = false
            };

        }

        /// <summary>
        /// Запуск чат бота
        /// </summary>
        public void ChemistStart()
        {
            string tokenChemist = "Chemist.txt";

            chemistDateStart = DateTime.Now;
            chemistCashBalance = 100;
            chemistCashBalanceIntermediateStorage = 100;
            chemistCashBalanceCheck = 100;
            //ChemistDeserialization();
            chemistBot = new TelegramBotClient(NewDoc(tokenChemist));
            chemistBot.OnMessage += ChemistMessageListener;

            chemistBot.StartReceiving();

        }

        /// <summary>
        /// Метод десерилизации данных для Единства
        /// </summary>
        public void ChemistDeserialization()
        {
            string json;

            //переменные
            string pathChemistDateStart = "Chemist\\Save\\ChemistDateStart.json";


            //Проверяем наличие файлов
            if (File.Exists(pathChemistDateStart) == true)
            {
                json = File.ReadAllText(pathChemistDateStart);
                chemistDateStart = Convert.ToDateTime(JsonConvert.DeserializeObject(json));
            }
        }

        /// <summary>
        /// Метод Серилизации данных для Единства
        /// </summary>
        public void ChemistSerialization()
        {
            string json;

            //переменные
            string pathInsurgentsDateStart = "Chemist\\Save\\InsurgentsDateStart.json";

            //Серилизация
            json = JsonConvert.SerializeObject(chemistDateStart);
            File.WriteAllText(pathInsurgentsDateStart, json);

        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="w"></param>
        public Chemist_Bot(MainWindow w)
        {
            this.w = w;
            Thread chemistStartTask = new Thread(ChemistStart);
            chemistStartTask.Start();
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
    }
}
