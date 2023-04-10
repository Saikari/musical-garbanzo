using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.ReactiveUI;
using ReactiveUI;
using MessageBox.Avalonia;

namespace AvaloniaApplication8.ViewModels
{
    public class MainWindowViewModel : ReactiveWindow<MainWindowViewModel> //ViewModelBase
    {
        private async Task DownloadGame()
        {
            //var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
            //.GetMessageBoxStandardWindow("title", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed...");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.plazmaburst2.com/launcher/time.php"); //""
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress.ToString());
                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    using (var contentStream = await content.ReadAsStreamAsync())
                    {
                        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "last_update.v");
                        using (var fs = new FileStream(path, FileMode.Create))
                        {
                            await contentStream.CopyToAsync(fs);
                        }
                    }

                    var a = MessageBoxManager.GetMessageBoxStandardWindow(title : "Title", text : "Text");
                    await a.ShowDialog(this);
                }
                else
                {



                }
            }
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://plazmaburst2.com/pb2/pb2_re34.swf"); //""
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress.ToString());
                if (response.IsSuccessStatusCode)
                {
                    HttpContent content = response.Content;
                    using (var contentStream = await content.ReadAsStreamAsync())
                    {
                        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pb2_re34.swf");
                        using (var fs = new FileStream(path, FileMode.Create))
                        {
                            await contentStream.CopyToAsync(fs);
                        }
                    }

                    //var a = await MessageBoxManager.GetMessageBoxInputWindow("asd", "asd");
                }
                else
                {



                }
            }
        }
        private async Task PlayGame()
        {

            switch (Environment.OSVersion.Platform)
            {
                case System.PlatformID.Win32NT:
                    if (!File.Exists("flashplayer11_7r700_224_win_sa.exe"))
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("https://raw.githubusercontent.com/Saikari/FlashPlayersArchive/main/flashplayer11_7r700_224_win_sa.exe"); //""
                            HttpResponseMessage response = await client.GetAsync(client.BaseAddress.ToString());
                            if (response.IsSuccessStatusCode)
                            {
                                HttpContent content = response.Content;
                                using (var contentStream = await content.ReadAsStreamAsync())
                                {
                                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "flashplayer11_7r700_224_win_sa.exe");
                                    using (var fs = new FileStream(path, FileMode.Create))
                                    {
                                        await contentStream.CopyToAsync(fs);
                                    }
                                }
                                //var a = await MessageBoxManager.GetMessageBoxInputWindow("asd", "asd");
                            }
                            else
                            {
                                //var a = await MessageBoxManager.GetMessageBoxInputWindow("asd", "asd");
                            }
                        }
                    }
                    string myparams = "pb2_re34.swf?from_standalone=1";
                    if (File.Exists("Plazma Burst 2.auth"))
                    {
                        string[] text = (await File.ReadAllTextAsync("Plazma Burst 2.auth")).Split('\n', 2);
                        myparams = "pb2_re34.swf?l=" + text[0] + "&p=" + text[1] + "&from_standalone=1";
                    }
                    ProcessStartInfo startInfo = new ProcessStartInfo("flashplayer11_7r700_224_win_sa.exe")
                    {
                        WindowStyle = ProcessWindowStyle.Normal,
                        Arguments = myparams
                    };
                    Process.Start(startInfo);
                    Environment.Exit(0);
                    break;
                case System.PlatformID.Unix:
                    if (!File.Exists("flashplayer-x86_64-unknown-linux-gnu"))
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("https://raw.githubusercontent.com/Saikari/FlashPlayersArchive/main/flashplayer-x86_64-unknown-linux-gnu"); //""
                            HttpResponseMessage response = await client.GetAsync(client.BaseAddress.ToString());
                            if (response.IsSuccessStatusCode)
                            {
                                HttpContent content = response.Content;
                                using (var contentStream = await content.ReadAsStreamAsync())
                                {
                                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "flashplayer-x86_64-unknown-linux-gnu");
                                    using (var fs = new FileStream(path, FileMode.Create))
                                    {
                                        await contentStream.CopyToAsync(fs);
                                    }
                                }
                                //var a = await MessageBoxManager.GetMessageBoxInputWindow("asd", "asd");
                            }
                            else
                            {
                                //var a = await MessageBoxManager.GetMessageBoxInputWindow("asd", "asd");
                            }
                        }
                    }
                    string myparams_linux = "flashplayer-x86_64-unknown-linux-gnu?from_standalone=1";
                    if (File.Exists("Plazma Burst 2.auth"))
                    {
                        string[] text = (await File.ReadAllTextAsync("Plazma Burst 2.auth")).Split('\n', 2);
                        myparams_linux = "flashplayer-x86_64-unknown-linux-gnu?l=" + text[0] + "&p=" + text[1] + "&from_standalone=1";
                    }
                    ProcessStartInfo startInfo_linux = new ProcessStartInfo("flashplayer-x86_64-unknown-linux-gnu")
                    {
                        WindowStyle = ProcessWindowStyle.Normal,
                        Arguments = myparams_linux
                    };
                    Process.Start(startInfo_linux);
                    Environment.Exit(0);
                    break;
            }
        }
        private async Task ChangeCredetinals()
        {

                var a = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Test", "Test");
                await a.ShowDialog(this);


        }

        private async Task<double> GetLauncherInfo()
        {
            string content;
            double result = 0.0;
            using HttpClient client = new HttpClient();
            content = await client.GetStringAsync("https://www.plazmaburst2.com/launcher/index.php");
            if (content != string.Empty)
            {
                Regex regex = new Regex(@">\w+ goal: \d{1,3} / (\d{1,3}) support</span>");
                MatchCollection matches = regex.Matches(content);
                double.TryParse(matches[-1].Value, out result);
            }
            return result;
        }
        public ICommand DownloadGameCommand { get; }
        public ICommand PlayGameCommand { get; }
        public ICommand ChangeCredetinalsCommand { get; }
        public MainWindowViewModel()
        {
            DownloadGameCommand = ReactiveCommand.CreateFromTask(DownloadGame);
            PlayGameCommand = ReactiveCommand.CreateFromTask(PlayGame);
            ChangeCredetinalsCommand = ReactiveCommand.CreateFromTask(ChangeCredetinals);
        }

        public double Donations => 100.0;


        public string Login => "Guest";
        public string LoginURL => "https://www.plazmaburst2.com/?a=&s=7&ac=Guest";
    }
}