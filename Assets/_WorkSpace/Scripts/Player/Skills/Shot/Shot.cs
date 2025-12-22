using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Shot : Skill
{
    [SerializeField] private GameObject _bulletPrefab;

    private ObjectPool<Bullet> _bulletPool;

    private void Start()
    {
        _bulletPool = new ObjectPool<Bullet>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet);

        for(int i = 0; i < 10; i++)
        {
            _bulletPool.Release(CreateBullet());
        }
    }

    protected override void ActivateSkill()
    {
        ShotData data = Data as ShotData;

        if(data == null)
        {
            Debug.LogError("Shot 스킬의 Data가 ShotData가 아닙니다.");
            return;
        }

        _bulletPool.Get();
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = DiContainer.InstantiatePrefab(_bulletPrefab, transform).GetComponent<Bullet>();
        bullet.ParentPool = _bulletPool;

        return bullet;
    }

    private void GetBullet(Bullet bullet)
    {
        ShotData data = Data as ShotData;

        if (data == null)
        {
            Debug.LogError("Shot 스킬의 Data가 ShotData가 아닙니다.");
            return;
        }

        Vector2 mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - Player.Position;
        mouse = mouse.normalized;

        bullet.transform.position = Player.Position;
        bullet.SetData(data.Damage, data.ShotSpeed, mouse, data.Sprite);
        bullet.gameObject.SetActive(true);
    }

    private void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void DestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
