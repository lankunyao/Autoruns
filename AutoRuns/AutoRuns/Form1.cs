using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace AutoRuns
{
    public partial class Autoruns : Form
    {
        public const string HKLM_Logon_Run = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        public const string HKLM_Logon_Runonce = "Software\\Microsoft\\Windows\\CurrentVersion\\Runonce";
        public const string HKLM_Logon_Run_64 = "Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run";
        public const string HKLM_Logon_Runonce_64 = "Software\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Runonce";
        public const string HKCU_Logon_Run = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        public const string HKCU_Logon_Runonce = "Software\\Microsoft\\Windows\\CurrentVersion\\Runonce";
        public const string Startup_Path = "C:\\Users\\lankunyao\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup";
        public const string HKLM_IE_BHO = "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Browser Helper Objects";
        public const string HKLM_CLSID = "Software\\Classes\\CLSID";
        public const string HKLM_Services = "System\\CurrentControlSet\\Services";
        public const string HKLM_Drivers = "System\\CurrentControlSet\\Services";
        public const string ScheduledTask_Path = "C:\\Windows\\System32\\Tasks";
        public const string HKLM_KnownDLLs = "System\\CurrentControlSet\\Control\\Session Manager\\KnownDlls";

        public Autoruns()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool flag = true;
            int i = 0;
            string key,name;
            int path_start = 0, path_end = 0;
            string file_path = "";
            ListViewItem listviewitem = new ListViewItem();
            listView1.Items.Clear();
            listView1.Groups.Clear();

            //LOGON
            listView2.Items.Clear();
            listView2.Groups.Clear();
            #region LocalMachine->Run
            ListViewGroup LM_LOGON_RUN = new ListViewGroup();
            LM_LOGON_RUN.Header = "HKLM\\" + HKLM_Logon_Run;
            listView1.Groups.Add(LM_LOGON_RUN);
            listView2.Groups.Add(LM_LOGON_RUN);
            RegistryKey HKLM_LOGON_RUN = Registry.LocalMachine.OpenSubKey(HKLM_Logon_Run);
            for (i=0;i<HKLM_LOGON_RUN.GetValueNames().Length;i++)
            {
                path_start = 0;
                path_end = 0;
                file_path = "";
                key = HKLM_LOGON_RUN.GetValueNames().ElementAt(i);
                name = HKLM_LOGON_RUN.GetValue(key).ToString();
                path_start = name.IndexOf(":") - 1;
                path_end = name.IndexOf(".exe") + 3;
                file_path = name.Substring(path_start, path_end - path_start + 1);
                listviewitem = new ListViewItem(key);
                ListViewItem listviewitem_temp = new ListViewItem(key);
                try
                {
                    FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(file_path);
                    listviewitem.SubItems.Add(fileinfo.FileDescription);
                    listviewitem_temp.SubItems.Add(fileinfo.FileDescription);
                    //listviewitem.SubItems.Add(fileinfo.LegalCopyright);
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                    //listviewitem.SubItems.Add("");
                }
                try
                {
                    X509Certificate cert = X509Certificate.CreateFromSignedFile(file_path);
                    int publisher_start = cert.Subject.IndexOf("O=") + 2;
                    int publisher_end = cert.Subject.IndexOf("L=") - 3;
                    //MessageBox.Show(cert.Subject);
                    X509Certificate2 cert2 = new X509Certificate2(cert.Handle);
                    bool valid = cert2.Verify();
                    if (valid)
                    {
                        listviewitem.SubItems.Add("(Verified)"+cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                    else
                    {
                        listviewitem.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                }
                listviewitem.SubItems.Add(file_path);
                listviewitem_temp.SubItems.Add(file_path);
                LM_LOGON_RUN.Items.Add(listviewitem);
                LM_LOGON_RUN.Items.Add(listviewitem_temp);
                listView2.Items.Add(listviewitem);
                listView1.Items.Add(listviewitem_temp);
                //MessageBox.Show(key + "\n" + name);

            }
            #endregion

            #region LocalMachine->RunOnce
            ListViewGroup LM_LOGON_RUNONCE = new ListViewGroup();
            LM_LOGON_RUNONCE.Header = "HKLM\\" + HKLM_Logon_Runonce;
            listView1.Groups.Add(LM_LOGON_RUNONCE);
            listView2.Groups.Add(LM_LOGON_RUNONCE);
            RegistryKey HKLM_LOGON_RUNONCE = Registry.LocalMachine.OpenSubKey(HKLM_Logon_Runonce);
            for (i = 0; i < HKLM_LOGON_RUNONCE.GetValueNames().Length; i++)
            {
                path_start = 0;
                path_end = 0;
                file_path = "";
                key = HKLM_LOGON_RUNONCE.GetValueNames().ElementAt(i);
                name = HKLM_LOGON_RUNONCE.GetValue(key).ToString();
                path_start = name.IndexOf(":") - 1;
                path_end = name.IndexOf(".exe") + 3;
                file_path = name.Substring(path_start, path_end - path_start + 1);
                listviewitem = new ListViewItem(key);
                ListViewItem listviewitem_temp = new ListViewItem(key);
                try
                {
                    FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(file_path);
                    listviewitem.SubItems.Add(fileinfo.FileDescription);
                    listviewitem_temp.SubItems.Add(fileinfo.FileDescription);
                    //listviewitem.SubItems.Add(fileinfo.LegalCopyright);
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                    //listviewitem.SubItems.Add("");
                }
                try
                {
                    X509Certificate cert = X509Certificate.CreateFromSignedFile(file_path);
                    int publisher_start = cert.Subject.IndexOf("O=") + 2;
                    int publisher_end = cert.Subject.IndexOf("L=") - 3;
                    //MessageBox.Show(cert.Subject);
                    X509Certificate2 cert2 = new X509Certificate2(cert.Handle);
                    bool valid = cert2.Verify();
                    if (valid)
                    {
                        listviewitem.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                    else
                    {
                        listviewitem.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                }
                listviewitem.SubItems.Add(file_path);
                listviewitem_temp.SubItems.Add(file_path);
                LM_LOGON_RUNONCE.Items.Add(listviewitem);
                LM_LOGON_RUNONCE.Items.Add(listviewitem_temp);
                listView2.Items.Add(listviewitem);
                listView1.Items.Add(listviewitem_temp);
                //MessageBox.Show(key + "\n" + name);

            }
            #endregion

            #region LocalMachine->Run64
            ListViewGroup LM_LOGON_RUN_64 = new ListViewGroup();
            LM_LOGON_RUN_64.Header = "HKLM\\" + HKLM_Logon_Run_64;
            listView1.Groups.Add(LM_LOGON_RUN_64);
            listView2.Groups.Add(LM_LOGON_RUN_64);
            RegistryKey HKLM_LOGON_RUN_64 = Registry.LocalMachine.OpenSubKey(HKLM_Logon_Run_64);
            for (i = 0; i < HKLM_LOGON_RUN_64.GetValueNames().Length; i++)
            {
                path_start = 0;
                path_end = 0;
                file_path = "";
                key = HKLM_LOGON_RUN_64.GetValueNames().ElementAt(i);
                name = HKLM_LOGON_RUN_64.GetValue(key).ToString();
                path_start = name.IndexOf(":") - 1;
                path_end = name.IndexOf(".exe") + 3;
                file_path = name.Substring(path_start, path_end - path_start + 1);
                listviewitem = new ListViewItem(key);
                ListViewItem listviewitem_temp = new ListViewItem(key);
                try
                {
                    FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(file_path);
                    listviewitem.SubItems.Add(fileinfo.FileDescription);
                    listviewitem_temp.SubItems.Add(fileinfo.FileDescription);
                    //listviewitem.SubItems.Add(fileinfo.LegalCopyright);
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                    //listviewitem.SubItems.Add("");
                }
                try
                {
                    X509Certificate cert = X509Certificate.CreateFromSignedFile(file_path);
                    int publisher_start = cert.Subject.IndexOf("O=") + 2;
                    int publisher_end = cert.Subject.IndexOf("L=") - 3;
                    //MessageBox.Show(cert.Subject);
                    X509Certificate2 cert2 = new X509Certificate2(cert.Handle);
                    bool valid = cert2.Verify();
                    if (valid)
                    {
                        listviewitem.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                    else
                    {
                        listviewitem.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                }
                listviewitem.SubItems.Add(file_path);
                listviewitem_temp.SubItems.Add(file_path);
                LM_LOGON_RUN_64.Items.Add(listviewitem);
                LM_LOGON_RUN_64.Items.Add(listviewitem_temp);
                listView2.Items.Add(listviewitem);
                listView1.Items.Add(listviewitem_temp);
                //MessageBox.Show(key + "\n" + name);

            }
            #endregion

            #region LocalMachine->RunOnce64
            ListViewGroup LM_LOGON_RUNONCE_64 = new ListViewGroup();
            LM_LOGON_RUNONCE_64.Header = "HKLM\\" + HKLM_Logon_Runonce_64;
            listView1.Groups.Add(LM_LOGON_RUNONCE_64);
            listView2.Groups.Add(LM_LOGON_RUNONCE_64);
            RegistryKey HKLM_LOGON_RUNONCE_64 = Registry.LocalMachine.OpenSubKey(HKLM_Logon_Runonce_64);
            for (i = 0; i < HKLM_LOGON_RUNONCE_64.GetValueNames().Length; i++)
            {
                path_start = 0;
                path_end = 0;
                file_path = "";
                key = HKLM_LOGON_RUNONCE_64.GetValueNames().ElementAt(i);
                name = HKLM_LOGON_RUNONCE_64.GetValue(key).ToString();
                path_start = name.IndexOf(":") - 1;
                path_end = name.IndexOf(".exe") + 3;
                file_path = name.Substring(path_start, path_end - path_start + 1);
                listviewitem = new ListViewItem(key);
                ListViewItem listviewitem_temp = new ListViewItem(key);
                try
                {
                    FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(file_path);
                    listviewitem.SubItems.Add(fileinfo.FileDescription);
                    listviewitem_temp.SubItems.Add(fileinfo.FileDescription);
                    //listviewitem.SubItems.Add(fileinfo.LegalCopyright);
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                    //listviewitem.SubItems.Add("");
                }
                try
                {
                    X509Certificate cert = X509Certificate.CreateFromSignedFile(file_path);
                    int publisher_start = cert.Subject.IndexOf("O=") + 2;
                    int publisher_end = cert.Subject.IndexOf("L=") - 3;
                    //MessageBox.Show(cert.Subject);
                    X509Certificate2 cert2 = new X509Certificate2(cert.Handle);
                    bool valid = cert2.Verify();
                    if (valid)
                    {
                        listviewitem.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                    else
                    {
                        listviewitem.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                }
                listviewitem.SubItems.Add(file_path);
                listviewitem_temp.SubItems.Add(file_path);
                LM_LOGON_RUNONCE_64.Items.Add(listviewitem);
                LM_LOGON_RUNONCE_64.Items.Add(listviewitem_temp);
                listView2.Items.Add(listviewitem);
                listView1.Items.Add(listviewitem_temp);
                //MessageBox.Show(key + "\n" + name);

            }
            #endregion

            #region CurrentUser->Run
            ListViewGroup CU_LOGON_RUN = new ListViewGroup();
            CU_LOGON_RUN.Header = "HKCU\\" + HKCU_Logon_Run;
            listView1.Groups.Add(CU_LOGON_RUN);
            listView2.Groups.Add(CU_LOGON_RUN);
            RegistryKey HKCU_LOGON_RUN = Registry.CurrentUser.OpenSubKey(HKCU_Logon_Run);
            for (i = 0; i < HKCU_LOGON_RUN.GetValueNames().Length; i++)
            {
                path_start = 0;
                path_end = 0;
                file_path = "";
                key = HKCU_LOGON_RUN.GetValueNames().ElementAt(i);
                name = HKCU_LOGON_RUN.GetValue(key).ToString();
                path_start = name.IndexOf(":") - 1;
                path_end = name.IndexOf(".exe") + 3;
                file_path = name.Substring(path_start, path_end - path_start + 1);
                listviewitem = new ListViewItem(key);
                ListViewItem listviewitem_temp = new ListViewItem(key);
                try
                {
                    FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(file_path);
                    listviewitem.SubItems.Add(fileinfo.FileDescription);
                    listviewitem_temp.SubItems.Add(fileinfo.FileDescription);
                    //listviewitem.SubItems.Add(fileinfo.LegalCopyright);
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                    //listviewitem.SubItems.Add("");
                }
                try
                {
                    X509Certificate cert = X509Certificate.CreateFromSignedFile(file_path);
                    //MessageBox.Show(cert.GetEffectiveDateString());
                    int publisher_start = cert.Subject.IndexOf("O=") + 2;
                    int publisher_end = cert.Subject.IndexOf("L=") - 3;
                    //MessageBox.Show(cert.Subject);
                    X509Certificate2 cert2 = new X509Certificate2(cert.Handle);
                    bool valid = cert2.Verify();
                    if (valid)
                    {
                        listviewitem.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                    else
                    {
                        listviewitem.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                }
                listviewitem.SubItems.Add(file_path);
                listviewitem_temp.SubItems.Add(file_path);
                CU_LOGON_RUN.Items.Add(listviewitem);
                CU_LOGON_RUN.Items.Add(listviewitem_temp);
                listView2.Items.Add(listviewitem);
                listView1.Items.Add(listviewitem_temp);
                //MessageBox.Show(key + "\n" + name);

            }
            #endregion

            #region CurrentUser->RunOnce
            ListViewGroup CU_LOGON_RUNONCE = new ListViewGroup();
            CU_LOGON_RUNONCE.Header = "HKCU\\" + HKCU_Logon_Runonce;
            listView1.Groups.Add(CU_LOGON_RUNONCE);
            listView2.Groups.Add(CU_LOGON_RUNONCE);
            RegistryKey HKCU_LOGON_RUNONCE = Registry.CurrentUser.OpenSubKey(HKCU_Logon_Runonce);
            for (i = 0; i < HKCU_LOGON_RUNONCE.GetValueNames().Length; i++)
            {
                path_start = 0;
                path_end = 0;
                file_path = "";
                key = HKCU_LOGON_RUNONCE.GetValueNames().ElementAt(i);
                name = HKCU_LOGON_RUNONCE.GetValue(key).ToString();
                path_start = name.IndexOf(":") - 1;
                path_end = name.IndexOf(".exe") + 3;
                file_path = name.Substring(path_start, path_end - path_start + 1);
                listviewitem = new ListViewItem(key);
                ListViewItem listviewitem_temp = new ListViewItem(key);
                try
                {
                    FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(file_path);
                    listviewitem.SubItems.Add(fileinfo.FileDescription);
                    listviewitem_temp.SubItems.Add(fileinfo.FileDescription);
                    //listviewitem.SubItems.Add(fileinfo.LegalCopyright);
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                    //listviewitem.SubItems.Add("");
                }
                try
                {
                    X509Certificate cert = X509Certificate.CreateFromSignedFile(file_path);
                    //MessageBox.Show(cert.GetEffectiveDateString());
                    int publisher_start = cert.Subject.IndexOf("O=") + 2;
                    int publisher_end = cert.Subject.IndexOf("L=") - 3;
                    //MessageBox.Show(cert.Subject);
                    X509Certificate2 cert2 = new X509Certificate2(cert.Handle);
                    bool valid = cert2.Verify();
                    if (valid)
                    {
                        listviewitem.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                    else
                    {
                        listviewitem.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                }
                listviewitem.SubItems.Add(file_path);
                listviewitem_temp.SubItems.Add(file_path);
                CU_LOGON_RUNONCE.Items.Add(listviewitem);
                CU_LOGON_RUNONCE.Items.Add(listviewitem_temp);
                listView2.Items.Add(listviewitem);
                listView1.Items.Add(listviewitem_temp);
                //MessageBox.Show(key + "\n" + name);

            }
            #endregion

            //MessageBox.Show(Environment.GetEnvironmentVariable("SystemRoot"));

            //SERVICES
            listView3.Items.Clear();
            listView3.Groups.Clear();
            #region LocalMachine->Services
            ListViewGroup LM_SERVICES = new ListViewGroup();
            LM_SERVICES.Header = "HKLM\\" + HKLM_Services;
            listView1.Groups.Add(LM_SERVICES);
            listView3.Groups.Add(LM_SERVICES);
            RegistryKey HKLM_SERVICES = Registry.LocalMachine.OpenSubKey(HKLM_Services);
            for (i = 0; i < HKLM_SERVICES.GetSubKeyNames().Length; i++)
            {
                flag = false;
                //MessageBox.Show("fine");
                path_start = 0;
                path_end = 0;
                file_path = "";
                string middle_key = HKLM_SERVICES.GetSubKeyNames().ElementAt(i);
                if (HKLM_SERVICES.OpenSubKey(middle_key).GetValue("Type") == null)
                    continue;
                int type = (int)HKLM_SERVICES.OpenSubKey(middle_key).GetValue("Type");
                if ((type != 16) && (type != 32) && (type != 272) && (type != 288))
                    continue;
                //if ((string)(HKLM_SERVICES.OpenSubKey(middle_key).GetValue("DisplayName")) == null) continue;
                //key = (string)(HKLM_SERVICES.OpenSubKey(middle_key).GetValue("DisplayName"));
                key = middle_key;
                if ((string)(HKLM_SERVICES.OpenSubKey(middle_key).GetValue("ImagePath")) == null) continue;
                name = (string)(HKLM_SERVICES.OpenSubKey(middle_key).GetValue("ImagePath"));
                //MessageBox.Show(key + "\n" + name);
                path_start = name.IndexOf(":") - 1;
                if(path_start < 0)
                {
                    path_start = 0;
                }
                path_end = name.IndexOf(".exe") + 3;
                if (path_end == 2) continue;
                file_path =name.Substring(path_start, path_end - path_start + 1);
                //MessageBox.Show(file_path);
                if (file_path.Equals("C:\\WINDOWS\\system32\\svchost.exe", StringComparison.OrdinalIgnoreCase))
                {
                    //MessageBox.Show("fine");
                    name = (string)(HKLM_SERVICES.OpenSubKey(middle_key).GetValue("DisplayName"));
                    path_start = name.IndexOf(":") - 1;
                    if (path_start < 0)
                    {
                        path_start = 0;
                    }
                    path_end = name.IndexOf(".exe") + 3;
                    if (path_end == 2)
                    {
                        path_end = name.IndexOf(".dll") + 3;
                    }
                    if (path_end == 2) continue;
                    file_path = name.Substring(path_start, path_end - path_start + 1);
                    file_path = file_path.Replace("@%SystemRoot%", Environment.GetEnvironmentVariable("SystemRoot"));
                    file_path = file_path.Replace("@%systemroot%", Environment.GetEnvironmentVariable("SystemRoot"));
                    file_path = file_path.Replace("@%windir%", Environment.GetEnvironmentVariable("windir"));
                    file_path = file_path.Replace("@%Systemroot%", Environment.GetEnvironmentVariable("SystemRoot"));
                    file_path = file_path.Replace("@", Environment.GetEnvironmentVariable("SystemRoot")+"\\system32");
                }
                listviewitem = new ListViewItem(key);
                ListViewItem listviewitem_temp = new ListViewItem(key);
                try
                {
                    FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(file_path);
                    listviewitem.SubItems.Add(fileinfo.FileDescription);
                    listviewitem_temp.SubItems.Add(fileinfo.FileDescription);
                    //listviewitem.SubItems.Add(fileinfo.LegalCopyright);
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                    //listviewitem.SubItems.Add("");
                }
                try
                {
                    X509Certificate cert = X509Certificate.CreateFromSignedFile(file_path);
                    int publisher_start = cert.Subject.IndexOf("O=") + 2;
                    int publisher_end = cert.Subject.IndexOf("L=") - 3;
                    //MessageBox.Show(cert.Subject);
                    X509Certificate2 cert2 = new X509Certificate2(cert.Handle);
                    if(cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1)=="Microsoft Corporation")
                    {
                        flag = true;
                    }
                    bool valid = cert2.Verify();
                    if (valid)
                    {
                        listviewitem.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                    else
                    {
                        listviewitem.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                }
                listviewitem.SubItems.Add(file_path);
                listviewitem_temp.SubItems.Add(file_path);
                LM_SERVICES.Items.Add(listviewitem);
                LM_SERVICES.Items.Add(listviewitem_temp);
                //MessageBox.Show(listviewitem.Text[2].ToString());
                /*if (string.Equals(listviewitem.Text[2], "(Verified)Microsoft Corporation"))
                {
                    continue;
                }*/
                //if (flag)
                //    continue;
                listView3.Items.Add(listviewitem);
                listView1.Items.Add(listviewitem_temp);
                //MessageBox.Show(key + "\n" + name);

            }
            #endregion

            //DRIVERS
            listView4.Items.Clear();
            listView4.Groups.Clear();
            #region LocalMachine->Drivers
            ListViewGroup LM_DRIVERS = new ListViewGroup();
            LM_DRIVERS.Header = "HKLM\\" + HKLM_Drivers;
            listView1.Groups.Add(LM_DRIVERS);
            listView4.Groups.Add(LM_DRIVERS);
            RegistryKey HKLM_DRIVERS = Registry.LocalMachine.OpenSubKey(HKLM_Drivers);
            for (i = 0; i < HKLM_DRIVERS.GetSubKeyNames().Length; i++)
            {
                flag = false;
                //MessageBox.Show("fine");
                path_start = 0;
                path_end = 0;
                file_path = "";
                string middle_key = HKLM_DRIVERS.GetSubKeyNames().ElementAt(i);
                if (HKLM_SERVICES.OpenSubKey(middle_key).GetValue("Type") == null)
                    continue;
                int type = (int)HKLM_SERVICES.OpenSubKey(middle_key).GetValue("Type");
                if ((type != 1) && (type != 2) && (type != 4) && (type != 8))
                    continue;
                if ((string)(HKLM_DRIVERS.OpenSubKey(middle_key).GetValue("DisplayName")) == null) continue;
                //key = (string)(HKLM_DRIVERS.OpenSubKey(middle_key).GetValue("DisplayName"));
                key = middle_key;
                if ((string)(HKLM_DRIVERS.OpenSubKey(middle_key).GetValue("ImagePath")) == null) continue;
                name = (string)(HKLM_DRIVERS.OpenSubKey(middle_key).GetValue("ImagePath"));
                //MessageBox.Show(key + "\n" + name);
                path_start = name.IndexOf("ystem32") - 1;
                if (path_start < 0)
                {
                    path_start = 0;
                }
                path_end = name.IndexOf(".sys") + 3;
                if (path_end == 2) continue;
                file_path = "C:\\Windows\\" + name.Substring(path_start, path_end - path_start + 1);
                listviewitem = new ListViewItem(key);
                ListViewItem listviewitem_temp = new ListViewItem(key);
                try
                {
                    FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(file_path);
                    listviewitem.SubItems.Add(fileinfo.FileDescription);
                    listviewitem_temp.SubItems.Add(fileinfo.FileDescription);
                    //listviewitem.SubItems.Add(fileinfo.LegalCopyright);
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                    //listviewitem.SubItems.Add("");
                }
                try
                {
                    X509Certificate cert = X509Certificate.CreateFromSignedFile(file_path);
                    int publisher_start = cert.Subject.IndexOf("O=") + 2;
                    int publisher_end = cert.Subject.IndexOf("L=") - 3;
                    //MessageBox.Show(cert.Subject);
                    X509Certificate2 cert2 = new X509Certificate2(cert.Handle);
                    if (cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1) == "Microsoft Corporation")
                    {
                        flag = true;
                    }
                    bool valid = cert2.Verify();
                    if (valid)
                    {
                        listviewitem.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                    else
                    {
                        listviewitem.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                }
                listviewitem.SubItems.Add(file_path);
                listviewitem_temp.SubItems.Add(file_path);
                LM_DRIVERS.Items.Add(listviewitem);
                LM_DRIVERS.Items.Add(listviewitem_temp);
                //MessageBox.Show(listviewitem.Text[2].ToString());
                /*if (string.Equals(listviewitem.Text[2], "(Verified)Microsoft Corporation"))
                {
                    continue;
                }*/
                //if (flag)
                //    continue;
                listView4.Items.Add(listviewitem);
                listView1.Items.Add(listviewitem_temp);
                //MessageBox.Show(key + "\n" + name);

            }
            #endregion

            //string test = "\" www.baidu.com \"";
            //MessageBox.Show(test + "\n" + test.Trim(new char[1] {'\"' }) );

            //SCHEDULED TASK
            listView5.Items.Clear();
            listView5.Groups.Clear();
            #region SCHEDULED TASK
            ListViewGroup SCHEDULED_TASK = new ListViewGroup();
            SCHEDULED_TASK.Header = ScheduledTask_Path;
            listView1.Groups.Add(SCHEDULED_TASK);
            listView5.Groups.Add(SCHEDULED_TASK);
            FileInfo file;
            DirectoryInfo directoryInfo = new DirectoryInfo(ScheduledTask_Path);
            for (i = 0; i < Directory.GetFiles(ScheduledTask_Path).Length; i++)
            {
                //MessageBox.Show("fine");
                path_start = 0;
                path_end = 0;
                file_path = "";

                file = directoryInfo.GetFiles().ElementAt(i);
                FileStream filestream = file.OpenRead();
                long filelength = filestream.Length;
                byte[] content = new byte[2048];
                int j = 0;
                string context = "";

                do
                {
                    filestream.Read(content, 0, 2048);
                    foreach (byte b in content)
                    {
                        if (b != 0)
                            context += (char)b;
                    }
                    j += 2048;
                } while (j < filelength);

                file_path = context.Substring(context.IndexOf("<Command>") + 9, context.IndexOf("</Command>") - context.IndexOf("<Command>") - 9);
                file_path = file_path.Trim(new char[1] { '\"'});
                file_path = file_path.Replace("%localappdata%", Environment.GetEnvironmentVariable("localappdata"));
                //MessageBox.Show(file_path);
                
                listviewitem = new ListViewItem(file.Name);
                ListViewItem listviewitem_temp = new ListViewItem(file.Name);
                try
                {
                    FileVersionInfo fileinfo = FileVersionInfo.GetVersionInfo(file_path);
                    listviewitem.SubItems.Add(fileinfo.FileDescription);
                    listviewitem_temp.SubItems.Add(fileinfo.FileDescription);
                    //listviewitem.SubItems.Add(fileinfo.LegalCopyright);
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                    //listviewitem.SubItems.Add("");
                }
                try
                {
                    X509Certificate cert = X509Certificate.CreateFromSignedFile(file_path);
                    int publisher_start = cert.Subject.IndexOf("O=") + 2;
                    int publisher_end = cert.Subject.IndexOf("L=") - 3;
                    //MessageBox.Show(cert.Subject);
                    X509Certificate2 cert2 = new X509Certificate2(cert.Handle);
                    if (cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1) == "Microsoft Corporation")
                    {
                        flag = true;
                    }
                    bool valid = cert2.Verify();
                    if (valid)
                    {
                        listviewitem.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                    else
                    {
                        listviewitem.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                        listviewitem_temp.SubItems.Add("(Not Verified)" + cert.Subject.Substring(publisher_start, publisher_end - publisher_start + 1));
                    }
                }
                catch
                {
                    listviewitem.SubItems.Add("");
                    listviewitem_temp.SubItems.Add("");
                }
                listviewitem.SubItems.Add(file_path);
                listviewitem_temp.SubItems.Add(file_path);
                LM_DRIVERS.Items.Add(listviewitem);
                LM_DRIVERS.Items.Add(listviewitem_temp);
                //MessageBox.Show(listviewitem.Text[2].ToString());
                /*if (string.Equals(listviewitem.Text[2], "(Verified)Microsoft Corporation"))
                {
                    continue;
                }*/
                //if (flag)
                //    continue;
                listView5.Items.Add(listviewitem);
                listView1.Items.Add(listviewitem_temp);
                //MessageBox.Show(key + "\n" + name);

            }
            #endregion

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void scheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void hideWindowsEntrysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem listViewItem = new ListViewItem();
            //for(int i = 0, i < )
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            
        }
    }
}
