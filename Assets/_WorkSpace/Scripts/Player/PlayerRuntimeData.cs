using R3;
using UnityEngine;

public class PlayerRuntimeData : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigid;

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

    public float DashCooldown = 2f;
    public bool IsDashOnCooldown;
    public bool CanDash = true;
    private Subject<Unit> _dashEvent = new Subject<Unit>();
    public Observable<Unit> DashEvent { get => _dashEvent.AsObservable(); }
    public void DashTrigger()
    {
        _dashEvent.OnNext(Unit.Default);
    }

    private void Awake()
    {
        if (_rigid == null)
        {
            _rigid = GetComponent<Rigidbody2D>();
        }
    }
}
