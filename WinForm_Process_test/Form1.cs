using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
//http://fecbob.pixnet.net/blog/post/38088805-c%23-%E5%95%9F%E5%8B%95%E5%A4%96%E9%83%A8%E7%A8%8B%E5%BC%8F
/*
C# 啟動外部程式的幾種方法：
1. 啟動外部程式，不等待其退出。
2. 啟動外部程式，等待其退出。
3. 啟動外部程式，無限等待其退出。
4. 啟動外部程式，通過事件監視其退出。

// using System.Diagnostics;
private string appName = "calc.exe";

/// <summary>
/// 1. 啟動外部程式，不等待其退出
/// </summary>
private void button1_Click(object sender, EventArgs e)
{
Process.Start(appName);
MessageBox.Show(String.Format("外部程式 {0} 啟動完成！", this.appName), this.Text,
MessageBoxButtons.OK, MessageBoxIcon.Information);
}

/// <summary>
/// 2. 啟動外部程式，等待其退出
/// </summary>
private void button2_Click(object sender, EventArgs e)
{
try
{
Process proc = Process.Start(appName);
if (proc != null)
{
proc.WaitForExit(3000);
if (proc.HasExited)
MessageBox.Show(String.Format("外部程式 {0} 已經退出！", this.appName), this.Text,
MessageBoxButtons.OK, MessageBoxIcon.Information);
else
{
// 如果外部程式沒有結束運行則強行終止之。
proc.Kill();
MessageBox.Show(String.Format("外部程式 {0} 被強行終止！", this.appName), this.Text,
MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
}
}
}
catch (ArgumentException ex)
{
MessageBox.Show(ex.Message, this.Text,
MessageBoxButtons.OK, MessageBoxIcon.Error);
}
}


/// <summary>
/// 3. 啟動外部程式，無限等待其退出
/// </summary>
private void button3_Click(object sender, EventArgs e)
{
try
{
Process proc = Process.Start(appName);
if (proc != null)
{
proc.WaitForExit();
MessageBox.Show(String.Format("外部程式 {0} 已經退出！", this.appName), this.Text,
MessageBoxButtons.OK, MessageBoxIcon.Information);
}
}
catch (ArgumentException ex)
{
MessageBox.Show(ex.Message, this.Text,
MessageBoxButtons.OK, MessageBoxIcon.Error);
}
}


/// <summary>
/// 4. 啟動外部程式，通過事件監視其退出
/// </summary>
private void button4_Click(object sender, EventArgs e)
{
try
{
// 啟動外部程式
Process proc = Process.Start(appName);
if (proc != null)
{
// 監視進程退出
proc.EnableRaisingEvents = true;
// 指定退出事件方法
proc.Exited += new EventHandler(proc_Exited);
}
}
catch (ArgumentException ex)
{
MessageBox.Show(ex.Message, this.Text,
MessageBoxButtons.OK, MessageBoxIcon.Error);
}
}

/// <summary>
/// 啟動外部程式退出事件
/// </summary>
void proc_Exited(object sender, EventArgs e)
{
MessageBox.Show(String.Format("外部程式 {0} 已經退出！", this.appName), this.Text,
MessageBoxButtons.OK, MessageBoxIcon.Information);
}
*/
namespace WinForm_Process_test
{
    public partial class Form1 : Form
    {
        private string appName = "123.bat";
        private Process m_pro;
        public int m_id;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_pro == null)
            {
                /*
                ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe","/c c:\\123.bat");
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                */
                m_pro = Process.Start("c:\\123.bat");
                m_id = m_pro.Id;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (m_pro != null)
            {
                if (!m_pro.HasExited)//取得程式是否已執行完成
                {
                    try
                    {
                        Process[] javaProcList = Process.GetProcessesByName("ping");//要先關閉BAT內的執行程式 否則無法關閉BAT 2015/12/02
                        foreach (Process javaProc in javaProcList)
                        {
                            javaProc.Kill();
                            javaProc.Close();
                            javaProc.Dispose();
                        }
                        m_pro.Kill();
                        m_pro.Close();
                        m_pro.Dispose();
                        m_pro = null;
                    }
                    catch (Exception exp)
                    {
                    }
                }
            }
        }
    }
}
