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

    public Dash Dash = new Dash();

    private void Awake()
    {
        if (_rigid == null)
        {
            _rigid = GetComponent<Rigidbody2D>();
        }
    }
}
