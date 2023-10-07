using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace SiofriaSoundboard
{
    class InputManager
    {
        private string inputFilePath;

        public delegate void OnInputType(int virtualKeyCode);
        public OnInputType OnInput;

        private StreamWriter outputStream;

        private const string inputProcessName = "SiofriaKeyboardHookEngine";

        private System.Windows.Forms.Timer inputTimer;
        private FileInfo inputFileInfo;
        private DateTime lastWriteTime;

        public InputManager(string inputFilePath, System.Windows.Forms.Timer inputTimer)
        {
            this.inputFilePath = inputFilePath;
            this.inputTimer = inputTimer;
            outputStream = new StreamWriter("hook_engine_log.txt");

            try
            {
                StopAllInputProcesses();
            }
            catch (Exception ex) { Log.Write(ex); }

            SetupFileWatching();
        }

        public void StartInputProcess()
        {
            Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = Path.Combine(Environment.CurrentDirectory, inputProcessName + ".exe");
            Console.WriteLine(process.StartInfo.FileName);

            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = false;
            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                if (!String.IsNullOrEmpty(e.Data))
                {
                    outputStream.WriteLine(e.Data);
                }
            });

            process.Start();
        }

        public bool InputProcessRunning()
        {
            Process[] pname = Process.GetProcessesByName(inputProcessName);
            return pname.Length != 0;
        }

        public void StopAllInputProcesses()
        {
            foreach (var process in Process.GetProcessesByName(inputProcessName))
                process.Kill();
        }

        private void SetupFileWatching()
        {
            inputFileInfo = new FileInfo(inputFilePath);
            lastWriteTime = inputFileInfo.LastWriteTime;
            inputTimer.Tick += inputTimer_tick;
        }

        private void inputTimer_tick(object sender, EventArgs e)
        {
            inputFileInfo.Refresh();
            if(inputFileInfo.LastWriteTime <= lastWriteTime.AddMilliseconds(100))
            {
                return;
            }
            
            lastWriteTime = inputFileInfo.LastWriteTime;
            try
            {
                using (TextReader reader = File.OpenText(inputFilePath))
                {
                    int virtualKeyCode = int.Parse(reader.ReadLine());
                    OnInput.Invoke(virtualKeyCode);
                }
            }
            catch (Exception err )
            {
                MessageBox.Show("Failed to parse value in file " + err.Message);
                Log.Write(err);
            }
        }
    }
}
