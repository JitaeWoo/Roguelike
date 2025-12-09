using UnityEngine;
using Zenject;

public class StageInstaller : MonoInstaller
{
    [SerializeField] private MapGenerator _mapGenerator;

    public override void InstallBindings()
    {
        Binding<MapGenerator>(_mapGenerator);
    }

    private void Binding<T>(T instance)
    {
        if (instance == null)
        {
            Container.Bind<T>().FromComponentSibling().AsSingle();
        }
        else
        {
            Container.Bind<T>().FromInstance(instance).AsSingle();
        }
    }
}