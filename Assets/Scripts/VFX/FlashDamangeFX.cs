using System.Collections;
using UnityEngine;

public class FlashDamangeFX : MonoBehaviour, IDamageable
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color damageColour = Color.red;
    [SerializeField] private float duration = 0.2f;
    

    private Color _originalColour;
    private Coroutine _playFlashCoHandler = null;

    void Start()
    {
        _originalColour = meshRenderer.material.color;    
    }

    // Not the greatest idea in the world, but we'll change this later.
    public void TakeDamage(float amount, Vector3 hitPoint, Vector3 hitDirection)
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
        meshRenderer.material.color = damageColour;
        yield return new WaitForSeconds(duration);
        meshRenderer.material.color = _originalColour;
    }
}
