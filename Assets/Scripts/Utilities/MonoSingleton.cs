using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    /// <summary> 脚本单例类
    /// 
    /// 解决方案：定义MonoSingleton类
    /// 1.适用性：场景中存在唯一的对象，即可让该对象继承当前类。
    /// 2.如何使用：
    ///     --在继承时必须传递子类类型。
    ///     --在任意脚本周期中，通过子类类型访问Instance属性。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> //Component    // Object  // class
    {
        // T 表示子类类型
        // public static T Instance { get; private set; }

        // 按需加载
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    // 在场景中根据类型查找引用
                    instance = FindObjectOfType<T>();   //where T : Object
                    if (instance == null)
                    {
                        // 创建脚本对象(会立即执行Awake！！)
                        instance = new GameObject("Singleton of " + typeof(T)).AddComponent<T>();       //where T : Component
                    }
                    else
                    {
                        // MARKER：  需表明父子类型关系才可调用  即where T : MonoSingleton<T>
                        instance.Init();
                    }
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this as T;       //where T : class
                Init();
            }
        }

        public virtual void Init()
        {

        }
    }
}
