using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using NetFwTypeLib;
namespace Tam_An_Food_Store_Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string find_IP()
        {
            System.Net.IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label_ip.Text = find_IP();
            addRuleTOFireWall();
            try {
                Process.Start("http:\\localhost");
            }
            catch {
                try
                {
                    Process.Start("chrome.exe", "http:\\localhost");
                    
                }
                catch {
                    try
                    {
                        Process.Start("firefox.exe", "http:\\localhost");
                    }
                    catch { }
                }
                
            }
            
        }
        private void addRuleTOFireWall()
        {
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(
                                            Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            foreach (INetFwRule rule in fwPolicy2.Rules)
            {
                if (rule.Name.CompareTo("All 80") == 0)
                    return; 
            }
            INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(
                                        Type.GetTypeFromProgID("HNetCfg.FWRule"));
            
            firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            firewallRule.Description = "Used to all port 80.";
            firewallRule.Enabled = true;
            firewallRule.InterfaceTypes = "All";
            firewallRule.Name = "All 80";
            firewallRule.Protocol = 6;
            firewallRule.LocalPorts = "80";
            
            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallPolicy.Rules.Add(firewallRule);


        }
    }
}
