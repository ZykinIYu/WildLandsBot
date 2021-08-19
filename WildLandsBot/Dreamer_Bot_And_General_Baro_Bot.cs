using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Dreamer_Bot;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using WildLandsBot;
using System.Windows;
using System.Diagnostics;

namespace Dreamer_Bot
{
    class Dreamer_Bot_And_General_Baro_Bot
    {
        /// <summary>
        /// переменная показывающая смог ли картель выполнить миссию 1
        /// </summary>
        private bool cartelMissionCompleted1;

        /// <summary>
        /// переменная показывающая смог ли картель выполнить миссию 2
        /// </summary>
        private bool cartelMissionCompleted2;

        /// <summary>
        /// переменная показывающая смог ли картель выполнить миссию 3
        /// </summary>
        private bool cartelMissionCompleted3;

        /// <summary>
        /// Переменная для хранения введенного текста
        /// </summary>
        private string messageText;

        /// <summary>
        /// Переменная для хранения названия фото
        /// </summary>
        private string messageTextCompletingTheMission;

        /// <summary>
        /// Коллекция для хранения миссий
        /// </summary>
        private List<string> missionPool;

        /// <summary>
        /// Денежный баланс картеля
        /// </summary>
        private double cartelCashBalance;

        /// <summary>
        /// Денежный баланс картеля для временного хранения для переводов
        /// </summary>
        private double cartelCashBalanceIntermediateStorage;

        /// <summary>
        /// Денежный баланс картеля для сравнения
        /// </summary>
        private double cartelCashBalanceCheck;

        /// <summary>
        /// Денежный баланс картеля разница
        /// </summary>
        private double cartelCashBalanceDifference;

        /// <summary>
        /// коммисия которую берет себе банк
        /// </summary>
        private double percentForTranslation;

        /// <summary>
        /// Хранилище коки картеля
        /// </summary>
        private double cartelCocaCash;

        /// <summary>
        /// Дата и время начала игры
        /// </summary>
        private DateTime dateStart;

        /// <summary>
        /// Дата и время для сравнения с датой и временем начала игры
        /// </summary>
        private DateTime comparisonDateFromStart;

        /// <summary>
        /// Дата и время для сравнения с датой и временем начала игры для зеленой плантации
        /// </summary>
        private DateTime cartelGreenPlantation;

        /// <summary>
        /// Дата и время для сравнения с датой и временем начала игры для желтой плантации
        /// </summary>
        private DateTime cartelYellowPlantation;

        /// <summary>
        /// Дата и время для сравнения с датой и временем начала игры для красной плантации
        /// </summary>
        private DateTime cartelRedPlantation;

        /// <summary>
        /// Массив даты и времени начала транспортировки грузовиков
        /// </summary>
        private DateTime[] cartelStartTransportationTruck;

        /// <summary>
        /// Массив даты и времени начала транспортировки вертолетов
        /// </summary>
        private DateTime[] cartelStartTransportationHelicopters;

        /// <summary>
        /// Массив даты и времени начала транспортировки самолетов
        /// </summary>
        private DateTime[] cartelStartTransportationAircraft;

        /// <summary>
        /// Дата и время выплаты дани Мечтателю
        /// </summary>
        private DateTime cartelTributeDreamer;

        /// <summary>
        /// Сравнение дат для расчета времени созревания плантаций
        /// </summary>
        private TimeSpan cartelComparisonDateFromStart;

        /// <summary>
        /// Переменная записи общего количества свободных грузовиков
        /// </summary>
        private int cartelNumberTrucks;

        /// <summary>
        /// Переменная записи общего количества грузовиков
        /// </summary>
        private int cartelNumberTrucksCount;

        /// <summary>
        /// Переменная записи общего количества свободных вертолетов
        /// </summary>
        private int cartelNumberHelicopters;

        /// <summary>
        /// Переменная записи общего количества вертолетов
        /// </summary>
        private int cartelNumberHelicoptersCount;

        /// <summary>
        /// Переменная записи общего количества свободных самолетов
        /// </summary>
        private int cartelNumberAircraft;

        /// <summary>
        /// Переменная записи общего количества самолетов
        /// </summary>
        private int cartelNumberAircraftCount;

        /// <summary>
        /// Переменная записи количества коки отправляемой на грузовике
        /// </summary>
        private double cartelNumberCocaTrucks;

        /// <summary>
        /// Переменная записи количества коки отправляемой на вертолете
        /// </summary>
        private double cartelNumberCocaHelicopters;

        /// <summary>
        /// Переменная записи количества коки отправляемой на самолете
        /// </summary>
        private double cartelNumberCocaAircraft;

        /// <summary>
        /// Переменная записи процента за транспортировку на грузовике
        /// </summary>
        private double cartelpercentForTranslationTrucks;

        /// <summary>
        /// Переменная записи процента за транспортировку на вертолете
        /// </summary>
        private double cartelpercentForTranslationHelicopters;

        /// <summary>
        /// Переменная записи процента за транспортировку на самолете
        /// </summary>
        private double cartelpercentForTranslationAircraft;

        /// <summary>
        /// Переменная записи времени доставки грузовиком
        /// </summary>
        private double cartelDeliveryTimeTrucks;

        /// <summary>
        /// Переменная записи времени доставки Вертолетом
        /// </summary>
        private double cartelDeliveryTimeHelicopters;

        /// <summary>
        /// Переменная записи времени доставки Самолетом
        /// </summary>
        private double cartelDeliveryTimeAircraft;

        /// <summary>
        /// Время запуска мисии1
        /// </summary>
        private double cartelStartTimeMission1;

        /// <summary>
        /// Время запуска мисии2
        /// </summary>
        private double cartelStartTimeMission2;

        /// <summary>
        /// Время запуска мисии3
        /// </summary>
        private double cartelStartTimeMission3;

        /// <summary>
        /// Время провала мисии1
        /// </summary>
        private double cartelFailedTimeMission1;

        /// <summary>
        /// Время провала мисии2
        /// </summary>
        private double cartelFailedTimeMission2;

        /// <summary>
        /// Время провала мисии3
        /// </summary>
        private double cartelFailedTimeMission3;

        /// <summary>
        /// Получение дополнительной миссии ровно 1 раз
        /// </summary>
        private bool cartelSupplementaryMission2;

        /// <summary>
        /// Время выплаты дани
        /// </summary>
        private double cartelTributePaymentTime;

        /// <summary>
        /// Дань Мечтателю
        /// </summary>
        private double tribute;

        /// <summary>
        /// Цена коки
        /// </summary>
        private double cartelCocaPrice;

        /// <summary>
        /// массив определяющий доступность грузовиков
        /// </summary>
        private bool[] cartelBoolTruckBusy;

        /// <summary>
        /// массив определяющий доступность вертолетов
        /// </summary>
        private bool[] cartelBoolHelicoptersBusy;

        /// <summary>
        /// массив определяющий доступность самолетов
        /// </summary>
        private bool[] cartelBoolAircraftBusy;

        /// <summary>
        /// массив определяющий доставку груза грузовиком
        /// </summary>
        private bool[] cartelBoolTruckBusyFinishe;

        /// <summary>
        /// массив определяющий доставку груза вертолетом
        /// </summary>
        private bool[] cartelBoolHelicoptersBusyFinishe;

        /// <summary>
        /// массив определяющий доставку груза самолетом
        /// </summary>
        private bool[] cartelBoolAircraftBusyFinishe;

        /// <summary>
        /// Переменная определяющая что можно собирать коку на зеленой плантации
        /// </summary>
        private bool boolGreenPlantation;

        /// <summary>
        /// Переменная определяющая что можно собирать коку на желтой плантации
        /// </summary>
        private bool boolYellowPlantation;

        /// <summary>
        /// Переменная определяющая что можно собирать коку на зеленой плантации
        /// </summary>
        private bool boolRedPlantation;

        /// <summary>
        /// Количество собераемой коки с зеленой плантации за раз
        /// </summary>
        private double amountCocaHarvestedGreenPlantation;

        /// <summary>
        /// Количество собераемой коки с желтой плантации за раз
        /// </summary>
        private double amountCocaHarvestedYellowPlantation;

        /// <summary>
        /// Количество собераемой коки с красной плантации за раз
        /// </summary>
        private double amountCocaHarvestedRedPlantation;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 1
        /// </summary>
        private bool gettingMission1;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 2
        /// </summary>
        private bool gettingMission2;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 3
        /// </summary>
        private bool gettingMission3;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 1
        /// </summary>
        private string mission1ID;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 2
        /// </summary>
        private string mission2ID;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 3
        /// </summary>
        private string mission3ID;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 1
        /// </summary>
        private string missionFailedID1;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 2
        /// </summary>
        private string missionFailedID2;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 3
        /// </summary>
        private string missionFailedID3;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 1
        /// </summary>
        private string missionComplete1ID;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 2
        /// </summary>
        private string missionComplete2ID;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 3
        /// </summary>
        private string missionComplete3ID;

        /// <summary>
        /// Переменная для записи ID аудио файла отказа от миссии 2
        /// </summary>
        private string missionAbandonmentID2;

        /// <summary>
        /// Экземпляр окна
        /// </summary>
        private MainWindow w;

        /// <summary>
        /// telegram бот клиент
        /// </summary>
        TelegramBotClient dreamerBot;

        /// <summary>
        /// Коллекция для логов
        /// </summary>
        public ObservableCollection<MessageLog> CartelBotMessageLog { get; set; }

        /// <summary>
        /// Определяем статическую встроенную клавиатуру
        /// </summary>
        private ReplyKeyboardMarkup cartelOperationsMenu;

        /// <summary>
        /// Меню выбора кому переводить
        /// </summary>
        private InlineKeyboardMarkup whoTranslateCartel;

        /// <summary>
        /// Клавиатура принятия миссии
        /// </summary>
        private InlineKeyboardMarkup keyboardAcceptOrRefuseAMission;

        /// <summary>
        /// Клавиатура выбора плантации картеля
        /// </summary>
        private InlineKeyboardMarkup cartelPlantationSelectionKeyboard;

        /// <summary>
        /// Клавиатура выбора транспортировки коки
        /// </summary>
        private InlineKeyboardMarkup cartelCocaTransportationKeyboard;

        /// <summary>
        /// переменная показывающая смогло ли Единство выполнить миссию 1
        /// </summary>
        private bool unityMissionCompleted1;

        /// <summary>
        /// переменная показывающая смогло ли Единство выполнить миссию 2
        /// </summary>
        private bool unityMissionCompleted2;

        /// <summary>
        /// переменная показывающая смогло ли Единство выполнить миссию 3
        /// </summary>
        private bool unityMissionCompleted3;

        /// <summary>
        /// Переменная для хранения введенного текста
        /// </summary>
        private string unityMessageText;

        /// <summary>
        /// Переменная для хранения названия фото
        /// </summary>
        private string unityMessageTextCompletingTheMission;

        /// <summary>
        /// Коллекция для хранения миссий
        /// </summary>
        private List<string> unityMissionPool;

        /// <summary>
        /// Денежный баланс Единства
        /// </summary>
        public double unityCashBalance;

        /// <summary>
        /// Денежный баланс единства для временного хранения для переводов
        /// </summary>
        private double unityCashBalanceIntermediateStorage;

        /// <summary>
        /// Денежный баланс единства для сравнения
        /// </summary>
        private double unityCashBalanceCheck;

        /// <summary>
        /// Денежный баланс единства разница
        /// </summary>
        private double unityCashBalanceDifference;

        /// <summary>
        /// коммисия которую берет себе банк
        /// </summary>
        private double unityPercentForTranslation;

        /// <summary>
        /// Дата и время начала игры
        /// </summary>
        private DateTime unityDateStart;

        /// <summary>
        /// Дата и время для сравнения с датой и временем начала игры
        /// </summary>
        private DateTime unityComparisonDateFromStart;

        /// <summary>
        /// Дата и время для сравнения с датой unityComparisonDateFromStart
        /// </summary>
        private DateTime unityDateSalary;

        /// <summary>
        /// Зарплата единства
        /// </summary>
        private double salary;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 1
        /// </summary>
        private bool unityGettingMission1;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 2
        /// </summary>
        private bool unityGettingMission2;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 3
        /// </summary>
        private bool unityGettingMission3;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 1
        /// </summary>
        private string unityMission1ID;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 2
        /// </summary>
        private string unityMission2ID;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 3
        /// </summary>
        private string unityMission3ID;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 1
        /// </summary>
        private string unityMissionFailedID1;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 2
        /// </summary>
        private string unityMissionFailedID2;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 3
        /// </summary>
        private string unityMissionFailedID3;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 1
        /// </summary>
        private string unityMissionComplete1ID;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 2
        /// </summary>
        private string unityMissionComplete2ID;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 3
        /// </summary>
        private string unityMissionComplete3ID;

