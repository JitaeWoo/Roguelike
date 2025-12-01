using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using R3;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    private InputAction _moveAction;

    private Vector2 _moveDir;

    private void Awake()
    {
        if(_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
        _moveAction = _playerInput.actions["Move"];
    }

    private void OnEnable()
    {
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
    }

    private void Start()
    {
        // 입력이 변화할 때마다 _moveDir을 기록한다.
        Observable.FromEvent<InputAction.CallbackContext>(
            f => _moveAction.performed += f,
            f => _moveAction.performed -= f)
            .Select(ctx => ctx.ReadValue<Vector2>())
            .Subscribe(v => _moveDir = v)
            .AddTo(this);

        // 입력이 끊기면 _moveDir을 0으로 만든다.
        Observable<InputAction.CallbackContext> cancelStream = Observable.FromEvent<InputAction.CallbackContext>(
            f => _moveAction.canceled += f,
            f => _moveAction.canceled -= f);
        cancelStream.Subscribe(_ => _moveDir = Vector2.zero)
            .AddTo(this);

        // 입력이 지속되는 동안 _moveDir 방향으로 움직인다.
        Observable.FromEvent<InputAction.CallbackContext>(
            f => _moveAction.started += f,
            f => _moveAction.started -= f)
            .SelectMany(_ => 
                Observable.EveryUpdate()
                .Select(_ => _moveDir)
                .TakeUntil(cancelStream))
            .Where(moveDir => moveDir != Vector2.zero)
            .Subscribe(moveDir => transform.position += (Vector3)moveDir * Time.deltaTime)
            .AddTo(this);
    }
}
