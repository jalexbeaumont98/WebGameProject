using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Void Event Channel", menuName = "Events/Void Event")]
public class VoidEventChannel : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised == null) return;
        OnEventRaised.Invoke();
    }
}