        /// <summary>
        /// Переменная для записи ID аудио файла отказа от миссии 2
        /// </summary>
        private string unityMissionAbandonmentID2;

        /// <summary>
        /// Время запуска мисии1
        /// </summary>
        private double unityStartTimeMission1;

        /// <summary>
        /// Время запуска мисии2
        /// </summary>
        private double unityStartTimeMission2;

        /// <summary>
        /// Время запуска мисии3
        /// </summary>
        private double unityStartTimeMission3;

        /// <summary>
        /// Время провала мисии1
        /// </summary>
        private double unityFailedTimeMission1;

        /// <summary>
        /// Время провала мисии2
        /// </summary>
        private double unityFailedTimeMission2;

        /// <summary>
        /// Время провала мисии3
        /// </summary>
        private double unityFailedTimeMission3;

        /// <summary>
        /// Получение дополнительной миссии ровно 1 раз
        /// </summary>
        private bool unitySupplementaryMission2;

        /// <summary>
        /// Время получения зарплаты
        /// </summary>
        private double unitySalaryPaymentTime;

        /// <summary>
        /// telegram бот клиент
        /// </summary>
        TelegramBotClient generalBaroBot;

        /// <summary>
        /// Определяем статическую встроенную клавиатуру
        /// </summary>
        private ReplyKeyboardMarkup unityOperationsMenu;

        /// <summary>
        /// Меню выбора кому переводить
        /// </summary>
        private InlineKeyboardMarkup whoTranslateUnity;

        /// <summary>
        /// Клавиатура принятия миссии
        /// </summary>
        private InlineKeyboardMarkup unityKeyboardAcceptOrRefuseAMission;

        /// <summary>
        /// Метод для ответа на сообщения от пользователей картеля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            comparisonDateFromStart = DateTime.Now;

            //записываем время, кто, ид, текст сообщения
            string text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

            //выводим на консоль время, кто, ид, текст сообщения
            Debug.WriteLine($"{text} TypeMessage: {e.Message.Type.ToString()}");
            var messageText = e.Message.Text;

            w.Dispatcher.Invoke(() =>
            {
                CartelBotMessageLog.Add(
                    new MessageLog(
                        DateTime.Now.ToLongTimeString(),
                        messageText,
                        e.Message.Chat.FirstName,
                        e.Message.Chat.Id));
            });

            //выводим тип сообщения
            Console.WriteLine($"TypeMessage: {e.Message.Type.ToString()}");
            Console.WriteLine();

