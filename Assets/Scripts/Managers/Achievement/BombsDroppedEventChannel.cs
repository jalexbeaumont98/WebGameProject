using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BombsDroppedEventChannel", menuName = "Scriptable Objects/Achievement/BombsDroppedEventChannel")]
public class BombsDroppedEventChannel : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised == null) return;
        OnEventRaised.Invoke();
    }
}
