using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EnemyDefeatedEventChannel", menuName = "Scriptable Objects/Achievement/EnemyDefeatedEventChannel")]
public class EnemyDefeatedEventChannel : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised == null) return;
        OnEventRaised.Invoke();
    }
}

