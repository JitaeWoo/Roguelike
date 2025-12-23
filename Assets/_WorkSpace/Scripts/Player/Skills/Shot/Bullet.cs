using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class Bullet : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayerMask;

    public ObjectPool<Bullet> ParentPool;

    private float _damage;
    private Vector2 _velocity;

    private bool _isDestroy;

    private PlayerManager _playerManager;
    private Rigidbody2D _rigid;
    private SpriteRenderer _renderer;

    [Inject]
    private void Init(PlayerManager playerManager, Rigidbody2D rigid, SpriteRenderer renderer)
    {
        _playerManager = playerManager;
        _rigid = rigid;
        _renderer = renderer;
    }

    public void SetData(float damage, float speed, Vector2 direction, Sprite sprite)
    {
        _damage = damage;
        _velocity = speed * direction;
        _isDestroy = false;
        _renderer.sprite = sprite;
    }

    private void FixedUpdate()
    {
        _rigid.velocity = _velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((_targetLayerMask.value & (1 << other.gameObject.layer)) == 0 || _isDestroy) return;

        if(LayerMask.LayerToName(other.gameObject.layer) == "Enemy")
        {
            other.gameObject.GetComponent<IDamagable>().TakeDamage(_playerManager.Data.Damage + _damage);
        }

        ParentPool.Release(this);

        _isDestroy = true;
    }
}
