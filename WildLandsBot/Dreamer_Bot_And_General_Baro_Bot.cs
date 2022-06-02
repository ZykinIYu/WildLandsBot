using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using WildLandsBot;
using System.Windows;
using System.Diagnostics;

namespace WildLandsBot
{
    class Dreamer_Bot_And_General_Baro_Bot
    {
        /// <summary>
        /// переменная показывающая смог ли картель выполнить миссию 1
        /// </summary>
        private bool cartelMissionCompleted1;

        /// <summary>
        /// Свойство для привязки выполнения миссии 1
        /// </summary>
        public bool СartelMissionCompleted1
        {
            get { return this.cartelMissionCompleted1; }
            set { this.cartelMissionCompleted1 = value; }
        }

        /// <summary>
        /// переменная показывающая смог ли картель выполнить миссию 2
        /// </summary>
        private bool cartelMissionCompleted2;

        /// <summary>
        /// Свойство для привязки выполнения миссии 2
        /// </summary>
        public bool СartelMissionCompleted2
        {
            get { return this.cartelMissionCompleted2; }
            set { this.cartelMissionCompleted2 = value; }
        }

        /// <summary>
        /// переменная показывающая смог ли картель выполнить миссию 3
        /// </summary>
        private bool cartelMissionCompleted3;

        /// <summary>
        /// Свойство для привязки выполнения миссии 3
        /// </summary>
        public bool СartelMissionCompleted3
        {
            get { return this.cartelMissionCompleted3; }
            set { this.cartelMissionCompleted3 = value; }
        }

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
        /// Денежный лимит картеля, для обработки коки
        /// </summary>
        private double cartelCashBalanceChemistLimit;

        /// <summary>
        /// Свойство для привязка денежного баланса в приложение
        /// </summary>
        public double CartelCashBalance
        {
            get { return this.cartelCashBalance; }
            set { this.cartelCashBalance = value; }
        }

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
        /// коммисия которую берет себе химик за каждую тонну коки
        /// </summary>
        private double percentForChemist;

        /// <summary>
        /// Хранилище коки картеля
        /// </summary>
        private double cartelCocaCash;

        /// <summary>
        /// Свойство для привязки баланса коки
        /// </summary>
        public double CartelCocaCash
        {
            get { return this.cartelCocaCash; }
            set { this.cartelCocaCash = value; }
        }

        /// <summary>
        /// Хранилище обработанной коки химиком 1
        /// </summary>
        private double cartelCocaCashChemist1;

        /// <summary>
        /// Хранилище обработанной коки химиком 2
        /// </summary>
        private double cartelCocaCashChemist2;

        /// <summary>
        /// Хранилище обработанной коки химиком 3
        /// </summary>
        private double cartelCocaCashChemist3;

        /// <summary>
        /// Хранилище обработанной коки химиком 4
        /// </summary>
        private double cartelCocaCashChemist4;

        /// <summary>
        /// Дата и время начала игры
        /// </summary>
        private DateTime dateStart;

        /// <summary>
        /// Дата и время для сравнения с датой и временем начала игры
        /// </summary>
        private DateTime comparisonDateFromStart;

        /// <summary>
        /// Дата и время покупки химика для сравнения с датой и временем начала игры
        /// </summary>
        private DateTime comparisonChemistDateFromStart;

        /// <summary>
        /// Дата и время покупки химика
        /// </summary>
        private DateTime comparisonChemistDateFromStartTime;




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
        /// Дата и время доступности нового химика
        /// </summary>
        private DateTime cartelChemistSaleDate;

        /// <summary>
        /// Дата и время когда химик начал работу
        /// </summary>
        private DateTime cartelChemistWorkDate;

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
        /// Время доступности нового химика
        /// </summary>
        private double cartelChemistPaymentTime;

        /// <summary>
        /// Дань Мечтателю
        /// </summary>
        private double tribute;

        /// <summary>
        /// Цена коки
        /// </summary>
        private double cartelCocaPrice;

        /// <summary>
        /// Время через которое можно купить нового химика
        /// </summary>
        private double cartelNowTimeChemist;

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
        /// Переменная для определения куплен ли химик
        /// </summary>
        private bool cartelChemistTry;

        /// <summary>
        /// Переменная для определения можно ли заменить химика
        /// </summary>
        private bool cartelChemistNowTry;

        /// <summary>
        /// Переменная определяющая цену химика
        /// </summary>
        private double cartelСhemistPrice;

        /// <summary>
        /// Переменная определяющая загрузку кокой Химика
        /// </summary>
        private double cartelLoadingCocaToTheChemist;

        /// <summary>
        /// Переменная определяющая время обработки 1 т. коки
        /// </summary>
        private double cartelCocaProcessingTimeByChemist;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemist;

        /// <summary>
        /// Переменная определяющая цену коки после обработки химиком
        /// </summary>
        private double cartelPriceOfCocaAfterChemicalTreatment;

        /// <summary>
        /// Переменная определяющая цену коки после обработки химиком 1, переменная посредник для зачисления $
        /// </summary>
        private double cartelPriceOfCocaAfterChemicalTreatmentIntermediary1;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 1, переменная посредник для зачисления баланса
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemistIntermediary1;

        /// <summary>
        /// Переменная определяющая цену коки после обработки химиком 2, переменная посредник для зачисления $
        /// </summary>
        private double cartelPriceOfCocaAfterChemicalTreatmentIntermediary2;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 2, переменная посредник для зачисления баланса
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemistIntermediary2;

        /// <summary>
        /// Переменная определяющая цену коки после обработки химиком 3, переменная посредник для зачисления $
        /// </summary>
        private double cartelPriceOfCocaAfterChemicalTreatmentIntermediary3;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 3, переменная посредник для зачисления баланса
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemistIntermediary3;

        /// <summary>
        /// Переменная определяющая цену коки после обработки химиком 4, переменная посредник для зачисления $
        /// </summary>
        private double cartelPriceOfCocaAfterChemicalTreatmentIntermediary4;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 4, переменная посредник для зачисления баланса
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemistIntermediary4;

        /// <summary>
        /// Переменная определяющая время обработки коки переданной химику
        /// </summary>
        private double cartelCocaProcessingTimeByChemistFool;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 1 всей отправленной коки
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemistFool1;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 2 всей отправленной коки
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemistFool2;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 3 всей отправленной коки
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemistFool3;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 4 всей отправленной коки
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemistFool4;

        /// <summary>
        /// Переменная определяющая цену коки после обработки химиком всей
        /// </summary>
        private double cartelPriceOfCocaAfterChemicalTreatmentFool;

        /// <summary>
        /// Переменная определяющая наличие для покупки химика 1
        /// </summary>
        private bool chemistBool1;

        /// <summary>
        /// Переменная определяющая цену химика 1 
        /// </summary>
        private double cartelСhemistPrice1;

        /// <summary>
        /// Переменная определяющая загрузку кокой Химика 1
        /// </summary>
        private double cartelLoadingCocaToTheChemist1;

        /// <summary>
        /// Переменная определяющая время обработки 1 т. коки 1
        /// </summary>
        private double cartelCocaProcessingTimeByChemist1;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 1
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemist1;

        /// <summary>
        /// Переменная определяющая цену коки после обработки химиком 1
        /// </summary>
        private double cartelPriceOfCocaAfterChemicalTreatment1;

        /// <summary>
        /// Переменная определяющая наличие для покупки химика 2
        /// </summary>
        private bool chemistBool2;

        /// <summary>
        /// Переменная определяющая цену химика 2
        /// </summary>
        private double cartelСhemistPrice2;

        /// <summary>
        /// Переменная определяющая загрузку кокой Химика 2
        /// </summary>
        private double cartelLoadingCocaToTheChemist2;

        /// <summary>
        /// Переменная определяющая время обработки 1 т. коки 2
        /// </summary>
        private double cartelCocaProcessingTimeByChemist2;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 2
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemist2;

        /// <summary>
        /// Переменная определяющая цену коки после обработки химиком 2
        /// </summary>
        private double cartelPriceOfCocaAfterChemicalTreatment2;

        /// <summary>
        /// Переменная определяющая наличие для покупки химика 3
        /// </summary>
        private bool chemistBool3;

        /// <summary>
        /// Переменная определяющая цену химика 3
        /// </summary>
        private double cartelСhemistPrice3;

        /// <summary>
        /// Переменная определяющая загрузку кокой Химика 3
        /// </summary>
        private double cartelLoadingCocaToTheChemist3;

        /// <summary>
        /// Переменная определяющая время обработки 1 т. коки 3
        /// </summary>
        private double cartelCocaProcessingTimeByChemist3;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 3
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemist3;

        /// <summary>
        /// Переменная определяющая цену коки после обработки химиком 3
        /// </summary>
        private double cartelPriceOfCocaAfterChemicalTreatment3;

        /// <summary>
        /// Переменная определяющая наличие для покупки химика 4
        /// </summary>
        private bool chemistBool4;

        /// <summary>
        /// Переменная определяющая цену химика 4
        /// </summary>
        private double cartelСhemistPrice4;

        /// <summary>
        /// Переменная определяющая загрузку кокой Химика 4
        /// </summary>
        private double cartelLoadingCocaToTheChemist4;

        /// <summary>
        /// Переменная определяющая время обработки 1 т. коки 4
        /// </summary>
        private double cartelCocaProcessingTimeByChemist4;

        /// <summary>
        /// Переменная определяющая массу коки после обработки химиком 4
        /// </summary>
        private double cartelMassOfCocaAfterTreatmentByAChemist4;

        /// <summary>
        /// Переменная определяющая цену коки после обработки химиком 4
        /// </summary>
        private double cartelPriceOfCocaAfterChemicalTreatment4;

        /// <summary>
        /// Переменная определяющая работает ли химик
        /// </summary>
        private bool chemistWorkBool;

        private int translationsID;

        /// <summary>
        /// Экземпляр окна
        /// </summary>
        private MainWindow w;

        /// <summary>
        /// telegram бот клиент
        /// </summary>
        TelegramBotClient dreamerBot;

        /// <summary>
        /// Коллекция для логов пользователя
        /// </summary>
        public ObservableCollection<MessageLog> CartelBotMessageLog { get; set; }

        /// <summary>
        /// Коллекция для логов бота
        /// </summary>
        public ObservableCollection<MessageLog> CartelDreamerBotMessageLog { get; set; }

        /// <summary>
        /// Общая коллекция
        /// </summary>
        public ObservableCollection<MessageLog> AllBotMessageLog { get; set; }

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
        /// Клавиатура выбора кому переводить
        /// </summary>
        private InlineKeyboardMarkup whoDoesTheTranslateToWhoCartel;

        /// <summary>
        /// Клавиатура покупки химика
        /// </summary>
        private InlineKeyboardMarkup chemistShoppingCartel;

