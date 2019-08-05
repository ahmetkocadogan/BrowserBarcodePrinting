using BrowserBarcodePrinting.Models;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace BrowserBarcodePrinting
{
    public partial class PrintService : ServiceBase
    {
        public PrintService()
        {
            InitializeComponent();
            ReadConfiguration();
        }
        ModelConfiguration _conf;
        private void ReadConfiguration()
        {
            _conf = new ModelConfiguration();
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configurations/configuration.json")))
            {
                _conf = JsonConvert.DeserializeObject<ModelConfiguration>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configurations/configuration.json"), Encoding.Default));
            }
            else
            {
                PrinterSettings settings = new PrinterSettings();
                _conf = new ModelConfiguration() { endpoint_port = 9876, base_address="http://localhost" };
            }
        }

        IDisposable webServer;

        protected override void OnStart(string[] args)
        {
            this.webServer = WebApp.Start<Startup>(url: $"{_conf.base_address}/{_conf.endpoint_port}/");
        }

        protected override void OnStop()
        {
            this.webServer.Dispose();
        }
    }
}
