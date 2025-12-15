using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public static class DiContainerExtention
{
    public static void Binding<T>(this DiContainer container, T instance)
    {
        if (instance == null)
        {
            container.Bind<T>().FromComponentInChildren().AsSingle();
        }
        else
        {
            container.Bind<T>().FromInstance(instance).AsSingle();
        }
    }
}
