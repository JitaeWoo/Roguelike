using UnityEngine;
using Zenject;

public class AfterImageInstaller : MonoInstaller
{
    [SerializeField] private SpriteRenderer _renderer;

    public override void InstallBindings()
    {
        Container.Binding<SpriteRenderer>(_renderer);
    }
}