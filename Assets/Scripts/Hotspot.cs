using UnityEngine;
using UnityEngine.Events;


public class Hotspot : MonoBehaviour
{
    public UnityEvent onClick;

    private void OnMouseDown()
    {
        onClick?.Invoke();
    }
}
