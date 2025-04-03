using System.Diagnostics;

namespace desktop_app.config
{
    public class VPN
    {
        public status get()
        {
            try
            {
                var processInfo = new ProcessStartInfo
                {
                    FileName = @"c:\Users\justf\source\repos\yanstribe\server\config\wg_show.bat",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                using (var process = new Process { StartInfo = processInfo })
                {
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string err = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        Console.WriteLine($"wg_show.bat error: {err}");
                        return status.Error;
                    }

                    bool hasRecentHandshake = output.Contains("latest handshake:") &&
                        !output.Contains("latest handshake: 0 seconds ago");
                    bool hasDataTransfer = output.Contains("transfer:") &&
                        !output.Contains("transfer: 0 B received, 0 B sent");

                    if (hasRecentHandshake && hasDataTransfer)
                    {
                        return status.Connected;
                    }
                    else
                    {
                        return status.Disconnected;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return status.Error;
            }
        }
    }
}