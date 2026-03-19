using System.Collections;
using UnityEngine;

// [RequireComponent(typeof(MeshRenderer))]
// [RequireComponent(typeof(Material))]
public class FlashDamageFX : MonoBehaviour
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

    public void Play()
    {
        if(_playFlashCoHandler != null)
        {
            StopCoroutine(_playFlashCoHandler);
            _playFlashCoHandler = null;
        }

        _playFlashCoHandler = StartCoroutine(PlayFlash());
    }

    private IEnumerator PlayFlash()
    {
        meshRenderer.material = flashDamageMaterial;
        yield return new WaitForSeconds(duration);
        meshRenderer.material = _originalMaterial;
    }
}
