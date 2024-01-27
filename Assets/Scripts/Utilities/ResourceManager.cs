using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public class ResourceManager
    {

        private static Dictionary<string, string> configMap;
        // 初始化类的静态数据成员
        // 类被加载时执行一次
        static ResourceManager()
        {
            // 加载文件
            // string fileContent = GetConfigFile();
            string fileContent = GetConfigFile("JWOA_ConfigMap.txt");

            // 解析文件(string ---> Dictionary<string,string>)
            BuildMap(fileContent);
        }

        /// <summary> 获取文件所在目录路径
        /// </summary>
        /// <param name="fileName">资源映射表文件名字</param>
        /// <returns>返回该资源映射表的内容</returns>
        public static string GetConfigFile(string fileName)
        {
            // ResourcesConfig.txt
            // 在大部分安卓平台上
            // string url = "file://"+Application.streamingAssetsPath + "/ConfigMap.txt";
            // string url = "file://"+Application.streamingAssetsPath + "/"+fileName;
        
            string url = "file://" + Application.dataPath + "/StreamingAssets/" + fileName;
            // 如果在编译器下......
            // if(Application.platform == RuntimePlatform.WindowsEditor)    //安卓：RuntimePlatform.Android  IOS：RuntimePlatform.IPhonePlayer
#if UNITY_EDIOTR
            url = "file://"+Application.dataPath + "/StreamingAssets/"+fileName;    //JWOA_ConfigMap.txt";        // Application.dataPath 定位到Assets文件夹
            // 否则如果在Iphone下.....
#elif UNITY_IPHONE
            // url = "file://"+Application.dataPath + "/Raw/JWOA_ConfigMap.txt";        
            url = "file://"+Application.dataPath + "/Raw/" +fileName;   //JWOA_ConfigMap.txt";        
            // 否则如果在android下......  
            // MARKER: file://  前需要加jar:    "!/assets/    注意前面有个 ！
#elif UNITY_ANDROID
            url = "jar:file://"+Application.dataPath + "!/assets/"+fileName;    //JWOA_ConfigMap.txt";  
#endif
            WWW www = new WWW(url);       //加载本地："file:// www"   加载外网则： "http:// www"
            while (true)
            {
                if (www.isDone)
                {
                    Debug.Log("         qqq                 " + www.text);
                    return www.text;
                }
            }
        }


        /// <summary> 将对应的资源映射关系 读取后存到 字典中
        /// </summary>
        /// <param name="fileContent">资源映射表文件内容</param>
        private static void BuildMap(string fileContent)
        {
            configMap = new Dictionary<string, string>();

            // 文件名=路径\r\n文件名=路径       \r\n表回车换行
            // 将对应的映射关系放到字典中！
            // fileContent.Split("=");        // 按照 = 拆分

            #region StringReader 字符串读取器，提供了逐行读取字符串功能 (两种写法
            // StringReader reader = new StringReader(fileContent);
            // reader.Dispose();       // 手动释放 流
            using (StringReader reader = new StringReader(fileContent))
            {
                Debug.Log(" using   " + reader.ReadLine());
                // string line;
                // while ((line = reader.ReadLine()) != null)
                // {
                //     // 解析行数据
                //     string[] keyValue = line.Split('=');
                //     // 文件名：KeyValue[0]  路径：KeyValue[1]
                //     configMap.Add(keyValue[0], keyValue[1]);
                //     Debug.Log($"    键      {keyValue[0]}   值  {keyValue[1]}");
                //     // line = reader.ReadLine();
                // }

                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] keyValue = line.Split('=');
                    // 文件名：KeyValue[0]  路径：KeyValue[1]
                    configMap.Add(keyValue[0], keyValue[1]);

                    line = reader.ReadLine();
                }
            }   // 当程序退出using代码块，将自动调用 reader.Dispose()方法  （不管正常或异常退出！！
            #endregion
        }

        /// <summary> 从字典中读取对应的资源映射（将预制件名字转换为对应的预制件所在路径path
        /// </summary>
        /// <typeparam name="T">泛型对象</typeparam>
        /// <param name="prefabName">预制件名称</param>
        /// <returns>返回该泛型对象即（GameObject 或其他派生自 UnityEngine.Object 的对象）</returns>
        public static T Load<T>(string prefabName) where T : Object
        {
            // prefabName ---> prefabPath
            string prefabPath = configMap[prefabName];
            return Resources.Load<T>(prefabPath);
        }

        // public static UnityEngine.Object Load(string prefabName) 
        // {
        //     // prefabName ---> prefabPath
        //     string prefabPath = configMap[prefabName];
        //     return Resources.Load(prefabPath);
        // }
    }
}
