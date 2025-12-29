using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseCollider : MonoBehaviour
{
    public Subject<Unit> OnEnter = new Subject<Unit>();
    public Subject<Unit> OnExit = new Subject<Unit>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        OnEnter.OnNext(Unit.Default);
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (!other.CompareTag("Player")) return;

        OnExit.OnNext(Unit.Default);
    }
}
