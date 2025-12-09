using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerRuntimeData _runtimeData;
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private PlayerInput _input;

    public override void InstallBindings()
    {
        Binding<PlayerRuntimeData>(_runtimeData);
        Binding<Rigidbody2D>(_rigid);
        Binding<PlayerInput>(_input);
    }

    private void Binding<T>(T instance)
    {
        if (instance == null)
        {
            Container.Bind<T>().FromComponentSibling().AsSingle();
        }
        else
        {
            Container.Bind<T>().FromInstance(instance).AsSingle();
        }
    }
}