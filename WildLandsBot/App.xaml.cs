using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WildLandsBot
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string tokenPacKatari = "PacKatari.txt";
            string tokenDreamer = "Dreamer.txt";
            string tokenGeneralBaro = "GeneralBaro.txt";
            string tokenKarenBowman = "KarenBowmanBot.txt";

            if (File.Exists(tokenDreamer) != true)
            {
                MessageBox.Show("В проекте отсутствует файл с токеном \"Dreamer.txt\".\nФайл был добавлен, необходимо закрыть приложение, написать в файле токен и запустить заново");
            }

            if (File.Exists(tokenGeneralBaro) != true)
            {
                MessageBox.Show("В проекте отсутствует файл с токеном \"GeneralBaro.txt\".\nФайл был добавлен, необходимо закрыть приложение, написать в файле токен и запустить заново");
            }

            if (File.Exists(tokenPacKatari) != true)
            {
                MessageBox.Show("В проекте отсутствует файл с токеном \"PacKatari.txt\".\nФайл был добавлен, необходимо закрыть приложение, написать в файле токен и запустить заново");
            }

            if (File.Exists(tokenKarenBowman) != true)
            {
                MessageBox.Show("В проекте отсутствует файл с токеном \"KarenBowmanBot.txt\".\nФайл был добавлен, необходимо закрыть приложение, написать в файле токен и запустить заново");
            }


        }
    }
}
