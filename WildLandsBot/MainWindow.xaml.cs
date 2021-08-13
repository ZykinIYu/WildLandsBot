using Dreamer_Bot;
using System;
using System.Collections.Generic;
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
        Pac_Katari_Bot pacKatari = new Pac_Katari_Bot();
        Karen_Bowman_Bot karenBowman = new Karen_Bowman_Bot();

        public MainWindow()
        {
            InitializeComponent();
            dreamerAndGeneralBaro = new Dreamer_Bot_And_General_Baro_Bot();
            loglist.ItemsSource = Dreamer_Bot_And_General_Baro_Bot.CartelBotMessageLog;


            //параллельный запуск ботов
            Thread cartelAndUnityStartTask = new Thread(dreamerAndGeneralBaro.CartelAndUnityStart);
            cartelAndUnityStartTask.Start();
            Thread insurgentsStartTask = new Thread(pacKatari.InsurgentsStart);
            insurgentsStartTask.Start();
            Thread ghostsStartTask = new Thread(karenBowman.GhostsStart);
            ghostsStartTask.Start();
        }
    }
}
