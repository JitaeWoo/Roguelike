using UnityEngine;
using Zenject;

public class PlayerRuntimeData : MonoBehaviour
{
    private PlayerManager _playerManager;
    private Rigidbody2D _rigid;

    public Vector2 MoveDir;
    public bool HasMoveInput;
    public Vector2 Velocity
    {
        get => _rigid.velocity;
        set
        {
            if (_rigid.velocity == value) return;

            _rigid.velocity = value;
        }
    }

    public Dash Dash = new Dash();
    public Attack Attack;

    [Inject]
    private void Init(PlayerManager manager, Rigidbody2D rigid)
    {
        _playerManager = manager;
        _rigid = rigid;
    }

    private void Awake()
    {
        Attack = new Attack(_playerManager, transform);
    }
}