            //Выводим меню
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && e.Message.Text == "/меню")
            {
                //Выводим основную клавиатуру картеля
                CartelOperationsMenuOperation();
            }

            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && e.Message.Text == "/start")
            {
                //Выводим основную клавиатуру картеля
                CartelOperationsMenuOperation();
                dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, "Добро пожаловать в Картель", replyMarkup: cartelOperationsMenu);
            }

            //если фото в виде документа то проводим ряд операций
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Document || e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                if (string.IsNullOrEmpty(e.Message.Caption) == false)
                {
                    messageTextCompletingTheMission = e.Message.Caption.ToLower();
                    //Выполнение миссий
                    switch (messageTextCompletingTheMission)
                    {
                        //Выполнение миссии 1
                        case "mission1":
                            if (missionPool.Contains("Миссия 1"))
                            {
                                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 1 Выполнена");
                                await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionComplete1ID);
                                missionPool.Remove($"Миссия 1");
                                cartelMissionCompleted1 = true;
                            }
                            break;

                        //Выполнение миссии 2
                        case "mission2":
                            if (missionPool.Contains("Миссия 2"))
                            {
                                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 Выполнена");
                                await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionComplete2ID);
                                missionPool.Remove($"Миссия 2");
                                cartelMissionCompleted2 = true;
                            }
                            break;

                        //Выполнение миссии 3
                        case "mission3":
                            if (missionPool.Contains("Миссия 3"))
                            {
                                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 Выполнена");
                                await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionComplete3ID);
                                missionPool.Remove($"Миссия 3");
                                cartelMissionCompleted3 = true;
                            }
                            break;

                        //Сбор на зеленой плантации
                        case "green":
                            if (boolGreenPlantation == true)
                            {
                                cartelGreenPlantation = DateTime.Now;
                                cartelCocaCash += amountCocaHarvestedGreenPlantation;
                                boolGreenPlantation = false;
                                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Кока с плантации собрана.\nУ вас {cartelCocaCash} т. коки");
                            }
                            break;

                        //Сбор на желтой плантации
                        case "yellow":
                            if (boolYellowPlantation == true)
                            {
                                cartelYellowPlantation = DateTime.Now;
                                cartelCocaCash += amountCocaHarvestedYellowPlantation;
                                boolYellowPlantation = false;
                                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Кока с плантации собрана.\nУ вас {cartelCocaCash} т. коки");
                            }
                            break;

                        //Сбор на красной плантации
                        case "red":
                            if (boolRedPlantation == true)
                            {
                                cartelRedPlantation = DateTime.Now;
                                cartelCocaCash += amountCocaHarvestedRedPlantation;
                                boolRedPlantation = false;
                                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Кока с плантации собрана.\nУ вас {cartelCocaCash} т. коки");
                            }
                            break;

                    }
                }
                else
                {
                    await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Данное фото без подписи, вышлите фото с подписью");
                }

            }

            //если прикладывается аудио, выводим его параметры и записываем ID аудио
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Audio)
            {
                messageText = e.Message.Audio.FileName;
                switch (messageText)
                {
                    case "Mission1.mp3":
                        mission1ID = e.Message.Audio.FileId;
                        break;

                    case "Mission2.mp3":
                        mission2ID = e.Message.Audio.FileId;
                        break;

                    case "Mission3.mp3":
                        mission3ID = e.Message.Audio.FileId;
                        break;

                    case "MissionFailed1.mp3":
                        missionFailedID1 = e.Message.Audio.FileId;
                        break;

                    case "MissionFailed2.mp3":
                        missionFailedID2 = e.Message.Audio.FileId;
                        break;

                    case "MissionFailed3.mp3":
                        missionFailedID3 = e.Message.Audio.FileId;
                        break;

                    case "MissionComplete1.mp3":
                        missionComplete1ID = e.Message.Audio.FileId;
                        break;

                    case "MissionComplete2.mp3":
                        missionComplete2ID = e.Message.Audio.FileId;
                        break;

                    case "MissionComplete3.mp3":
                        missionComplete3ID = e.Message.Audio.FileId;
                        break;

                    case "MissionAbandonment2.mp3":
                        missionAbandonmentID2 = e.Message.Audio.FileId;
                        break;

                }
                Console.WriteLine(e.Message.Audio.FileId);
                Console.WriteLine(e.Message.Audio.FileName);
                Console.WriteLine(e.Message.Audio.FileSize);
                Console.WriteLine(e.Message.Audio.MimeType);
                Console.WriteLine(e.Message.Audio.Thumb);

            }

            //Получаем обязательную миссию 1
            if (comparisonDateFromStart >= dateStart.AddMinutes(cartelStartTimeMission1) && gettingMission1 == false)
            {
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Получена миссия 1");
                await dreamerBot.SendAudioAsync(e.Message.Chat.Id, mission1ID);
                gettingMission1 = true;

                if (missionPool.Contains("Миссия 1") == false)
                {
                    missionPool.Add($"Миссия 1");
                }

            }

            //Получаем дополнительную миссию 2
            if (comparisonDateFromStart >= dateStart.AddMinutes(cartelStartTimeMission2) && gettingMission2 == false && cartelSupplementaryMission2 == false)
            {
                cartelSupplementaryMission2 = true;
                AcceptOrRefuseAMission();
                dreamerBot.OnCallbackQuery -= AcceptanceOrRefusalOfAMission;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Поступила дополнительная миссия 2. Принять миссию?", replyMarkup: keyboardAcceptOrRefuseAMission);
                dreamerBot.OnCallbackQuery += AcceptanceOrRefusalOfAMission;
            }

            //Получаем обязательную миссию 3
            if (comparisonDateFromStart >= dateStart.AddMinutes(cartelStartTimeMission3) && gettingMission3 == false)
            {
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Получена миссия 3");
                await dreamerBot.SendAudioAsync(e.Message.Chat.Id, mission3ID);
                gettingMission3 = true;

                if (missionPool.Contains("Миссия 3") == false)
                {
                    missionPool.Add($"Миссия 3");
                }

            }

            //Проваливаем обязательную миссию 1
            if (comparisonDateFromStart >= dateStart.AddMinutes(cartelFailedTimeMission1) && missionPool.Contains("Миссия 1"))
            {
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 1 провалена");
                await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionFailedID1);
                missionPool.Remove($"Миссия 1");
            }

            //Проваливаем дополнительную миссию 2
            if (comparisonDateFromStart >= dateStart.AddMinutes(cartelFailedTimeMission2) && missionPool.Contains("Миссия 2"))
            {
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 провалена");
                await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionFailedID2);
                missionPool.Remove($"Миссия 2");
            }

            //Проваливаем обязательную миссию 3
            if (comparisonDateFromStart >= dateStart.AddMinutes(cartelFailedTimeMission3) && missionPool.Contains("Миссия 3"))
            {
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 провалена");
                await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionFailedID3);
                missionPool.Remove($"Миссия 3");
            }

            //Проверяем изменения баланса
            if (cartelCashBalanceIntermediateStorage != cartelCashBalanceCheck)
            {
                cartelCashBalanceDifference = cartelCashBalanceIntermediateStorage - cartelCashBalanceCheck;
                cartelCashBalance = cartelCashBalanceIntermediateStorage;
                cartelCashBalanceCheck = cartelCashBalance;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Ваш счет пополнился на {cartelCashBalanceDifference}\nСумма на кошельке: {cartelCashBalance}");
            }

            //Приехал грузовик 1
            if (comparisonDateFromStart >= cartelStartTransportationTruck[0].AddMinutes(cartelDeliveryTimeTrucks) && cartelBoolTruckBusyFinishe[0] == true)
            {
                var profit = (cartelNumberCocaTrucks * cartelCocaPrice) - (cartelNumberCocaTrucks * cartelCocaPrice * cartelpercentForTranslationTrucks);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                cartelBoolTruckBusyFinishe[0] = false;
            }

            //Приехал грузовик 2
            if (comparisonDateFromStart >= cartelStartTransportationTruck[1].AddMinutes(cartelDeliveryTimeTrucks) && cartelBoolTruckBusyFinishe[1] == true)
            {
                var profit = (cartelNumberCocaTrucks * cartelCocaPrice) - (cartelNumberCocaTrucks * cartelCocaPrice * cartelpercentForTranslationTrucks);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                cartelBoolTruckBusyFinishe[1] = false;
            }

            //Приехал грузовик 3
            if (comparisonDateFromStart >= cartelStartTransportationTruck[2].AddMinutes(cartelDeliveryTimeTrucks) && cartelBoolTruckBusyFinishe[2] == true)
            {
                var profit = (cartelNumberCocaTrucks * cartelCocaPrice) - (cartelNumberCocaTrucks * cartelCocaPrice * cartelpercentForTranslationTrucks);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                cartelBoolTruckBusyFinishe[2] = false;
            }

            //Приехал грузовик 4
            if (comparisonDateFromStart >= cartelStartTransportationTruck[3].AddMinutes(cartelDeliveryTimeTrucks) && cartelBoolTruckBusyFinishe[3] == true)
            {
                var profit = (cartelNumberCocaTrucks * cartelCocaPrice) - (cartelNumberCocaTrucks * cartelCocaPrice * cartelpercentForTranslationTrucks);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                cartelBoolTruckBusyFinishe[3] = false;
            }

            //Приехал грузовик 5
            if (comparisonDateFromStart >= cartelStartTransportationTruck[4].AddMinutes(cartelDeliveryTimeTrucks) && cartelBoolTruckBusyFinishe[4] == true)
            {
                var profit = (cartelNumberCocaTrucks * cartelCocaPrice) - (cartelNumberCocaTrucks * cartelCocaPrice * cartelpercentForTranslationTrucks);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                cartelBoolTruckBusyFinishe[4] = false;
            }

            //Прилетел вертолет 1
            if (comparisonDateFromStart >= cartelStartTransportationHelicopters[0].AddMinutes(cartelDeliveryTimeHelicopters) && cartelBoolHelicoptersBusyFinishe[0] == true)
            {
                var profit = (cartelNumberCocaHelicopters * cartelCocaPrice) - (cartelNumberCocaHelicopters * cartelCocaPrice * cartelpercentForTranslationHelicopters);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Вертолет доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                cartelBoolHelicoptersBusyFinishe[0] = false;
            }

            //Прилетел вертолет 2
            if (comparisonDateFromStart >= cartelStartTransportationHelicopters[1].AddMinutes(cartelDeliveryTimeHelicopters) && cartelBoolHelicoptersBusyFinishe[1] == true)
            {
                var profit = (cartelNumberCocaHelicopters * cartelCocaPrice) - (cartelNumberCocaHelicopters * cartelCocaPrice * cartelpercentForTranslationHelicopters);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Вертолет доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                cartelBoolHelicoptersBusyFinishe[1] = false;
            }

            //Прилетел самолет 1
            if (comparisonDateFromStart >= cartelStartTransportationAircraft[0].AddMinutes(cartelDeliveryTimeAircraft) && cartelBoolAircraftBusyFinishe[0] == true)
            {
                var profit = (cartelNumberCocaAircraft * cartelCocaPrice) - (cartelNumberCocaAircraft * cartelCocaPrice * cartelpercentForTranslationAircraft);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Самолет доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                cartelBoolAircraftBusyFinishe[0] = false;
            }

            //Платим дань Мечтателю
            if (comparisonDateFromStart >= cartelTributeDreamer.AddMinutes(cartelTributePaymentTime))
            {
                cartelTributeDreamer = DateTime.Now;

                if (cartelCashBalance >= 20)
                {
                    cartelCashBalance -= tribute;
                    cartelCashBalanceCheck -= tribute;
                    cartelCashBalanceIntermediateStorage -= tribute;
                    await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Вы заплатили дань Мечтателю в размере: {tribute}\nСумма на кошельке: {cartelCashBalance}");
                }
                else
                {
                    await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Вы не смогли выплатить дань, убейте с особой жестокостью 2 членов Картеля, а трупы отнесите Кашевару на переработку");
                }

            }

            Thread.Sleep(500);
            //Удаляем все введенные запросы от пользователя
            dreamerBot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

            //если сообщение текстовое, то записываем в переменную текст сообщения и проводим операции КОШЕЛЕК, ПЕРЕВОДЫ, КВЕСТЫ, ПЛАНТАЦИИ
            messageText = e.Message.Text;
            switch (messageText)
            {
                //Проверяем баланс кошелька
                case "Кошелек":
                    await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Сумма на кошельке: {cartelCashBalance}");
                    break;

                //Организуем переводы между картелем и единством
                case "Переводы":
                    WhoDoesTheCartelTranslateTo();
                    dreamerBot.OnCallbackQuery -= CartelTransfers;
                    await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Банк возьмет коммисию в размере {percentForTranslation * 100}%. Какую сумму хотите перевести Единству?", replyMarkup: whoTranslateCartel);
                    dreamerBot.OnCallbackQuery += CartelTransfers;
                    break;

                //Проверяем текущие квесты
                case "Квесты":
                    if (missionPool.Count == 0)
                    {
                        await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Здраствуйте, Активных квестов нет");
                    }
                    else
                    {
                        for (int i = 0; i < missionPool.Count; i++)
                        {
                            await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"{missionPool[i]}");
                        }
                    }
                    break;

                //Проверяем зрелость плантаций
                case "Плантации":
                    CartelPlantationSelectionKeyboardInline();
                    dreamerBot.OnCallbackQuery -= CartelHittingThePlantation;
                    await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Ваше хранилище содержит {cartelCocaCash}т. коки.\nВыберите необходимую плантацию", replyMarkup: cartelPlantationSelectionKeyboard);
                    dreamerBot.OnCallbackQuery += CartelHittingThePlantation;
                    break;
            }

            //CartelSerialization();
            Thread.Sleep(200);

            //Если сообщение не текстовое, то выходим из метода
            if (e.Message.Text == null)
            {
                return;
            }
        }

        /// <summary>
        /// Метод для отображения клавиатуры с выбором суммы перевода денег для картеля
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private void CartelTransfers(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var message = ev.CallbackQuery.Message;
            if (ev.CallbackQuery.Data == "5")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                double withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                if (withdrawalAmount <= cartelCashBalance)
                {
                    cartelCashBalance -= withdrawalAmount;
                    cartelCashBalanceCheck = cartelCashBalance;
                    cartelCashBalanceIntermediateStorage = cartelCashBalance;
                    unityCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                }
                else
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                }
                dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
            }
            else
            if (ev.CallbackQuery.Data == "10")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                double withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                if (withdrawalAmount <= cartelCashBalance)
                {
                    cartelCashBalance -= withdrawalAmount;
                    cartelCashBalanceCheck = cartelCashBalance;
                    cartelCashBalanceIntermediateStorage = cartelCashBalance;
                    unityCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                }
                else
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                }
                dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
            }
            else
            if (ev.CallbackQuery.Data == "15")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                double withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                if (withdrawalAmount <= cartelCashBalance)
                {
                    cartelCashBalance -= withdrawalAmount;
                    cartelCashBalanceCheck = cartelCashBalance;
                    cartelCashBalanceIntermediateStorage = cartelCashBalance;
                    unityCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                }
                else
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                }
                dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
            }
            else
            if (ev.CallbackQuery.Data == "20")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                double withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                if (withdrawalAmount <= cartelCashBalance)
                {
                    cartelCashBalance -= withdrawalAmount;
                    cartelCashBalanceCheck = cartelCashBalance;
                    cartelCashBalanceIntermediateStorage = cartelCashBalance;
                    unityCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                }
                else
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                }
                dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
            }
        }

        /// <summary>
        /// Метод для отображения клавиатуры с выбором плантации картеля
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private async void CartelHittingThePlantation(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var message = ev.CallbackQuery.Message;
            if (ev.CallbackQuery.Data == "GreenPlantation")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                if (comparisonDateFromStart >= cartelGreenPlantation.AddMinutes(1))
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Плантация созрела можно начинать собирать\n для подтверждения сбора необходимо выслать фотографию с урожаем, фото высылается как документ. Подпись к фотографии дожна быть \"green\" без ковычек");
                    boolGreenPlantation = true;
                }
                else
                {
                    cartelComparisonDateFromStart = cartelGreenPlantation.AddMinutes(1).Subtract(comparisonDateFromStart);
                    int fullSeconds = Convert.ToInt32(cartelComparisonDateFromStart.TotalSeconds);
                    int timepiece = (fullSeconds / 60) / 60;
                    int minutes = (fullSeconds / 60) % 60;
                    int seconds = fullSeconds % 60;
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Плантация созреет через {timepiece} ч. {minutes} мин. {seconds} сек.");
                }
            }
            else
            if (ev.CallbackQuery.Data == "YellowPlantation")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                if (comparisonDateFromStart >= cartelYellowPlantation.AddMinutes(2))
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Плантация созрела можно начинать собирать\n для подтверждения сбора необходимо выслать фотографию с урожаем, фото высылается как документ. Подпись к фотографии дожна быть \"yellow\" без ковычек");
                    boolYellowPlantation = true;
                }
                else
                {
                    cartelComparisonDateFromStart = cartelYellowPlantation.AddMinutes(2).Subtract(comparisonDateFromStart);
                    int fullSeconds = Convert.ToInt32(cartelComparisonDateFromStart.TotalSeconds);
                    int timepiece = (fullSeconds / 60) / 60;
                    int minutes = (fullSeconds / 60) % 60;
                    int seconds = fullSeconds % 60;
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Плантация созреет через {timepiece} ч. {minutes} мин. {seconds} сек.");
                }
            }
            else
            if (ev.CallbackQuery.Data == "RedPlantation")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                if (comparisonDateFromStart >= cartelRedPlantation.AddMinutes(3))
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Плантация созрела можно начинать собирать\n для подтверждения сбора необходимо выслать фотографию с урожаем, фото высылается как документ. Подпись к фотографии дожна быть \"red\" без ковычек");
                    boolRedPlantation = true;
                }
                else
                {
                    cartelComparisonDateFromStart = cartelRedPlantation.AddMinutes(3).Subtract(comparisonDateFromStart);
                    int fullSeconds = Convert.ToInt32(cartelComparisonDateFromStart.TotalSeconds);
                    int timepiece = (fullSeconds / 60) / 60;
                    int minutes = (fullSeconds / 60) % 60;
                    int seconds = fullSeconds % 60;
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Плантация созреет через {timepiece} ч. {minutes} мин. {seconds} сек.");
                }
            }
            else
            if (ev.CallbackQuery.Data == "CocaTransportation")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                CocaTransportationKeyboardInline();
                dreamerBot.OnCallbackQuery -= CartelSendingTheHarvestedCoca;
                await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Каким видом транспорта будете отправлять коку?\nСвободных грузовиков: {cartelNumberTrucks}, водитель возмет {cartelpercentForTranslationTrucks * 100} % с продажи коки, вместимость {cartelNumberCocaTrucks} т. коки\nСвободных вертолетов: {cartelNumberHelicopters}, пилот возмет {cartelpercentForTranslationHelicopters * 100} % с продажи коки, вместимость {cartelNumberCocaHelicopters} т. коки\nСвободных самолетов: {cartelNumberAircraft}, пилот возмет {cartelpercentForTranslationAircraft * 100} % с продажи коки, вместимость {cartelNumberCocaAircraft} т. коки\nВодители и пилоты транспортируют груз, только если ТС полностью заполнено кокой", replyMarkup: cartelCocaTransportationKeyboard);
                dreamerBot.OnCallbackQuery += CartelSendingTheHarvestedCoca;
            }
        }

        /// <summary>
        /// Метод для отображения клавиатуры с выбором транспортировки коки картеля
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private void CartelSendingTheHarvestedCoca(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var message = ev.CallbackQuery.Message;
            if (ev.CallbackQuery.Data == "Truck")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                if (cartelNumberTrucks > 0)
                {
                    for (int i = 0; i < cartelNumberTrucksCount; i++)
                    {
                        if (cartelBoolTruckBusy[i] == false)
                        {
                            cartelBoolTruckBusy[i] = true;
                            cartelBoolTruckBusyFinishe[i] = true;
                            cartelNumberTrucks -= 1;
                            cartelCocaCash -= cartelNumberCocaTrucks;
                            cartelStartTransportationTruck[i] = DateTime.Now;
                            dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"В грузовик погрузили {cartelNumberCocaTrucks} т. коки, груз будет доствален через {cartelDeliveryTimeTrucks} мин.\nВаше хранилище содержит {cartelCocaCash} т. коки");
                            break;
                        }
                    }
                }
                else
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Свободных грузовиков больше нет, выберите другой вид транспортировки");
                }

            }
            else
            if (ev.CallbackQuery.Data == "Helicopter")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                if (cartelNumberHelicopters > 0)
                {
                    for (int i = 0; i < cartelNumberHelicoptersCount; i++)
                    {
                        if (cartelBoolHelicoptersBusy[i] == false)
                        {
                            cartelBoolHelicoptersBusy[i] = true;
                            cartelBoolHelicoptersBusyFinishe[i] = true;
                            cartelNumberHelicopters -= 1;
                            cartelCocaCash -= cartelNumberCocaHelicopters;
                            cartelStartTransportationHelicopters[i] = DateTime.Now;
                            dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"В вертолет погрузили {cartelNumberCocaHelicopters} т. коки, груз будет доствален через {cartelDeliveryTimeHelicopters} мин.\nВаше хранилище содержит {cartelCocaCash} т. коки");
                            break;
                        }
                    }
                }
                else
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Свободных вертолетов больше нет, выберите другой вид транспортировки");
                }
            }
            else
            if (ev.CallbackQuery.Data == "Plane")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                if (cartelNumberAircraft > 0)
                {
                    for (int i = 0; i < cartelNumberAircraftCount; i++)
                    {
                        if (cartelBoolAircraftBusy[i] == false)
                        {
                            cartelBoolAircraftBusy[i] = true;
                            cartelBoolAircraftBusyFinishe[i] = true;
                            cartelNumberAircraft -= 1;
                            cartelCocaCash -= cartelNumberCocaAircraft;
                            cartelStartTransportationAircraft[i] = DateTime.Now;
                            dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"В самолет погрузили {cartelNumberCocaAircraft} т. коки, груз будет доствален через {cartelDeliveryTimeAircraft} мин.\nВаше хранилище содержит {cartelCocaCash} т. коки");
                            break;
                        }
                    }
                }
                else
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Свободных самолетов больше нет, выберите другой вид транспортировки");
                }
            }
        }

        /// <summary>
        /// Метод скачивания документа
        /// </summary>
        /// <param name="fileId">ИД документа</param>
        /// <param name="path"> путь сохранения файла</param>
        async void DownLoad(string fileId, string path)
        {
            var file = await dreamerBot.GetFileAsync(fileId);
            FileStream fs = new FileStream("Cartel\\MissionPhoto\\Cartel_" + path + ".jpeg", FileMode.Create);
            await dreamerBot.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            fs.Dispose();
        }

        /// <summary>
        /// Статическая клавиатура вопросов для картеля
        /// </summary>
        private void CartelOperationsMenuOperation()
        {
            cartelOperationsMenu = new ReplyKeyboardMarkup(new[]
            {
                new[] {new KeyboardButton("Кошелек"), new KeyboardButton("Переводы")},
                new[] {new KeyboardButton("Квесты"), new KeyboardButton("Плантации")},
            })
            {
                OneTimeKeyboard = false
            };

        }

        /// <summary>
        /// Меню переводов картеля
        /// </summary>
        private void WhoDoesTheCartelTranslateTo()
        {
            whoTranslateCartel = new InlineKeyboardMarkup(new[]
            {
                new[] {InlineKeyboardButton.WithCallbackData("5", "5"), InlineKeyboardButton.WithCallbackData("10","10")},
                new[] {InlineKeyboardButton.WithCallbackData("15","15"), InlineKeyboardButton.WithCallbackData("20","20") },
            });

        }

        /// <summary>
        /// Меню принятия/отказа дополнительной миссии
        /// </summary>
        private void AcceptOrRefuseAMission()
        {
            keyboardAcceptOrRefuseAMission = new InlineKeyboardMarkup(new[]
            {
                new[] {InlineKeyboardButton.WithCallbackData("Да", "Yes"), InlineKeyboardButton.WithCallbackData("Нет","No")},
            });
        }

        /// <summary>
        /// Меню плантаций картеля
        /// </summary>
        private void CartelPlantationSelectionKeyboardInline()
        {
            cartelPlantationSelectionKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] {InlineKeyboardButton.WithCallbackData("Зеленая плантация", "GreenPlantation"), InlineKeyboardButton.WithCallbackData("Желтая плантация", "YellowPlantation") },
                new[] {InlineKeyboardButton.WithCallbackData("Красная плантация", "RedPlantation"), InlineKeyboardButton.WithCallbackData("Перевозка коки", "CocaTransportation") },
            });

        }

        /// <summary>
        /// Меню транспортировки коки
        /// </summary>
        private void CocaTransportationKeyboardInline()
        {
            cartelCocaTransportationKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[] {InlineKeyboardButton.WithCallbackData("Грузовик", "Truck"), InlineKeyboardButton.WithCallbackData("Вертолет", "Helicopter") },
                new[] {InlineKeyboardButton.WithCallbackData("Самолет", "Plane") },
            });

        }

        /// <summary>
        /// Метод для отображения клавиатуры с выбором принятия миссии для картеля
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private async void AcceptanceOrRefusalOfAMission(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var message = ev.CallbackQuery.Message;
            Console.WriteLine($"{ev.CallbackQuery.Data}");
            if (ev.CallbackQuery.Data == "Yes")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Получена миссия 2");
                await dreamerBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, mission2ID);
                gettingMission2 = true;

                if (missionPool.Contains("Миссия 2") == false)
                {
                    missionPool.Add($"Миссия 2");
                }

            }
            else if (ev.CallbackQuery.Data == "No")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ты точно уверен, что это хороший выбор? Привет семье");
                await dreamerBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, missionAbandonmentID2);
                gettingMission2 = true;
            }
        }

        /// <summary>
        /// Метод для ответа на сообщения от пользователей для Единства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void UnityMessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            unityComparisonDateFromStart = DateTime.Now;

            //записываем время, кто, ид, текст сообщения
            string text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

            //выводим на консоль время, кто, ид, текст сообщения
            Console.WriteLine(text);

            //выводим тип сообщения
            Console.WriteLine($"TypeMessage: {e.Message.Type.ToString()}");
            Console.WriteLine();

            //Выводим меню
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && e.Message.Text == "/меню")
            {
                //Выводим основную клавиатуру картеля
                UnityOperationsMenuOperation();
            }

            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && e.Message.Text == "/start")
            {
                //Выводим основную клавиатуру Единства
                UnityOperationsMenuOperation();
                generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, "Добро пожаловать в Единство", replyMarkup: unityOperationsMenu);
            }

            //если фото в виде документа, выводим его параметры и выполняем миссию соответствующую названию фото
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Document || e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                if (string.IsNullOrEmpty(e.Message.Caption) == false)
                {
                    unityMessageTextCompletingTheMission = e.Message.Caption.ToLower();
                    switch (unityMessageTextCompletingTheMission)
                    {
                        case "mission1":
                            if (unityMissionPool.Contains("Миссия 1"))
                            {
                                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 1 Выполнена");
                                await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionComplete1ID);
                                unityMissionPool.Remove($"Миссия 1");
                                unityMissionCompleted1 = true;
                            }
                            break;

                        case "mission2":
                            if (unityMissionPool.Contains("Миссия 2"))
                            {
                                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 Выполнена");
                                await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionComplete2ID);
                                unityMissionPool.Remove($"Миссия 2");
                                unityMissionCompleted2 = true;
                            }
                            break;

                        case "mission3":
                            if (unityMissionPool.Contains("Миссия 3"))
                            {
                                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 Выполнена");
                                await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionComplete3ID);
                                unityMissionPool.Remove($"Миссия 3");
                                unityMissionCompleted3 = true;
                            }
                            break;
                    }
                }
            }

            //если прикладывается аудио, выводим его параметры и записываем ID аудио
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Audio)
            {
                unityMessageText = e.Message.Audio.FileName;
                switch (unityMessageText)
                {
                    case "UnityMission1.mp3":
                        unityMission1ID = e.Message.Audio.FileId;
                        break;

                    case "UnityMission2.mp3":
                        unityMission2ID = e.Message.Audio.FileId;
                        break;

                    case "UnityMission3.mp3":
                        unityMission3ID = e.Message.Audio.FileId;
                        break;

                    case "UnityMissionFailed1.mp3":
                        unityMissionFailedID1 = e.Message.Audio.FileId;
                        break;

                    case "UnityMissionFailed2.mp3":
                        unityMissionFailedID2 = e.Message.Audio.FileId;
                        break;

                    case "UnityMissionFailed3.mp3":
                        unityMissionFailedID3 = e.Message.Audio.FileId;
                        break;

                    case "UnityMissionComplete1.mp3":
                        unityMissionComplete1ID = e.Message.Audio.FileId;
                        break;

                    case "UnityMissionComplete2.mp3":
                        unityMissionComplete2ID = e.Message.Audio.FileId;
                        break;

                    case "UnityMissionComplete3.mp3":
                        unityMissionComplete3ID = e.Message.Audio.FileId;
                        break;

                    case "UnityMissionAbandonment2.mp3":
                        unityMissionAbandonmentID2 = e.Message.Audio.FileId;
                        break;

                }
                Console.WriteLine(e.Message.Audio.FileId);
                Console.WriteLine(e.Message.Audio.FileName);
                Console.WriteLine(e.Message.Audio.FileSize);
                Console.WriteLine(e.Message.Audio.MimeType);
                Console.WriteLine(e.Message.Audio.Thumb);

            }

            Thread.Sleep(500);
            //Удаляем все введенные запросы от пользователя
            generalBaroBot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

            //Получаем обязательную миссию 1
            if (unityComparisonDateFromStart >= unityDateStart.AddMinutes(unityStartTimeMission1) && unityGettingMission1 == false)
            {
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Получена миссия 1");
                await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMission1ID);
                unityGettingMission1 = true;

                if (unityMissionPool.Contains("Миссия 1") == false)
                {
                    unityMissionPool.Add($"Миссия 1");
                }

            }

            //Получаем дополнительную миссию 2
            if (unityComparisonDateFromStart >= unityDateStart.AddMinutes(unityStartTimeMission2) && unityGettingMission2 == false && unitySupplementaryMission2 == false)
            {
                unitySupplementaryMission2 = true;
                UnityAcceptOrRefuseAMission();
                generalBaroBot.OnCallbackQuery -= UnityAcceptanceOrRefusalOfAMission;
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Поступила дополнительная миссия 2. Принять миссию?", replyMarkup: unityKeyboardAcceptOrRefuseAMission);
                generalBaroBot.OnCallbackQuery += UnityAcceptanceOrRefusalOfAMission;
            }

            //Получаем обязательную миссию 3
            if (unityComparisonDateFromStart >= unityDateStart.AddMinutes(unityStartTimeMission3) && unityGettingMission3 == false)
            {
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Получена миссия 3");
                await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMission3ID);
                unityGettingMission3 = true;

                if (unityMissionPool.Contains("Миссия 3") == false)
                {
                    unityMissionPool.Add($"Миссия 3");
                }

            }

            //Проваливаем обязательную миссию 1
            if (unityComparisonDateFromStart >= unityDateStart.AddMinutes(unityFailedTimeMission1) && unityMissionPool.Contains("Миссия 1"))
            {
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 1 провалена");
                await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionFailedID1);
                unityMissionPool.Remove($"Миссия 1");
            }

            //Проваливаем дополнительную миссию 2
            if (unityComparisonDateFromStart >= unityDateStart.AddMinutes(unityFailedTimeMission2) && unityMissionPool.Contains("Миссия 2"))
            {
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 провалена");
                await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionFailedID2);
                unityMissionPool.Remove($"Миссия 2");
            }

            //Проваливаем обязательную миссию 3
            if (unityComparisonDateFromStart >= unityDateStart.AddMinutes(unityFailedTimeMission3) && unityMissionPool.Contains("Миссия 3"))
            {
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 провалена");
                await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionFailedID3);
                unityMissionPool.Remove($"Миссия 3");
            }

            //Начисляем зарплату Единству
            if (unityComparisonDateFromStart >= unityDateSalary.AddMinutes(unitySalaryPaymentTime))
            {
                unityDateSalary = DateTime.Now;
                unityCashBalance += salary;
                unityCashBalanceCheck += salary;
                unityCashBalanceIntermediateStorage += salary;
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Начислена заработная плата в размере: {salary}\nСумма на кошельке: {unityCashBalance}");
            }

            //Проверяем изменения баланса
            if (unityCashBalanceIntermediateStorage != unityCashBalanceCheck)
            {
                unityCashBalanceDifference = unityCashBalanceIntermediateStorage - unityCashBalanceCheck;
                unityCashBalance = unityCashBalanceIntermediateStorage;
                unityCashBalanceCheck = unityCashBalance;
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Ваш счет пополнился на {unityCashBalanceDifference}\nСумма на кошельке: {unityCashBalance}");
            }

            //если сообщение текстовое, то записываем в переменную текст сообщения и проводим операции КОШЕЛЕК, ПЕРЕВОДЫ, КВЕСТЫ
            unityMessageText = e.Message.Text;
            switch (unityMessageText)
            {
                //Проверяем баланс кошелька
                case "Кошелек":
                    await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Сумма на кошельке: {unityCashBalance}");
                    break;

                //Организуем переводы между картелем и единством
                case "Переводы":
                    WhoDoesTheUnityTranslateTo();
                    generalBaroBot.OnCallbackQuery -= UnityTransfers;
                    await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Банк возьмет коммисию в размере {unityPercentForTranslation * 100}%. Какую сумму хотите перевести Картелю?", replyMarkup: whoTranslateUnity);
                    generalBaroBot.OnCallbackQuery += UnityTransfers;
                    break;

                //Проверяем текущие квесты
                case "Квесты":
                    if (unityMissionPool.Count == 0)
                    {
                        await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Здраствуйте, Активных квестов нет");
                    }
                    else
                    {
                        for (int i = 0; i < unityMissionPool.Count; i++)
                        {
                            await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"{unityMissionPool[i]}");
                        }
                    }
                    break;
            }

            //UnitySerialization();
            Thread.Sleep(200);

            //Если сообщение не текстовое, то выходим из метода
            if (e.Message.Text == null)
            {
                return;
            }
        }

        /// <summary>
        /// Метод для отображения клавиатуры с выбором суммы перевода денег
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private void UnityTransfers(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var message = ev.CallbackQuery.Message;
            if (ev.CallbackQuery.Data == "5")
            {
                generalBaroBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                double withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * unityPercentForTranslation);
                if (withdrawalAmount <= unityCashBalance)
                {
                    unityCashBalance -= withdrawalAmount;
                    unityCashBalanceCheck = unityCashBalance;
                    unityCashBalanceIntermediateStorage = unityCashBalance;
                    cartelCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                }
                else
                {
                    generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                }
                generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {unityCashBalance}");
            }
            else
            if (ev.CallbackQuery.Data == "10")
            {
                generalBaroBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                double withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * unityPercentForTranslation);
                if (withdrawalAmount <= unityCashBalance)
                {
                    unityCashBalance -= withdrawalAmount;
                    unityCashBalanceCheck = unityCashBalance;
                    unityCashBalanceIntermediateStorage = unityCashBalance;
                    cartelCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                }
                else
                {
                    generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                }
                generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {unityCashBalance}");
            }
            else
            if (ev.CallbackQuery.Data == "15")
            {
                generalBaroBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                double withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * unityPercentForTranslation);
                if (withdrawalAmount <= unityCashBalance)
                {
                    unityCashBalance -= withdrawalAmount;
                    unityCashBalanceCheck = unityCashBalance;
                    unityCashBalanceIntermediateStorage = unityCashBalance;
                    cartelCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                }
                else
                {
                    generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                }
                generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {unityCashBalance}");
            }
            else
            if (ev.CallbackQuery.Data == "20")
            {
                generalBaroBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                double withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * unityPercentForTranslation);
                if (withdrawalAmount <= unityCashBalance)
                {
                    unityCashBalance -= withdrawalAmount;
                    unityCashBalanceCheck = unityCashBalance;
                    unityCashBalanceIntermediateStorage = unityCashBalance;
                    cartelCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                }
                else
                {
                    generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                }
                generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {unityCashBalance}");
            }
        }

        /// <summary>
        /// Метод скачивания документа
        /// </summary>
        /// <param name="fileId">ИД документа</param>
        /// <param name="path"> путь сохранения файла</param>
        async void UnityDownLoad(string fileId, string path)
        {
            var file = await generalBaroBot.GetFileAsync(fileId);
            FileStream fs = new FileStream("Unity\\MissionPhoto\\Unity_" + path, FileMode.Create);
            await generalBaroBot.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            fs.Dispose();
        }

        /// <summary>
        /// Статическая клавиатура вопросов для Единства
        /// </summary>
        private void UnityOperationsMenuOperation()
        {
            unityOperationsMenu = new ReplyKeyboardMarkup(new[]
            {
                new[] {new KeyboardButton("Кошелек"), new KeyboardButton("Переводы")},
                new[] {new KeyboardButton("Квесты")},
            })
            {
                OneTimeKeyboard = false
            };

        }

        /// <summary>
        /// Меню переводов картеля
        /// </summary>
        private void WhoDoesTheUnityTranslateTo()
        {
            whoTranslateUnity = new InlineKeyboardMarkup(new[]
            {
                new[] {InlineKeyboardButton.WithCallbackData("5", "5"), InlineKeyboardButton.WithCallbackData("10","10")},
                new[] {InlineKeyboardButton.WithCallbackData("15","15"), InlineKeyboardButton.WithCallbackData("20","20") },
            });

        }

        /// <summary>
        /// Меню принятия/отказа дополнительной миссии
        /// </summary>
        private void UnityAcceptOrRefuseAMission()
        {
            unityKeyboardAcceptOrRefuseAMission = new InlineKeyboardMarkup(new[]
            {
                new[] {InlineKeyboardButton.WithCallbackData("Да", "Yes"), InlineKeyboardButton.WithCallbackData("Нет","No")},
            });
        }

        /// <summary>
        /// Метод для отображения клавиатуры с выбором принятия миссии
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private async void UnityAcceptanceOrRefusalOfAMission(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var message = ev.CallbackQuery.Message;
            if (ev.CallbackQuery.Data == "Yes")
            {
                generalBaroBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                await generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Получена миссия 2");
                await generalBaroBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, unityMission2ID);
                unityGettingMission2 = true;

                if (unityMissionPool.Contains("Миссия21") == false)
                {
                    unityMissionPool.Add($"Миссия 2");
                }

            }
            else if (ev.CallbackQuery.Data == "No")
            {
                generalBaroBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                await generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ты точно уверен, что это хороший выбор? Привет семье");
                await generalBaroBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, unityMissionAbandonmentID2);
                unityGettingMission2 = true;
            }
        }

        /// <summary>
        /// Запуск чат бота для картеля
        /// </summary>
        public void CartelStart()
        {
                       
            Debug.WriteLine(dateStart);
            Debug.WriteLine(dateStart.AddMinutes(1));
            Debug.WriteLine(dateStart.AddHours(1));

            string token = File.ReadAllText("Dreamer.txt");
            cartelCashBalance = 200;
            cartelCashBalanceIntermediateStorage = 200;
            cartelCashBalanceCheck = 200;
            percentForTranslation = 0.1;
            cartelpercentForTranslationTrucks = 0.1;
            cartelpercentForTranslationHelicopters = 0.15;
            cartelpercentForTranslationAircraft = 0.25;
            cartelCocaCash = 200;
            cartelCocaPrice = 5.5;
            dateStart = DateTime.Now;
            cartelGreenPlantation = dateStart;
            cartelYellowPlantation = dateStart;
            cartelRedPlantation = dateStart;
            cartelTributeDreamer = dateStart;
            gettingMission1 = false;
            gettingMission2 = false;
            gettingMission3 = false;
            boolGreenPlantation = false;
            boolYellowPlantation = false;
            boolRedPlantation = false;
            cartelSupplementaryMission2 = false;
            amountCocaHarvestedGreenPlantation = 3;
            amountCocaHarvestedYellowPlantation = 4;
            amountCocaHarvestedRedPlantation = 5;
            cartelNumberTrucks = 5;

            cartelBoolTruckBusy = new bool[cartelNumberTrucks];
            cartelStartTransportationTruck = new DateTime[cartelNumberTrucks];
            cartelBoolTruckBusyFinishe = new bool[cartelNumberTrucks];

            for (int i = 0; i < cartelBoolTruckBusy.Length; i++)
            {
                cartelBoolTruckBusy[i] = false;
                cartelStartTransportationTruck[i] = dateStart;
                cartelBoolTruckBusyFinishe[i] = false;
            }

            cartelNumberHelicopters = 2;

            cartelBoolHelicoptersBusy = new bool[cartelNumberHelicopters];
            cartelStartTransportationHelicopters = new DateTime[cartelNumberHelicopters];
            cartelBoolHelicoptersBusyFinishe = new bool[cartelNumberHelicopters];

            for (int i = 0; i < cartelBoolHelicoptersBusy.Length; i++)
            {
                cartelBoolHelicoptersBusy[i] = false;
                cartelStartTransportationHelicopters[i] = dateStart;
                cartelBoolHelicoptersBusyFinishe[i] = false;
            }

            cartelNumberAircraft = 1;

            cartelBoolAircraftBusy = new bool[cartelNumberAircraft];
            cartelStartTransportationAircraft = new DateTime[cartelNumberAircraft];
            cartelBoolAircraftBusyFinishe = new bool[cartelNumberAircraft];

            for (int i = 0; i < cartelBoolAircraftBusy.Length; i++)
            {
                cartelBoolAircraftBusy[i] = false;
                cartelStartTransportationAircraft[i] = dateStart;
                cartelBoolAircraftBusyFinishe[i] = false;
            }

            cartelNumberTrucksCount = cartelNumberTrucks;
            cartelNumberHelicoptersCount = cartelNumberHelicopters;
            cartelNumberAircraftCount = cartelNumberAircraft;
            cartelNumberCocaTrucks = 8;
            cartelNumberCocaHelicopters = 15;
            cartelNumberCocaAircraft = 25;
            cartelDeliveryTimeTrucks = 1;
            cartelDeliveryTimeHelicopters = 1;
            cartelDeliveryTimeAircraft = 1;
            cartelStartTimeMission1 = 10;
            cartelStartTimeMission2 = 20;
            cartelStartTimeMission3 = 30;
            cartelFailedTimeMission1 = 240;
            cartelFailedTimeMission2 = 300;
            cartelFailedTimeMission3 = 360;
            cartelTributePaymentTime = 1;
            tribute = 20;
            missionPool = new List<string>();
            cartelMissionCompleted1 = false;
            cartelMissionCompleted2 = false;
            cartelMissionCompleted3 = false;
            //CartelDeserialization();
            Console.WriteLine(dateStart);
            Console.WriteLine(dateStart.AddMinutes(1));
            Console.WriteLine(dateStart.AddHours(1));
            dreamerBot = new TelegramBotClient(token);
            dreamerBot.OnMessage += MessageListener;

            dreamerBot.StartReceiving();


        }

        /// <summary>
        /// Запуск чат бота для Единства
        /// </summary>
        public void UnityStart()
        {
            string token = File.ReadAllText("GeneralBaro.txt");
            unityCashBalance = 100000;
            unityCashBalanceIntermediateStorage = 100000;
            unityCashBalanceCheck = 100000;
            unityPercentForTranslation = 0.1;
            unityDateStart = DateTime.Now;
            unityDateSalary = unityDateStart;
            salary = 5;
            unityGettingMission1 = false;
            unityGettingMission2 = false;
            unityGettingMission3 = false;
            unitySupplementaryMission2 = false;
            unityStartTimeMission1 = 10;
            unityStartTimeMission2 = 20;
            unityStartTimeMission3 = 30;
            unityFailedTimeMission1 = 240;
            unityFailedTimeMission2 = 300;
            unityFailedTimeMission3 = 360;
            unitySalaryPaymentTime = 1;
            unityMissionPool = new List<string>();
            unityMissionCompleted1 = false;
            unityMissionCompleted2 = false;
            unityMissionCompleted3 = false;
            //UnityDeserialization();
            Console.WriteLine(unityDateStart);
            Console.WriteLine(unityDateStart.AddMinutes(1));
            generalBaroBot = new TelegramBotClient(token);
            generalBaroBot.OnMessage += UnityMessageListener;

            generalBaroBot.StartReceiving();

        }

        /// <summary>
        /// Метод десерилизации данных для картеля
        /// </summary>
        public void CartelDeserialization()
        {
            string json;

            //переменные
            string pathСartelCashBalance = "Cartel\\Save\\СartelCashBalance.json";
            string pathСartelCashBalanceIntermediateStorage = "Cartel\\Save\\СartelCashBalanceIntermediateStorage.json";
            string pathСartelCashBalanceCheck = "Cartel\\Save\\СartelCashBalanceCheck.json";
            string pathPercentForTranslation = "Cartel\\Save\\PercentForTranslation.json";
            string pathСartelpercentForTranslationTrucks = "Cartel\\Save\\СartelpercentForTranslationTrucks.json";
            string pathСartelpercentForTranslationHelicopters = "Cartel\\Save\\СartelpercentForTranslationHelicopters.json";
            string pathСartelpercentForTranslationAircraft = "Cartel\\Save\\СartelpercentForTranslationAircraft.json";
            string pathСartelCocaCash = "Cartel\\Save\\СartelCocaCash.json";
            string pathСartelCocaPrice = "Cartel\\Save\\СartelCocaPrice.json";
            string pathDateStart = "Cartel\\Save\\DateStart.json";
            string pathСartelGreenPlantation = "Cartel\\Save\\СartelGreenPlantation.json";
            string pathСartelYellowPlantation = "Cartel\\Save\\СartelYellowPlantation.json";
            string pathСartelRedPlantation = "Cartel\\Save\\СartelRedPlantation.json";
            string pathСartelTributeDreamer = "Cartel\\Save\\СartelTributeDreamer.json";
            string pathGettingMission1 = "Cartel\\Save\\GettingMission1.json";
            string pathGettingMission2 = "Cartel\\Save\\GettingMission2.json";
            string pathGettingMission3 = "Cartel\\Save\\GettingMission3.json";
            string pathBoolGreenPlantation = "Cartel\\Save\\BoolGreenPlantation.json";
            string pathBoolYellowPlantation = "Cartel\\Save\\BoolYellowPlantation.json";
            string pathBoolRedPlantation = "Cartel\\Save\\BoolRedPlantation.json";
            string pathСartelSupplementaryMission2 = "Cartel\\Save\\СartelSupplementaryMission2.json";
            string pathAmountCocaHarvestedGreenPlantation = "Cartel\\Save\\AmountCocaHarvestedGreenPlantation.json";
            string pathAmountCocaHarvestedYellowPlantation = "Cartel\\Save\\AmountCocaHarvestedYellowPlantation.json";
            string pathAmountCocaHarvestedRedPlantation = "Cartel\\Save\\СmountCocaHarvestedRedPlantation.json";
            string pathСartelNumberTrucks = "Cartel\\Save\\СartelNumberTrucks.json";
            string pathСartelNumberHelicopters = "Cartel\\Save\\СartelNumberHelicopters.json";
            string pathСartelNumberAircraft = "Cartel\\Save\\СartelNumberAircraft.json";
            string pathСartelNumberTrucksCount = "Cartel\\Save\\СartelNumberTrucksCount.json";
            string pathСartelNumberHelicoptersCount = "Cartel\\Save\\СartelNumberHelicoptersCount.json";
            string pathСartelNumberAircraftCount = "Cartel\\Save\\СartelNumberAircraftCount.json";
            string pathСartelNumberCocaTrucks = "Cartel\\Save\\СartelNumberCocaTrucks.json";
            string pathСartelNumberCocaHelicopters = "Cartel\\Save\\СartelNumberCocaHelicopters.json";
            string pathСartelNumberCocaAircraft = "Cartel\\Save\\СartelNumberCocaAircraft.json";
            string pathСartelDeliveryTimeTrucks = "Cartel\\Save\\СartelDeliveryTimeTrucks.json";
            string pathСartelDeliveryTimeHelicopters = "Cartel\\Save\\СartelDeliveryTimeHelicopters.json";
            string pathСartelDeliveryTimeAircraft = "Cartel\\Save\\СartelDeliveryTimeAircraft.json";
            string pathСartelStartTimeMission1 = "Cartel\\Save\\СartelStartTimeMission1.json";
            string pathСartelStartTimeMission2 = "Cartel\\Save\\СartelStartTimeMission2.json";
            string pathСartelStartTimeMission3 = "Cartel\\Save\\СartelStartTimeMission3.json";
            string pathСartelFailedTimeMission1 = "Cartel\\Save\\СartelFailedTimeMission1.json";
            string pathСartelFailedTimeMission2 = "Cartel\\Save\\СartelFailedTimeMission2.json";
            string pathСartelFailedTimeMission3 = "Cartel\\Save\\СartelFailedTimeMission3.json";
            string pathСartelTributePaymentTime = "Cartel\\Save\\СartelTributePaymentTime.json";
            string pathTribute = "Cartel\\Save\\Tribute.json";
            string pathCartelMissionCompleted1 = "Cartel\\Save\\CartelMissionCompleted1.json";
            string pathCartelMissionCompleted2 = "Cartel\\Save\\CartelMissionCompleted2.json";
            string pathCartelMissionCompleted3 = "Cartel\\Save\\CartelMissionCompleted3.json";

            //матрицы
            string pathСartelBoolTruckBusy = "Cartel\\Save\\СartelBoolTruckBusy.json";
            string pathСartelStartTransportationTruck = "Cartel\\Save\\СartelStartTransportationTruck.json";
            string pathСartelBoolTruckBusyFinishe = "Cartel\\Save\\СartelBoolTruckBusyFinishe.json";
            string pathСartelBoolHelicoptersBusy = "Cartel\\Save\\СartelBoolHelicoptersBusy.json";
            string pathСartelStartTransportationHelicopters = "Cartel\\Save\\СartelStartTransportationHelicopters.json";
            string pathСartelBoolHelicoptersBusyFinishe = "Cartel\\Save\\СartelBoolHelicoptersBusyFinishe.json";
            string pathСartelBoolAircraftBusy = "Cartel\\Save\\СartelBoolAircraftBusy.json";
            string pathСartelStartTransportationAircraft = "Cartel\\Save\\СartelStartTransportationAircraft.json";
            string pathСartelBoolAircraftBusyFinishe = "Cartel\\Save\\СartelBoolAircraftBusyFinishe.json";

            //Коллекции
            string pathMissionPool = "Cartel\\Save\\MissionPool.json";


            //Проверяем наличие файлов
            if (File.Exists(pathСartelCashBalance) == true)
            {
                json = File.ReadAllText(pathСartelCashBalance);
                cartelCashBalance = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelCashBalanceIntermediateStorage) == true)
            {
                json = File.ReadAllText(pathСartelCashBalanceIntermediateStorage);
                cartelCashBalanceIntermediateStorage = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelCashBalanceCheck) == true)
            {
                json = File.ReadAllText(pathСartelCashBalanceCheck);
                cartelCashBalanceCheck = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathPercentForTranslation) == true)
            {
                json = File.ReadAllText(pathPercentForTranslation);
                percentForTranslation = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelpercentForTranslationTrucks) == true)
            {
                json = File.ReadAllText(pathСartelpercentForTranslationTrucks);
                cartelpercentForTranslationTrucks = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelpercentForTranslationHelicopters) == true)
            {
                json = File.ReadAllText(pathСartelpercentForTranslationHelicopters);
                cartelpercentForTranslationHelicopters = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelpercentForTranslationAircraft) == true)
            {
                json = File.ReadAllText(pathСartelpercentForTranslationAircraft);
                cartelpercentForTranslationAircraft = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelCocaCash) == true)
            {
                json = File.ReadAllText(pathСartelCocaCash);
                cartelCocaCash = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelCocaPrice) == true)
            {
                json = File.ReadAllText(pathСartelCocaPrice);
                cartelCocaPrice = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathDateStart) == true)
            {
                json = File.ReadAllText(pathDateStart);
                dateStart = Convert.ToDateTime(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelGreenPlantation) == true)
            {
                json = File.ReadAllText(pathСartelGreenPlantation);
                cartelGreenPlantation = Convert.ToDateTime(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelYellowPlantation) == true)
            {
                json = File.ReadAllText(pathСartelYellowPlantation);
                cartelYellowPlantation = Convert.ToDateTime(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelRedPlantation) == true)
            {
                json = File.ReadAllText(pathСartelRedPlantation);
                cartelRedPlantation = Convert.ToDateTime(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelTributeDreamer) == true)
            {
                json = File.ReadAllText(pathСartelTributeDreamer);
                cartelTributeDreamer = Convert.ToDateTime(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGettingMission1) == true)
            {
                json = File.ReadAllText(pathGettingMission1);
                gettingMission1 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGettingMission2) == true)
            {
                json = File.ReadAllText(pathGettingMission2);
                gettingMission2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGettingMission3) == true)
            {
                json = File.ReadAllText(pathGettingMission3);
                gettingMission3 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathBoolGreenPlantation) == true)
            {
                json = File.ReadAllText(pathBoolGreenPlantation);
                boolGreenPlantation = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathBoolYellowPlantation) == true)
            {
                json = File.ReadAllText(pathBoolYellowPlantation);
                boolYellowPlantation = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathBoolRedPlantation) == true)
            {
                json = File.ReadAllText(pathBoolRedPlantation);
                boolRedPlantation = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelSupplementaryMission2) == true)
            {
                json = File.ReadAllText(pathСartelSupplementaryMission2);
                cartelSupplementaryMission2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathAmountCocaHarvestedGreenPlantation) == true)
            {
                json = File.ReadAllText(pathAmountCocaHarvestedGreenPlantation);
                amountCocaHarvestedGreenPlantation = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathAmountCocaHarvestedYellowPlantation) == true)
            {
                json = File.ReadAllText(pathAmountCocaHarvestedYellowPlantation);
                amountCocaHarvestedYellowPlantation = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathAmountCocaHarvestedRedPlantation) == true)
            {
                json = File.ReadAllText(pathAmountCocaHarvestedRedPlantation);
                amountCocaHarvestedRedPlantation = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelNumberTrucks) == true)
            {
                json = File.ReadAllText(pathСartelNumberTrucks);
                cartelNumberTrucks = Convert.ToInt32(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelNumberHelicopters) == true)
            {
                json = File.ReadAllText(pathСartelNumberHelicopters);
                cartelNumberHelicopters = Convert.ToInt32(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelNumberAircraft) == true)
            {
                json = File.ReadAllText(pathСartelNumberAircraft);
                cartelNumberAircraft = Convert.ToInt32(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelNumberTrucksCount) == true)
            {
                json = File.ReadAllText(pathСartelNumberTrucksCount);
                cartelNumberTrucksCount = Convert.ToInt32(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelNumberHelicoptersCount) == true)
            {
                json = File.ReadAllText(pathСartelNumberHelicoptersCount);
                cartelNumberHelicoptersCount = Convert.ToInt32(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelNumberAircraftCount) == true)
            {
                json = File.ReadAllText(pathСartelNumberAircraftCount);
                cartelNumberAircraftCount = Convert.ToInt32(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelNumberCocaTrucks) == true)
            {
                json = File.ReadAllText(pathСartelNumberCocaTrucks);
                cartelNumberCocaTrucks = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelNumberCocaHelicopters) == true)
            {
                json = File.ReadAllText(pathСartelNumberCocaHelicopters);
                cartelNumberCocaHelicopters = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelNumberCocaAircraft) == true)
            {
                json = File.ReadAllText(pathСartelNumberCocaAircraft);
                cartelNumberCocaAircraft = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelDeliveryTimeTrucks) == true)
            {
                json = File.ReadAllText(pathСartelDeliveryTimeTrucks);
                cartelDeliveryTimeTrucks = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelDeliveryTimeHelicopters) == true)
            {
                json = File.ReadAllText(pathСartelDeliveryTimeHelicopters);
                cartelDeliveryTimeHelicopters = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelDeliveryTimeAircraft) == true)
            {
                json = File.ReadAllText(pathСartelDeliveryTimeAircraft);
                cartelDeliveryTimeAircraft = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelStartTimeMission1) == true)
            {
                json = File.ReadAllText(pathСartelStartTimeMission1);
                cartelStartTimeMission1 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelStartTimeMission2) == true)
            {
                json = File.ReadAllText(pathСartelStartTimeMission2);
                cartelStartTimeMission2 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelStartTimeMission3) == true)
            {
                json = File.ReadAllText(pathСartelStartTimeMission3);
                cartelStartTimeMission3 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelFailedTimeMission1) == true)
            {
                json = File.ReadAllText(pathСartelFailedTimeMission1);
                cartelFailedTimeMission1 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelFailedTimeMission2) == true)
            {
                json = File.ReadAllText(pathСartelFailedTimeMission2);
                cartelFailedTimeMission2 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelFailedTimeMission3) == true)
            {
                json = File.ReadAllText(pathСartelFailedTimeMission3);
                cartelFailedTimeMission3 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelTributePaymentTime) == true)
            {
                json = File.ReadAllText(pathСartelTributePaymentTime);
                cartelTributePaymentTime = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathTribute) == true)
            {
                json = File.ReadAllText(pathTribute);
                tribute = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathСartelBoolTruckBusy) == true)
            {
                json = File.ReadAllText(pathСartelBoolTruckBusy);
                cartelBoolTruckBusy = JsonConvert.DeserializeObject<bool[]>(json);
            }

            if (File.Exists(pathСartelStartTransportationTruck) == true)
            {
                json = File.ReadAllText(pathСartelStartTransportationTruck);
                cartelStartTransportationTruck = JsonConvert.DeserializeObject<DateTime[]>(json);
            }

            if (File.Exists(pathСartelBoolTruckBusyFinishe) == true)
            {
                json = File.ReadAllText(pathСartelBoolTruckBusyFinishe);
                cartelBoolTruckBusyFinishe = JsonConvert.DeserializeObject<bool[]>(json);
            }

            if (File.Exists(pathСartelBoolHelicoptersBusy) == true)
            {
                json = File.ReadAllText(pathСartelBoolHelicoptersBusy);
                cartelBoolHelicoptersBusy = JsonConvert.DeserializeObject<bool[]>(json);
            }

            if (File.Exists(pathСartelStartTransportationHelicopters) == true)
            {
                json = File.ReadAllText(pathСartelStartTransportationHelicopters);
                cartelStartTransportationHelicopters = JsonConvert.DeserializeObject<DateTime[]>(json);
            }

            if (File.Exists(pathСartelBoolHelicoptersBusyFinishe) == true)
            {
                json = File.ReadAllText(pathСartelBoolHelicoptersBusyFinishe);
                cartelBoolHelicoptersBusyFinishe = JsonConvert.DeserializeObject<bool[]>(json);
            }

            if (File.Exists(pathСartelBoolAircraftBusy) == true)
            {
                json = File.ReadAllText(pathСartelBoolAircraftBusy);
                cartelBoolAircraftBusy = JsonConvert.DeserializeObject<bool[]>(json);
            }

            if (File.Exists(pathСartelStartTransportationAircraft) == true)
            {
                json = File.ReadAllText(pathСartelStartTransportationAircraft);
                cartelStartTransportationAircraft = JsonConvert.DeserializeObject<DateTime[]>(json);
            }

            if (File.Exists(pathСartelBoolAircraftBusyFinishe) == true)
            {
                json = File.ReadAllText(pathСartelBoolAircraftBusyFinishe);
                cartelBoolAircraftBusyFinishe = JsonConvert.DeserializeObject<bool[]>(json);
            }

            if (File.Exists(pathMissionPool) == true)
            {
                json = File.ReadAllText(pathMissionPool);
                missionPool = JsonConvert.DeserializeObject<List<string>>(json);
            }

            if (File.Exists(pathCartelMissionCompleted1) == true)
            {
                json = File.ReadAllText(pathCartelMissionCompleted1);
                cartelMissionCompleted1 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathCartelMissionCompleted2) == true)
            {
                json = File.ReadAllText(pathCartelMissionCompleted2);
                cartelMissionCompleted2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathCartelMissionCompleted3) == true)
            {
                json = File.ReadAllText(pathCartelMissionCompleted3);
                cartelMissionCompleted3 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

        }

        /// <summary>
        /// Метод Серилизации данных для картеля
        /// </summary>
        public void CartelSerialization()
        {
            string json;

            //переменные
            string pathСartelCashBalance = "Cartel\\Save\\СartelCashBalance.json";
            string pathСartelCashBalanceIntermediateStorage = "Cartel\\Save\\СartelCashBalanceIntermediateStorage.json";
            string pathСartelCashBalanceCheck = "Cartel\\Save\\СartelCashBalanceCheck.json";
            string pathPercentForTranslation = "Cartel\\Save\\PercentForTranslation.json";
            string pathСartelpercentForTranslationTrucks = "Cartel\\Save\\СartelpercentForTranslationTrucks.json";
            string pathСartelpercentForTranslationHelicopters = "Cartel\\Save\\СartelpercentForTranslationHelicopters.json";
            string pathСartelpercentForTranslationAircraft = "Cartel\\Save\\СartelpercentForTranslationAircraft.json";
            string pathСartelCocaCash = "Cartel\\Save\\СartelCocaCash.json";
            string pathСartelCocaPrice = "Cartel\\Save\\СartelCocaPrice.json";
            string pathDateStart = "Cartel\\Save\\DateStart.json";
            string pathСartelGreenPlantation = "Cartel\\Save\\СartelGreenPlantation.json";
            string pathСartelYellowPlantation = "Cartel\\Save\\СartelYellowPlantation.json";
            string pathСartelRedPlantation = "Cartel\\Save\\СartelRedPlantation.json";
            string pathСartelTributeDreamer = "Cartel\\Save\\СartelTributeDreamer.json";
            string pathGettingMission1 = "Cartel\\Save\\GettingMission1.json";
            string pathGettingMission2 = "Cartel\\Save\\GettingMission2.json";
            string pathGettingMission3 = "Cartel\\Save\\GettingMission3.json";
            string pathBoolGreenPlantation = "Cartel\\Save\\BoolGreenPlantation.json";
            string pathBoolYellowPlantation = "Cartel\\Save\\BoolYellowPlantation.json";
            string pathBoolRedPlantation = "Cartel\\Save\\BoolRedPlantation.json";
            string pathСartelSupplementaryMission2 = "Cartel\\Save\\СartelSupplementaryMission2.json";
            string pathAmountCocaHarvestedGreenPlantation = "Cartel\\Save\\AmountCocaHarvestedGreenPlantation.json";
            string pathAmountCocaHarvestedYellowPlantation = "Cartel\\Save\\AmountCocaHarvestedYellowPlantation.json";
            string pathAmountCocaHarvestedRedPlantation = "Cartel\\Save\\СmountCocaHarvestedRedPlantation.json";
            string pathСartelNumberTrucks = "Cartel\\Save\\СartelNumberTrucks.json";
            string pathСartelNumberHelicopters = "Cartel\\Save\\СartelNumberHelicopters.json";
            string pathСartelNumberAircraft = "Cartel\\Save\\СartelNumberAircraft.json";
            string pathСartelNumberTrucksCount = "Cartel\\Save\\СartelNumberTrucksCount.json";
            string pathСartelNumberHelicoptersCount = "Cartel\\Save\\СartelNumberHelicoptersCount.json";
            string pathСartelNumberAircraftCount = "Cartel\\Save\\СartelNumberAircraftCount.json";
            string pathСartelNumberCocaTrucks = "Cartel\\Save\\СartelNumberCocaTrucks.json";
            string pathСartelNumberCocaHelicopters = "Cartel\\Save\\СartelNumberCocaHelicopters.json";
            string pathСartelNumberCocaAircraft = "Cartel\\Save\\СartelNumberCocaAircraft.json";
            string pathСartelDeliveryTimeTrucks = "Cartel\\Save\\СartelDeliveryTimeTrucks.json";
            string pathСartelDeliveryTimeHelicopters = "Cartel\\Save\\СartelDeliveryTimeHelicopters.json";
            string pathСartelDeliveryTimeAircraft = "Cartel\\Save\\СartelDeliveryTimeAircraft.json";
            string pathСartelStartTimeMission1 = "Cartel\\Save\\СartelStartTimeMission1.json";
            string pathСartelStartTimeMission2 = "Cartel\\Save\\СartelStartTimeMission2.json";
            string pathСartelStartTimeMission3 = "Cartel\\Save\\СartelStartTimeMission3.json";
            string pathСartelFailedTimeMission1 = "Cartel\\Save\\СartelFailedTimeMission1.json";
            string pathСartelFailedTimeMission2 = "Cartel\\Save\\СartelFailedTimeMission2.json";
            string pathСartelFailedTimeMission3 = "Cartel\\Save\\СartelFailedTimeMission3.json";
            string pathСartelTributePaymentTime = "Cartel\\Save\\СartelTributePaymentTime.json";
            string pathTribute = "Cartel\\Save\\Tribute.json";
            string pathCartelMissionCompleted1 = "Cartel\\Save\\CartelMissionCompleted1.json";
            string pathCartelMissionCompleted2 = "Cartel\\Save\\CartelMissionCompleted2.json";
            string pathCartelMissionCompleted3 = "Cartel\\Save\\CartelMissionCompleted3.json";

            //матрицы
            string pathСartelBoolTruckBusy = "Cartel\\Save\\СartelBoolTruckBusy.json";
            string pathСartelStartTransportationTruck = "Cartel\\Save\\СartelStartTransportationTruck.json";
            string pathСartelBoolTruckBusyFinishe = "Cartel\\Save\\СartelBoolTruckBusyFinishe.json";
            string pathСartelBoolHelicoptersBusy = "Cartel\\Save\\СartelBoolHelicoptersBusy.json";
            string pathСartelStartTransportationHelicopters = "Cartel\\Save\\СartelStartTransportationHelicopters.json";
            string pathСartelBoolHelicoptersBusyFinishe = "Cartel\\Save\\СartelBoolHelicoptersBusyFinishe.json";
            string pathСartelBoolAircraftBusy = "Cartel\\Save\\СartelBoolAircraftBusy.json";
            string pathСartelStartTransportationAircraft = "Cartel\\Save\\СartelStartTransportationAircraft.json";
            string pathСartelBoolAircraftBusyFinishe = "Cartel\\Save\\СartelBoolAircraftBusyFinishe.json";

            //Коллекции
            string pathMissionPool = "Cartel\\Save\\MissionPool.json";

            //Серилизация
            json = JsonConvert.SerializeObject(cartelCashBalance);
            File.WriteAllText(pathСartelCashBalance, json);

            json = JsonConvert.SerializeObject(cartelCashBalanceIntermediateStorage);
            File.WriteAllText(pathСartelCashBalanceIntermediateStorage, json);

            json = JsonConvert.SerializeObject(cartelCashBalanceCheck);
            File.WriteAllText(pathСartelCashBalanceCheck, json);

            json = JsonConvert.SerializeObject(percentForTranslation);
            File.WriteAllText(pathPercentForTranslation, json);

            json = JsonConvert.SerializeObject(cartelpercentForTranslationTrucks);
            File.WriteAllText(pathСartelpercentForTranslationTrucks, json);

            json = JsonConvert.SerializeObject(cartelpercentForTranslationHelicopters);
            File.WriteAllText(pathСartelpercentForTranslationHelicopters, json);

            json = JsonConvert.SerializeObject(cartelpercentForTranslationAircraft);
            File.WriteAllText(pathСartelpercentForTranslationAircraft, json);

            json = JsonConvert.SerializeObject(cartelCocaCash);
            File.WriteAllText(pathСartelCocaCash, json);

            json = JsonConvert.SerializeObject(cartelCocaPrice);
            File.WriteAllText(pathСartelCocaPrice, json);

            json = JsonConvert.SerializeObject(dateStart);
            File.WriteAllText(pathDateStart, json);

            json = JsonConvert.SerializeObject(cartelGreenPlantation);
            File.WriteAllText(pathСartelGreenPlantation, json);

            json = JsonConvert.SerializeObject(cartelYellowPlantation);
            File.WriteAllText(pathСartelYellowPlantation, json);

            json = JsonConvert.SerializeObject(cartelRedPlantation);
            File.WriteAllText(pathСartelRedPlantation, json);

            json = JsonConvert.SerializeObject(cartelTributeDreamer);
            File.WriteAllText(pathСartelTributeDreamer, json);

            json = JsonConvert.SerializeObject(gettingMission1);
            File.WriteAllText(pathGettingMission1, json);

            json = JsonConvert.SerializeObject(gettingMission2);
            File.WriteAllText(pathGettingMission2, json);

            json = JsonConvert.SerializeObject(gettingMission3);
            File.WriteAllText(pathGettingMission3, json);

            json = JsonConvert.SerializeObject(boolGreenPlantation);
            File.WriteAllText(pathBoolGreenPlantation, json);

            json = JsonConvert.SerializeObject(boolYellowPlantation);
            File.WriteAllText(pathBoolYellowPlantation, json);

            json = JsonConvert.SerializeObject(boolRedPlantation);
            File.WriteAllText(pathBoolRedPlantation, json);

            json = JsonConvert.SerializeObject(cartelSupplementaryMission2);
            File.WriteAllText(pathСartelSupplementaryMission2, json);

            json = JsonConvert.SerializeObject(amountCocaHarvestedGreenPlantation);
            File.WriteAllText(pathAmountCocaHarvestedGreenPlantation, json);

            json = JsonConvert.SerializeObject(amountCocaHarvestedYellowPlantation);
            File.WriteAllText(pathAmountCocaHarvestedYellowPlantation, json);

            json = JsonConvert.SerializeObject(amountCocaHarvestedRedPlantation);
            File.WriteAllText(pathAmountCocaHarvestedRedPlantation, json);

            json = JsonConvert.SerializeObject(cartelNumberTrucks);
            File.WriteAllText(pathСartelNumberTrucks, json);

            json = JsonConvert.SerializeObject(cartelNumberHelicopters);
            File.WriteAllText(pathСartelNumberHelicopters, json);

            json = JsonConvert.SerializeObject(cartelNumberAircraft);
            File.WriteAllText(pathСartelNumberAircraft, json);

            json = JsonConvert.SerializeObject(cartelNumberTrucksCount);
            File.WriteAllText(pathСartelNumberTrucksCount, json);

            json = JsonConvert.SerializeObject(cartelNumberHelicoptersCount);
            File.WriteAllText(pathСartelNumberHelicoptersCount, json);

            json = JsonConvert.SerializeObject(cartelNumberAircraftCount);
            File.WriteAllText(pathСartelNumberAircraftCount, json);

            json = JsonConvert.SerializeObject(cartelNumberCocaTrucks);
            File.WriteAllText(pathСartelNumberCocaTrucks, json);

            json = JsonConvert.SerializeObject(cartelNumberCocaHelicopters);
            File.WriteAllText(pathСartelNumberCocaHelicopters, json);

            json = JsonConvert.SerializeObject(cartelNumberCocaAircraft);
            File.WriteAllText(pathСartelNumberCocaAircraft, json);

            json = JsonConvert.SerializeObject(cartelDeliveryTimeTrucks);
            File.WriteAllText(pathСartelDeliveryTimeTrucks, json);

            json = JsonConvert.SerializeObject(cartelDeliveryTimeHelicopters);
            File.WriteAllText(pathСartelDeliveryTimeHelicopters, json);

            json = JsonConvert.SerializeObject(cartelDeliveryTimeAircraft);
            File.WriteAllText(pathСartelDeliveryTimeAircraft, json);

            json = JsonConvert.SerializeObject(cartelStartTimeMission1);
            File.WriteAllText(pathСartelStartTimeMission1, json);

            json = JsonConvert.SerializeObject(cartelStartTimeMission2);
            File.WriteAllText(pathСartelStartTimeMission2, json);

            json = JsonConvert.SerializeObject(cartelStartTimeMission3);
            File.WriteAllText(pathСartelStartTimeMission3, json);

            json = JsonConvert.SerializeObject(cartelFailedTimeMission1);
            File.WriteAllText(pathСartelFailedTimeMission1, json);

            json = JsonConvert.SerializeObject(cartelFailedTimeMission2);
            File.WriteAllText(pathСartelFailedTimeMission2, json);

            json = JsonConvert.SerializeObject(cartelFailedTimeMission3);
            File.WriteAllText(pathСartelFailedTimeMission3, json);

            json = JsonConvert.SerializeObject(cartelTributePaymentTime);
            File.WriteAllText(pathСartelTributePaymentTime, json);

            json = JsonConvert.SerializeObject(tribute);
            File.WriteAllText(pathTribute, json);

            json = JsonConvert.SerializeObject(cartelBoolTruckBusy);
            File.WriteAllText(pathСartelBoolTruckBusy, json);

            json = JsonConvert.SerializeObject(cartelStartTransportationTruck);
            File.WriteAllText(pathСartelStartTransportationTruck, json);

            json = JsonConvert.SerializeObject(cartelBoolTruckBusyFinishe);
            File.WriteAllText(pathСartelBoolTruckBusyFinishe, json);

            json = JsonConvert.SerializeObject(cartelBoolHelicoptersBusy);
            File.WriteAllText(pathСartelBoolHelicoptersBusy, json);

            json = JsonConvert.SerializeObject(cartelStartTransportationHelicopters);
            File.WriteAllText(pathСartelStartTransportationHelicopters, json);

            json = JsonConvert.SerializeObject(cartelBoolHelicoptersBusyFinishe);
            File.WriteAllText(pathСartelBoolHelicoptersBusyFinishe, json);

            json = JsonConvert.SerializeObject(cartelBoolAircraftBusy);
            File.WriteAllText(pathСartelBoolAircraftBusy, json);

            json = JsonConvert.SerializeObject(cartelStartTransportationAircraft);
            File.WriteAllText(pathСartelStartTransportationAircraft, json);

            json = JsonConvert.SerializeObject(cartelBoolAircraftBusyFinishe);
            File.WriteAllText(pathСartelBoolAircraftBusyFinishe, json);

            json = JsonConvert.SerializeObject(missionPool);
            File.WriteAllText(pathMissionPool, json);

            json = JsonConvert.SerializeObject(cartelMissionCompleted1);
            File.WriteAllText(pathCartelMissionCompleted1, json);

            json = JsonConvert.SerializeObject(cartelMissionCompleted1);
            File.WriteAllText(pathCartelMissionCompleted1, json);

            json = JsonConvert.SerializeObject(cartelMissionCompleted2);
            File.WriteAllText(pathCartelMissionCompleted2, json);

            json = JsonConvert.SerializeObject(cartelMissionCompleted3);
            File.WriteAllText(pathCartelMissionCompleted3, json);

        }

        /// <summary>
        /// Метод десерилизации данных для Единства
        /// </summary>
        public void UnityDeserialization()
        {
            string json;

            //переменные
            string pathUnityCashBalance = "Unity\\Save\\UnityCashBalance.json";
            string pathUnityCashBalanceIntermediateStorage = "Unity\\Save\\UnityCashBalanceIntermediateStorage.json";
            string pathUnityCashBalanceCheck = "Unity\\Save\\UnityCashBalanceCheck.json";
            string pathUnityPercentForTranslation = "Unity\\Save\\UnityPercentForTranslation.json";
            string pathUnityDateStart = "Unity\\Save\\UnityDateStart.json";
            string pathUnityDateSalary = "Unity\\Save\\UnityDateSalary.json";
            string pathSalary = "Unity\\Save\\Salary.json";
            string pathUnityGettingMission1 = "Unity\\Save\\UnityGettingMission1.json";
            string pathUnityGettingMission2 = "Unity\\Save\\UnityGettingMission2.json";
            string pathUnityGettingMission3 = "Unity\\Save\\UnityGettingMission3.json";
            string pathUnitySupplementaryMission2 = "Unity\\Save\\UnitySupplementaryMission2.json";
            string pathUnityStartTimeMission1 = "Unity\\Save\\UnityStartTimeMission1.json";
            string pathUnityStartTimeMission2 = "Unity\\Save\\UnityStartTimeMission2.json";
            string pathUnityStartTimeMission3 = "Unity\\Save\\UnityStartTimeMission3.json";
            string pathUnityFailedTimeMission1 = "Unity\\Save\\UnityFailedTimeMission1.json";
            string pathUnityFailedTimeMission2 = "Unity\\Save\\UnityFailedTimeMission2.json";
            string pathUnityFailedTimeMission3 = "Unity\\Save\\UnityFailedTimeMission3.json";
            string pathUnitySalaryPaymentTime = "Unity\\Save\\UnitySalaryPaymentTime.json";
            string pathUnityMissionCompleted1 = "Unity\\Save\\UnityMissionCompleted1.json";
            string pathUnityMissionCompleted2 = "Unity\\Save\\UnityMissionCompleted2.json";
            string pathUnityMissionCompleted3 = "Unity\\Save\\UnityMissionCompleted3.json";

            //Коллекции
            string pathUnityMissionPool = "Unity\\Save\\UnityMissionPool.json";


            //Проверяем наличие файлов
            if (File.Exists(pathUnityCashBalance) == true)
            {
                json = File.ReadAllText(pathUnityCashBalance);
                unityCashBalance = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityCashBalanceIntermediateStorage) == true)
            {
                json = File.ReadAllText(pathUnityCashBalanceIntermediateStorage);
                unityCashBalanceIntermediateStorage = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityCashBalanceCheck) == true)
            {
                json = File.ReadAllText(pathUnityCashBalanceCheck);
                unityCashBalanceCheck = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityPercentForTranslation) == true)
            {
                json = File.ReadAllText(pathUnityPercentForTranslation);
                unityPercentForTranslation = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityDateStart) == true)
            {
                json = File.ReadAllText(pathUnityDateStart);
                unityDateStart = Convert.ToDateTime(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityDateSalary) == true)
            {
                json = File.ReadAllText(pathUnityDateSalary);
                unityDateSalary = Convert.ToDateTime(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathSalary) == true)
            {
                json = File.ReadAllText(pathSalary);
                salary = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityGettingMission1) == true)
            {
                json = File.ReadAllText(pathUnityGettingMission1);
                unityGettingMission1 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityGettingMission2) == true)
            {
                json = File.ReadAllText(pathUnityGettingMission2);
                unityGettingMission2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityGettingMission3) == true)
            {
                json = File.ReadAllText(pathUnityGettingMission3);
                unityGettingMission3 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnitySupplementaryMission2) == true)
            {
                json = File.ReadAllText(pathUnitySupplementaryMission2);
                unitySupplementaryMission2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityStartTimeMission1) == true)
            {
                json = File.ReadAllText(pathUnityStartTimeMission1);
                unityStartTimeMission1 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityStartTimeMission2) == true)
            {
                json = File.ReadAllText(pathUnityStartTimeMission2);
                unityStartTimeMission2 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityStartTimeMission3) == true)
            {
                json = File.ReadAllText(pathUnityStartTimeMission3);
                unityStartTimeMission3 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityFailedTimeMission1) == true)
            {
                json = File.ReadAllText(pathUnityFailedTimeMission1);
                unityFailedTimeMission1 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityFailedTimeMission2) == true)
            {
                json = File.ReadAllText(pathUnityFailedTimeMission2);
                unityFailedTimeMission2 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityFailedTimeMission3) == true)
            {
                json = File.ReadAllText(pathUnityFailedTimeMission3);
                unityFailedTimeMission3 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnitySalaryPaymentTime) == true)
            {
                json = File.ReadAllText(pathUnitySalaryPaymentTime);
                unitySalaryPaymentTime = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityMissionPool) == true)
            {
                json = File.ReadAllText(pathUnityMissionPool);
                unityMissionPool = JsonConvert.DeserializeObject<List<string>>(json);
            }

            if (File.Exists(pathUnityMissionCompleted1) == true)
            {
                json = File.ReadAllText(pathUnityMissionCompleted1);
                unityMissionCompleted1 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityMissionCompleted2) == true)
            {
                json = File.ReadAllText(pathUnityMissionCompleted2);
                unityMissionCompleted2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathUnityMissionCompleted3) == true)
            {
                json = File.ReadAllText(pathUnityMissionCompleted3);
                unityMissionCompleted3 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

        }

        /// <summary>
        /// Метод Серилизации данных для Единства
        /// </summary>
        public void UnitySerialization()
        {
            string json;

            //переменные
            string pathUnityCashBalance = "Unity\\Save\\UnityCashBalance.json";
            string pathUnityCashBalanceIntermediateStorage = "Unity\\Save\\UnityCashBalanceIntermediateStorage.json";
            string pathUnityCashBalanceCheck = "Unity\\Save\\UnityCashBalanceCheck.json";
            string pathUnityPercentForTranslation = "Unity\\Save\\UnityPercentForTranslation.json";
            string pathUnityDateStart = "Unity\\Save\\UnityDateStart.json";
            string pathUnityDateSalary = "Unity\\Save\\UnityDateSalary.json";
            string pathSalary = "Unity\\Save\\Salary.json";
            string pathUnityGettingMission1 = "Unity\\Save\\UnityGettingMission1.json";
            string pathUnityGettingMission2 = "Unity\\Save\\UnityGettingMission2.json";
            string pathUnityGettingMission3 = "Unity\\Save\\UnityGettingMission3.json";
            string pathUnitySupplementaryMission2 = "Unity\\Save\\UnitySupplementaryMission2.json";
            string pathUnityStartTimeMission1 = "Unity\\Save\\UnityStartTimeMission1.json";
            string pathUnityStartTimeMission2 = "Unity\\Save\\UnityStartTimeMission2.json";
            string pathUnityStartTimeMission3 = "Unity\\Save\\UnityStartTimeMission3.json";
            string pathUnityFailedTimeMission1 = "Unity\\Save\\UnityFailedTimeMission1.json";
            string pathUnityFailedTimeMission2 = "Unity\\Save\\UnityFailedTimeMission2.json";
            string pathUnityFailedTimeMission3 = "Unity\\Save\\UnityFailedTimeMission3.json";
            string pathUnitySalaryPaymentTime = "Unity\\Save\\UnitySalaryPaymentTime.json";
            string pathUnityMissionCompleted1 = "Unity\\Save\\UnityMissionCompleted1.json";
            string pathUnityMissionCompleted2 = "Unity\\Save\\UnityMissionCompleted2.json";
            string pathUnityMissionCompleted3 = "Unity\\Save\\UnityMissionCompleted3.json";

            //Коллекции
            string pathUnityMissionPool = "Unity\\Save\\UnityMissionPool.json";

            //Серилизация
            json = JsonConvert.SerializeObject(unityCashBalance);
            File.WriteAllText(pathUnityCashBalance, json);

            json = JsonConvert.SerializeObject(unityCashBalanceIntermediateStorage);
            File.WriteAllText(pathUnityCashBalanceIntermediateStorage, json);

            json = JsonConvert.SerializeObject(unityCashBalanceCheck);
            File.WriteAllText(pathUnityCashBalanceCheck, json);

            json = JsonConvert.SerializeObject(unityPercentForTranslation);
            File.WriteAllText(pathUnityPercentForTranslation, json);

            json = JsonConvert.SerializeObject(unityDateStart);
            File.WriteAllText(pathUnityDateStart, json);

            json = JsonConvert.SerializeObject(unityDateSalary);
            File.WriteAllText(pathUnityDateSalary, json);

            json = JsonConvert.SerializeObject(salary);
            File.WriteAllText(pathSalary, json);

            json = JsonConvert.SerializeObject(unityGettingMission1);
            File.WriteAllText(pathUnityGettingMission1, json);

            json = JsonConvert.SerializeObject(unityGettingMission2);
            File.WriteAllText(pathUnityGettingMission2, json);

            json = JsonConvert.SerializeObject(unityGettingMission3);
            File.WriteAllText(pathUnityGettingMission3, json);

            json = JsonConvert.SerializeObject(unitySupplementaryMission2);
            File.WriteAllText(pathUnitySupplementaryMission2, json);

            json = JsonConvert.SerializeObject(unityStartTimeMission1);
            File.WriteAllText(pathUnityStartTimeMission1, json);

            json = JsonConvert.SerializeObject(unityStartTimeMission2);
            File.WriteAllText(pathUnityStartTimeMission2, json);

            json = JsonConvert.SerializeObject(unityStartTimeMission3);
            File.WriteAllText(pathUnityStartTimeMission3, json);

            json = JsonConvert.SerializeObject(unityFailedTimeMission1);
            File.WriteAllText(pathUnityFailedTimeMission1, json);

            json = JsonConvert.SerializeObject(unityFailedTimeMission2);
            File.WriteAllText(pathUnityFailedTimeMission2, json);

            json = JsonConvert.SerializeObject(unityFailedTimeMission3);
            File.WriteAllText(pathUnityFailedTimeMission3, json);

            json = JsonConvert.SerializeObject(unitySalaryPaymentTime);
            File.WriteAllText(pathUnitySalaryPaymentTime, json);

            json = JsonConvert.SerializeObject(unityMissionPool);
            File.WriteAllText(pathUnityMissionPool, json);

            json = JsonConvert.SerializeObject(unityMissionCompleted1);
            File.WriteAllText(pathUnityMissionCompleted1, json);

            json = JsonConvert.SerializeObject(unityMissionCompleted2);
            File.WriteAllText(pathUnityMissionCompleted2, json);

            json = JsonConvert.SerializeObject(unityMissionCompleted3);
            File.WriteAllText(pathUnityMissionCompleted3, json);
        }

        public Dreamer_Bot_And_General_Baro_Bot(MainWindow w)
        {
            this.CartelBotMessageLog = new ObservableCollection<MessageLog>();
            this.w = w;
            Thread cartelStartTask = new Thread(CartelStart);
            cartelStartTask.Start();

            Thread unityStartTask = new Thread(UnityStart);
            unityStartTask.Start();

        }

        /// <summary>
        /// Ручная рассылка
        /// </summary>
        /// <param name="Text">Текст</param>
        /// <param name="Id">ИД</param>
        public void CartelSendMessage(string Text, string Id)
        {
            long id = Convert.ToInt64(Id);
            dreamerBot.SendTextMessageAsync(id, Text);
        }
    }
}

