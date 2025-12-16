using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class AfterimageController : MonoBehaviour
{
    [SerializeField] private GameObject _afterimagePrefab;

    [SerializeField] private float _duration;
    [SerializeField] private float _interval;

    private ObjectPool<AfterImage> _pool;

    private SpriteRenderer _renderer;

    CancellationTokenSource _sorce;

    [Inject]
    private void Init(SpriteRenderer renderer)
    {
        _renderer = renderer;
    }

    private void Awake()
    {
        int count = (int)(_duration / _interval) + 1;

        _pool = new ObjectPool<AfterImage>(CreatePool, GetPool, ReleasePool, DestroyPool, defaultCapacity: count);

        for(int i = 0; i < count; i++)
        {
            _pool.Release(CreatePool());
        }
    }

    private void OnDestroy()
    {
        if (_sorce == null) return;

        _sorce.Cancel();
        _sorce.Dispose();
    }

    public void Play()
    {
        if(_sorce != null)
        {
            _sorce.Cancel();
            _sorce.Dispose();
        }

        _sorce = new CancellationTokenSource();

        StartAfterimage().Forget();
    }

    public void Stop()
    {
        if (_sorce == null) return;

        _sorce.Cancel();
        _sorce.Dispose();
        _sorce = null;
    }

    private async UniTaskVoid StartAfterimage()
    {
        while(true)
        {
            _pool.Get();

            await UniTask.WaitForSeconds(_duration, cancellationToken: _sorce.Token);
        }
    }

    #region ObjectPool

    private AfterImage CreatePool()
    {
        AfterImage image = Instantiate(_afterimagePrefab).GetComponent<AfterImage>();
        image.transform.parent = transform;
        image.SetInfo(_pool, _renderer, _duration);

        return image;
    }

    private void GetPool(AfterImage image)
    {
        image.transform.parent = null;
        image.transform.position = transform.position;
        image.gameObject.SetActive(true);
    }

    private void ReleasePool(AfterImage image)
    {
        image.transform.parent = transform;
        image.gameObject.SetActive(false);
    }

    private void DestroyPool(AfterImage image)
    {
        Destroy(image.gameObject);
    }

    #endregion
}
