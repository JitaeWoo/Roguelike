using UnityEngine;
using Zenject;

public class MonsterInataller : MonoInstaller
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Collider2D _collider;

    public override void InstallBindings()
    {
        Container.Binding<SpriteRenderer>(_renderer);
        Container.Binding<Collider2D>(_collider);
    }
}