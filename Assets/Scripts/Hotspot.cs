using UnityEngine;
using UnityEngine.Events;


public class Hotspot : MonoBehaviour
{
    public UnityEvent onClick;

    private void Awake() {
        int c = onClick.GetPersistentEventCount();
        for (int i = 0; i < c; i++) {
            Debug.Log(onClick.GetPersistentTarget(i));
            Debug.Log(onClick.GetPersistentMethodName(i));
        }
    }

    private void OnMouseDown()
    {
        onClick?.Invoke();
    }
}
