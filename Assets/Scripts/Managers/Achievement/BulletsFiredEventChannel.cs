using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BulletsFiredEventChannel", menuName = "Scriptable Objects/Achievement/BulletsFiredEventChannel")]
public class BulletsFiredEventChannel : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised == null) return;
        OnEventRaised.Invoke();
    }
}
