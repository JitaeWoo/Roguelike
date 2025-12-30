using UnityEngine;
using Zenject;

public class MonsterInataller : MonoInstaller
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private MonsterData _data;
    [SerializeField] private MonsterChaseCollider _chase;
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private Animator _animator;

    public override void InstallBindings()
    {
        Container.Binding<SpriteRenderer>(_renderer);
        Container.Binding<Collider2D>(_collider);
        Container.Binding<MonsterData>(_data);
        Container.Binding<MonsterChaseCollider>(_chase);
        Container.Binding<Rigidbody2D>(_rigid);
        Container.Binding<Animator>(_animator);
    }
}