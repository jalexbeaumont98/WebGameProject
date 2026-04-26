using System.Collections;
using UnityEngine;

public class ProjectileBomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefabFx;
    [SerializeField] private GameObject megaExplosionPrefabFx;
    [SerializeField, Range(0f, 1f)] private float megaExplosionProbability = 0.2f;

    private void Start()
    {
        StartCoroutine(BombTimer());
    }

    private IEnumerator BombTimer()
    {
        var clipLength = AudioManager.Instance.PlayAndGetLength(SoundType.BombTimer);
        yield return new WaitForSeconds(clipLength + .1f);
        Explode();
    }

    private void Explode()
    {
        GameObject selectedExplosion = Random.value < megaExplosionProbability
            ? megaExplosionPrefabFx
            : explosionPrefabFx;

        Instantiate(selectedExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
