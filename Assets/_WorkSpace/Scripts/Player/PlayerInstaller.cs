using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerRuntimeData _runtimeData;
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private AfterimageController _afterimage;

    public override void InstallBindings()
    {
        Container.Binding<PlayerRuntimeData>(_runtimeData);
        Container.Binding<Rigidbody2D>(_rigid);
        Container.Binding<PlayerInput>(_input);
        Container.Binding<SpriteRenderer>(_renderer);
        Container.Binding<AfterimageController>(_afterimage);
    }
}