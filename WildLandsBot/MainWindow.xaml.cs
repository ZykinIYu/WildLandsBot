using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WildLandsBot
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dreamer_Bot_And_General_Baro_Bot dreamerAndGeneralBaro;
        Pac_Katari_Bot pacKatari;
        Karen_Bowman_Bot karenBowman;


        public MainWindow()
        {
            InitializeComponent();

            dreamerAndGeneralBaro = new Dreamer_Bot_And_General_Baro_Bot(this);
            pacKatari = new Pac_Katari_Bot(this);
            karenBowman = new Karen_Bowman_Bot(this);

            cartelLoglist.ItemsSource = dreamerAndGeneralBaro.CartelBotMessageLog;
            cartelLoglistBot.ItemsSource = dreamerAndGeneralBaro.CartelDreamerBotMessageLog;
            textCartelCashBalance.Text = Convert.ToString(dreamerAndGeneralBaro.CartelCashBalance);
            textCartelCocaCash.Text = Convert.ToString(dreamerAndGeneralBaro.CartelCocaCash);
            textСartelMissionCompleted1.Text = Convert.ToString(dreamerAndGeneralBaro.СartelMissionCompleted1);
            textСartelMissionCompleted2.Text = Convert.ToString(dreamerAndGeneralBaro.СartelMissionCompleted2);
            textСartelMissionCompleted3.Text = Convert.ToString(dreamerAndGeneralBaro.СartelMissionCompleted3);

            unityLoglist.ItemsSource = dreamerAndGeneralBaro.UnityBotMessageLog;
            unityLoglistBot.ItemsSource = dreamerAndGeneralBaro.UnityGeneralBaroBotMessageLog;
            textUnityCashBalance.Text = Convert.ToString(dreamerAndGeneralBaro.UnityCashBalance);
            textUnityMissionCompleted1.Text = Convert.ToString(dreamerAndGeneralBaro.UnityMissionCompleted1);
            textUnityMissionCompleted2.Text = Convert.ToString(dreamerAndGeneralBaro.UnityMissionCompleted2);
            textUnityMissionCompleted3.Text = Convert.ToString(dreamerAndGeneralBaro.UnityMissionCompleted3);

            insurgentLoglist.ItemsSource = pacKatari.InsurgentBotMessageLog;
            insurgentLoglistBot.ItemsSource = pacKatari.InsurgentPacKatariBotMessageLog;
            textInsurgentsMissionCompleted1.Text = Convert.ToString(pacKatari.InsurgentsMissionCompleted1);
            textInsurgentsMissionCompleted2.Text = Convert.ToString(pacKatari.InsurgentsMissionCompleted2);
            textInsurgentsMissionCompleted3.Text = Convert.ToString(pacKatari.InsurgentsMissionCompleted3);

            ghostsLoglist.ItemsSource = karenBowman.GhostsBotMessageLog;
            ghostsLoglistBot.ItemsSource = karenBowman.GhostsKarenBowmanBotMessageLog;
            textGhostsMissionCompleted1.Text = Convert.ToString(karenBowman.GhostsMissionCompleted1);
            textGhostsMissionCompleted2.Text = Convert.ToString(karenBowman.GhostsMissionCompleted2);
            textGhostsMissionCompleted3.Text = Convert.ToString(karenBowman.GhostsMissionCompleted3);

        }

        /// <summary>
        /// Клик для отправки письма от бота Мечтатель
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CartelBtnCartelClick(object sender, RoutedEventArgs e)
        {
            dreamerAndGeneralBaro.CartelSendMessage(cartelTxtMsgSend.Text, cartelTargetSend.Text);
            dreamerAndGeneralBaro.Logging(dreamerAndGeneralBaro.CartelDreamerBotMessageLog, cartelTxtMsgSend.Text, "Мечтатель", 1);
        }

        /// <summary>
        /// Клик для отправки письма от бота Генерал Баро
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnityBtnUnityClick(object sender, RoutedEventArgs e)
        {
            dreamerAndGeneralBaro.UnitySendMessage(unityTxtMsgSend.Text, unityTargetSend.Text);
            dreamerAndGeneralBaro.Logging(dreamerAndGeneralBaro.UnityGeneralBaroBotMessageLog, unityTxtMsgSend.Text, "Генерал Баро", 2);
        }

        /// <summary>
        /// Клик для отправки письма от бота Пак Катари
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InsurgentsBtnInsurgentsClick(object sender, RoutedEventArgs e)
        {
            pacKatari.InsurgentsSendMessage(insurgentTxtMsgSend.Text, insurgentTargetSend.Text);
            pacKatari.Logging(pacKatari.InsurgentPacKatariBotMessageLog, insurgentTxtMsgSend.Text, "Пак Катари", 3);
        }

        /// <summary>
        /// Клик для отправки письма от бота Карен Боуман
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GhostsBtnGhostsClick(object sender, RoutedEventArgs e)
        {
            karenBowman.GhostsSendMessage(ghostsTxtMsgSend.Text, ghostsTargetSend.Text);
            karenBowman.Logging(karenBowman.GhostsKarenBowmanBotMessageLog, ghostsTxtMsgSend.Text, "Карен Боуман", 4);
        }

        /// <summary>
        /// Клик для обновления параметров для всех ботов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cartelBtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            textCartelCashBalance.Text = Convert.ToString(dreamerAndGeneralBaro.CartelCashBalance);
            textCartelCocaCash.Text = Convert.ToString(dreamerAndGeneralBaro.CartelCocaCash);
            textСartelMissionCompleted1.Text = Convert.ToString(dreamerAndGeneralBaro.СartelMissionCompleted1);
            textСartelMissionCompleted2.Text = Convert.ToString(dreamerAndGeneralBaro.СartelMissionCompleted2);
            textСartelMissionCompleted3.Text = Convert.ToString(dreamerAndGeneralBaro.СartelMissionCompleted3);

            textUnityCashBalance.Text = Convert.ToString(dreamerAndGeneralBaro.UnityCashBalance);
            textUnityMissionCompleted1.Text = Convert.ToString(dreamerAndGeneralBaro.UnityMissionCompleted1);
            textUnityMissionCompleted2.Text = Convert.ToString(dreamerAndGeneralBaro.UnityMissionCompleted2);
            textUnityMissionCompleted3.Text = Convert.ToString(dreamerAndGeneralBaro.UnityMissionCompleted3);

            textInsurgentsMissionCompleted1.Text = Convert.ToString(pacKatari.InsurgentsMissionCompleted1);
            textInsurgentsMissionCompleted2.Text = Convert.ToString(pacKatari.InsurgentsMissionCompleted2);
            textInsurgentsMissionCompleted3.Text = Convert.ToString(pacKatari.InsurgentsMissionCompleted3);

            textGhostsMissionCompleted1.Text = Convert.ToString(karenBowman.GhostsMissionCompleted1);
            textGhostsMissionCompleted2.Text = Convert.ToString(karenBowman.GhostsMissionCompleted2);
            textGhostsMissionCompleted3.Text = Convert.ToString(karenBowman.GhostsMissionCompleted3);

        }
    }
}
