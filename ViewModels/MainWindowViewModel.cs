using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace AvaloniaApplication8.ViewModels
{
    public class MainWindowViewModel : ReactiveWindow<MainWindowViewModel> //ReactiveWindow<MainWindowViewModel> //ViewModelBase
    {
        private async Task DownloadGame()
        {
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
                }
                else
                {
                    var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(
                        new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Error",
                            ContentHeader = "Error",
                            ContentMessage = "Failed to download https://www.plazmaburst2.com/launcher/time.php. Check your internet connection.",
                            WindowIcon = new WindowIcon(@"favicon.ico")
                        });
                    await messageBoxStandardWindow.Show();
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
                    var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(
                        new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Success",
                            ContentHeader = "Success",
                            ContentMessage = "Successfully updated the game.",
                            WindowIcon = new WindowIcon(@"favicon.ico")
                        });
                    await messageBoxStandardWindow.Show();
                }
                else
                {
                    var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(
                        new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Error",
                            ContentHeader = "Error",
                            ContentMessage = "Failed to download https://plazmaburst2.com/pb2/pb2_re34.swf. Check your internet connection.",
                            WindowIcon = new WindowIcon(@"favicon.ico")
                        });
                    await messageBoxStandardWindow.Show();
                }
            }
        }
        private async Task PlayGame()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
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
                            }
                            else
                            {
                                var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(
                                    new MessageBoxStandardParams
                                    {
                                        ButtonDefinitions = ButtonEnum.Ok,
                                        ContentTitle = "Error",
                                        ContentHeader = "Error",
                                        ContentMessage = "Failed to download flashplayer11_7r700_224_win_sa.exe. Check your internet connection.",
                                        WindowIcon = new WindowIcon(@"favicon.ico")
                                    });
                                await messageBoxStandardWindow.Show();
                            }
                        }
                    }
                    string myparams = "pb2_re34.swf?from_standalone=1";
                    if (File.Exists("Plazma Burst 2.auth"))
                    {
                        string[] text = (await File.ReadAllTextAsync("Plazma Burst 2.auth")).Split('\n', 2);
                        myparams = $"pb2_re34.swf?l={text[0]}&p={text[1]}&from_standalone=1";
                    }
                    ProcessStartInfo startInfo = new ProcessStartInfo("flashplayer11_7r700_224_win_sa.exe")
                    {
                        WindowStyle = ProcessWindowStyle.Normal,
                        Arguments = myparams
                    };
                    Process.Start(startInfo);
                    Environment.Exit(0);
                    break;
                case PlatformID.Unix:
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
                            }
                            else
                            {
                                var messageBoxStandardWindow = MessageBoxManager.GetMessageBoxStandardWindow(
                                    new MessageBoxStandardParams
                                    {
                                        ButtonDefinitions = ButtonEnum.Ok,
                                        ContentTitle = "Error",
                                        ContentHeader = "Error",
                                        ContentMessage = "Failed to download flashplayer-x86_64-unknown-linux-gnu. Check your internet connection.",
                                        WindowIcon = new WindowIcon(@"favicon.ico")
                                    });
                                await messageBoxStandardWindow.Show();
                            }
                        }
                    }
                    string myparams_linux = "flashplayer-x86_64-unknown-linux-gnu?from_standalone=1";
                    if (File.Exists("Plazma Burst 2.auth"))
                    {
                        string[] text = (await File.ReadAllTextAsync("Plazma Burst 2.auth")).Split('\n', 2);
                        myparams_linux =
                            $"flashplayer-x86_64-unknown-linux-gnu?l={text[0]}&p={text[1]}&from_standalone=1";
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
            var messageBoxWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxInputWindow(new MessageBoxInputParams() 
                    {  ContentHeader = "Login", WindowIcon = new WindowIcon(@"favicon.ico"), ContentTitle = "Login", ContentMessage = "Enter your login", WindowStartupLocation = WindowStartupLocation.CenterOwner, ShowInCenter = true});
            var login = await messageBoxWindow.Show(); 
            var messageBoxWindow2 = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxInputWindow(new MessageBoxInputParams() 
                    {  ContentHeader = "Login", WindowIcon = new WindowIcon(@"favicon.ico"), ContentTitle = "Password", ContentMessage = "Enter your password", WindowStartupLocation = WindowStartupLocation.CenterOwner, ShowInCenter = true, IsPassword = true});
            var password = await messageBoxWindow2.Show();
            SaveLoginPassword(login.Message, password.Message);
        }
        private async void SaveLoginPassword(string l, string p)
        {
            await File.WriteAllTextAsync("Plazma Burst 2.auth", $"{l}\n{p}");
            LoginTemp = l;
            LoginURLTemp = $"https://www.plazmaburst2.com/?a=&s=7&ac={l}";
        }
        private async Task<string> GetLogin()
        {
            if (!File.Exists("Plazma Burst 2.auth")) return "Guest";
            string text = await File.ReadAllTextAsync("Plazma Burst 2.auth");
            text = text.Split('\n', 2)[0];
            return text;
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

        public string LoginTemp = "Guest";
        public string Login {
            get => LoginTemp;
            set => LoginTemp = value;
        }
        public string LoginURLTemp = "https://www.plazmaburst2.com/?a=&s=7&ac=.guest";
        public string LoginURL
        {
            get => LoginURLTemp; 
            set => LoginURLTemp = value;
        }
    }
}