        /// <summary>
        /// Клавиатура для определения сколько коки необходимо передать химику
        /// </summary>
        private InlineKeyboardMarkup quantityOfCocaForChemistCartel;

        /// <summary>
        /// Клавиатура выбора покупки нового химика или отправки коки на переработку старому
        /// </summary>
        private InlineKeyboardMarkup cartelBuyingOrDownloadingAChemistKeyboard;


        private InlineKeyboardMarkup cartelTransportationOfCocaKeyboard;

        /// <summary>
        /// переменная показывающая смогло ли Единство выполнить миссию 1
        /// </summary>
        private bool unityMissionCompleted1;

        /// <summary>
        /// Свойство для привязки выполнения миссии 1 у Единства
        /// </summary>
        public bool UnityMissionCompleted1
        {
            get { return this.unityMissionCompleted1; }
            set { this.unityMissionCompleted1 = value; }
        }

        /// <summary>
        /// переменная показывающая смогло ли Единство выполнить миссию 2
        /// </summary>
        private bool unityMissionCompleted2;

        /// <summary>
        /// Свойство для привязки выполнения миссии 2 у Единства
        /// </summary>
        public bool UnityMissionCompleted2
        {
            get { return this.unityMissionCompleted2; }
            set { this.unityMissionCompleted2 = value; }
        }

        /// <summary>
        /// переменная показывающая смогло ли Единство выполнить миссию 3
        /// </summary>
        private bool unityMissionCompleted3;

        /// <summary>
        /// Свойство для привязки выполнения миссии 3 у Единства
        /// </summary>
        public bool UnityMissionCompleted3
        {
            get { return this.unityMissionCompleted3; }
            set { this.unityMissionCompleted3 = value; }
        }

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
        private double unityCashBalance;

        /// <summary>
        /// Свойство для привязка денежного баланса в приложение для Единства
        /// </summary>
        public double UnityCashBalance
        {
            get { return this.unityCashBalance; }
            set { this.unityCashBalance = value; }
        }

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
        /// Коллекция для логов пользователя
        /// </summary>
        public ObservableCollection<MessageLog> UnityBotMessageLog { get; set; }

        /// <summary>
        /// Коллекция для логов бота Единства
        /// </summary>
        public ObservableCollection<MessageLog> UnityGeneralBaroBotMessageLog { get; set; }

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

            //dreamerBot.SendTextMessageAsync("-549065909", $"Привет", replyMarkup: cartelOperationsMenu);

            //записываем время, кто, ид, текст сообщения
            string text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

            //выводим на консоль время, кто, ид, текст сообщения
            Debug.WriteLine($"{text} TypeMessage: {e.Message.Type.ToString()}");
            var dreamerMessageText = "NULL";

            Logging(CartelBotMessageLog, e.Message.Text, e.Message.Chat.FirstName, e.Message.Chat.Id);

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
                dreamerMessageText = $"Добро пожаловать в Картель";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
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
                                dreamerMessageText = $"Миссия 1 Выполнена";
                                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                                //await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionComplete1ID);
                                missionPool.Remove($"Миссия 1");
                                cartelMissionCompleted1 = true;
                            }
                            break;

                        //Выполнение миссии 2
                        case "mission2":
                            if (missionPool.Contains("Миссия 2"))
                            {
                                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 Выполнена");
                                dreamerMessageText = $"Миссия 2 Выполнена";
                                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                                //await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionComplete2ID);
                                missionPool.Remove($"Миссия 2");
                                cartelMissionCompleted2 = true;
                            }
                            break;

