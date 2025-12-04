using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : Skill
{
    private Collider2D[] _targetColliders = new Collider2D[10];

    public Attack(Transform player) : base(player)
    {
        Cooldown = 0.8f;
    }

    protected override void ActivateSkill()
    {
        int count = Physics2D.OverlapCircleNonAlloc(_player.position, 1.5f, _targetColliders, LayerMask.GetMask("Enemy"));

        if (count > 0)
        {
            for(int i = 0; i < count; i++)
            {
                Vector2 dir = _targetColliders[i].transform.position - _player.position;
                Vector2 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _player.position;

                if (Vector2.Angle(dir, mouse) > 90) continue;

                _targetColliders[i].GetComponent<IDamagable>().TakeDamage(Manager.Player.Data.Damage);
            }
        }
    }
}
