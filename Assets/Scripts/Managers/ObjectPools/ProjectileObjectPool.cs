using UnityEngine;
using System.Collections.Generic;

public class ProjectileObjectPool : PersistentSingleton<ProjectileObjectPool>
{
    [SerializeField] private ProjectileBullet projectileBulletPrefab; // MUST HAVE Rigidbody
    private Queue<ProjectileBullet> pool = new Queue<ProjectileBullet>();

    public ProjectileBullet Get()
    {
        if (pool.Count == 0) AddProjectile(1);
        return pool.Dequeue();
    }

    private void AddProjectile(int count)
    {
        for (int i = 0; i < count; i++)
        {
            ProjectileBullet prefab = Instantiate(projectileBulletPrefab);
            prefab.gameObject.SetActive(false);
            pool.Enqueue(prefab);
        }
    }

    public void ReturnToPool(ProjectileBullet bullet)
    {
        bullet.gameObject.SetActive(false);
        pool.Enqueue(bullet);
    }
}
