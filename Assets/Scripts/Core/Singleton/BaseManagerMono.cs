using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mono单例基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseManagerMono<T> : MonoBehaviour where T : MonoBehaviour, new()
{
    private static T instance;

    public static T CreateInstance()
    {
        GameObject obj = new GameObject();
        obj.name = typeof(T).ToString();
        DontDestroyOnLoad(obj); // 过场不摧毁
        return obj.AddComponent<T>();
    }

    public static T GetInstance()
    {
        if (instance == null)
            instance = CreateInstance();
        return instance;
    }

    public static T Inst => GetInstance();
    //public static T Instance => GetInstance();

    public virtual void InitManager()
    {
    }
}
