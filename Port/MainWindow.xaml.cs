using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Port
{
    public partial class MainWindow : Window
    {
        private TcpListener listener;
        private int listeningPort;
        private CancellationTokenSource cancellationTokenSource;

        public MainWindow()
        {
            InitializeComponent();
            LoadPortData();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadPortData(); // Вызов метода для обновления данных
            MessageBox.Show("Жаңарды");
        }

        private void LoadPortData()
        {
            StringBuilder openPorts = new StringBuilder("Ашық порттар:\n");
            StringBuilder closedPorts = new StringBuilder("Жабық порттар:\n");
            StringBuilder noOptionPorts = new StringBuilder("Белгісіз порттар:\n");

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/c netstat -a -n -o",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    if (line.StartsWith("  TCP") || line.StartsWith("  UDP"))
                    {
                        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        var port = parts[1].Split(':')[1];
                        var state = parts.Length > 3 ? parts[3] : string.Empty;

                        switch (state)
                        {
                            case "LISTENING":
                                openPorts.AppendLine($"TCP {port}");
                                break;
                            case "ESTABLISHED":
                            case "CLOSE_WAIT":
                            case "TIME_WAIT":
                                closedPorts.AppendLine($"TCP {port}");
                                break;
                            default:
                                noOptionPorts.AppendLine($"TCP {port}");
                                break;
                        }
                    }
                }
            }

            Dispatcher.Invoke(() => {
                openPortsTextBox.Text = openPorts.ToString();
                closedPortsTextBox.Text = closedPorts.ToString();
                noOptionPortsTextBox.Text = noOptionPorts.ToString();
            });
        }

        private void OpenPort_Click(object sender, RoutedEventArgs e)
        {
            string port = portTextBox.Text;
            int portNumber;
            if (int.TryParse(port, out portNumber))
            {
                string script = $"New-NetFirewallRule -DisplayName 'OpenPort{port}' -Direction Inbound -LocalPort {port} -Protocol TCP -Action Allow";
                RunPowershellScript(script);
                StartTcpListener(portNumber);
                MessageBox.Show($"Порт {port} ашылды.");
            }
            else
            {
                MessageBox.Show("Порт номері дұрыс емес");
            }
        }

        private void ClosePort_Click(object sender, RoutedEventArgs e)
        {
            string port = portTextBox.Text;
            int portNumber;
            if (int.TryParse(port, out portNumber))
            {
                StopTcpListener();
                KillProcessByPort(port);
                MessageBox.Show($"Порт {port} жабылды.");
            }
            else
            {
                MessageBox.Show("Порт номері дұрыс емес");
            }
        }

        private void RunPowershellScript(string script)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = "powershell.exe";
                    process.StartInfo.Arguments = $"-command \"{script}\"";
                    process.StartInfo.Verb = "runas"; // Запуск от имени администратора
                    process.StartInfo.UseShellExecute = true;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => MessageBox.Show($"Ошибка при выполнении скрипта PowerShell: {ex.Message}"));
            }
        }

        private void StartTcpListener(int port)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);

                // Устанавливаем опцию повторного использования адреса
                listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                listener.Start();
                listeningPort = port;
                cancellationTokenSource = new CancellationTokenSource();
                listener.BeginAcceptTcpClient(AcceptTcpClientCallback, listener);
                Dispatcher.Invoke(() => MessageBox.Show($"Порт {port} открыт и слушает входящие соединения."));
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => MessageBox.Show($"Ошибка при открытии порта {port}: {ex.Message}"));
            }
        }

        private void AcceptTcpClientCallback(IAsyncResult ar)
        {
            if (listener == null || !listener.Server.IsBound)
            {
                return; // Слушатель остановлен, ничего не делаем
            }

            try
            {
                var listener = (TcpListener)ar.AsyncState;
                var client = listener.EndAcceptTcpClient(ar);
                // Можно обработать соединение с клиентом

                // Продолжаем принимать соединения
                listener.BeginAcceptTcpClient(AcceptTcpClientCallback, listener);
            }
            catch (ObjectDisposedException)
            {
                // Игнорируем исключение при закрытии слушателя
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => MessageBox.Show($"Ошибка при обработке входящего соединения: {ex.Message}"));
            }
        }

        private void StopTcpListener()
        {
            try
            {
                if (listener != null)
                {
                    listener.Stop();
                    listener = null;
                    listeningPort = 0;
                    cancellationTokenSource.Cancel();
                    cancellationTokenSource = null;
                    Dispatcher.Invoke(() => MessageBox.Show($"Порт закрыт."));
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => MessageBox.Show($"Ошибка при закрытии порта: {ex.Message}"));
            }
        }

        private void KillProcessByPort(string port)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c netstat -aon | findstr \":{port}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string[] lines = output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in lines)
                    {
                        if (line.Trim().Length > 0)
                        {
                            string[] tokens = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            if (tokens.Length >= 5)
                            {
                                string pid = tokens[4].Trim();
                                KillProcess(pid);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => MessageBox.Show($"Ошибка: {ex.Message}"));
            }
        }

        private void KillProcess(string pid)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c taskkill /pid {pid} /F",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => MessageBox.Show($"Ошибка при завершении процесса: {ex.Message}"));
            }
        }
    }
}
