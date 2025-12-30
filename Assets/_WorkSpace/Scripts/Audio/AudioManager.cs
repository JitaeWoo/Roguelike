using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioData _bgmData;

    public ObjectPool<SfxController> SfxPool { get; private set; }

    private float _bgmVolume = 1f;
    public float BgmVolume
    {
        get => _bgmVolume;
        set
        {
            _bgmVolume = value;
            _bgmSource.volume = _bgmVolume;
        }
    }
    public float _sfxVolume = 1f;

    private AudioSource _bgmSource;

    private void Awake()
    {
        _bgmSource = gameObject.GetOrAddComponent<AudioSource>();
        _bgmSource.loop = true;

        SfxPool = new ObjectPool<SfxController>(CreateSfx, GetSfx, ReleaseSfx, DestroySfx);
    }

    public void BgmPlay(AudioData data)
    {
        if (data == null)
        {
            data = _bgmData;
        }

        AudioClip clip = data.Clip;

        if (_bgmSource.clip == clip) return;

        _bgmSource.Stop();
        _bgmSource.clip = clip;
        _bgmSource.Play();
    }

    public void SfxPlay(AudioData data, Transform parent = null)
    {
        if(parent == null)
        {
            parent = Camera.main.transform;
        }

        if (data == null)
        {
            Debug.LogError($"[AudioManager] {data} AudioData를 찾을 수 없습니다.");
            return;
        }

        SfxController sfx = SfxPool.Get();
        sfx.transform.parent = parent;
        sfx.transform.localPosition = Vector3.zero;
        sfx.SfxPlay(data, Mathf.Clamp01(_sfxVolume * data.Volume));
    }

    private SfxController CreateSfx()
    {
        GameObject obj = new GameObject("SfxController");
        obj.transform.parent = transform;
        AudioSource audioSource = obj.AddComponent<AudioSource>();

        audioSource.spatialBlend = 1.0f;       
        audioSource.minDistance = 5f;        
        audioSource.maxDistance = 30f;      
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;

        return obj.AddComponent<SfxController>();
    }

    private void GetSfx(SfxController sfx)
    {
        sfx.gameObject.SetActive(true);
    }

    private void ReleaseSfx(SfxController sfx)
    {
        sfx.transform.parent = transform;
        sfx.gameObject.SetActive(false);
    }

    private void DestroySfx(SfxController sfx)
    {
        Destroy(sfx.gameObject);
    }
}
