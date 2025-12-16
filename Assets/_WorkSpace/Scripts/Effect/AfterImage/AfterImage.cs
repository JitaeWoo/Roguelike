using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

public class AfterImage : MonoBehaviour
{
    [SerializeField] private Material _material;

    private ObjectPool<AfterImage> _parentPool;
    private SpriteRenderer _parentRenderer;

    private float _duration;

    private SpriteRenderer _renderer;

    [Inject]
    private void Init(SpriteRenderer renderer)
    {
        _renderer = renderer;
        _renderer.material = _material;
    }

    public void SetInfo(ObjectPool<AfterImage> parentPool, SpriteRenderer parentRenderer, float duration)
    {
        _parentPool = parentPool;
        _parentRenderer = parentRenderer;
        _duration = duration;
    }

    private void OnEnable()
    {
        if (_parentPool == null) return;

        _renderer.sprite = _parentRenderer.sprite;

        _renderer.material.SetFloat("_AfterimageValue", 1);

        _renderer.material.DOFloat(0, "_AfterimageValue", _duration).OnComplete(() => _parentPool.Release(this));
    }
}
