using UnityEngine;
using Zenject;

public class StageInstaller : MonoInstaller
{
    [SerializeField] private MapGenerator _mapGenerator;

    public override void InstallBindings()
    {
        Container.Binding<MapGenerator>(_mapGenerator);
    }
}