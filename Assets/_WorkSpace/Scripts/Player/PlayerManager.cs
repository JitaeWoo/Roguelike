using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    public PlayerData Data = new PlayerData();
    private Transform _transform;
    public Vector3 Position => _transform.position;

    private DiContainer _diContainer;

    [Inject]
    private void Init(DiContainer di)
    {
        _diContainer = di;
    }

    public GameObject SpawnPlayer(Vector2 position = default)
    {
        GameObject player = _diContainer.InstantiatePrefab(_playerPrefab, position, Quaternion.identity, null);
        player.transform.SetParent(null);
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetActiveScene());

        _transform = player.transform;

        return player;
    }
}
