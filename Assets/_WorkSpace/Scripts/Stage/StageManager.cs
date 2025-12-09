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

    private MapGenerator _mapGenerator;
    private ReactiveProperty<int> _monsterCount = new ReactiveProperty<int>();

    private MapNode _root;

    private DiContainer _diContainer;

    [Inject]
    private void Init(MapGenerator map, DiContainer di)
    {
        _mapGenerator = map;
        _diContainer = di;
    }

    private void OnEnable()
    {
        _monsterCount.Where(count => count == 0)
            .Skip(1)
            .Subscribe(v => GoNextScene())
            .AddTo(this);
    }

    private void Start()
    {
        _root = _mapGenerator.MapGenerate();

        _diContainer.InstantiatePrefab(_playerPrefab, _root.LeftNode.RoomRect.center, Quaternion.identity, null);

        while (_monsterCount.Value <= 0)
        {
            MobGenerate(_root, 0);
        }
    }

    private void MobGenerate(MapNode node, int depth)
    {
        if (depth == _mapGenerator.MaxDepth)
        {
            if (node.RoomRect.center == _root.LeftNode.RoomRect.center) return;

            bool success = Random.value < 0.5f;

            if (!success) return;

            Monster monster = Instantiate(_monsterPrefab, node.RoomRect.center, Quaternion.identity).GetComponent<Monster>();
            monster.Hp.Where(hp => hp <= 0)
                .Skip(1)
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

    private void GoNextScene()
    {
        SceneManager.LoadScene("Stage");
    }
}
