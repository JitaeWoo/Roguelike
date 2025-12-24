using Cinemachine;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _monsterPrefab;
    [SerializeField] private CinemachineVirtualCamera _playerCamera;

    private MapGenerator _mapGenerator;
    private ReactiveProperty<int> _monsterCount = new ReactiveProperty<int>();

    private MapNode _root;

    private DiContainer _diContainer;
    private PlayerManager _playerManager;
    private GameManager _gameManager;

    [Inject]
    private void Init(MapGenerator map, DiContainer di, PlayerManager playerManager, GameManager gameManager)
    {
        _mapGenerator = map;
        _playerManager = playerManager;
        _diContainer = di;
        _gameManager = gameManager;
    }

    private void Start()
    {
        _root = _mapGenerator.MapGenerate();
        
        GameObject player = _playerManager.SpawnPlayer(_root.LeftNode.RoomRect.center);
        _playerCamera.Follow = player.transform;

        while (_monsterCount.Value <= 0)
        {
            MobGenerate(_root, 0);
        }

        _monsterCount.Where(count => count == 0)
            .Subscribe(v => StageClear())
            .AddTo(this);

        _playerManager.Data.Skill1.Value = $"Shot{_gameManager.CurStage}";
    }

    private void MobGenerate(MapNode node, int depth)
    {
        if (depth == _mapGenerator.MaxDepth)
        {
            if (node.RoomRect.center == _root.LeftNode.RoomRect.center) return;

            bool success = Random.value < 0.5f;

            if (!success) return;

            Monster monster = _diContainer.InstantiatePrefab(_monsterPrefab, node.RoomRect.center, Quaternion.identity, null).GetComponent<Monster>();
            monster.Hp.Where(hp => hp <= 0)
                .Subscribe(v => _monsterCount.Value--)
                .AddTo(monster);

            _monsterCount.Value++;
        }
        else
        {
            MobGenerate(node.LeftNode, depth + 1);
            MobGenerate(node.RightNode, depth + 1);
        }
    }

    private void StageClear()
    {
        _gameManager.StageClear();
    }
}
