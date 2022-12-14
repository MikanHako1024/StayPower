using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseManager<T> where T : new()
{
    private static T instance;

    public static T CreateInstance()
    {
        return new T();
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
