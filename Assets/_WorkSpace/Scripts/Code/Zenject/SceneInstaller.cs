using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private StageManager _stageManager;

    public override void InstallBindings()
    {
        Container.Bind<StageManager>().FromInstance(_stageManager).AsSingle();
    }
}