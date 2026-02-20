using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator m_serviceLocator;
    private Dictionary<Type, object> m_servise = new();
    public static void Register <T>(T instance)
    {
        m_serviceLocator ??= new ServiceLocator();
        m_serviceLocator.m_servise.Add(typeof(T), instance);
    }
    public static T Resolve<T>()
    {
        if(m_serviceLocator is null)
        {
            throw new NullReferenceException("Servise locator is null");
        }
        return m_serviceLocator.m_servise[typeof(T)] as T;
    }
}
