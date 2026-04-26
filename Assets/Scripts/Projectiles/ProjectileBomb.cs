using System.Collections;
using UnityEngine;

public class ProjectileBomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefabFx;

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
        Instantiate(explosionPrefabFx, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