                        //Выполнение миссии 3
                        case "mission3":
                            if (missionPool.Contains("Миссия 3"))
                            {
                                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 Выполнена");
                                dreamerMessageText = $"Миссия 3 Выполнена";
                                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                                //await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionComplete3ID);
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
                                dreamerMessageText = $"Кока с плантации собрана.\nУ вас {cartelCocaCash} т. коки";
                                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
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
                                dreamerMessageText = $"Кока с плантации собрана.\nУ вас {cartelCocaCash} т. коки";
                                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
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
                                dreamerMessageText = $"Кока с плантации собрана.\nУ вас {cartelCocaCash} т. коки";
                                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                            }
                            break;

                    }
                }
                else
                {
                    await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Данное фото без подписи, вышлите фото с подписью");
                    dreamerMessageText = $"Данное фото без подписи, вышлите фото с подписью";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
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
            }

            //Получаем обязательную миссию 1
            if (comparisonDateFromStart >= dateStart.AddMinutes(cartelStartTimeMission1) && gettingMission1 == false)
            {
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Получена миссия 1");
                dreamerMessageText = $"Получена миссия 1";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                //await dreamerBot.SendAudioAsync(e.Message.Chat.Id, mission1ID);
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
                dreamerMessageText = $"Поступила дополнительная миссия 2. Принять миссию?";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                dreamerBot.OnCallbackQuery += AcceptanceOrRefusalOfAMission;
            }

            //Получаем обязательную миссию 3
            if (comparisonDateFromStart >= dateStart.AddMinutes(cartelStartTimeMission3) && gettingMission3 == false)
            {
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Получена миссия 3");
                dreamerMessageText = $"Получена миссия 3";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                //await dreamerBot.SendAudioAsync(e.Message.Chat.Id, mission3ID);
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
                dreamerMessageText = $"Миссия 1 провалена";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                //await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionFailedID1);
                missionPool.Remove($"Миссия 1");
            }

            //Проваливаем дополнительную миссию 2
            if (comparisonDateFromStart >= dateStart.AddMinutes(cartelFailedTimeMission2) && missionPool.Contains("Миссия 2"))
            {
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 провалена");
                dreamerMessageText = $"Миссия 2 провалена";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                //await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionFailedID2);
                missionPool.Remove($"Миссия 2");
            }

            //Проваливаем обязательную миссию 3
            if (comparisonDateFromStart >= dateStart.AddMinutes(cartelFailedTimeMission3) && missionPool.Contains("Миссия 3"))
            {
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 провалена");
                dreamerMessageText = $"Миссия 3 провалена";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                //await dreamerBot.SendAudioAsync(e.Message.Chat.Id, missionFailedID3);
                missionPool.Remove($"Миссия 3");
            }

            //Проверяем изменения баланса
            if (cartelCashBalanceIntermediateStorage != cartelCashBalanceCheck)
            {
                cartelCashBalanceDifference = cartelCashBalanceIntermediateStorage - cartelCashBalanceCheck;
                cartelCashBalance = cartelCashBalanceIntermediateStorage;
                cartelCashBalanceCheck = cartelCashBalance;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Ваш счет пополнился на {cartelCashBalanceDifference}\nСумма на кошельке: {cartelCashBalance}");
                dreamerMessageText = $"Ваш счет пополнился на {cartelCashBalanceDifference}\nСумма на кошельке: {cartelCashBalance}";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
            }

            //Приехал грузовик 1
            if (comparisonDateFromStart >= cartelStartTransportationTruck[0].AddMinutes(cartelDeliveryTimeTrucks) && cartelBoolTruckBusyFinishe[0] == true)
            {
                var profit = (cartelNumberCocaTrucks * cartelCocaPrice) - (cartelNumberCocaTrucks * cartelCocaPrice * cartelpercentForTranslationTrucks);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                dreamerMessageText = $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                cartelBoolTruckBusyFinishe[0] = false;
            }

            //Приехал грузовик 2
            if (comparisonDateFromStart >= cartelStartTransportationTruck[1].AddMinutes(cartelDeliveryTimeTrucks) && cartelBoolTruckBusyFinishe[1] == true)
            {
                var profit = (cartelNumberCocaTrucks * cartelCocaPrice) - (cartelNumberCocaTrucks * cartelCocaPrice * cartelpercentForTranslationTrucks);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                dreamerMessageText = $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                cartelBoolTruckBusyFinishe[1] = false;
            }

            //Приехал грузовик 3
            if (comparisonDateFromStart >= cartelStartTransportationTruck[2].AddMinutes(cartelDeliveryTimeTrucks) && cartelBoolTruckBusyFinishe[2] == true)
            {
                var profit = (cartelNumberCocaTrucks * cartelCocaPrice) - (cartelNumberCocaTrucks * cartelCocaPrice * cartelpercentForTranslationTrucks);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                dreamerMessageText = $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                cartelBoolTruckBusyFinishe[2] = false;
            }

            //Приехал грузовик 4
            if (comparisonDateFromStart >= cartelStartTransportationTruck[3].AddMinutes(cartelDeliveryTimeTrucks) && cartelBoolTruckBusyFinishe[3] == true)
            {
                var profit = (cartelNumberCocaTrucks * cartelCocaPrice) - (cartelNumberCocaTrucks * cartelCocaPrice * cartelpercentForTranslationTrucks);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                dreamerMessageText = $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                cartelBoolTruckBusyFinishe[3] = false;
            }

            //Приехал грузовик 5
            if (comparisonDateFromStart >= cartelStartTransportationTruck[4].AddMinutes(cartelDeliveryTimeTrucks) && cartelBoolTruckBusyFinishe[4] == true)
            {
                var profit = (cartelNumberCocaTrucks * cartelCocaPrice) - (cartelNumberCocaTrucks * cartelCocaPrice * cartelpercentForTranslationTrucks);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                dreamerMessageText = $"Грузовик доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                cartelBoolTruckBusyFinishe[4] = false;
            }

            //Прилетел вертолет 1
            if (comparisonDateFromStart >= cartelStartTransportationHelicopters[0].AddMinutes(cartelDeliveryTimeHelicopters) && cartelBoolHelicoptersBusyFinishe[0] == true)
            {
                var profit = (cartelNumberCocaHelicopters * cartelCocaPrice) - (cartelNumberCocaHelicopters * cartelCocaPrice * cartelpercentForTranslationHelicopters);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Вертолет доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                dreamerMessageText = $"Вертолет доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                cartelBoolHelicoptersBusyFinishe[0] = false;
            }

            //Прилетел вертолет 2
            if (comparisonDateFromStart >= cartelStartTransportationHelicopters[1].AddMinutes(cartelDeliveryTimeHelicopters) && cartelBoolHelicoptersBusyFinishe[1] == true)
            {
                var profit = (cartelNumberCocaHelicopters * cartelCocaPrice) - (cartelNumberCocaHelicopters * cartelCocaPrice * cartelpercentForTranslationHelicopters);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Вертолет доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                dreamerMessageText = $"Вертолет доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                cartelBoolHelicoptersBusyFinishe[1] = false;
            }

            //Прилетел самолет 1
            if (comparisonDateFromStart >= cartelStartTransportationAircraft[0].AddMinutes(cartelDeliveryTimeAircraft) && cartelBoolAircraftBusyFinishe[0] == true)
            {
                var profit = (cartelNumberCocaAircraft * cartelCocaPrice) - (cartelNumberCocaAircraft * cartelCocaPrice * cartelpercentForTranslationAircraft);
                cartelCashBalance += profit;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Самолет доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}");
                dreamerMessageText = $"Самолет доставил груз, ваша прибыль: {profit}\nВаш баланс: {cartelCashBalance}";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
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
                    dreamerMessageText = $"Вы заплатили дань Мечтателю в размере: {tribute}\nСумма на кошельке: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                }
                else
                {
                    await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Вы не смогли выплатить дань, убейте с особой жестокостью 2 членов Картеля, а трупы отнесите Кашевару на переработку");
                    dreamerMessageText = $"Вы не смогли выплатить дань, убейте с особой жестокостью 2 членов Картеля, а трупы отнесите Кашевару на переработку";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                }

            }

            //Ждем доступности покупки нового химика
            if (comparisonDateFromStart >= cartelChemistSaleDate.AddMinutes(cartelChemistPaymentTime) && cartelChemistNowTry == false && cartelChemistTry == true)
            {
                cartelChemistNowTry = true;
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Появились новые химики для наема. Можете заменить старого на нового.");
                dreamerMessageText = $"Появились новые химики для наема. Можете заменить старого на нового.";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);

            }

            //Ждем окончания обработки коки химиком
            if (comparisonDateFromStart >= cartelChemistWorkDate.AddMinutes(cartelCocaProcessingTimeByChemistFool) && chemistWorkBool == true)
            {
                chemistWorkBool = false;

                cartelCocaCashChemist1 += cartelMassOfCocaAfterTreatmentByAChemistFool1;
                cartelCocaCashChemist2 += cartelMassOfCocaAfterTreatmentByAChemistFool2;
                cartelCocaCashChemist3 += cartelMassOfCocaAfterTreatmentByAChemistFool3;
                cartelCocaCashChemist4 += cartelMassOfCocaAfterTreatmentByAChemistFool4;

                dreamerMessageText = $"Обработка коки завершена\nБаланс коки: {cartelCocaCash}" +
                    $"\n Баланс коки обработанной химиком 1 {cartelCocaCashChemist1}" +
                    $"\n Баланс коки обработанной химиком 2 {cartelCocaCashChemist2}" +
                    $"\n Баланс коки обработанной химиком 3 {cartelCocaCashChemist3}" +
                    $"\n Баланс коки обработанной химиком 4 {cartelCocaCashChemist4}";
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, dreamerMessageText);
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);

            }

            

            //Thread.Sleep(500);
            //Удаляем все введенные запросы от пользователя
            dreamerBot.DeleteMessageAsync(e.Message.Chat.Id, e.Message.MessageId);

            //если сообщение текстовое, то записываем в переменную текст сообщения и проводим операции КОШЕЛЕК, ПЕРЕВОДЫ, КВЕСТЫ, ПЛАНТАЦИИ
            messageText = e.Message.Text;
            CartelMainKeyboardActions(messageText, dreamerMessageText, e);

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
            switch (translationsID)
            {
                case 1:
                    CartelTransfersID1(sc, ev);
                    break;

                case 2:
                    CartelTransfersID2(sc, ev);
                    break;

                case 3:
                    CartelTransfersID3(sc, ev);
                    break;
            }
        }

        /// <summary>
        /// Логика переводов от картеля в Единство
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private void CartelTransfersID1(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var dreamerMessageText = $"NULL";
            var message = ev.CallbackQuery.Message;
            double withdrawalAmount = 0.0;
            switch (ev.CallbackQuery.Data)
            {
                case "5":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
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
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "10":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
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
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "15":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
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
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "20":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
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
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;
            }
        }

        /// <summary>
        /// Логика переводов от картеля в Черный рынок
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private void CartelTransfersID2(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var dreamerMessageText = $"NULL";
            var message = ev.CallbackQuery.Message;
            double withdrawalAmount = 0.0;
            switch (ev.CallbackQuery.Data)
            {
                case "5":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                    if (withdrawalAmount <= cartelCashBalance)
                    {
                        cartelCashBalance -= withdrawalAmount;
                        cartelCashBalanceCheck = cartelCashBalance;
                        cartelCashBalanceIntermediateStorage = cartelCashBalance;
                        //blackMarketCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                    }
                    else
                    {
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "10":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                    if (withdrawalAmount <= cartelCashBalance)
                    {
                        cartelCashBalance -= withdrawalAmount;
                        cartelCashBalanceCheck = cartelCashBalance;
                        cartelCashBalanceIntermediateStorage = cartelCashBalance;
                        //blackMarketCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                    }
                    else
                    {
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "15":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                    if (withdrawalAmount <= cartelCashBalance)
                    {
                        cartelCashBalance -= withdrawalAmount;
                        cartelCashBalanceCheck = cartelCashBalance;
                        cartelCashBalanceIntermediateStorage = cartelCashBalance;
                        //blackMarketCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                    }
                    else
                    {
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "20":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                    if (withdrawalAmount <= cartelCashBalance)
                    {
                        cartelCashBalance -= withdrawalAmount;
                        cartelCashBalanceCheck = cartelCashBalance;
                        cartelCashBalanceIntermediateStorage = cartelCashBalance;
                        //blackMarketCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                    }
                    else
                    {
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;
            }
        }

        /// <summary>
        /// Логика переводов от картеля Химику
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private void CartelTransfersID3(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var dreamerMessageText = $"NULL";
            var message = ev.CallbackQuery.Message;
            double withdrawalAmount = 0.0;
            switch (ev.CallbackQuery.Data)
            {
                case "5":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                    if (withdrawalAmount <= cartelCashBalance)
                    {
                        cartelCashBalance -= withdrawalAmount;
                        cartelCashBalanceCheck = cartelCashBalance;
                        cartelCashBalanceIntermediateStorage = cartelCashBalance;
                        Chemist_Bot.chemistCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelCashBalanceChemistLimit += Convert.ToDouble(ev.CallbackQuery.Data);
                    }
                    else
                    {
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "10":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                    if (withdrawalAmount <= cartelCashBalance)
                    {
                        cartelCashBalance -= withdrawalAmount;
                        cartelCashBalanceCheck = cartelCashBalance;
                        cartelCashBalanceIntermediateStorage = cartelCashBalance;
                        Chemist_Bot.chemistCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelCashBalanceChemistLimit += Convert.ToDouble(ev.CallbackQuery.Data);
                    }
                    else
                    {
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "15":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                    if (withdrawalAmount <= cartelCashBalance)
                    {
                        cartelCashBalance -= withdrawalAmount;
                        cartelCashBalanceCheck = cartelCashBalance;
                        cartelCashBalanceIntermediateStorage = cartelCashBalance;
                        Chemist_Bot.chemistCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelCashBalanceChemistLimit += Convert.ToDouble(ev.CallbackQuery.Data);
                    }
                    else
                    {
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "20":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) + (Convert.ToDouble(ev.CallbackQuery.Data) * percentForTranslation);
                    if (withdrawalAmount <= cartelCashBalance)
                    {
                        cartelCashBalance -= withdrawalAmount;
                        cartelCashBalanceCheck = cartelCashBalance;
                        cartelCashBalanceIntermediateStorage = cartelCashBalance;
                        Chemist_Bot.chemistCashBalanceIntermediateStorage += Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelCashBalanceChemistLimit += Convert.ToDouble(ev.CallbackQuery.Data);
                    }
                    else
                    {
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Перевод невозможно осуществить, так как на счете недостаточно средств");
                        dreamerMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {cartelCashBalance}");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;
            }
        }

        /// <summary>
        /// Метод для отображения действий при выборе кнопки кому переводить
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        async private void CartelSelectTransferAction(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var dreamerMessageText = $"NULL";
            var message = ev.CallbackQuery.Message;


            switch (ev.CallbackQuery.Data)
            {
                case "Unity":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    translationsID = 1;
                    WhoDoesTheCartelTranslateTo();
                    dreamerBot.OnCallbackQuery -= CartelTransfers;
                    await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Банк возьмет коммисию в размере {percentForTranslation * 100}%. Какую сумму хотите перевести Единству?", replyMarkup: whoTranslateCartel);
                    dreamerMessageText = $"Банк возьмет коммисию в размере {percentForTranslation * 100}%. Какую сумму хотите перевести Единству?";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    dreamerBot.OnCallbackQuery += CartelTransfers;
                    break;

                case "BlackMarket":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                    translationsID = 2;
                    WhoDoesTheCartelTranslateTo();
                    dreamerBot.OnCallbackQuery -= CartelTransfers;
                    await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Банк возьмет коммисию в размере {percentForTranslation * 100}%. Какую сумму хотите перевести в Черный рынок?", replyMarkup: whoTranslateCartel);
                    dreamerMessageText = $"Банк возьмет коммисию в размере {percentForTranslation * 100}%. Какую сумму хотите перевести в черный рынок?";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    dreamerBot.OnCallbackQuery += CartelTransfers;
                    break;

                case "Chemist":
                    if (cartelChemistTry)
                    {
                        dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                        translationsID = 3;
                        WhoDoesTheCartelTranslateTo();
                        dreamerBot.OnCallbackQuery -= CartelTransfers;
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Банк возьмет коммисию в размере {percentForTranslation * 100}%. Химик возьмет себе {percentForTranslation * 100}% Какую сумму хотите перевести Химику?", replyMarkup: whoTranslateCartel);
                        dreamerMessageText = $"Банк возьмет коммисию в размере {percentForTranslation * 100}%. Какую сумму хотите перевести Химику?";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelTransfers;
                    }
                    break;

            }

        }

        /// <summary>
        /// Метод для отображения действий при выборе химика
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        async private void CartelSelectChemist(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var dreamerMessageText = $"NULL";
            var message = ev.CallbackQuery.Message;


            switch (ev.CallbackQuery.Data)
            {
                case "Chemist1":
                    if (cartelCashBalance >= cartelСhemistPrice1)
                    {
                        dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                        cartelСhemistPrice = cartelСhemistPrice1;
                        cartelLoadingCocaToTheChemist = cartelLoadingCocaToTheChemist1;
                        cartelCocaProcessingTimeByChemist = cartelCocaProcessingTimeByChemist1;
                        cartelMassOfCocaAfterTreatmentByAChemist = cartelMassOfCocaAfterTreatmentByAChemist1;
                        cartelPriceOfCocaAfterChemicalTreatment = cartelPriceOfCocaAfterChemicalTreatment1;
                        cartelCashBalance -= cartelСhemistPrice;
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Вы преобрели химика с параметрами:\n\nЦена химика: {cartelСhemistPrice}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist} мин.\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment}$\n\nВаш баланс {CartelCashBalance}$");
                        dreamerMessageText = $"Вы преобрели химика с параметрами:\n\nЦена химика: {cartelСhemistPrice}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist} мин.\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment}$\n\nВаш баланс {CartelCashBalance}$";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        cartelChemistTry = true;
                        cartelChemistNowTry = false;
                        chemistBool1 = false;
                        cartelChemistSaleDate = DateTime.Now;

                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary1 = cartelPriceOfCocaAfterChemicalTreatment;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary2 = 0;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary3 = 0;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary4 = 0;

                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 = cartelMassOfCocaAfterTreatmentByAChemist;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 = 0;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 = 0;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 = 0;

                    }
                    else
                    {
                        dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"На балансе недостаточно средств для покупки\nВаш баланс {CartelCashBalance}$");
                        dreamerMessageText = $"На балансе недостаточно средств для покупки\nВаш баланс {CartelCashBalance}$";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    break;

                case "Chemist2":
                    if (cartelCashBalance >= cartelСhemistPrice2)
                    {
                        dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                        cartelСhemistPrice = cartelСhemistPrice2;
                        cartelLoadingCocaToTheChemist = cartelLoadingCocaToTheChemist2;
                        cartelCocaProcessingTimeByChemist = cartelCocaProcessingTimeByChemist2;
                        cartelMassOfCocaAfterTreatmentByAChemist = cartelMassOfCocaAfterTreatmentByAChemist2;
                        cartelPriceOfCocaAfterChemicalTreatment = cartelPriceOfCocaAfterChemicalTreatment2;
                        cartelCashBalance -= cartelСhemistPrice;
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Вы преобрели химика с параметрами:\n\nЦена химика: {cartelСhemistPrice}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist} мин.\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment}$\n\nВаш баланс {CartelCashBalance}$");
                        dreamerMessageText = $"Вы преобрели химика с параметрами:\n\nЦена химика: {cartelСhemistPrice}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist} мин.\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment}$\n\nВаш баланс {CartelCashBalance}$";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        cartelChemistTry = true;
                        cartelChemistNowTry = false;
                        chemistBool2 = false;
                        cartelChemistSaleDate = DateTime.Now;

                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary1 = 0;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary2 = cartelPriceOfCocaAfterChemicalTreatment;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary3 = 0;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary4 = 0;

                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 = 0;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 = cartelMassOfCocaAfterTreatmentByAChemist;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 = 0;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 = 0;
                    }
                    else
                    {
                        dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"На балансе недостаточно средств для покупки\nВаш баланс {CartelCashBalance}$");
                        dreamerMessageText = $"На балансе недостаточно средств для покупки\nВаш баланс {CartelCashBalance}$";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    break;

                case "Chemist3":
                    if (cartelCashBalance >= cartelСhemistPrice3)
                    {
                        dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                        cartelСhemistPrice = cartelСhemistPrice3;
                        cartelLoadingCocaToTheChemist = cartelLoadingCocaToTheChemist3;
                        cartelCocaProcessingTimeByChemist = cartelCocaProcessingTimeByChemist3;
                        cartelMassOfCocaAfterTreatmentByAChemist = cartelMassOfCocaAfterTreatmentByAChemist3;
                        cartelPriceOfCocaAfterChemicalTreatment = cartelPriceOfCocaAfterChemicalTreatment3;
                        cartelCashBalance -= cartelСhemistPrice;
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Вы преобрели химика с параметрами:\n\nЦена химика: {cartelСhemistPrice}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist} мин.\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment}$\n\nВаш баланс {CartelCashBalance}$");
                        dreamerMessageText = $"Вы преобрели химика с параметрами:\n\nЦена химика: {cartelСhemistPrice}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist} мин.\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment}$\n\nВаш баланс {CartelCashBalance}$";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        cartelChemistTry = true;
                        cartelChemistNowTry = false;
                        chemistBool3 = false;
                        cartelChemistSaleDate = DateTime.Now;

                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary1 = 0;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary2 = 0;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary3 = cartelPriceOfCocaAfterChemicalTreatment;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary4 = 0;

                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 = 0;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 = 0;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 = cartelMassOfCocaAfterTreatmentByAChemist;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 = 0;
                    }
                    else
                    {
                        dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"На балансе недостаточно средств для покупки\nВаш баланс {CartelCashBalance}$");
                        dreamerMessageText = $"На балансе недостаточно средств для покупки\nВаш баланс {CartelCashBalance}$";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    break;

                case "Chemist4":
                    if (cartelCashBalance >= cartelСhemistPrice4)
                    {
                        dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                        cartelСhemistPrice = cartelСhemistPrice4;
                        cartelLoadingCocaToTheChemist = cartelLoadingCocaToTheChemist4;
                        cartelCocaProcessingTimeByChemist = cartelCocaProcessingTimeByChemist4;
                        cartelMassOfCocaAfterTreatmentByAChemist = cartelMassOfCocaAfterTreatmentByAChemist4;
                        cartelPriceOfCocaAfterChemicalTreatment = cartelPriceOfCocaAfterChemicalTreatment4;
                        cartelCashBalance -= cartelСhemistPrice;
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Вы преобрели химика с параметрами:\n\nЦена химика: {cartelСhemistPrice}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist} мин.\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment}$\n\nВаш баланс {CartelCashBalance}$");
                        dreamerMessageText = $"Вы преобрели химика с параметрами:\n\nЦена химика: {cartelСhemistPrice}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist} мин.\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment}$\n\nВаш баланс {CartelCashBalance}$";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        cartelChemistTry = true;
                        cartelChemistNowTry = false;
                        chemistBool4 = false;
                        cartelChemistSaleDate = DateTime.Now;

                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary1 = 0;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary2 = 0;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary3 = 0;
                        //cartelPriceOfCocaAfterChemicalTreatmentIntermediary4 = cartelPriceOfCocaAfterChemicalTreatment;

                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 = 0;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 = 0;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 = 0;
                        cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 = cartelMassOfCocaAfterTreatmentByAChemist;
                    }
                    else
                    {
                        dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"На балансе недостаточно средств для покупки\nВаш баланс {CartelCashBalance}$");
                        dreamerMessageText = $"На балансе недостаточно средств для покупки\nВаш баланс {CartelCashBalance}$";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    break;

            }

        }

        /// <summary>
        /// Метод для отображения действий при выборе количества коки
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        async private void CartelStepsForChoosingTheAmountOfCoca(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var dreamerMessageText = $"NULL";
            var message = ev.CallbackQuery.Message;
            double withdrawalAmount = 0.0;

            switch (ev.CallbackQuery.Data)
            {
                case "1":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);

                    break;

                case "2":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "3":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "4":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "5":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "6":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "7":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "8":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "9":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "10":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "11":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "12":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "13":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "14":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "15":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                case "16":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    withdrawalAmount = Convert.ToDouble(ev.CallbackQuery.Data) * cartelCocaPrice * percentForChemist;
                    if (withdrawalAmount <= cartelCashBalanceChemistLimit)
                    {
                        cartelChemistWorkDate = DateTime.Now;
                        cartelCashBalanceChemistLimit -= withdrawalAmount;
                        cartelCocaProcessingTimeByChemistFool = cartelCocaProcessingTimeByChemist * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool1 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary1 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool2 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary2 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool3 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary3 * Convert.ToDouble(ev.CallbackQuery.Data);
                        cartelMassOfCocaAfterTreatmentByAChemistFool4 = cartelMassOfCocaAfterTreatmentByAChemistIntermediary4 * Convert.ToDouble(ev.CallbackQuery.Data);
                        chemistWorkBool = true;
                        dreamerMessageText = $"Кока принята в работу, будет готова через {cartelCocaProcessingTimeByChemistFool} мин.";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        dreamerMessageText = $"На лимите Химика недостаточно средств, переведите химику оплату за работу в размере {withdrawalAmount}$";
                        dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, dreamerMessageText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"\nВаш баланс: {cartelCashBalance}$\nВаш лимит у химика: {cartelCashBalanceChemistLimit}$");
                    dreamerMessageText = $"Ваш баланс: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

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
            var dreamerMessageText = $"NULL";
            if (ev.CallbackQuery.Data == "GreenPlantation")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                if (comparisonDateFromStart >= cartelGreenPlantation.AddMinutes(1))
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Плантация созрела можно начинать собирать\n для подтверждения сбора необходимо выслать фотографию с урожаем, фото высылается как документ. Подпись к фотографии дожна быть \"green\" без ковычек");
                    dreamerMessageText = $"Плантация созрела можно начинать собирать\n для подтверждения сбора необходимо выслать фотографию с урожаем, фото высылается как документ. Подпись к фотографии дожна быть \"green\" без ковычек";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
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
                    dreamerMessageText = $"Плантация созреет через {timepiece} ч. {minutes} мин. {seconds} сек.";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                }
            }
            else
            if (ev.CallbackQuery.Data == "YellowPlantation")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                if (comparisonDateFromStart >= cartelYellowPlantation.AddMinutes(2))
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Плантация созрела можно начинать собирать\n для подтверждения сбора необходимо выслать фотографию с урожаем, фото высылается как документ. Подпись к фотографии дожна быть \"yellow\" без ковычек");
                    dreamerMessageText = $"Плантация созрела можно начинать собирать\n для подтверждения сбора необходимо выслать фотографию с урожаем, фото высылается как документ. Подпись к фотографии дожна быть \"yellow\" без ковычек";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
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
                    dreamerMessageText = $"Плантация созреет через {timepiece} ч. {minutes} мин. {seconds} сек.";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                }
            }
            else
            if (ev.CallbackQuery.Data == "RedPlantation")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                if (comparisonDateFromStart >= cartelRedPlantation.AddMinutes(3))
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Плантация созрела можно начинать собирать\n для подтверждения сбора необходимо выслать фотографию с урожаем, фото высылается как документ. Подпись к фотографии дожна быть \"red\" без ковычек");
                    dreamerMessageText = $"Плантация созрела можно начинать собирать\n для подтверждения сбора необходимо выслать фотографию с урожаем, фото высылается как документ. Подпись к фотографии дожна быть \"red\" без ковычек";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
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
                    dreamerMessageText = $"Плантация созреет через {timepiece} ч. {minutes} мин. {seconds} сек.";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                }
            }
        }

        /// <summary>
        /// Метод для отображения клавиатуры с выбором транспортировки коки картеля
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        private void CartelSendingTheHarvestedCoca(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var dreamerMessageText = "NULL";
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
                            dreamerMessageText = $"В грузовик погрузили {cartelNumberCocaTrucks} т. коки, груз будет доствален через {cartelDeliveryTimeTrucks} мин.\nВаше хранилище содержит {cartelCocaCash} т. коки";
                            Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                            break;
                        }
                    }
                }
                else
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Свободных грузовиков больше нет, выберите другой вид транспортировки");
                    dreamerMessageText = $"Свободных грузовиков больше нет, выберите другой вид транспортировки";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
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
                            dreamerMessageText = $"В вертолет погрузили {cartelNumberCocaHelicopters} т. коки, груз будет доствален через {cartelDeliveryTimeHelicopters} мин.\nВаше хранилище содержит {cartelCocaCash} т. коки";
                            Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                            break;
                        }
                    }
                }
                else
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Свободных вертолетов больше нет, выберите другой вид транспортировки");
                    dreamerMessageText = $"Свободных вертолетов больше нет, выберите другой вид транспортировки";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
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
                            dreamerMessageText = $"В самолет погрузили {cartelNumberCocaAircraft} т. коки, груз будет доствален через {cartelDeliveryTimeAircraft} мин.\nВаше хранилище содержит {cartelCocaCash} т. коки";
                            Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                            break;
                        }
                    }
                }
                else
                {
                    dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Свободных самолетов больше нет, выберите другой вид транспортировки");
                    dreamerMessageText = $"Свободных самолетов больше нет, выберите другой вид транспортировки";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                }
            }
        }

        //Метод описывающий нажатия основных кнопок клавиатуры
        async private void CartelMainKeyboardActions(string messageText, string dreamerMessageText, Telegram.Bot.Args.MessageEventArgs e)
        {
            switch (messageText)
            {
                //Проверяем баланс кошелька
                case "Кошелек":
                    await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Сумма на кошельке: {cartelCashBalance}");
                    dreamerMessageText = $"Сумма на кошельке: {cartelCashBalance}";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    break;

                //Организуем переводы
                case "Переводы":
                    if (cartelChemistTry)
                    {
                        WhoDoesTheCartelTranslateToWho(1);
                        dreamerBot.OnCallbackQuery -= CartelSelectTransferAction;
                        await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Кому хотите осуществить перевод?", replyMarkup: whoDoesTheTranslateToWhoCartel);
                        dreamerMessageText = $"Кому хотите осуществить перевод?";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectTransferAction;
                    }
                    else
                    {
                        WhoDoesTheCartelTranslateToWho(0);
                        dreamerBot.OnCallbackQuery -= CartelSelectTransferAction;
                        await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Кому хотите осуществить перевод?", replyMarkup: whoDoesTheTranslateToWhoCartel);
                        dreamerMessageText = $"Кому хотите осуществить перевод?";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectTransferAction;
                    }

                    break;

                //Проверяем текущие квесты
                case "Квесты":
                    if (missionPool.Count == 0)
                    {
                        await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Здраствуйте, Активных квестов нет");
                        dreamerMessageText = $"Здраствуйте, Активных квестов нет";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    }
                    else
                    {
                        for (int i = 0; i < missionPool.Count; i++)
                        {
                            await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"{missionPool[i]}");
                            dreamerMessageText = $"{missionPool[i]}";
                            Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        }
                    }
                    break;

                //Проверяем зрелость плантаций
                case "Плантации":
                    CartelPlantationSelectionKeyboardInline();
                    dreamerBot.OnCallbackQuery -= CartelHittingThePlantation;
                    await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Ваше хранилище содержит {cartelCocaCash}т. коки.\nВыберите необходимую плантацию", replyMarkup: cartelPlantationSelectionKeyboard);
                    dreamerMessageText = $"Ваше хранилище содержит {cartelCocaCash}т. коки.\nВыберите необходимую плантацию";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    dreamerBot.OnCallbackQuery += CartelHittingThePlantation;
                    break;

                //Действия с химиком
                case "Химик":
                    if (cartelChemistTry)
                    {
                        if (!cartelChemistNowTry)
                        {
                            SendingCocaForProcessingToAChemist(dreamerMessageText, e);
                        }
                        else
                        {
                            if (!chemistWorkBool)
                            {
                                CartelChemistShoppingKeyboardInline(15);
                                dreamerBot.OnCallbackQuery -= BuyingOrDownloadingAChemist;
                                var chemistText = $"Хотите отправить коку на переработку или купить нового химика?";
                                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                                dreamerMessageText = chemistText;
                                Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                                dreamerBot.OnCallbackQuery += BuyingOrDownloadingAChemist;    
                            }
                            else
                            {
                                var chemistText = $"На данный момент химик занят обработкой коки";
                                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, chemistText);
                                Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                            }
                            
                            //await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"ПОЖАЛУЙСТА хватить нажимать кнопку \"Химик\", он до сих пор в кокаиновом запое, выйдет из него явно не скоро :-(");
                        }
                    }
                    else
                    {
                        CartelChemistShoppingKeyboardInline(0);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 1\nЦена химика: {cartelСhemistPrice1}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist1} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist1} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist1} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment1}$\n\n" +
                            $"Химик 2\nЦена химика: {cartelСhemistPrice2}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist2} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist2} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist2} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment2}$\n\n" +
                            $"Химик 3\nЦена химика: {cartelСhemistPrice3}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist3} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist3} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist3} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment3}$\n\n" +
                            $"Химик 4\nЦена химика: {cartelСhemistPrice4}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist4} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist4} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist4} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment4}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = $"Какого химика хотите преобрести?";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    
                    break;

                case "Транспортировка коки":

                    if (cartelCocaCash < cartelNumberCocaTrucks && cartelCocaCashChemist1 < cartelNumberCocaTrucks && cartelCocaCashChemist2 < cartelNumberCocaTrucks && cartelCocaCashChemist3 < cartelNumberCocaTrucks && cartelCocaCashChemist4 < cartelNumberCocaTrucks)
                    {
                        var chemistText = $"Вам пока нечего перевозить\n" +
                            $"Баланс коки: {cartelCocaCash}\n" +
                            $"Баланс коки переработанной химиком 1: {cartelCocaCashChemist1}\n" +
                            $"Баланс коки переработанной химиком 2: {cartelCocaCashChemist2}\n" +
                            $"Баланс коки переработанной химиком 3: {cartelCocaCashChemist3}\n" +
                            $"Баланс коки переработанной химиком 4: {cartelCocaCashChemist4}\n";
                        await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, chemistText);
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        break;
                    }
                    else if (cartelCocaCashChemist1 < cartelNumberCocaTrucks && cartelCocaCashChemist2 < cartelNumberCocaTrucks && cartelCocaCashChemist3 < cartelNumberCocaTrucks && cartelCocaCashChemist4 < cartelNumberCocaTrucks)
                    {
                        CocaTransportationKeyboardInline();
                        dreamerBot.OnCallbackQuery -= CartelSendingTheHarvestedCoca;
                        await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, $"Каким видом транспорта будете отправлять коку?\nСвободных грузовиков: {cartelNumberTrucks}, водитель возмет {cartelpercentForTranslationTrucks * 100} % с продажи коки, вместимость {cartelNumberCocaTrucks} т. коки\nСвободных вертолетов: {cartelNumberHelicopters}, пилот возмет {cartelpercentForTranslationHelicopters * 100} % с продажи коки, вместимость {cartelNumberCocaHelicopters} т. коки\nСвободных самолетов: {cartelNumberAircraft}, пилот возмет {cartelpercentForTranslationAircraft * 100} % с продажи коки, вместимость {cartelNumberCocaAircraft} т. коки\nВодители и пилоты транспортируют груз, только если ТС полностью заполнено кокой", replyMarkup: cartelCocaTransportationKeyboard);
                        dreamerMessageText = $"Каким видом транспорта будете отправлять коку?\nСвободных грузовиков: {cartelNumberTrucks}, водитель возмет {cartelpercentForTranslationTrucks * 100} % с продажи коки, вместимость {cartelNumberCocaTrucks} т. коки\nСвободных вертолетов: {cartelNumberHelicopters}, пилот возмет {cartelpercentForTranslationHelicopters * 100} % с продажи коки, вместимость {cartelNumberCocaHelicopters} т. коки\nСвободных самолетов: {cartelNumberAircraft}, пилот возмет {cartelpercentForTranslationAircraft * 100} % с продажи коки, вместимость {cartelNumberCocaAircraft} т. коки\nВодители и пилоты транспортируют груз, только если ТС полностью заполнено кокой";
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSendingTheHarvestedCoca;
                        break;
                    }
                    else if (cartelCocaCash < cartelNumberCocaTrucks && (cartelCocaCashChemist1 >= cartelNumberCocaTrucks || cartelCocaCashChemist2 >= cartelNumberCocaTrucks || cartelCocaCashChemist3 >= cartelNumberCocaTrucks || cartelCocaCashChemist4 >= cartelNumberCocaTrucks))
                    {

                    }
                    else
                    {
                        CartelTransportationOfCoca(1);
                        dreamerBot.OnCallbackQuery -= ChoiceOfSendingCoca;
                        dreamerMessageText = $"Какую хотите отправить коку? Сырую или переработанную?";
                        await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, dreamerMessageText, replyMarkup: cartelTransportationOfCocaKeyboard);                       
                        Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += ChoiceOfSendingCoca;
                        break;
                    }

                    break;



            }
        }

        /// <summary>
        /// Метод определяющий действия кнопок выбора транспортировки сырой или переработанной коки
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        async private void ChoiceOfSendingCoca(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var dreamerMessageText = $"NULL";
            var message = ev.CallbackQuery.Message;

            switch (ev.CallbackQuery.Data)
            {
                case "TransportationOfRawCoca":
                    CocaTransportationKeyboardInline();
                    dreamerBot.OnCallbackQuery -= CartelSendingTheHarvestedCoca;
                    await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Каким видом транспорта будете отправлять коку?\nСвободных грузовиков: {cartelNumberTrucks}, водитель возмет {cartelpercentForTranslationTrucks * 100} % с продажи коки, вместимость {cartelNumberCocaTrucks} т. коки\nСвободных вертолетов: {cartelNumberHelicopters}, пилот возмет {cartelpercentForTranslationHelicopters * 100} % с продажи коки, вместимость {cartelNumberCocaHelicopters} т. коки\nСвободных самолетов: {cartelNumberAircraft}, пилот возмет {cartelpercentForTranslationAircraft * 100} % с продажи коки, вместимость {cartelNumberCocaAircraft} т. коки\nВодители и пилоты транспортируют груз, только если ТС полностью заполнено кокой", replyMarkup: cartelCocaTransportationKeyboard);
                    dreamerMessageText = $"Каким видом транспорта будете отправлять коку?\nСвободных грузовиков: {cartelNumberTrucks}, водитель возмет {cartelpercentForTranslationTrucks * 100} % с продажи коки, вместимость {cartelNumberCocaTrucks} т. коки\nСвободных вертолетов: {cartelNumberHelicopters}, пилот возмет {cartelpercentForTranslationHelicopters * 100} % с продажи коки, вместимость {cartelNumberCocaHelicopters} т. коки\nСвободных самолетов: {cartelNumberAircraft}, пилот возмет {cartelpercentForTranslationAircraft * 100} % с продажи коки, вместимость {cartelNumberCocaAircraft} т. коки\nВодители и пилоты транспортируют груз, только если ТС полностью заполнено кокой";
                    Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                    dreamerBot.OnCallbackQuery += CartelSendingTheHarvestedCoca;
                    break;

                case "TransportationOfProcessedCoca":

                    break;
            }
        }

        /// <summary>
        /// выбор действия, отправка коки химику на переработку или покупка нового химика
        /// </summary>
        /// <param name="sc"></param>
        /// <param name="ev"></param>
        async private void BuyingOrDownloadingAChemist(object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev)
        {
            var dreamerMessageText = $"NULL";
            var message = ev.CallbackQuery.Message;

            switch (ev.CallbackQuery.Data)
            {
                case "BuyingAChemist":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    if (!chemistWorkBool)
                    {
                        CartelQuantityOfCocaForChemist(Convert.ToInt32(cartelLoadingCocaToTheChemist));
                        dreamerBot.OnCallbackQuery -= CartelStepsForChoosingTheAmountOfCoca;
                        var chemistText = $"Сколько т. коки хотите передать химику на обработку?\nХимик возьмет комиссию  в размере {percentForChemist * 100}% с каждой т. коки";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: quantityOfCocaForChemistCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelStepsForChoosingTheAmountOfCoca;
                    }
                    else
                    {
                        var chemistText = $"На данный момент химик занят обработкой коки";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText);
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                    }

                    break;

                case "ReplacingAChemistWithANewOne":
                    dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);

                    comparisonChemistDateFromStartTime = DateTime.Now;
                    if (chemistBool1 == true && chemistBool2 == true && chemistBool3 == true && chemistBool4 == true)
                    {
                        CartelChemistShoppingKeyboardInline(0);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 1\nЦена химика: {cartelСhemistPrice1}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist1} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist1} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist1} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment1}$\n\n" +
                            $"Химик 2\nЦена химика: {cartelСhemistPrice2}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist2} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist2} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist2} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment2}$\n\n" +
                            $"Химик 3\nЦена химика: {cartelСhemistPrice3}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist3} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist3} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist3} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment3}$\n\n" +
                            $"Химик 4\nЦена химика: {cartelСhemistPrice4}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist4} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist4} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist4} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment4}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == false && chemistBool2 == true && chemistBool3 == true && chemistBool4 == true)
                    {
                        CartelChemistShoppingKeyboardInline(1);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 2\nЦена химика: {cartelСhemistPrice2}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist2} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist2} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist2} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment2}$\n\n" +
                            $"Химик 3\nЦена химика: {cartelСhemistPrice3}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist3} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist3} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist3} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment3}$\n\n" +
                            $"Химик 4\nЦена химика: {cartelСhemistPrice4}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist4} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist4} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist4} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment4}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == true && chemistBool2 == false && chemistBool3 == true && chemistBool4 == true)
                    {
                        CartelChemistShoppingKeyboardInline(2);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 1\nЦена химика: {cartelСhemistPrice1}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist1} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist1} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist1} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment1}$\n\n" +
                            $"Химик 3\nЦена химика: {cartelСhemistPrice3}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist3} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist3} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist3} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment3}$\n\n" +
                            $"Химик 4\nЦена химика: {cartelСhemistPrice4}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist4} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist4} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist4} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment4}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == true && chemistBool2 == true && chemistBool3 == false && chemistBool4 == true)
                    {
                        CartelChemistShoppingKeyboardInline(3);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 1\nЦена химика: {cartelСhemistPrice1}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist1} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist1} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist1} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment1}$\n\n" +
                            $"Химик 2\nЦена химика: {cartelСhemistPrice2}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist2} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist2} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist2} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment2}$\n\n" +
                            $"Химик 4\nЦена химика: {cartelСhemistPrice4}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist4} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist4} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist4} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment4}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == true && chemistBool2 == true && chemistBool3 == true && chemistBool4 == false)
                    {
                        CartelChemistShoppingKeyboardInline(4);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 1\nЦена химика: {cartelСhemistPrice1}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist1} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist1} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist1} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment1}$\n\n" +
                            $"Химик 2\nЦена химика: {cartelСhemistPrice2}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist2} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist2} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist2} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment2}$\n\n" +
                            $"Химик 3\nЦена химика: {cartelСhemistPrice3}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist3} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist3} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist3} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment3}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == false && chemistBool2 == false && chemistBool3 == true && chemistBool4 == true)
                    {
                        CartelChemistShoppingKeyboardInline(5);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 3\nЦена химика: {cartelСhemistPrice3}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist3} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist3} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist3} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment3}$\n\n" +
                            $"Химик 4\nЦена химика: {cartelСhemistPrice4}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist4} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist4} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist4} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment4}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == false && chemistBool2 == true && chemistBool3 == false && chemistBool4 == true)
                    {
                        CartelChemistShoppingKeyboardInline(6);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 2\nЦена химика: {cartelСhemistPrice2}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist2} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist2} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist2} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment2}$\n\n" +
                            $"Химик 4\nЦена химика: {cartelСhemistPrice4}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist4} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist4} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist4} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment4}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == false && chemistBool2 == true && chemistBool3 == true && chemistBool4 == false)
                    {
                        CartelChemistShoppingKeyboardInline(7);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 2\nЦена химика: {cartelСhemistPrice2}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist2} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist2} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist2} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment2}$\n\n" +
                            $"Химик 3\nЦена химика: {cartelСhemistPrice3}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist3} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist3} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist3} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment3}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == true && chemistBool2 == false && chemistBool3 == false && chemistBool4 == true)
                    {
                        CartelChemistShoppingKeyboardInline(8);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 1\nЦена химика: {cartelСhemistPrice1}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist1} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist1} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist1} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment1}$\n\n" +
                            $"Химик 4\nЦена химика: {cartelСhemistPrice4}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist4} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist4} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist4} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment4}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == true && chemistBool2 == false && chemistBool3 == true && chemistBool4 == false)
                    {
                        CartelChemistShoppingKeyboardInline(9);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 1\nЦена химика: {cartelСhemistPrice1}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist1} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist1} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist1} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment1}$\n\n" +
                            $"Химик 3\nЦена химика: {cartelСhemistPrice3}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist3} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist3} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist3} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment3}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == true && chemistBool2 == true && chemistBool3 == false && chemistBool4 == false)
                    {
                        CartelChemistShoppingKeyboardInline(10);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 1\nЦена химика: {cartelСhemistPrice1}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist1} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist1} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist1} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment1}$\n\n" +
                            $"Химик 2\nЦена химика: {cartelСhemistPrice2}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist2} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist2} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist2} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment2}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == true && chemistBool2 == false && chemistBool3 == false && chemistBool4 == false)
                    {
                        CartelChemistShoppingKeyboardInline(11);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 1\nЦена химика: {cartelСhemistPrice1}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist1} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist1} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist1} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment1}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == false && chemistBool2 == true && chemistBool3 == false && chemistBool4 == false)
                    {
                        CartelChemistShoppingKeyboardInline(12);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 2\nЦена химика: {cartelСhemistPrice2}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist2} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist2} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist2} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment2}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == false && chemistBool2 == false && chemistBool3 == true && chemistBool4 == false)
                    {
                        CartelChemistShoppingKeyboardInline(13);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 3\nЦена химика: {cartelСhemistPrice3}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist3} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist3} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist3} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment3}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else if (chemistBool1 == false && chemistBool2 == false && chemistBool3 == false && chemistBool4 == true)
                    {
                        CartelChemistShoppingKeyboardInline(14);
                        dreamerBot.OnCallbackQuery -= CartelSelectChemist;
                        var chemistText = $"Доступные химики:\n\nХимик 4\nЦена химика: {cartelСhemistPrice4}$\nЗагрузка коки: {cartelLoadingCocaToTheChemist4} т.\nВремя обработки 1 т. коки: {cartelCocaProcessingTimeByChemist4} мин\nмасса 1 т. коки после обработки: {cartelMassOfCocaAfterTreatmentByAChemist4} т.\nцена 1 т. коки после обработки: {cartelPriceOfCocaAfterChemicalTreatment4}$\n\n" +
                            $"Какого химика хотите преобрести?";
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, chemistText, replyMarkup: chemistShoppingCartel);
                        dreamerMessageText = chemistText;
                        Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                        dreamerBot.OnCallbackQuery += CartelSelectChemist;
                    }
                    else
                    {
                        await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Все химики закончились");
                        dreamerMessageText = $"Все химики закончились";
                    }

                    break;

            }
        }

        /// <summary>
        /// Метод отправки коки химику на переработку
        /// </summary>
        async private void SendingCocaForProcessingToAChemist(string dreamerMessageText, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (!chemistWorkBool)
            {
                CartelQuantityOfCocaForChemist(Convert.ToInt32(cartelLoadingCocaToTheChemist));
                dreamerBot.OnCallbackQuery -= CartelStepsForChoosingTheAmountOfCoca;
                var chemistText = $"Сколько т. коки хотите передать химику на обработку?\nХимик возьмет комиссию  в размере {percentForChemist * 100}% с каждой т. коки";
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, chemistText, replyMarkup: quantityOfCocaForChemistCartel);
                dreamerMessageText = chemistText;
                Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
                dreamerBot.OnCallbackQuery += CartelStepsForChoosingTheAmountOfCoca;
            }
            else
            {
                var chemistText = $"На данный момент химик занят обработкой коки";
                await dreamerBot.SendTextMessageAsync(e.Message.Chat.Id, chemistText);
                Logging(CartelDreamerBotMessageLog, chemistText, "Мечтатель", 1);
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
                new[] {new KeyboardButton("Кошелек"), new KeyboardButton("Переводы"), new KeyboardButton("Квесты") },
                new[] {new KeyboardButton("Плантации"), new KeyboardButton("Химик"), new KeyboardButton("Транспортировка коки")},
            })
            {
                OneTimeKeyboard = false
            };

        }

        /// <summary>
        /// Меню суммы переводов картеля
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
        /// Меню покупки химика
        /// </summary>
        private void CartelChemistShoppingKeyboardInline(int chemistShoppingCartelInKey)
        {
            switch (chemistShoppingCartelInKey)
            {
                case 0:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 1", "Chemist1"), InlineKeyboardButton.WithCallbackData($"Химик 2", "Chemist2") },
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 3", "Chemist3"), InlineKeyboardButton.WithCallbackData($"Химик 4", "Chemist4") },
                    });
                    break;

                case 1:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 2", "Chemist2"),  InlineKeyboardButton.WithCallbackData($"Химик 3", "Chemist3")},
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 4", "Chemist4") },
                    });
                    break;

                case 2:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 1", "Chemist1"), InlineKeyboardButton.WithCallbackData($"Химик 3", "Chemist3") },
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 4", "Chemist4") },
                    });
                    break;

                case 3:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 1", "Chemist1"), InlineKeyboardButton.WithCallbackData($"Химик 2", "Chemist2") },
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 4", "Chemist4") },
                    });
                    break;

                case 4:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 1", "Chemist1"), InlineKeyboardButton.WithCallbackData($"Химик 2", "Chemist2") },
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 3", "Chemist3") },
                    });
                    break;

                case 5:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 3", "Chemist3") },
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 4", "Chemist4") },
                    });
                    break;

                case 6:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 2", "Chemist2") },
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 4", "Chemist4") },
                    });
                    break;

                case 7:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 2", "Chemist2") },
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 3", "Chemist3") },
                    });
                    break;

                case 8:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 1", "Chemist1") },
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 4", "Chemist4") },
                    });
                    break;

                case 9:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 1", "Chemist1") },
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 3", "Chemist3") },
                    });
                    break;

                case 10:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 1", "Chemist1") },
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 2", "Chemist2") },
                    });
                    break;

                case 11:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 1", "Chemist1") },
                    });
                    break;

                case 12:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 2", "Chemist2") },
                    });
                    break;

                case 13:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 3", "Chemist3") },
                    });
                    break;

                case 14:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"Химик 4", "Chemist4") },
                    });
                    break;

                case 15:
                    chemistShoppingCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] { InlineKeyboardButton.WithCallbackData($"Отправить коку на обработку", "BuyingAChemist"), InlineKeyboardButton.WithCallbackData($"Замена химика новым", "ReplacingAChemistWithANewOne") },
                    });
                    break;

            }


        }

        /// <summary>
        /// Меню отправки коки химику на обработку
        /// </summary>
        private void CartelQuantityOfCocaForChemist(int amountCoca)
        {
            switch (amountCoca)
            {
                case 10:
                    quantityOfCocaForChemistCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"1", "1"), InlineKeyboardButton.WithCallbackData($"2", "2"), InlineKeyboardButton.WithCallbackData($"3", "3"), InlineKeyboardButton.WithCallbackData($"4", "4") },
                        new[] {InlineKeyboardButton.WithCallbackData($"5", "5"), InlineKeyboardButton.WithCallbackData($"6", "6"), InlineKeyboardButton.WithCallbackData($"7", "7"), InlineKeyboardButton.WithCallbackData($"8", "8") },
                        new[] {InlineKeyboardButton.WithCallbackData($"9", "9"), InlineKeyboardButton.WithCallbackData($"10", "10") },
                    });
                    break;

                case 16:
                    quantityOfCocaForChemistCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData($"1", "1"), InlineKeyboardButton.WithCallbackData($"2", "2"), InlineKeyboardButton.WithCallbackData($"3", "3"), InlineKeyboardButton.WithCallbackData($"4", "4") },
                        new[] {InlineKeyboardButton.WithCallbackData($"5", "5"), InlineKeyboardButton.WithCallbackData($"6", "6"), InlineKeyboardButton.WithCallbackData($"7", "7"), InlineKeyboardButton.WithCallbackData($"8", "8") },
                        new[] {InlineKeyboardButton.WithCallbackData($"9", "9"), InlineKeyboardButton.WithCallbackData($"10", "10"), InlineKeyboardButton.WithCallbackData($"11", "11"), InlineKeyboardButton.WithCallbackData($"12", "12") },
                        new[] {InlineKeyboardButton.WithCallbackData($"13", "13"), InlineKeyboardButton.WithCallbackData($"14", "14"), InlineKeyboardButton.WithCallbackData($"15", "15"), InlineKeyboardButton.WithCallbackData($"16", "16") },
                    });
                    break;

            }


        }

        /// <summary>
        /// Меню кому картель хочет перевести
        /// </summary>
        private void WhoDoesTheCartelTranslateToWho(int whoTranslateCartelInKey)
        {
            switch (whoTranslateCartelInKey)
            {
                case 1:
                    whoDoesTheTranslateToWhoCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Единство", "Unity"), InlineKeyboardButton.WithCallbackData("Черный рынок","BlackMarket")},
                        new[] {InlineKeyboardButton.WithCallbackData("Химик","Chemist")},
                    });
                    break;

                default:
                    whoDoesTheTranslateToWhoCartel = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Единство", "Unity"), InlineKeyboardButton.WithCallbackData("Черный рынок", "BlackMarket") },
                    });
                    break;
            }

            

        }

        /// <summary>
        /// Меню кому картель хочет перевести
        /// </summary>
        private void CartelTransportationOfCoca(int intCartelTransportationOfCoca)
        {
            switch (intCartelTransportationOfCoca)
            {

                case 1:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка сырой коки", "TransportationOfRawCoca") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка переработанной коки", "TransportationOfProcessedCoca") },
                    });
                    break;

                case 2:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 1", "TransportationOfProcessedCocaByAChemist1") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 2", "TransportationOfProcessedCocaByAChemist2") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 3", "TransportationOfProcessedCocaByAChemist3") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 4", "TransportationOfProcessedCocaByAChemist4") },
                    });
                    break;

                case 3:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 2", "TransportationOfProcessedCocaByAChemist2") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 3", "TransportationOfProcessedCocaByAChemist3") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 4", "TransportationOfProcessedCocaByAChemist4") },
                    });
                    break;

                case 4:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 1", "TransportationOfProcessedCocaByAChemist1") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 3", "TransportationOfProcessedCocaByAChemist3") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 4", "TransportationOfProcessedCocaByAChemist4") },
                    });
                    break;

                case 5:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 1", "TransportationOfProcessedCocaByAChemist1") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 2", "TransportationOfProcessedCocaByAChemist2") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 4", "TransportationOfProcessedCocaByAChemist4") },
                    });
                    break;

                case 6:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 1", "TransportationOfProcessedCocaByAChemist1") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 2", "TransportationOfProcessedCocaByAChemist2") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 3", "TransportationOfProcessedCocaByAChemist3") },
                    });
                    break;

                case 7:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 3", "TransportationOfProcessedCocaByAChemist3") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 4", "TransportationOfProcessedCocaByAChemist4") },
                    });
                    break;

                case 8:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 2", "TransportationOfProcessedCocaByAChemist2") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 4", "TransportationOfProcessedCocaByAChemist4") },
                    });
                    break;

                case 9:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 2", "TransportationOfProcessedCocaByAChemist2") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 3", "TransportationOfProcessedCocaByAChemist3") },
                    });
                    break;

                case 10:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 1", "TransportationOfProcessedCocaByAChemist1") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 4", "TransportationOfProcessedCocaByAChemist4") },
                    });
                    break;

                case 11:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 1", "TransportationOfProcessedCocaByAChemist1") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 3", "TransportationOfProcessedCocaByAChemist3") },
                    });
                    break;

                case 12:
                    cartelTransportationOfCocaKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 1", "TransportationOfProcessedCocaByAChemist1") },
                        new[] {InlineKeyboardButton.WithCallbackData("Транспортировка обработанной коки химиком 2", "TransportationOfProcessedCocaByAChemist2") },
                    });
                    break;


            }



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
                new[] {InlineKeyboardButton.WithCallbackData("Красная плантация", "RedPlantation")},
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
            var dreamerMessageText = "NULL";
            var message = ev.CallbackQuery.Message;
            Console.WriteLine($"{ev.CallbackQuery.Data}");
            if (ev.CallbackQuery.Data == "Yes")
            {
                dreamerBot.DeleteMessageAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId);
                await dreamerBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Получена миссия 2");
                dreamerMessageText = $"Получена миссия 2";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                //await dreamerBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, mission2ID);
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
                dreamerMessageText = $"Ты точно уверен, что это хороший выбор? Привет семье";
                Logging(CartelDreamerBotMessageLog, dreamerMessageText, "Мечтатель", 1);
                //await dreamerBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, missionAbandonmentID2);
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
            Debug.WriteLine($"{text} TypeMessage: {e.Message.Type.ToString()}");
            var unityMessageText = $"NULL";

            Logging(UnityBotMessageLog, e.Message.Text, e.Message.Chat.FirstName, e.Message.Chat.Id);

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
                unityMessageText = $"Добро пожаловать в Единство";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
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
                                unityMessageText = $"Миссия 1 Выполнена";
                                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                                //await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionComplete1ID);
                                unityMissionPool.Remove($"Миссия 1");
                                unityMissionCompleted1 = true;
                            }
                            break;

                        case "mission2":
                            if (unityMissionPool.Contains("Миссия 2"))
                            {
                                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 Выполнена");
                                unityMessageText = $"Миссия 2 Выполнена";
                                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                                //await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionComplete2ID);
                                unityMissionPool.Remove($"Миссия 2");
                                unityMissionCompleted2 = true;
                            }
                            break;

                        case "mission3":
                            if (unityMissionPool.Contains("Миссия 3"))
                            {
                                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 Выполнена");
                                unityMessageText = $"Миссия 3 Выполнена";
                                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                                //await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionComplete3ID);
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
                unityMessageText = $"Получена миссия 1";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                //await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMission1ID);
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
                unityMessageText = $"Поступила дополнительная миссия 2. Принять миссию?";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                generalBaroBot.OnCallbackQuery += UnityAcceptanceOrRefusalOfAMission;
            }

            //Получаем обязательную миссию 3
            if (unityComparisonDateFromStart >= unityDateStart.AddMinutes(unityStartTimeMission3) && unityGettingMission3 == false)
            {
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Получена миссия 3");
                unityMessageText = $"Получена миссия 3";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                //await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMission3ID);
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
                unityMessageText = $"Миссия 1 провалена";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                //await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionFailedID1);
                unityMissionPool.Remove($"Миссия 1");
            }

            //Проваливаем дополнительную миссию 2
            if (unityComparisonDateFromStart >= unityDateStart.AddMinutes(unityFailedTimeMission2) && unityMissionPool.Contains("Миссия 2"))
            {
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 2 провалена");
                unityMessageText = $"Миссия 2 провалена";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                //await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionFailedID2);
                unityMissionPool.Remove($"Миссия 2");
            }

            //Проваливаем обязательную миссию 3
            if (unityComparisonDateFromStart >= unityDateStart.AddMinutes(unityFailedTimeMission3) && unityMissionPool.Contains("Миссия 3"))
            {
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Миссия 3 провалена");
                unityMessageText = $"Миссия 3 провалена";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                //await generalBaroBot.SendAudioAsync(e.Message.Chat.Id, unityMissionFailedID3);
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
                unityMessageText = $"Начислена заработная плата в размере: {salary}\nСумма на кошельке: {unityCashBalance}";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
            }

            //Проверяем изменения баланса
            if (unityCashBalanceIntermediateStorage != unityCashBalanceCheck)
            {
                unityCashBalanceDifference = unityCashBalanceIntermediateStorage - unityCashBalanceCheck;
                unityCashBalance = unityCashBalanceIntermediateStorage;
                unityCashBalanceCheck = unityCashBalance;
                await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Ваш счет пополнился на {unityCashBalanceDifference}\nСумма на кошельке: {unityCashBalance}");
                unityMessageText = $"Ваш счет пополнился на {unityCashBalanceDifference}\nСумма на кошельке: {unityCashBalance}";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
            }

            //если сообщение текстовое, то записываем в переменную текст сообщения и проводим операции КОШЕЛЕК, ПЕРЕВОДЫ, КВЕСТЫ
            unityMessageText = e.Message.Text;
            switch (unityMessageText)
            {
                //Проверяем баланс кошелька
                case "Кошелек":
                    await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Сумма на кошельке: {unityCashBalance}");
                    unityMessageText = $"Сумма на кошельке: {unityCashBalance}";
                    Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                    break;

                //Организуем переводы между картелем и единством
                case "Переводы":
                    WhoDoesTheUnityTranslateTo();
                    generalBaroBot.OnCallbackQuery -= UnityTransfers;
                    await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Банк возьмет коммисию в размере {unityPercentForTranslation * 100}%. Какую сумму хотите перевести Картелю?", replyMarkup: whoTranslateUnity);
                    unityMessageText = $"Банк возьмет коммисию в размере {unityPercentForTranslation * 100}%. Какую сумму хотите перевести Картелю?";
                    Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                    generalBaroBot.OnCallbackQuery += UnityTransfers;
                    break;

                //Проверяем текущие квесты
                case "Квесты":
                    if (unityMissionPool.Count == 0)
                    {
                        await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"Здраствуйте, Активных квестов нет");
                        unityMessageText = $"Здраствуйте, Активных квестов нет";
                        Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                    }
                    else
                    {
                        for (int i = 0; i < unityMissionPool.Count; i++)
                        {
                            await generalBaroBot.SendTextMessageAsync(e.Message.Chat.Id, $"{unityMissionPool[i]}");
                            unityMessageText = $"{unityMissionPool[i]}";
                            Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                        }
                    }
                    break;
            }

            UnitySerialization();
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
                    unityMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                    Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                }
                generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {unityCashBalance}");
                unityMessageText = $"Ваш баланс: {unityCashBalance}";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
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
                    unityMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                    Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                }
                generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {unityCashBalance}");
                unityMessageText = $"Ваш баланс: {unityCashBalance}";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
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
                    unityMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                    Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                }
                generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {unityCashBalance}");
                unityMessageText = $"Ваш баланс: {unityCashBalance}";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
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
                    unityMessageText = $"Перевод невозможно осуществить, так как на счете недостаточно средств";
                    Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                }
                generalBaroBot.SendTextMessageAsync(ev.CallbackQuery.Message.Chat.Id, $"Ваш баланс: {unityCashBalance}");
                unityMessageText = $"Ваш баланс: {unityCashBalance}";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
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
                unityMessageText = $"Получена миссия 2";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                //await generalBaroBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, unityMission2ID);
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
                unityMessageText = $"Ты точно уверен, что это хороший выбор? Привет семье";
                Logging(UnityGeneralBaroBotMessageLog, unityMessageText, "Генерал Баро", 2);
                //await generalBaroBot.SendAudioAsync(ev.CallbackQuery.Message.Chat.Id, unityMissionAbandonmentID2);
                unityGettingMission2 = true;
            }
        }

        /// <summary>
        /// Запуск чат бота для картеля
        /// </summary>
        public void CartelStart()
        {
            string tokenDreamer = "Dreamer.txt";

            cartelCashBalance = 200000000;
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
            cartelStartTimeMission1 = 5;
            cartelStartTimeMission2 = 10;
            cartelStartTimeMission3 = 15;
            cartelFailedTimeMission1 = 20;
            cartelFailedTimeMission2 = 25;
            cartelFailedTimeMission3 = 30;
            cartelTributePaymentTime = 1;
            tribute = 20;
            missionPool = new List<string>();
            cartelMissionCompleted1 = false;
            cartelMissionCompleted2 = false;
            cartelMissionCompleted3 = false;

            //новые параметры
            cartelChemistTry = false;
            cartelChemistNowTry = false;
            cartelСhemistPrice1 = 105;
            cartelLoadingCocaToTheChemist1 = 10;
            //cartelCocaProcessingTimeByChemist1 = 5;
            cartelCocaProcessingTimeByChemist1 = 0.125;
            cartelMassOfCocaAfterTreatmentByAChemist1 = 0.5;
            cartelPriceOfCocaAfterChemicalTreatment1 = 20;
            cartelСhemistPrice2 = 145;
            cartelLoadingCocaToTheChemist2 = 16;
            //cartelCocaProcessingTimeByChemist2 = 5;
            cartelCocaProcessingTimeByChemist2 = 0.125;
            cartelMassOfCocaAfterTreatmentByAChemist2 = 1;
            cartelPriceOfCocaAfterChemicalTreatment2 = 15;
            cartelСhemistPrice3 = 140;
            cartelLoadingCocaToTheChemist3 = 10;
            //cartelCocaProcessingTimeByChemist3 = 3;
            cartelCocaProcessingTimeByChemist3 = 0.125;
            cartelMassOfCocaAfterTreatmentByAChemist3 = 0.5;
            cartelPriceOfCocaAfterChemicalTreatment3 = 18.5;
            cartelСhemistPrice4 = 160;
            cartelLoadingCocaToTheChemist4 = 10;
            //cartelCocaProcessingTimeByChemist4 = 15;
            cartelCocaProcessingTimeByChemist4 = 0.125;
            cartelMassOfCocaAfterTreatmentByAChemist4 = 1;
            cartelPriceOfCocaAfterChemicalTreatment4 = 35;
            translationsID = 0;
            comparisonChemistDateFromStart = dateStart;
            comparisonChemistDateFromStartTime = comparisonChemistDateFromStart;
            cartelNowTimeChemist = 1;
            chemistBool1 = true;
            chemistBool2 = true;
            chemistBool3 = true;
            chemistBool4 = true;
            cartelChemistSaleDate = dateStart;
            cartelChemistPaymentTime = 1;
            percentForChemist = 0.1;
            cartelCashBalanceChemistLimit = 0;
            chemistWorkBool = false;
            cartelCocaCashChemist1 = 0;
            cartelCocaCashChemist2 = 0;
            cartelCocaCashChemist3 = 0;
            cartelCocaCashChemist4 = 0;

            CartelDeserialization();
            Console.WriteLine(dateStart);
            Console.WriteLine(dateStart.AddMinutes(1));
            Console.WriteLine(dateStart.AddHours(1));
            dreamerBot = new TelegramBotClient(NewDoc(tokenDreamer));

            dreamerBot.OnMessage += MessageListener;
            dreamerBot.StartReceiving();


        }

        /// <summary>
        /// Запуск чат бота для Единства
        /// </summary>
        public void UnityStart()
        {
            string tokenGeneralBaro = "GeneralBaro.txt";
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
            unityStartTimeMission1 = 5;
            unityStartTimeMission2 = 10;
            unityStartTimeMission3 = 15;
            unityFailedTimeMission1 = 20;
            unityFailedTimeMission2 = 25;
            unityFailedTimeMission3 = 30;
            unitySalaryPaymentTime = 1;
            unityMissionPool = new List<string>();
            unityMissionCompleted1 = false;
            unityMissionCompleted2 = false;
            unityMissionCompleted3 = false;
            UnityDeserialization();
            Console.WriteLine(unityDateStart);
            Console.WriteLine(unityDateStart.AddMinutes(1));
            generalBaroBot = new TelegramBotClient(NewDoc(tokenGeneralBaro));
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

        /// <summary>
        /// конструктро
        /// </summary>
        /// <param name="w"></param>
        public Dreamer_Bot_And_General_Baro_Bot(MainWindow w)
        {
            this.CartelBotMessageLog = new ObservableCollection<MessageLog>();
            this.CartelDreamerBotMessageLog = new ObservableCollection<MessageLog>();
            this.UnityBotMessageLog = new ObservableCollection<MessageLog>();
            this.UnityGeneralBaroBotMessageLog = new ObservableCollection<MessageLog>();
            this.w = w;

            Thread cartelStartTask = new Thread(CartelStart);
            cartelStartTask.Start();

            Thread unityStartTask = new Thread(UnityStart);
            unityStartTask.Start();

        }

        /// <summary>
        /// Ручная рассылка от Мечтателя
        /// </summary>
        /// <param name="Text">Текст</param>
        /// <param name="Id">ИД</param>
        public void CartelSendMessage(string Text, string Id)
        {
            long id = Convert.ToInt64(Id);
            dreamerBot.SendTextMessageAsync(id, Text);
        }

        /// <summary>
        /// Ручная рассылка от Генерала Баро
        /// </summary>
        /// <param name="Text">Текст</param>
        /// <param name="Id">ИД</param>
        public void UnitySendMessage(string Text, string Id)
        {
            long id = Convert.ToInt64(Id);
            generalBaroBot.SendTextMessageAsync(id, Text);
        }

        /// <summary>
        /// Метод для наполнения коллекции логов сообщений
        /// </summary>
        /// <param name="MessageLog">название коллекции</param>
        /// <param name="messageText">текст сообщения</param>
        /// <param name="firstName">кто отправил сообщение</param>
        /// <param name="id">идентификатор того кто направил сообщение</param>
        public void Logging(ObservableCollection<MessageLog> MessageLog, string messageText, string firstName, long id)
        {
            AllBotMessageLog = MessageLog;
            w.Dispatcher.Invoke(() =>
            {
                AllBotMessageLog.Add(
                    new MessageLog(
                        DateTime.Now.ToLongTimeString(),
                        messageText,
                        firstName,
                        id));
            });
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

