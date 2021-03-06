﻿using System;
using System.Windows.Forms;

public class Singleton<T> where T : class, new()
{
    private static T m_Instance = null;

    public static T instance
    {
        get
        {
            if(m_Instance == null)
            {
                m_Instance = new T();
            }

            return m_Instance;
        }
    }
}

public class SingletonForm<T> : Form where T : class, new()
{
    private static T m_Instance = null;

    public static T instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new T();
            }

            return m_Instance;
        }
    }
}