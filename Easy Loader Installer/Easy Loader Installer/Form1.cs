using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;


namespace Easy_Loader_Installer
{



    public partial class Form1 : MaterialForm
    {
        private static readonly HttpClient _http = new HttpClient();
        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            //materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.AddFormToManage(this);
            materialProgressBar1.Minimum = 0;
            materialProgressBar1.Maximum = 100;
            materialProgressBar1.Value = 0;

        }


        MaterialSkinManager TManager = MaterialSkinManager.Instance;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme
            (Primary.Orange800, Primary.Orange900, Primary.Orange500, Accent.Orange200, TextShade.WHITE);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.Green800, Primary.Green900, Primary.Green500, Accent.Green200, TextShade.WHITE);
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }

        private void darkThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.Theme = MaterialSkinManager.Themes.DARK;
        }

        private void lightThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.Blue800, Primary.Blue900, Primary.Blue500, Accent.Blue200, TextShade.WHITE);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red800, Primary.Red500, Accent.Red200, TextShade.WHITE);
        }

        private void pinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.Pink900, Primary.Pink800, Primary.Pink500, Accent.Pink200, TextShade.WHITE);
        }

        private void purpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.Purple900, Primary.Purple800, Primary.Purple500, Accent.Purple200, TextShade.WHITE);
        }

        private void deepPurpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.DeepPurple900, Primary.DeepPurple800, Primary.DeepPurple500, Accent.DeepPurple200, TextShade.WHITE);
        }

        private void indigoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.Indigo900, Primary.Indigo800, Primary.Indigo500, Accent.Indigo200, TextShade.WHITE);
        }

        private void cyanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.Cyan900, Primary.Cyan800, Primary.Cyan500, Accent.Cyan200, TextShade.WHITE);
        }

        private void tealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.Teal900, Primary.Teal800, Primary.Teal500, Accent.Teal200, TextShade.WHITE);
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.Yellow900, Primary.Yellow800, Primary.Yellow500, Accent.Yellow200, TextShade.WHITE);
        }

        private void brownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.Brown900, Primary.Brown800, Primary.Brown500, Accent.Amber200, TextShade.WHITE);
        }

        private void greyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TManager.ColorScheme = new ColorScheme(Primary.Grey900, Primary.BlueGrey800, Primary.Grey500, Accent.Amber100, TextShade.WHITE);
        }

        private async void materialButton1_Click(object sender, EventArgs e)
        {
            
        }

        private static readonly Guid FOLDERID_Downloads =
        new Guid("374DE290-123F-4565-9164-39C4925E467B");

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        private static extern int SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
            uint dwFlags,
            IntPtr hToken,
            out IntPtr ppszPath
        );

        private static string GetDownloadsFolder()
        {
            IntPtr pPath;
            int hr = SHGetKnownFolderPath(FOLDERID_Downloads, 0, IntPtr.Zero, out pPath);
            if (hr != 0) Marshal.ThrowExceptionForHR(hr);

            try
            {
                return Marshal.PtrToStringUni(pPath);
            }
            finally
            {
                Marshal.FreeCoTaskMem(pPath);
            }
        }




        private static async Task DownloadFileWithProgressAsync(string url, string filePath, Action<int> reportProgress)
        {
            using (var response = await _http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var total = response.Content.Headers.ContentLength; // may be null

                using (var input = await response.Content.ReadAsStreamAsync())
                using (var output = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] buffer = new byte[81920];
                    long readTotal = 0;
                    int read;

                    while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, read);
                        readTotal += read;

                        if (total.HasValue && total.Value > 0)
                        {
                            int percent = (int)(readTotal * 100L / total.Value);
                            reportProgress(Math.Min(100, Math.Max(0, percent)));
                        }
                    }
                }
            }

            reportProgress(100);
        }

        private void materialProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private async void materialButton2_Click(object sender, EventArgs e)
        {
           
        }

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://launcher-public-service-prod06.ol.epicgames.com/launcher/api/installer/download/EpicGamesLauncherInstaller.msi";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "EpicGamesLauncherInstaller.msi"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox1.Enabled = true;
            }
        }

        private async void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox2.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://cdn.akamai.steamstatic.com/client/installer/SteamSetup.exe";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "SteamSetup.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox2.Enabled = true;
            }
        }

        private async void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox3.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://downloader.battle.net//download/getInstallerForGame?os=win&gameProgram=BATTLENET_APP&version=Live";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "Battle.net-Setup.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox3.Enabled = true;
            }
        }

        private async void pictureBox6_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox6.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://origin-a.akamaihd.net/EA-Desktop-Client-Download/installer-releases/EAappInstaller.exe";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "EAappInstaller.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox6.Enabled = true;
            }
        }

        private async void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox4.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://launcher.escapefromtarkov.com/launcher/download";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "BsgLauncher.14.7.2.4271.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox4.Enabled = true;
            }
        }

        private async void pictureBox8_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox8.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://discord.com/api/downloads/distributions/app/installers/latest?channel=stable&platform=win&arch=x64";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "DiscordSetup.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox8.Enabled = true;
            }
        }

        private async void pictureBox7_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox7.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://ubi.li/4vxt9";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "UbisoftConnectInstaller.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox7.Enabled = true;
            }
        }

        private async void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox5.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://webinstallers.gog-statics.com/download/GOG_Galaxy_2.0.exe?payload=SwKVNvDkmZ6P2FZne7dGlBKPI8vvrvz4gh3QTiHj9PEjj4n8Qi-q5P8Yyj11SY7GePWFY-mjaHPRVn24vcZb90erXnH2wJvtYmV0ijzVXf9ziVBBGHSqggHuhHAHvIPJi6CeoKl-l4sehy7htl8tocTKjSeHcEkbVXbjnG6vu15-VbiDhPVHImShDCsYd3r4Nj8LJocfAJsLXOeU23bGeMf81zhCehg09riacUsd_oEVzrg8zjFis5Y0fjhBueGJQDMXPc7Mqm88ghhmLbfIKGN6_rD48Jyhtc-EI1h7Wu2lo5jWlSQbof-mFiGjKQd1JmTP0JUITHQJp74UFLwfJ997jbzBGmydQv-gZex0caAVXtZiDxsMnTwQv5CbwVVfsyRBsuZ_qHXqsjYmt1ul_AJfKmuJNbd1_iEjdmODEB-VaYW9X35NewQ9rpLXmtCSJ8KJODzgam_H7AXI5VEAsldhWZqDYFjDGbnORUUgIu9YLSXSfDT6FrLvUOKgd48zKLmTvRVi1yDyDwpIVX5NaVZNqVhyJG6Tw3ctevaLHRNJqO0oRPB68xGiKzuGFQPJVTYey5Oxd8Hs-sHv1_tCLSiMLp4xKNkCY2LY7TKWCS-uuVU5ZNyRbXp5Zj9EZy5YCYSxa2jsDncCuYmts7seW3hCFUPg-ZPR0LumIPGEIs2M_jiuEwQtf7PUXtCy_5vXibrBuaLRRE2HtUpJ_B8i1qFAnpIils4vVFwpS2RrRyxA3Ibj2KSftN4n3pqdHBuRbjWqcRJLHfE6w4rlFywAcTO37dJiezDXx4kQQh_C0NslV55740fomaODIcRJ6NzmUBZDNkCZIyGl8cxyVfwjpMQ.";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "GOG_Galaxy_2.0.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox5.Enabled = true;
            }
        }

        private async void pictureBox9_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox9.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://aka.ms/xboxinstaller";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "XboxInstaller.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox9.Enabled = true;
            }
        }

        private async void pictureBox10_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox10.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://gamedownloads.rockstargames.com/public/installer/Rockstar-Games-Launcher.exe?_gl=1*r3fvw*_gcl_au*NDY4MDYyNzE3LjE3NjgxOTMyODc.*_ga*MjA1ODU1NDU1OS4xNzY4MTkzMjQ5*_ga_PJQ2JYZDQC*czE3NjgxOTMyNDkkbzEkZzEkdDE3NjgxOTMyODckajIyJGwwJGgw";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "Rockstar-Games-Launcher.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox10.Enabled = true;
            }
        }

        private async void pictureBox11_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox11.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://us.download.nvidia.com/nvapp/client/11.0.5.420/NVIDIA_app_v11.0.5.420.exe";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "NVIDIA_app_v11.0.5.420.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox11.Enabled = true;
            }
        }

        private async void pictureBox12_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox12.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://limewire.com/d/Ti6Ua#UbsMmbTaFz";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "OperaGXSetup.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox12.Enabled = true;
            }
        }

        private async void pictureBox14_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox14.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://www.cpuid.com/downloads/hwmonitor/hwmonitor_1.61.exe";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "hwmonitor_1.61.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox14.Enabled = true;
            }
        }

        private async void pictureBox15_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox15.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://download.msi.com/uti_exe/vga/MSIAfterburnerSetup.zip?__token__=exp=1768367147~acl=/*~hmac=a966ea70034b0da8ca6a2a0c35bcd434f263d9fab33af7b4019298b76e28d81b";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "MSIAfterburnerSetup.zip"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox15.Enabled = true;
            }
        }

        private async void pictureBox16_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox16.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://rzr.to/synapse-4-pc-download";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "RazerSynapseInstaller.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox16.Enabled = true;
            }
        }

        private async void pictureBox17_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox17.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://cdn-fastly.obsproject.com/downloads/OBS-Studio-32.0.4-Windows-x64-Installer.exe";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "OBS-Studio-32.0.4-Windows-x64-Installer.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox17.Enabled = true;
            }
        }

        private async void pictureBox18_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox18.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://streamlabs.com/streamlabs-desktop/download?sdb=1";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "Streamlabs+Desktop+Setup+1.19.6-safkF1opAbyUrja.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox18.Enabled = true;
            }
        }

        private async void pictureBox19_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox19.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://www.win-rar.com/fileadmin/winrar-versions/winrar/winrar-x64-713.exe";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "winrar-x64-713.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox19.Enabled = true;
            }
        }

        private async void pictureBox20_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox20.Enabled = false;
                materialProgressBar1.Value = 0;

                string url = "https://download01.logi.com/web/ftp/pub/techsupport/gaming/lghub_installer.exe";

                string savePath = System.IO.Path.Combine(
                    GetDownloadsFolder(),
                    "lghub_installer.exe"
                );

                // pass savePath into your download method
                await DownloadFileWithProgressAsync(url, savePath, p => materialProgressBar1.Value = p);

                MessageBox.Show($"Downloaded to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download failed:\n" + ex.Message);
            }
            finally
            {
                pictureBox20.Enabled = true;
            }
        }
    }
}
