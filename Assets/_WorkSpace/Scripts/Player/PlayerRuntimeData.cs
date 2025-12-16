using R3;
using UnityEngine;
using Zenject;

public class PlayerRuntimeData : MonoBehaviour
{
    public ReactiveProperty<bool> IsMove = new ReactiveProperty<bool>();
    public ReactiveProperty<bool> IsDash = new ReactiveProperty<bool>();

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

    private PlayerManager _playerManager;
    private Rigidbody2D _rigid;

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
