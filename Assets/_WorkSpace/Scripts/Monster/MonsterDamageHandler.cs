using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using Zenject;

public class MonsterDamageHandler : MonoBehaviour, IDamagable
{

    private bool _isCollision = false;
    private float _cooldown = 1f;
    private bool _isCooldown = false;

    private MonsterData _data;

    [Inject]
    private void Init(MonsterData data)
    {
        _data = data;
    }

    public void TakeDamage(float amount)
    {
        if (_data.Hp.Value <= 0) return;

        _data.Hp.Value -= amount;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        _isCollision = true;

        if (_isCooldown) return;

        Attack(other.gameObject.GetComponent<IDamagable>()).Forget();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        _isCollision = false;
    }


    private async UniTaskVoid Attack(IDamagable other)
    {
        if (_isCooldown) return;
        _isCooldown = true;

        while (_isCollision)
        {
            other.TakeDamage(_data.Damage);

            await UniTask.WaitForSeconds(_cooldown, cancellationToken: this.destroyCancellationToken);
        }
            
        _isCooldown = false;
    }
}
