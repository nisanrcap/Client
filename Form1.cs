using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;


namespace CLIENT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        SimpleTcpClient client;
        List<string> ip = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            richtxtReceivedData.Enabled = false;
            richtxtSendData.Enabled = false;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //if (txtIP.Text.Length==15)
            //{
            //    client.Connect(txtIP.Text, Convert.ToInt32(txtPORT.Text));
            //    btnConnect.Enabled = false;
            //    ip.Add(txtIP.Text);
            //    Baglanti();
            //}
            //else
            //{
            //    try
            //    {
            //        client.Connect("192.168.1.75", 23);
            //        btnConnect.Enabled = false;
            //        ip.Add("192.168.1.75");
            //        Baglanti();
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("Server Bulunamadı");
            //        btnConnect.Enabled = true;
            //        BaglantiSonrasiRenk();
            //    }
            //}
            client.Connect(txtIP.Text, Convert.ToInt32(txtPORT.Text));
        }

        private void mesajGonderme()
        {
            try
            {
                client.WriteLineAndGetReply(txtSend.Text, TimeSpan.FromSeconds(3));
                richtxtSendData.Text += " -- " + txtSend.Text + "\n";
            }
            catch (Exception)
            {
                MessageBox.Show("Bağlantı Kesildi");
                btnConnect.Enabled = true;
                BaglantiSonrasiRenk();

            }
        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            int timeoutMilliseconds = 5000; // 5 saniye

            if (txtIP.Text.Length == 15)
            {
                using (TcpClient client = new TcpClient())
                {
                    IAsyncResult result = client.BeginConnect(txtIP.Text, Convert.ToInt32(txtPORT.Text), null, null);
                    bool success = result.AsyncWaitHandle.WaitOne(timeoutMilliseconds);

                    if (!success)
                    {
                        MessageBox.Show("Sunucuya bağlantı sağlanamadı. Tekrar giriş yapmayı deneyiniz.");
                        btnConnect.Enabled = true;
                        txtSend.Clear();
                        BaglantiSonrasiRenk();
                    }
                    else
                    {
                        // Bağlantı başarılı.
                        mesajGonderme();
                    }
                }
            }
            else
            {
                try
                {
                    if (true)
                    {
                        using (TcpClient client = new TcpClient())
                        {
                            IAsyncResult result = client.BeginConnect("192.168.1.75", 23, null, null);
                            bool success = result.AsyncWaitHandle.WaitOne(timeoutMilliseconds);

                            if (!success)
                            {
                                MessageBox.Show("Sunucuya bağlantı sağlanamadı. Tekrar giriş yapmayı deneyiniz.");
                                btnConnect.Enabled = true;
                                txtSend.Clear();
                                BaglantiSonrasiRenk();
                            }
                            else
                            {
                                // Bağlantı başarılı.
                                mesajGonderme();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Server Bulunamadı");
                    btnConnect.Enabled = true;
                    BaglantiSonrasiRenk();
                }
            }

        }


        private void BaglantiSonrasiRenk()
        {
            if (ip.Contains("192.168.1.75"))
            {
                txtSaat.ForeColor = Color.Red;
                txtSaat.Text += Environment.NewLine + DateTime.Now.ToLongTimeString() + " -> " + "Bağlantı Kesildi.";
                ip.Remove("192.168.1.75");
            }
            else if (ip.Contains(txtIP.Text))
            {
                txtSaat.ForeColor = Color.Red;
                txtSaat.Text += Environment.NewLine + DateTime.Now.ToLongTimeString() + " -> " + "Bağlantı Kesildi.";
                ip.Remove(txtIP.Text);
            }
        }

        private void Baglanti()
        {
            txtSaat.Clear();
            for (int i = 0; i < ip.Count; i++)
            {
                txtSaat.ForeColor = Color.Green;
                txtSaat.Text += DateTime.Now.ToLongTimeString() + " -> " + ip[i];
            }
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
