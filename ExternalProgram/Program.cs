using System;
using System.Diagnostics;

namespace ExternalProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = RunProcess("cmd.exe", "/c ipconfig");
            Console.WriteLine(result);

            Console.WriteLine("==================================");
            ExternalProcess externalProcess = new ExternalProcess();
            externalProcess.SetStartInfo("cmd.exe");

            string result2 = externalProcess.GetResult("/c ipconfig");
            Console.WriteLine(result2);
            string result3 = externalProcess.GetResult("/c dir");
            Console.WriteLine(result3);

            //////////////////////////////////////////////////////////
            // 외부 프로그램으로 parameter 전달이 아닌 standard input 전달 시
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @".\SIGNAGE.EXE";
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            process.StandardInput.WriteLine("ABCDEFG");
            process.StandardInput.Flush();
            process.Close();

        }

        static private string RunProcess(String FileName, String Args)
        {
            Process p = new Process();

            p.StartInfo.FileName = FileName;
            p.StartInfo.Arguments = Args;
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            p.Start();
            string result = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            return result;
        }
    }

    public class ExternalProcess
    {
        ProcessStartInfo startInfo;
        public void SetStartInfo(string aPath)
        {
            startInfo = new ProcessStartInfo();
            startInfo.FileName = aPath;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
        }
        public string GetResult(string aInput)
        {
            string result = string.Empty;
            try
            {
                using (Process process = new Process())
                {
                    startInfo.Arguments = aInput;
                    process.StartInfo = startInfo;
                    process.Start();

                    result = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }
    }
}
