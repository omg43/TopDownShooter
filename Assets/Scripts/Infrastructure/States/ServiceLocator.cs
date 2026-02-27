using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator m_serviceLocator;

    private Dictionary<Type, object> m_servises = new();

    public static void Register <T>(T instance)
    {
        m_serviceLocator ??= new ServiceLocator();
        m_serviceLocator.m_servises.Add(typeof(T), instance);
    }
    public static T Resolve<T>()
            where T : class
    {
        if (m_serviceLocator == null)
        {
            throw new NullReferenceException("ServiceLocator is null");
        }

        return m_serviceLocator.m_servises[typeof(T)] as T;
    }
}
