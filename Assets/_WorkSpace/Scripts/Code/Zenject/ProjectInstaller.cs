using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameObject _playerManagerPrefab;
    [SerializeField] private GameObject _skillManagerPrefab;
    [SerializeField] private GameObject _dataManagerPrefab;
    [SerializeField] private GameObject _gameManagerPrefab;

    public override void InstallBindings()
    {
        Container.Bind<PlayerManager>().FromComponentInNewPrefab(_playerManagerPrefab).AsSingle();
        Container.Bind<SkillManager>().FromComponentInNewPrefab(_skillManagerPrefab).AsSingle();
        Container.Bind<DataManager>().FromComponentInNewPrefab(_dataManagerPrefab).AsSingle();
        Container.Bind<GameManager>().FromComponentInNewPrefab(_gameManagerPrefab).AsSingle();
    }
}