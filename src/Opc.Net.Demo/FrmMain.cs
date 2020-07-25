using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Opc.Net;

namespace Opc.Net.Demo
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        string barcode;


        private OpcManager opcManager;
        private System.Timers.Timer opcTimer;
        private int[] pErrors;
        private Dictionary<int, object> items;
        /// <summary>
        /// 写入采煤机位置数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            items = new Dictionary<int, object>();
            items.Add(2, textBox2.Text);
            //opcManager.Write(items, 1,  pErrors);
            opcManager.Write(items, 1, out pErrors);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            opcManager = new OpcManager(AppDomain.CurrentDomain.BaseDirectory + "\\Opc.config.xml");
            opcManager.OnReadCompleted += new EventHandler<OpcDaCustomAsyncEventArgs>(opcManager_OnReadCompleted);

            opcTimer = new System.Timers.Timer()
            {
                Interval = 100,
                AutoReset = true,
                Enabled = true
            };
            opcTimer.Elapsed += new ElapsedEventHandler(opcTimer_Elapsed);
        }

        void opcTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            opcManager.Read();
        }

        void opcManager_OnReadCompleted(object sender, OpcDaCustomAsyncEventArgs e)
        {
            //Invoke((ThreadStart)(() =>
            //{

            //    if (OpcHelper.ShowValue(e, 3) != null)
            //    {
            //        textBox1.Text = OpcHelper.ShowValue(e, 3).ToString();
            //    }
            //}));



            Invoke((ThreadStart)(() =>
            {
                this.textBox1.Clear();
                if (OpcHelper.ShowValue(e, 1) != null)
                {
                    short[] b = (short[])OpcHelper.ShowValue(e, 1);
                    foreach (var item in b)
                    {
                        this.textBox1.AppendText(item.ToString() + ",");
                    }
                }
            }));


            barcode = this.textBox1.Text;


            //string sql = $"INSERT into barcode(barcode,Var01,Var02,Var03,Var04,Var05,Var06,Var07,Var08,CreateTime,IsPrinted) values('{barcode}','{Var01}','{Var02}','{Var03}','{Var04}','{Var05}','{Var06}','{Var07}','{Var08}',0,0)";


            string sql = $"INSERT into barcode(barcode) values('{barcode}')";

            succeed = SqlSugarHelper.DB.Ado.ExecuteCommand(sql);


        }
        int succeed = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {




            //this.textBox1.AppendText(item.ToString() + ",");


            //Invoke((ThreadStart)(() =>
            //{
            //    this.textBox1.Clear();
            //    if (OpcHelper.ShowValue(e, 1) != null)
            //    {
            //        short[] b = (short[])OpcHelper.ShowValue(e, 1);
            //        foreach (var item in b)
            //        {
            //            this.textBox1.AppendText(item.ToString() + ",");
            //        }
            //    }
            //}));

            //barcode = this.textBox1.Text;


            string a = barcode;

            string sql = $"INSERT into barcode(barcode) values('{barcode}')";



            succeed = SqlSugarHelper.DB.Ado.ExecuteCommand(sql);
        }
    }
}
