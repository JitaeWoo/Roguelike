using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    public PlayerData Data = new PlayerData();
    public Transform Transform;

    private DiContainer _diContainer;

    [Inject]
    private void Init(DiContainer di)
    {
        _diContainer = di;
    }

    public GameObject SpawnPlayer(Vector2 position = default)
    {
        GameObject player = _diContainer.InstantiatePrefab(_playerPrefab, position, Quaternion.identity, null);

        Transform = player.transform;

        return player;
    }
}
