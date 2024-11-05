using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomEvent : UnityEvent<Component, object> { }

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;

    public CustomEvent response;

    void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}
