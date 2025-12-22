using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameObject _playerManagerPrefab;
    [SerializeField] private GameObject _skillManagerPrefab;

    public override void InstallBindings()
    {
        Container.Bind<PlayerManager>().FromComponentInNewPrefab(_playerManagerPrefab).AsSingle();
        Container.Bind<SkillManager>().FromComponentInNewPrefab(_skillManagerPrefab).AsSingle();
    }
}