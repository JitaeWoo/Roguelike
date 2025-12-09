using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Attack : Skill
{
    private Collider2D[] _targetColliders = new Collider2D[10];

    public Attack(PlayerManager manager, Transform player) : base(manager, player)
    {
        Cooldown = 0.8f;
    }

    protected override void ActivateSkill()
    {
        int count = Physics2D.OverlapCircleNonAlloc(Player.position, 1.5f, _targetColliders, LayerMask.GetMask("Enemy"));

        if (count > 0)
        {
            for(int i = 0; i < count; i++)
            {
                Vector2 dir = _targetColliders[i].transform.position - Player.position;
                Vector2 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - Player.position;

                if (Vector2.Angle(dir, mouse) > 90) continue;

                _targetColliders[i].GetComponent<IDamagable>().TakeDamage(PlayerManager.Data.Damage);
            }
        }
    }
}
