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
    struct Karen_Bowman_Bot
    {
        /// <summary>
        /// переменная показывающая смогли ли призраки выполнить миссию 1
        /// </summary>
        private static bool ghostsMissionCompleted1;

        /// <summary>
        /// переменная показывающая смогли ли призраки выполнить миссию 2
        /// </summary>
        private static bool ghostsMissionCompleted2;

        /// <summary>
        /// переменная показывающая смогли ли призраки выполнить миссию 3
        /// </summary>
        private static bool ghostsMissionCompleted3;

        /// <summary>
        /// Переменная для хранения введенного текста
        /// </summary>
        private static string ghostsMessageText;

        /// <summary>
        /// Переменная для хранения названия фото
        /// </summary>
        private static string ghostsMessageTextCompletingTheMission;

        /// <summary>
        /// Коллекция для хранения миссий
        /// </summary>
        private static List<string> ghostsMissionPool;

        /// <summary>
        /// Дата и время начала игры
        /// </summary>
        private static DateTime ghostsDateStart;

        /// <summary>
        /// Дата и время для сравнения с датой и временем начала игры
        /// </summary>
        private static DateTime ghostsComparisonDateFromStart;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 1
        /// </summary>
        private static bool ghostsGettingMission1;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 2
        /// </summary>
        private static bool ghostsGettingMission2;

        /// <summary>
        /// переменная определяет срабатывание получения миссии 3
        /// </summary>
        private static bool ghostsGettingMission3;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 1
        /// </summary>
        private static string ghostsMission1ID;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 2
        /// </summary>
        private static string ghostsMission2ID;

        /// <summary>
        /// Переменная для записи ID аудио файла задач миссии 3
        /// </summary>
        private static string ghostsMission3ID;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 1
        /// </summary>
        private static string ghostsMissionFailedID1;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 2
        /// </summary>
        private static string ghostsMissionFailedID2;

        /// <summary>
        /// Переменная для записи ID аудио файла провала миссии 3
        /// </summary>
        private static string ghostsMissionFailedID3;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 1
        /// </summary>
        private static string ghostsMissionComplete1ID;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 2
        /// </summary>
        private static string ghostsMissionComplete2ID;

        /// <summary>
        /// Переменная для записи ID аудио файла выполнения миссии 3
        /// </summary>
        private static string ghostsMissionComplete3ID;

        /// <summary>
        /// Переменная для записи ID аудио файла отказа от миссии 2
        /// </summary>
        private static string ghostsMissionAbandonmentID2;

        /// <summary>
        /// Время запуска мисии1
        /// </summary>
        private static double ghostsStartTimeMission1;

        /// <summary>
        /// Время запуска мисии2
        /// </summary>
        private static double ghostsStartTimeMission2;

        /// <summary>
        /// Время запуска мисии3
        /// </summary>
        private static double ghostsStartTimeMission3;

        /// <summary>
        /// Время провала мисии1
        /// </summary>
        private static double ghostsFailedTimeMission1;

        /// <summary>
        /// Время провала мисии2
        /// </summary>
        private static double ghostsFailedTimeMission2;

        /// <summary>
        /// Время провала мисии3
        /// </summary>
        private static double ghostsFailedTimeMission3;

        /// <summary>
        /// Получение дополнительной миссии ровно 1 раз
        /// </summary>
        private static bool ghostsSupplementaryMission2;

        /// <summary>
        /// telegram бот клиент
        /// </summary>
        static TelegramBotClient karenBowmanBot;

        /// <summary>
        /// Определяем статическую встроенную клавиатуру повстанцев
        /// </summary>
        private static ReplyKeyboardMarkup ghostsOperationsMenu;

        /// <summary>
        /// Клавиатура принятия миссии
        /// </summary>
        private static InlineKeyboardMarkup ghostsKeyboardAcceptOrRefuseAMission;

        /// <summary>
        /// Метод для ответа на сообщения от пользователей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static async void GhostsMessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            ghostsComparisonDateFromStart = DateTime.Now;

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
                GhostsOperationsMenuOperation();
                karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, "Меню активировано", replyMarkup: ghostsOperationsMenu);
            }

            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && e.Message.Text == "/start")
            {
                //Выводим основную клавиатуру повстанцев
                GhostsOperationsMenuOperation();
                karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, "Добро пожаловать в призраки", replyMarkup: ghostsOperationsMenu);
            }

            //если фото в виде документа, выводим его параметры и выполняем миссию соответствующую названию фото
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Document || e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                if (string.IsNullOrEmpty(e.Message.Caption) == false)
                {
                    ghostsMessageTextCompletingTheMission = e.Message.Caption.ToLower();
                    switch (ghostsMessageTextCompletingTheMission)
                    {
                        case "mission1":
                            if (ghostsMissionPool.Contains("Миссия 1"))
                            {
                                await karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 1 Выполнена");
                                await karenBowmanBot.SendAudioAsync(e.Message.Chat.Id, ghostsMissionComplete1ID);
                                ghostsMissionPool.Remove($"Миссия 1");
                                ghostsMissionCompleted1 = true;
                            }
                            break;

                        case "mission2":
                            if (ghostsMissionPool.Contains("Миссия 2"))
                            {
                                await karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 Выполнена");
                                await karenBowmanBot.SendAudioAsync(e.Message.Chat.Id, ghostsMissionComplete2ID);
                                ghostsMissionPool.Remove($"Миссия 2");
                                ghostsMissionCompleted2 = true;
                            }
                            break;

                        case "mission3":
                            if (ghostsMissionPool.Contains("Миссия 3"))
                            {
                                await karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 Выполнена");
                                await karenBowmanBot.SendAudioAsync(e.Message.Chat.Id, ghostsMissionComplete3ID);
                                ghostsMissionPool.Remove($"Миссия 3");
                                ghostsMissionCompleted3 = true;
                            }
                            break;
                    }
                }
            }

            //если прикладывается аудио, выводим его параметры и записываем ID аудио
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Audio)
            {
                ghostsMessageText = e.Message.Audio.FileName;
                switch (ghostsMessageText)
                {
                    case "GhostsMission1.mp3":
                        ghostsMission1ID = e.Message.Audio.FileId;
                        break;

                    case "GhostsMission2.mp3":
                        ghostsMission2ID = e.Message.Audio.FileId;
                        break;

                    case "GhostsMission3.mp3":
                        ghostsMission3ID = e.Message.Audio.FileId;
                        break;

                    case "GhostsMissionFailed1.mp3":
                        ghostsMissionFailedID1 = e.Message.Audio.FileId;
                        break;

                    case "GhostsMissionFailed2.mp3":
                        ghostsMissionFailedID2 = e.Message.Audio.FileId;
                        break;

                    case "GhostsMissionFailed3.mp3":
                        ghostsMissionFailedID3 = e.Message.Audio.FileId;
                        break;

                    case "GhostsMissionComplete1.mp3":
                        ghostsMissionComplete1ID = e.Message.Audio.FileId;
                        break;

                    case "GhostsMissionComplete2.mp3":
                        ghostsMissionComplete2ID = e.Message.Audio.FileId;
                        break;

                    case "GhostsMissionComplete3.mp3":
                        ghostsMissionComplete3ID = e.Message.Audio.FileId;
                        break;

                    case "GhostsMissionAbandonment2.mp3":
                        ghostsMissionAbandonmentID2 = e.Message.Audio.FileId;
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
            karenBowmanBot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

            //Получаем обязательную миссию 1
            if (ghostsComparisonDateFromStart >= ghostsDateStart.AddMinutes(ghostsStartTimeMission1) && ghostsGettingMission1 == false)
            {
                await karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, $"Получена миссия 1");
                await karenBowmanBot.SendAudioAsync(e.Message.Chat.Id, ghostsMission1ID);
                ghostsGettingMission1 = true;

                if (ghostsMissionPool.Contains("Миссия 1") == false)
                {
                    ghostsMissionPool.Add($"Миссия 1");
                }

            }

            //Получаем дополнительную миссию 2
            if (ghostsComparisonDateFromStart >= ghostsDateStart.AddMinutes(ghostsStartTimeMission2) && ghostsGettingMission2 == false && ghostsSupplementaryMission2 == false)
            {
                ghostsSupplementaryMission2 = true;
                GhostsAcceptOrRefuseAMission();
                karenBowmanBot.OnCallbackQuery -= GhostsAcceptanceOrRefusalOfAMission;
                await karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, $"Поступила дополнительная миссия 2. Принять миссию?", replyMarkup: ghostsKeyboardAcceptOrRefuseAMission);
                karenBowmanBot.OnCallbackQuery += GhostsAcceptanceOrRefusalOfAMission;
            }

            //Получаем обязательную миссию 3
            if (ghostsComparisonDateFromStart >= ghostsDateStart.AddMinutes(ghostsStartTimeMission3) && ghostsGettingMission3 == false)
            {
                await karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, $"Получена миссия 3");
                await karenBowmanBot.SendAudioAsync(e.Message.Chat.Id, ghostsMission3ID);
                ghostsGettingMission3 = true;

                if (ghostsMissionPool.Contains("Миссия 3") == false)
                {
                    ghostsMissionPool.Add($"Миссия 3");
                }

            }

            //Проваливаем обязательную миссию 1
            if (ghostsComparisonDateFromStart >= ghostsDateStart.AddMinutes(ghostsFailedTimeMission1) && ghostsMissionPool.Contains("Миссия 1"))
            {
                await karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 1 провалена");
                await karenBowmanBot.SendAudioAsync(e.Message.Chat.Id, ghostsMissionFailedID1);
                ghostsMissionPool.Remove($"Миссия 1");
            }

            //Проваливаем дополнительную миссию 2
            if (ghostsComparisonDateFromStart >= ghostsDateStart.AddMinutes(ghostsFailedTimeMission2) && ghostsMissionPool.Contains("Миссия 2"))
            {
                await karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 провалена");
                await karenBowmanBot.SendAudioAsync(e.Message.Chat.Id, ghostsMissionFailedID2);
                ghostsMissionPool.Remove($"Миссия 2");
            }

            //Проваливаем обязательную миссию 3
            if (ghostsComparisonDateFromStart >= ghostsDateStart.AddMinutes(ghostsFailedTimeMission3) && ghostsMissionPool.Contains("Миссия 3"))
            {
                await karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 провалена");
                await karenBowmanBot.SendAudioAsync(e.Message.Chat.Id, ghostsMissionFailedID3);
                ghostsMissionPool.Remove($"Миссия 3");
            }

            //если сообщение текстовое, то записываем в переменную текст сообщения и проводим операции КВЕСТЫ
            ghostsMessageText = e.Message.Text;
            switch (ghostsMessageText)
            {
                //Проверяем текущие квесты
                case "Квесты":
                    if (ghostsMissionPool.Count == 0)
                    {
                        await karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, $"Здраствуйте, Активных квестов нет");
                    }
                    else
                    {
                        for (int i = 0; i < ghostsMissionPool.Count; i++)
                        {
                            await karenBowmanBot.SendTextMessageAsync(e.Message.Chat.Id, $"{ghostsMissionPool[i]}");
                        }
                    }
                    break;
            }

            GhostsSerialization();
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
        static async void GhostsDownLoad(string fileId, string path)
        {
            var file = await karenBowmanBot.GetFileAsync(fileId);
            FileStream fs = new FileStream("Ghosts\\MissionPhoto\\Ghosts_" + path, FileMode.Create);
            await karenBowmanBot.DownloadFileAsync(file.FilePath, fs);
            fs.Close();

            fs.Dispose();
        }

        /// <summary>
        /// Статическая клавиатура вопросов для повстанцев
        /// </summary>
        private static void GhostsOperationsMenuOperation()
        {
            ghostsOperationsMenu = new ReplyKeyboardMarkup(new[]
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
        private static void GhostsAcceptOrRefuseAMission()
        {
            ghostsKeyboardAcceptOrRefuseAMission = new InlineKeyboardMarkup(new[]
            {
                new[] {InlineKeyboardButton.WithCallbackData("Да", "Yes"), InlineKeyboardButton.WithCallbackData("Нет","No")},
            });
        }

        /// <summary>
        /// Метод для отображения клавиатуры с выбором принятия миссии
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private static async void GhostsAcceptanceOrRefusalOfAMission(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var message = ev.CallbackQuery.Message;
            Console.WriteLine($"{ev.CallbackQuery.Data}");
            if (ev.CallbackQuery.Data == "Yes")
            {
                karenBowmanBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                await karenBowmanBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Получена миссия 2");
                await karenBowmanBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, ghostsMission2ID);
                ghostsGettingMission2 = true;

                if (ghostsMissionPool.Contains("Миссия 2") == false)
                {
                    ghostsMissionPool.Add($"Миссия 2");
                }
            }
            else if (ev.CallbackQuery.Data == "No")
            {
                karenBowmanBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                await karenBowmanBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ты точно уверен, что это хороший выбор? Привет семье");
                await karenBowmanBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, ghostsMissionAbandonmentID2);
                ghostsGettingMission2 = true;
            }
        }

        /// <summary>
        /// Запуск чат бота
        /// </summary>
        public void GhostsStart()
        {
            string token = File.ReadAllText("KarenBowmanBot.txt");
            ghostsDateStart = DateTime.Now;
            ghostsGettingMission1 = false;
            ghostsGettingMission2 = false;
            ghostsGettingMission3 = false;
            ghostsSupplementaryMission2 = false;
            ghostsStartTimeMission1 = 10;
            ghostsStartTimeMission2 = 20;
            ghostsStartTimeMission3 = 30;
            ghostsFailedTimeMission1 = 240;
            ghostsFailedTimeMission2 = 300;
            ghostsFailedTimeMission3 = 360;
            ghostsMissionPool = new List<string>();
            ghostsMissionCompleted1 = false;
            ghostsMissionCompleted2 = false;
            ghostsMissionCompleted3 = false;
            GhostsDeserialization();
            karenBowmanBot = new TelegramBotClient(token);
            karenBowmanBot.OnMessage += GhostsMessageListener;

            karenBowmanBot.StartReceiving();

        }

        /// <summary>
        /// Метод десерилизации данных для Единства
        /// </summary>
        public static void GhostsDeserialization()
        {
            string json;

            //переменные
            string pathGhostsDateStart = "Ghosts\\Save\\GhostsDateStart.json";
            string pathGhostsGettingMission1 = "Ghosts\\Save\\GhostsGettingMission1.json";
            string pathGhostsGettingMission2 = "Ghosts\\Save\\GhostsGettingMission2.json";
            string pathGhostsGettingMission3 = "Ghosts\\Save\\GhostsGettingMission3.json";
            string pathGhostsSupplementaryMission2 = "Ghosts\\Save\\GhostsSupplementaryMission2.json";
            string pathGhostsStartTimeMission1 = "Ghosts\\Save\\GhostsStartTimeMission1.json";
            string pathGhostsStartTimeMission2 = "Ghosts\\Save\\GhostsStartTimeMission2.json";
            string pathGhostsStartTimeMission3 = "Ghosts\\Save\\GhostsStartTimeMission3.json";
            string pathGhostsFailedTimeMission1 = "Ghosts\\Save\\GhostsFailedTimeMission1.json";
            string pathGhostsFailedTimeMission2 = "Ghosts\\Save\\GhostsFailedTimeMission2.json";
            string pathGhostsFailedTimeMission3 = "Ghosts\\Save\\GhostsFailedTimeMission3.json";
            string pathGhostsMissionCompleted1 = "Ghosts\\Save\\GhostsMissionCompleted1.json";
            string pathGhostsMissionCompleted2 = "Ghosts\\Save\\GhostsMissionCompleted2.json";
            string pathGhostsMissionCompleted3 = "Ghosts\\Save\\GhostsMissionCompleted3.json";

            //Коллекции
            string pathGhostsMissionPool = "Ghosts\\Save\\GhostsMissionPool.json";


            //Проверяем наличие файлов
            if (File.Exists(pathGhostsDateStart) == true)
            {
                json = File.ReadAllText(pathGhostsDateStart);
                ghostsDateStart = Convert.ToDateTime(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsGettingMission1) == true)
            {
                json = File.ReadAllText(pathGhostsGettingMission1);
                ghostsGettingMission1 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsGettingMission2) == true)
            {
                json = File.ReadAllText(pathGhostsGettingMission2);
                ghostsGettingMission2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsGettingMission3) == true)
            {
                json = File.ReadAllText(pathGhostsGettingMission3);
                ghostsGettingMission3 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsSupplementaryMission2) == true)
            {
                json = File.ReadAllText(pathGhostsSupplementaryMission2);
                ghostsSupplementaryMission2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsStartTimeMission1) == true)
            {
                json = File.ReadAllText(pathGhostsStartTimeMission1);
                ghostsStartTimeMission1 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsStartTimeMission2) == true)
            {
                json = File.ReadAllText(pathGhostsStartTimeMission2);
                ghostsStartTimeMission2 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsStartTimeMission3) == true)
            {
                json = File.ReadAllText(pathGhostsStartTimeMission3);
                ghostsStartTimeMission3 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsFailedTimeMission1) == true)
            {
                json = File.ReadAllText(pathGhostsFailedTimeMission1);
                ghostsFailedTimeMission1 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsFailedTimeMission2) == true)
            {
                json = File.ReadAllText(pathGhostsFailedTimeMission2);
                ghostsFailedTimeMission2 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsFailedTimeMission3) == true)
            {
                json = File.ReadAllText(pathGhostsFailedTimeMission3);
                ghostsFailedTimeMission3 = Convert.ToDouble(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsMissionPool) == true)
            {
                json = File.ReadAllText(pathGhostsMissionPool);
                ghostsMissionPool = JsonConvert.DeserializeObject<List<string>>(json);
            }

            if (File.Exists(pathGhostsMissionCompleted1) == true)
            {
                json = File.ReadAllText(pathGhostsMissionCompleted1);
                ghostsMissionCompleted1 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsMissionCompleted2) == true)
            {
                json = File.ReadAllText(pathGhostsMissionCompleted2);
                ghostsMissionCompleted2 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }

            if (File.Exists(pathGhostsMissionCompleted3) == true)
            {
                json = File.ReadAllText(pathGhostsMissionCompleted3);
                ghostsMissionCompleted3 = Convert.ToBoolean(JsonConvert.DeserializeObject(json));
            }
        }

        /// <summary>
        /// Метод Серилизации данных для Единства
        /// </summary>
        public static void GhostsSerialization()
        {
            string json;

            //переменные
            string pathGhostsDateStart = "Ghosts\\Save\\GhostsDateStart.json";
            string pathGhostsGettingMission1 = "Ghosts\\Save\\GhostsGettingMission1.json";
            string pathGhostsGettingMission2 = "Ghosts\\Save\\GhostsGettingMission2.json";
            string pathGhostsGettingMission3 = "Ghosts\\Save\\GhostsGettingMission3.json";
            string pathGhostsSupplementaryMission2 = "Ghosts\\Save\\GhostsSupplementaryMission2.json";
            string pathGhostsStartTimeMission1 = "Ghosts\\Save\\GhostsStartTimeMission1.json";
            string pathGhostsStartTimeMission2 = "Ghosts\\Save\\GhostsStartTimeMission2.json";
            string pathGhostsStartTimeMission3 = "Ghosts\\Save\\GhostsStartTimeMission3.json";
            string pathGhostsFailedTimeMission1 = "Ghosts\\Save\\GhostsFailedTimeMission1.json";
            string pathGhostsFailedTimeMission2 = "Ghosts\\Save\\GhostsFailedTimeMission2.json";
            string pathGhostsFailedTimeMission3 = "Ghosts\\Save\\GhostsFailedTimeMission3.json";
            string pathGhostsMissionCompleted1 = "Ghosts\\Save\\GhostsMissionCompleted1.json";
            string pathGhostsMissionCompleted2 = "Ghosts\\Save\\GhostsMissionCompleted2.json";
            string pathGhostsMissionCompleted3 = "Ghosts\\Save\\GhostsMissionCompleted3.json";

            //Коллекции
            string pathGhostsMissionPool = "Ghosts\\Save\\GhostsMissionPool.json";

            //Серилизация
            json = JsonConvert.SerializeObject(ghostsDateStart);
            File.WriteAllText(pathGhostsDateStart, json);

            json = JsonConvert.SerializeObject(ghostsGettingMission1);
            File.WriteAllText(pathGhostsGettingMission1, json);

            json = JsonConvert.SerializeObject(ghostsGettingMission2);
            File.WriteAllText(pathGhostsGettingMission2, json);

            json = JsonConvert.SerializeObject(ghostsGettingMission3);
            File.WriteAllText(pathGhostsGettingMission3, json);

            json = JsonConvert.SerializeObject(ghostsSupplementaryMission2);
            File.WriteAllText(pathGhostsSupplementaryMission2, json);

            json = JsonConvert.SerializeObject(ghostsStartTimeMission1);
            File.WriteAllText(pathGhostsStartTimeMission1, json);

            json = JsonConvert.SerializeObject(ghostsStartTimeMission2);
            File.WriteAllText(pathGhostsStartTimeMission2, json);

            json = JsonConvert.SerializeObject(ghostsStartTimeMission3);
            File.WriteAllText(pathGhostsStartTimeMission3, json);

            json = JsonConvert.SerializeObject(ghostsFailedTimeMission1);
            File.WriteAllText(pathGhostsFailedTimeMission1, json);

            json = JsonConvert.SerializeObject(ghostsFailedTimeMission2);
            File.WriteAllText(pathGhostsFailedTimeMission2, json);

            json = JsonConvert.SerializeObject(ghostsFailedTimeMission3);
            File.WriteAllText(pathGhostsFailedTimeMission3, json);

            json = JsonConvert.SerializeObject(ghostsMissionPool);
            File.WriteAllText(pathGhostsMissionPool, json);

            json = JsonConvert.SerializeObject(ghostsMissionCompleted1);
            File.WriteAllText(pathGhostsMissionCompleted1, json);

            json = JsonConvert.SerializeObject(ghostsMissionCompleted2);
            File.WriteAllText(pathGhostsMissionCompleted2, json);

            json = JsonConvert.SerializeObject(ghostsMissionCompleted3);
            File.WriteAllText(pathGhostsMissionCompleted3, json);

        }
    }
}

