﻿using System;
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

namespace Dreamer_Bot
{
    struct Pac_Katari_Bot
    {
        /// <summary>
        /// переменная показывающая смогли ли повстанцы выполнить миссию 1
        /// </summary>
        private static bool insurgentsMissionCompleted1;

        /// <summary>
        /// переменная показывающая смогли ли повстанцы выполнить миссию 2
        /// </summary>
        private static bool insurgentsMissionCompleted2;

        /// <summary>
        /// переменная показывающая смогли ли повстанцы выполнить миссию 3
        /// </summary>
        private static bool insurgentsMissionCompleted3;

        /// <summary>
        /// Переменная для хранения введенного текста
        /// </summary>
        private static string insurgentsMessageText;

        /// <summary>
        /// Переменная для хранения названия фото
        /// </summary>
        private static string insurgentsMessageTextCompletingTheMission;

        /// <summary>
        /// Коллекция для хранения миссий
        /// </summary>
        private static List<string> insurgentsMissionPool;

        /// <summary>
        /// Дата и время начала игры
        /// </summary>
        private static DateTime insurgentsDateStart;

        /// <summary>
        /// Дата и время для сравнения с датой и временем начала игры
        /// </summary>
        private static DateTime insurgentsComparisonDateFromStart;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 1
        /// </summary>
        private static bool insurgentsGettingMission1;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 2
        /// </summary>
        private static bool insurgentsGettingMission2;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 3
        /// </summary>
        private static bool insurgentsGettingMission3;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 1
        /// </summary>
        private static string insurgentsMission1ID;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 2
        /// </summary>
        private static string insurgentsMission2ID;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 3
        /// </summary>
        private static string insurgentsMission3ID;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 1
        /// </summary>
        private static string insurgentsMissionFailedID1;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 2
        /// </summary>
        private static string insurgentsMissionFailedID2;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 3
        /// </summary>
        private static string insurgentsMissionFailedID3;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 1
        /// </summary>
        private static string insurgentsMissionComplete1ID;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 2
        /// </summary>
        private static string insurgentsMissionComplete2ID;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 3
        /// </summary>
        private static string insurgentsMissionComplete3ID;

        /// <summary>
        /// Переменная для записи ID аудио файла отказа от миссии 2
        /// </summary>
        private static string insurgentsMissionAbandonmentID2;

        /// <summary>
        /// Время запуска мисии1
        /// </summary>
        private static double insurgentsStartTimeMission1;

        /// <summary>
        /// Время запуска мисии2
        /// </summary>
        private static double insurgentsStartTimeMission2;

        /// <summary>
        /// Время запуска мисии3
        /// </summary>
        private static double insurgentsStartTimeMission3;

        /// <summary>
        /// Время провала мисии1
        /// </summary>
        private static double insurgentsFailedTimeMission1;

        /// <summary>
        /// Время провала мисии2
        /// </summary>
        private static double insurgentsFailedTimeMission2;

        /// <summary>
        /// Время провала мисии3
        /// </summary>
        private static double insurgentsFailedTimeMission3;

        /// <summary>
        /// Получение дополнительной миссии ровно 1 раз
        /// </summary>
        private static bool insurgentsSupplementaryMission2;

        /// <summary>
        /// telegram бот клиент
        /// </summary>
        static TelegramBotClient pacKatariBot;

        /// <summary>
        /// Определяем статическую встроенную клавиатуру повстанцев
        /// </summary>
        private static ReplyKeyboardMarkup insurgentsOperationsMenu;

        /// <summary>
        /// Клавиатура принятия миссии
        /// </summary>
        private static InlineKeyboardMarkup insurgentsKeyboardAcceptOrRefuseAMission;

