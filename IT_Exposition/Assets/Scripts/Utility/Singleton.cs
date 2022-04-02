using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Singleton<T> where T : class
{
    private static T _instance = null;
    private static object _syncObj = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)
                CreateInstance();

            return _instance;
        }
    }

    private static void CreateInstance()
    {
        lock (_syncObj)
        {
            if (_instance == null)
            {
                var t = typeof(T);

                ConstructorInfo[] ctors = t.GetConstructors();
                if (ctors.Length > 0)
                {
                    throw new InvalidOperationException(string.Format("{0} has at least one accesible ctor making it impossible to enforce singleton behaviour", t.Name));
                }

                _instance = (T)Activator.CreateInstance(t, true);
            }
        }
    }
}
