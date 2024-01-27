using UnityEditor;
using System.IO;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary>
    /// 
    /// 生成配置文件类（资源映射表
    /// 
    /// 1.编译器代码 ：继承自Editor类，只需要在Unity编译器中执行的代码
    /// 2.菜单项 特性 [MeuItem("......")] ：用于修饰需要在Unity编译器中产生菜单按钮的方法
    /// 3.AssetDataBase ：包含了只适用于编译器中操作资源的相关功能。 （在编译器中 找资源、该资源等操作  导入导出包等
    /// 4.StreamingAssets ：Unity特殊目录之一，该目录的文件不会被压缩，适合在移动端读取资源（在PC端还可以写入）。（Unity内部目录
    /// 该 持久化路径 Application.persistentDataPath（安装程序时才产生，发布之后才能用）  路径可以在 运行 时进行读写操作，（Unity外部目录。
    /// </summary>
    public class GenerateResourceConfig : Editor
    {
        [MenuItem("Tools/Resources/Generate ResourceConfig File")]
        public static void GenerateResource()
        {
            // Application.persistentDataPath 支持运行时的读取操作

            // 生成资源配置文件
            // 1. 查找 Resources 目录下所有预制件完整路径
            // t:prefab 类型为prefab
            // 获取所有预制件的 GUID
            string[] resourceFiles = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources" }); // 重载参数（过滤器filter ，文件夹路径） 
            for (int i = 0; i < resourceFiles.Length; i++)
            {
                // GUID 转换为 文件所在路径
                resourceFiles[i] = AssetDatabase.GUIDToAssetPath(resourceFiles[i]);
                // 2. 生成对应关系
                //     名称=路径
                // 获取去除后缀的文件名！
                string fileName = Path.GetFileNameWithoutExtension(resourceFiles[i]);
                // 将"Assets/Resources"  和 ".prefab" 替换为空！ 
                string filePath = resourceFiles[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);
                // resourceFiles[i].LastIndexOf('/');

                resourceFiles[i] = fileName + "=" + filePath;
            }

            // 3. 写入文件   配置映射表
            File.WriteAllLines("Assets/StreamingAssets/JWOA_ConfigMap.txt", resourceFiles);    // StreamingAssets目录 保证移动端可以读取到资源

            // 刷新  （可选  或者 手动刷新 unity文件夹目录不会自动刷新，切换到unity外的页面回来才会刷新
            AssetDatabase.Refresh();
        }
    }
}
