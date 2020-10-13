using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Singleton<T> where T : new()
{
    private static T _singleTon = default(T);
    private static object _objectLock = new object();
    public static T Instance
    {
        get
        {
            if (_singleTon == null)
            {
                object obj;
                Monitor.Enter(obj = _objectLock);//加锁防止多线程创建单例
                try
                {
                    if (_singleTon == null)
                    {
                        _singleTon = ((default(T) == null) ? Activator.CreateInstance<T>() : default(T));//创建单例的实例
                    }
                }
                finally
                {
                    Monitor.Exit(obj);
                }
            }
            return _singleTon;
        }
    }

    protected Singleton()
    {
    }
}


public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance = default;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    var go = new GameObject(typeof(T).Name);
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
            Destroy(gameObject);
    }
}