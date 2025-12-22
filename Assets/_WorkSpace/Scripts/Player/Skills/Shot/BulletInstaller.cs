using UnityEngine;
using Zenject;

public class BulletInstaller : MonoInstaller
{
    [SerializeField] Rigidbody2D _rigid;
    [SerializeField] SpriteRenderer _renderer;

    public override void InstallBindings()
    {
        Container.Binding<Rigidbody2D>(_rigid);
        Container.Binding<SpriteRenderer>(_renderer);
    }
}