using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WebApi
{
    class SSH_NewConnection
    {

        //public static string Connection(string host, int port, string pass)
        public static string NewConnection(string host, int port)
        {
            string localPath = @"C:\\temp";
            string user = "video";
            var keyFile = new PrivateKeyFile(@"C:\\cygwin64\\home\\OpenSSH\\.ssh\\id_rsa_new");
            var keyFiles = new[] { keyFile };
            var IsConnect = false;
            //var sftPResult = "";
            Task<string> sftPResult;
            //PasswordAuthenticationMethod passwd = new PasswordAuthenticationMethod(user, pass),
            PrivateKeyAuthenticationMethod method = new PrivateKeyAuthenticationMethod(user, keyFiles);
            ConnectionInfo con = new ConnectionInfo(host, port, user, method);
            //Set up the SSH connection
            using (SshClient client = new SshClient(con))
            {

                //Start the connection
                try
                {
                    client.Connect();
                    Console.WriteLine(client.IsConnected);
                    var output = client.RunCommand("pwd");
                    Console.WriteLine(output.Result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                if (client.IsConnected)
                {
                    IsConnect = client.IsConnected;
                    sftPResult = Sftp1ConnectionAsync(localPath, con);
                    client.Dispose();
                }
                else {
                    return "IsConnect :" + IsConnect + "\n";
                }

            }
            return "IsConnect :" + IsConnect + "\n" + "sFTP content :" + sftPResult.Result.ToString();
        }
        public static async Task<string> Sftp1ConnectionAsync(string localPath, ConnectionInfo con)
        {

            //con.AuthenticationMethods
            using (SftpClient client = new SftpClient(con))
            {
                client.Connect();
                var files = client.ListDirectory("");
                JObject obj = new JObject();
                JObject obj2 = new JObject();
                object jsn;
                List<Task> tasks = new List<Task>();
                //string [,] input = { };
                //object[,] input2 = { };
                List<object> array4 = new List<object>() { }; // инициализация
                //List<string> array5 = new List<string>(); // инициализация
                List<string> pointList = new List<string>() { };
                List<object[,]> pointList2 = new List<object[,]>();
                int i = -1;
                foreach (var file in files)
                {
                    
                    tasks.Add(DownloadFileAsync(file.FullName, localPath + "\\" + file.Name, client));
                    Console.WriteLine(file.LastWriteTime);
                    //Console.WriteLine(file.FullName);
                    string[] input = { file.FullName };
                    //input2 = {{ file.FullName }, { file.Name } };
                    object[,] input2 = { { file.FullName, file.LastWriteTime } };
                    //Console.WriteLine(input2.GetValue(0, 0) + "count"+ i);
                    pointList.AddRange(input);
                    pointList2.Add(input2);
                    
                }
                string[] array = pointList.ToArray();
                for (i = 2; i < pointList.Count; i++)
                {
                    jsn = array.GetValue(i);
                    //object jsn2 = array.GetValue(6);
                    obj[$"{i}"] = $"{jsn}";
                    //obj[$"{2}"] = $"{jsn2}";
                }
                var serialized = JsonConvert.SerializeObject(obj);
                Console.WriteLine(obj.ToString());
                Console.WriteLine(serialized.ToString());
                object array5 = "";

                //string[] a;
                object[] array2 = pointList2.ToArray();
                //Console.WriteLine(array2.GetValue(1));
                string json = $"{array2}";
                JArray a = JArray.Parse("[" + serialized + "]");
                //Console.WriteLine(a.Path + "result");
                //object[,] str_ = { };
                //Console.WriteLine(array2.GetValue(0).ToString());
                i = 0;
                foreach (object[,] str_ in pointList2)
                {

                    //array5 = str_.GetValue(0, i);
                    Console.WriteLine($"{str_.GetValue(0, 0) + ", " + str_.GetValue(0, 1)}");
                    //array4.Add(str_.GetValue(0, 0));
                    obj2[$"{i}"] = $"{str_.GetValue(0, 0) + ", " + str_.GetValue(0, 1)}";
                    i++;
                }
                var serialized2 = JsonConvert.SerializeObject(obj2);
                Console.WriteLine(serialized2.ToString());
                JArray b = JArray.Parse("[" + serialized2 + "]");
                string response1 = b.ToString().Replace(@"{", "").Replace("[", "");
                response1 = response1.Replace(@"}", "").Replace("]", ""); ;
                string result = response1.Replace(" ", String.Empty); //
                //Console.WriteLine(result);
                Console.WriteLine(result + "1");
                //Console.WriteLine(obj2.ToString());
                //Console.WriteLine(array.GetValue(6));
                await Task.WhenAll(tasks).ConfigureAwait(true);
                client.Dispose();
                return result;
                //return array4.ToString();
            }

            static async Task DownloadFileAsync(string source, string destination, SftpClient client)
            {
                //using var saveFile = File.OpenWrite(destination);
                //var task = Task.Factory.FromAsync(client.BeginDownloadFile(source, saveFile), client.EndDownloadFile);
                //await task.ConfigureAwait(true);

            }


            //return await Task.Run(() => sFTPConnection(host, port, pass, localPath, con)).ConfigureAwait(true);
        }

    }
}