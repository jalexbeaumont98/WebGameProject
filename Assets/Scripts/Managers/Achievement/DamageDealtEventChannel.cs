using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DamageDealtEventChannel", menuName = "Scriptable Objects/Achievement/DamageDealtEventChannel")]
public class DamageDealtEventChannel : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int damageValue)
    {
        if (OnEventRaised == null) return;
        OnEventRaised.Invoke(damageValue);
    }
}
