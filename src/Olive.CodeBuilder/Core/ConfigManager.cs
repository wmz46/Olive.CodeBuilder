using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Olive.CodeBuilder.Core
{
    public class ConfigManager
    {
        public string ConnStr { get; set; }
        public string TplPath { get; set; }

        public static ConfigManager GetConfig()
        {

            var tempFile =Path.Combine(Path.GetTempPath(),"Olive.VSIX.Config.json");
            using (FileStream fs = new FileStream(tempFile, FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    var str = sr.ReadToEnd();
                    if (string.IsNullOrEmpty(str))
                    {
                        return new ConfigManager();
                    }

                    try
                    {
                        using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(str)))
                        {

                            DataContractJsonSerializer deseralizer =
                                new DataContractJsonSerializer(typeof(ConfigManager));
                            ConfigManager model = (ConfigManager) deseralizer.ReadObject(ms); // //反序列化ReadObject
                            return model;
                        }
                    }
                    catch
                    {
                        return new ConfigManager();
                    }
                }
            }

        }
        public static void SetConfig(ConfigManager value)
        {
            var tempFile = Path.Combine(Path.GetTempPath(), "Olive.VSIX.Config.json");
            using (FileStream fs = new FileStream(tempFile, FileMode.Open, FileAccess.Write))
            {
                //序列化
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(ConfigManager));
                using (MemoryStream msObj = new MemoryStream())
                {
                    //将序列化之后的Json格式数据写入流中
                    js.WriteObject(msObj, value);
                    msObj.Position = 0;
                    //从0这个位置开始读取流中的数据
                    StreamReader sr = new StreamReader(msObj, Encoding.UTF8);
                    string str = sr.ReadToEnd();
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(str);
                    }
                }
            }
        }
    }
}