        /// <summary>
        /// Метод для ответа на сообщения от пользователей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static async void InsurgentsMessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            insurgentsComparisonDateFromStart = DateTime.Now;

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
                InsurgentsOperationsMenuOperation();
            }

            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && e.Message.Text == "/start")
            {
                //Выводим основную клавиатуру повстанцев
                InsurgentsOperationsMenuOperation();
                pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, "Добро пожаловать к повстанцам", replyMarkup: insurgentsOperationsMenu);
            }

            //если фото в виде документа, выводим его параметры и выполняем миссию соответствующую названию фото
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Document || e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                if (string.IsNullOrEmpty(e.Message.Caption) == false)
                {
                    insurgentsMessageTextCompletingTheMission = e.Message.Caption.ToLower();
                    switch (insurgentsMessageTextCompletingTheMission)
                    {
                        case "mission1":
                            if (insurgentsMissionPool.Contains("Миссия 1"))
                            {
                                await pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 1 Выполнена");
                                await pacKatariBot.SendAudioAsync(e.Message.Chat.Id, insurgentsMissionComplete1ID);
                                insurgentsMissionPool.Remove($"Миссия 1");
                                insurgentsMissionCompleted1 = true;
                            }
                            break;

                        case "mission2":
                            if (insurgentsMissionPool.Contains("Миссия 2"))
                            {
                                await pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 Выполнена");
                                await pacKatariBot.SendAudioAsync(e.Message.Chat.Id, insurgentsMissionComplete2ID);
                                insurgentsMissionPool.Remove($"Миссия 2");
                                insurgentsMissionCompleted2 = true;
                            }
                            break;

                        case "mission3":
                            if (insurgentsMissionPool.Contains("Миссия 3"))
                            {
                                await pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 Выполнена");
                                await pacKatariBot.SendAudioAsync(e.Message.Chat.Id, insurgentsMissionComplete3ID);
                                insurgentsMissionPool.Remove($"Миссия 3");
                                insurgentsMissionCompleted3 = true;
                            }
                            break;
                    }
                }
            }

            //если прикладывается аудио, выводим его параметры и записываем ID аудио
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Audio)
            {
                insurgentsMessageText = e.Message.Audio.FileName;
                switch (insurgentsMessageText)
                {
                    case "InsurgentsMission1.mp3":
                        insurgentsMission1ID = e.Message.Audio.FileId;
                        break;

                    case "InsurgentsMission2.mp3":
                        insurgentsMission2ID = e.Message.Audio.FileId;
                        break;

                    case "InsurgentsMission3.mp3":
                        insurgentsMission3ID = e.Message.Audio.FileId;
                        break;

                    case "InsurgentsMissionFailed1.mp3":
                        insurgentsMissionFailedID1 = e.Message.Audio.FileId;
                        break;

                    case "InsurgentsMissionFailed2.mp3":
                        insurgentsMissionFailedID2 = e.Message.Audio.FileId;
                        break;

                    case "InsurgentsMissionFailed3.mp3":
                        insurgentsMissionFailedID3 = e.Message.Audio.FileId;
                        break;

                    case "InsurgentsMissionComplete1.mp3":
                        insurgentsMissionComplete1ID = e.Message.Audio.FileId;
                        break;

                    case "InsurgentsMissionComplete2.mp3":
                        insurgentsMissionComplete2ID = e.Message.Audio.FileId;
                        break;

                    case "InsurgentsMissionComplete3.mp3":
                        insurgentsMissionComplete3ID = e.Message.Audio.FileId;
                        break;

                    case "InsurgentsMissionAbandonment2.mp3":
                        insurgentsMissionAbandonmentID2 = e.Message.Audio.FileId;
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
            pacKatariBot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

            //Получаем обязательную миссию 1
            if (insurgentsComparisonDateFromStart >= insurgentsDateStart.AddMinutes(insurgentsStartTimeMission1) && insurgentsGettingMission1 == false)
            {
                await pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, $"Получена миссия 1");
                await pacKatariBot.SendAudioAsync(e.Message.Chat.Id, insurgentsMission1ID);
                insurgentsGettingMission1 = true;

                if (insurgentsMissionPool.Contains("Миссия 1") == false)
                {
                    insurgentsMissionPool.Add($"Миссия 1");
                }
            }

            //Получаем дополнительную миссию 2
            if (insurgentsComparisonDateFromStart >= insurgentsDateStart.AddMinutes(insurgentsStartTimeMission2) && insurgentsGettingMission2 == false && insurgentsSupplementaryMission2 == false)
            {
                insurgentsSupplementaryMission2 = true;
                InsurgentsAcceptOrRefuseAMission();
                pacKatariBot.OnCallbackQuery -= InsurgentsAcceptanceOrRefusalOfAMission;
                await pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, $"Поступила дополнительная миссия 2. Принять миссию?", replyMarkup: insurgentsKeyboardAcceptOrRefuseAMission);
                pacKatariBot.OnCallbackQuery += InsurgentsAcceptanceOrRefusalOfAMission;
            }

            //Получаем обязательную миссию 3
            if (insurgentsComparisonDateFromStart >= insurgentsDateStart.AddMinutes(insurgentsStartTimeMission3) && insurgentsGettingMission3 == false)
            {
                await pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, $"Получена миссия 3");
                await pacKatariBot.SendAudioAsync(e.Message.Chat.Id, insurgentsMission3ID);
                insurgentsGettingMission3 = true;

                if (insurgentsMissionPool.Contains("Миссия 3") == false)
                {
                    insurgentsMissionPool.Add($"Миссия 3");
                }

            }

            //Проваливаем обязательную миссию 1
            if (insurgentsComparisonDateFromStart >= insurgentsDateStart.AddMinutes(insurgentsFailedTimeMission1) && insurgentsMissionPool.Contains("Миссия 1"))
            {
                await pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 1 провалена");
                await pacKatariBot.SendAudioAsync(e.Message.Chat.Id, insurgentsMissionFailedID1);
                insurgentsMissionPool.Remove($"Миссия 1");
            }

            //Проваливаем дополнительную миссию 2
            if (insurgentsComparisonDateFromStart >= insurgentsDateStart.AddMinutes(insurgentsFailedTimeMission2) && insurgentsMissionPool.Contains("Миссия 2"))
            {
                await pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 провалена");
                await pacKatariBot.SendAudioAsync(e.Message.Chat.Id, insurgentsMissionFailedID2);
                insurgentsMissionPool.Remove($"Миссия 2");
            }

            //Проваливаем обязательную миссию 3
            if (insurgentsComparisonDateFromStart >= insurgentsDateStart.AddMinutes(insurgentsFailedTimeMission3) && insurgentsMissionPool.Contains("Миссия 3"))
            {
                await pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 провалена");
                await pacKatariBot.SendAudioAsync(e.Message.Chat.Id, insurgentsMissionFailedID3);
                insurgentsMissionPool.Remove($"Миссия 3");
            }

            //если сообщение текстовое, то записываем в переменную текст сообщения и проводим операции КВЕСТЫ
            insurgentsMessageText = e.Message.Text;
            switch (insurgentsMessageText)
            {
                //Проверяем текущие квесты
                case "Квесты":
                    if (insurgentsMissionPool.Count == 0)
                    {
                        await pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, $"Здраствуйте, Активных квестов нет");
                    }
                    else
                    {
                        for (int i = 0; i < insurgentsMissionPool.Count; i++)
                        {
                            await pacKatariBot.SendTextMessageAsync(e.Message.Chat.Id, $"{insurgentsMissionPool[i]}");
                        }
                    }
                    break;
            }

            InsurgentsSerialization();
            Thread.Sleep(200);

            //Если сообщение не текстовое, то выходим из метода
            if (e.Message.Text == null)
            {
                return;
            }
        }

        /// <summary>
        /// Метод скачивания документа
        /// </summary>
        /// <param name="fileId">ИД документа</param>
        /// <param name="path"> путь сохранения файла</param>
        static async void InsurgentsDownLoad(string fileId, string path)
        {
            var file = await pacKatariBot.GetFileAsync(fileId);
            FileStream fs = new FileStream("Insurgents\\MissionPhoto\\Insurgents_" + path, FileMode.Create);
            await pacKatariBot.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            fs.Dispose();
        }

        /// <summary>
        /// Статическая клавиатура вопросов для повстанцев
        /// </summary>
        private static void InsurgentsOperationsMenuOperation()
        {
            insurgentsOperationsMenu = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton("Квесты"),
            })
            {
                OneTimeKeyboard = false
            };

        }

        /// <summary>
        /// Меню принятия/отказа дополнительной миссии
        /// </summary>
        private static void InsurgentsAcceptOrRefuseAMission()
        {
            insurgentsKeyboardAcceptOrRefuseAMission = new InlineKeyboardMarkup(new[]
            {
                new[] {InlineKeyboardButton.WithCallbackData("Да", "Yes"), InlineKeyboardButton.WithCallbackData("Нет","No")},
            });
        }

        /// <summary>
        /// Метод для отображения клавиатуры с выбором принятия миссии
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private static async void InsurgentsAcceptanceOrRefusalOfAMission(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var message = ev.CallbackQuery.Message;
            Console.WriteLine($"{ev.CallbackQuery.Data}");
            if (ev.CallbackQuery.Data == "Yes")
            {
                pacKatariBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                await pacKatariBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Получена миссия 2");
                await pacKatariBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, insurgentsMission2ID);
                insurgentsGettingMission2 = true;

                if (insurgentsMissionPool.Contains("Миссия 2") == false)
                {
                    insurgentsMissionPool.Add($"Миссия 2");
                }
            }
            else if (ev.CallbackQuery.Data == "No")
            {
                pacKatariBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                await pacKatariBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ты точно уверен, что это хороший выбор? Привет семье");
                await pacKatariBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, insurgentsMissionAbandonmentID2);
                insurgentsGettingMission2 = true;
            }
        }

        /// <summary>
        /// Запуск чат бота
        /// </summary>
        public void InsurgentsStart()
        {
            string token = File.ReadAllText("PacKatari.txt");
            insurgentsDateStart = DateTime.Now;
            insurgentsGettingMission1 = false;
            insurgentsGettingMission2 = false;
            insurgentsGettingMission3 = false;
            insurgentsSupplementaryMission2 = false;
            insurgentsStartTimeMission1 = 10;
            insurgentsStartTimeMission2 = 20;
            insurgentsStartTimeMission3 = 30;
            insurgentsFailedTimeMission1 = 240;
            insurgentsFailedTimeMission2 = 300;
            insurgentsFailedTimeMission3 = 360;
            insurgentsMissionPool = new List<string>();
            insurgentsMissionCompleted1 = false;
            insurgentsMissionCompleted2 = false;
            insurgentsMissionCompleted3 = false;
            InsurgentsDeserialization();
            pacKatariBot = new TelegramBotClient(token);
            pacKatariBot.OnMessage += InsurgentsMessageListener;

            pacKatariBot.StartReceiving();

        }

        /// <summary>
        /// Метод десерилизации данных для Единства
        /// </summary>
        public static void InsurgentsDeserialization()
        {
            string json;

            //переменные
            string pathInsurgentsDateStart = "Insurgents\\Save\\InsurgentsDateStart.json";
            string pathInsurgentsGettingMission1 = "Insurgents\\Save\\InsurgentsGettingMission1.json";
            string pathInsurgentsGettingMission2 = "Insurgents\\Save\\InsurgentsGettingMission2.json";
            string pathInsurgentsGettingMission3 = "Insurgents\\Save\\InsurgentsGettingMission3.json";
            string pathInsurgentsSupplementaryMission2 = "Insurgents\\Save\\InsurgentsSupplementaryMission2.json";
            string pathInsurgentsStartTimeMission1 = "Insurgents\\Save\\InsurgentsStartTimeMission1.json";
            string pathInsurgentsStartTimeMission2 = "Insurgents\\Save\\InsurgentsStartTimeMission2.json";
            string pathInsurgentsStartTimeMission3 = "Insurgents\\Save\\InsurgentsStartTimeMission3.json";
            string pathInsurgentsFailedTimeMission1 = "Insurgents\\Save\\InsurgentsFailedTimeMission1.json";
            string pathInsurgentsFailedTimeMission2 = "Insurgents\\Save\\InsurgentsFailedTimeMission2.json";
            string pathInsurgentsFailedTimeMission3 = "Insurgents\\Save\\InsurgentsFailedTimeMission3.json";
            string pathInsurgentsMissionCompleted1 = "Insurgents\\Save\\InsurgentsMissionCompleted1.json";
            string pathInsurgentsMissionCompleted2 = "Insurgents\\Save\\InsurgentsMissionCompleted2.json";
            string pathInsurgentsMissionCompleted3 = "Insurgents\\Save\\InsurgentsMissionCompleted3.json";

            //Коллекции
            string pathInsurgentsMissionPool = "Insurgents\\Save\\InsurgentsMissionPool.json";


            //Проверяем наличие файлов
            if (File.Exists(pathInsurgentsDateStart) == true)
            {
                json = File.ReadAllText(pathInsurgentsDateStart);
                insurgentsDateStart = Convert.ToDateTime(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsGettingMission1) == true)
            {
                json = File.ReadAllText(pathInsurgentsGettingMission1);
                insurgentsGettingMission1 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsGettingMission2) == true)
            {
                json = File.ReadAllText(pathInsurgentsGettingMission2);
                insurgentsGettingMission2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsGettingMission3) == true)
            {
                json = File.ReadAllText(pathInsurgentsGettingMission3);
                insurgentsGettingMission3 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsSupplementaryMission2) == true)
            {
                json = File.ReadAllText(pathInsurgentsSupplementaryMission2);
                insurgentsSupplementaryMission2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsStartTimeMission1) == true)
            {
                json = File.ReadAllText(pathInsurgentsStartTimeMission1);
                insurgentsStartTimeMission1 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsStartTimeMission2) == true)
            {
                json = File.ReadAllText(pathInsurgentsStartTimeMission2);
                insurgentsStartTimeMission2 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsStartTimeMission3) == true)
            {
                json = File.ReadAllText(pathInsurgentsStartTimeMission3);
                insurgentsStartTimeMission3 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsFailedTimeMission1) == true)
            {
                json = File.ReadAllText(pathInsurgentsFailedTimeMission1);
                insurgentsFailedTimeMission1 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsFailedTimeMission2) == true)
            {
                json = File.ReadAllText(pathInsurgentsFailedTimeMission2);
                insurgentsFailedTimeMission2 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsFailedTimeMission3) == true)
            {
                json = File.ReadAllText(pathInsurgentsFailedTimeMission3);
                insurgentsFailedTimeMission3 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsMissionPool) == true)
            {
                json = File.ReadAllText(pathInsurgentsMissionPool);
                insurgentsMissionPool = JsonConvert.DeserializeObject<List<string>>(json);
            }

            if (File.Exists(pathInsurgentsMissionCompleted1) == true)
            {
                json = File.ReadAllText(pathInsurgentsMissionCompleted1);
                insurgentsMissionCompleted1 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsMissionCompleted2) == true)
            {
                json = File.ReadAllText(pathInsurgentsMissionCompleted2);
                insurgentsMissionCompleted2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathInsurgentsMissionCompleted3) == true)
            {
                json = File.ReadAllText(pathInsurgentsMissionCompleted3);
                insurgentsMissionCompleted3 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }
        }

        /// <summary>
        /// Метод Серилизации данных для Единства
        /// </summary>
        public static void InsurgentsSerialization()
        {
            string json;

            //переменные
            string pathInsurgentsDateStart = "Insurgents\\Save\\InsurgentsDateStart.json";
            string pathInsurgentsGettingMission1 = "Insurgents\\Save\\InsurgentsGettingMission1.json";
            string pathInsurgentsGettingMission2 = "Insurgents\\Save\\InsurgentsGettingMission2.json";
            string pathInsurgentsGettingMission3 = "Insurgents\\Save\\InsurgentsGettingMission3.json";
            string pathInsurgentsSupplementaryMission2 = "Insurgents\\Save\\InsurgentsSupplementaryMission2.json";
            string pathInsurgentsStartTimeMission1 = "Insurgents\\Save\\InsurgentsStartTimeMission1.json";
            string pathInsurgentsStartTimeMission2 = "Insurgents\\Save\\InsurgentsStartTimeMission2.json";
            string pathInsurgentsStartTimeMission3 = "Insurgents\\Save\\InsurgentsStartTimeMission3.json";
            string pathInsurgentsFailedTimeMission1 = "Insurgents\\Save\\InsurgentsFailedTimeMission1.json";
            string pathInsurgentsFailedTimeMission2 = "Insurgents\\Save\\InsurgentsFailedTimeMission2.json";
            string pathInsurgentsFailedTimeMission3 = "Insurgents\\Save\\InsurgentsFailedTimeMission3.json";
            string pathInsurgentsMissionCompleted1 = "Insurgents\\Save\\InsurgentsMissionCompleted1.json";
            string pathInsurgentsMissionCompleted2 = "Insurgents\\Save\\InsurgentsMissionCompleted2.json";
            string pathInsurgentsMissionCompleted3 = "Insurgents\\Save\\InsurgentsMissionCompleted3.json";

            //Коллекции
            string pathInsurgentsMissionPool = "Insurgents\\Save\\InsurgentsMissionPool.json";

            //Серилизация
            json = JsonConvert.SerializeObject(insurgentsDateStart);
            File.WriteAllText(pathInsurgentsDateStart, json);

            json = JsonConvert.SerializeObject(insurgentsGettingMission1);
            File.WriteAllText(pathInsurgentsGettingMission1, json);

            json = JsonConvert.SerializeObject(insurgentsGettingMission2);
            File.WriteAllText(pathInsurgentsGettingMission2, json);

            json = JsonConvert.SerializeObject(insurgentsGettingMission3);
            File.WriteAllText(pathInsurgentsGettingMission3, json);

            json = JsonConvert.SerializeObject(insurgentsSupplementaryMission2);
            File.WriteAllText(pathInsurgentsSupplementaryMission2, json);

            json = JsonConvert.SerializeObject(insurgentsStartTimeMission1);
            File.WriteAllText(pathInsurgentsStartTimeMission1, json);

            json = JsonConvert.SerializeObject(insurgentsStartTimeMission2);
            File.WriteAllText(pathInsurgentsStartTimeMission2, json);

            json = JsonConvert.SerializeObject(insurgentsStartTimeMission3);
            File.WriteAllText(pathInsurgentsStartTimeMission3, json);

            json = JsonConvert.SerializeObject(insurgentsFailedTimeMission1);
            File.WriteAllText(pathInsurgentsFailedTimeMission1, json);

            json = JsonConvert.SerializeObject(insurgentsFailedTimeMission2);
            File.WriteAllText(pathInsurgentsFailedTimeMission2, json);

            json = JsonConvert.SerializeObject(insurgentsFailedTimeMission3);
            File.WriteAllText(pathInsurgentsFailedTimeMission3, json);

            json = JsonConvert.SerializeObject(insurgentsMissionPool);
            File.WriteAllText(pathInsurgentsMissionPool, json);

            json = JsonConvert.SerializeObject(insurgentsMissionCompleted1);
            File.WriteAllText(pathInsurgentsMissionCompleted1, json);

            json = JsonConvert.SerializeObject(insurgentsMissionCompleted2);
            File.WriteAllText(pathInsurgentsMissionCompleted2, json);

            json = JsonConvert.SerializeObject(insurgentsMissionCompleted3);
            File.WriteAllText(pathInsurgentsMissionCompleted3, json);

        }
    }
}
