using System.Collections;
using UnityEngine;

// [RequireComponent(typeof(MeshRenderer))]
// [RequireComponent(typeof(Material))]
public class FlashDamageFX : MonoBehaviour, IDamageable
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material flashDamageMaterial;
    [SerializeField] private float duration = 0.2f;

    private Material _originalMaterial;
    private Coroutine _playFlashCoHandler = null;

    void Start()
    {
        _originalMaterial = meshRenderer.material;
    }

    // Not the greatest idea in the world, but we'll change this later.
    public void TakeDamage(float amount, Vector3 hitPoint, Vector3 hitDirection)
    {
        Debug.Log("Called: 23");
        if(_playFlashCoHandler != null)
        {
            StopCoroutine(_playFlashCoHandler);
            _playFlashCoHandler = null;
        }

        Debug.Log("Called: 30");
        _playFlashCoHandler = StartCoroutine(PlayFlash());
    }

    private IEnumerator PlayFlash()
    {
        meshRenderer.material = flashDamageMaterial;
        yield return new WaitForSeconds(duration);
        meshRenderer.material = _originalMaterial;
    }
}
