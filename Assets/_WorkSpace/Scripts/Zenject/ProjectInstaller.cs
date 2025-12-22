using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameObject _playerManagerPrefab;
    public override void InstallBindings()
    {
        Container.Bind<PlayerManager>().AsSingle();
        Container.Bind<PlayerManager>().FromComponentInNewPrefab(_playerManagerPrefab).AsSingle();
    }
}