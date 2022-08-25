using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Background : MonoBehaviour
{
    [SerializeField] UnityEvent events;

    private void OnMouseUpAsButton()
    {
        Garden.Instance.SetSelectedGardenTile(null);
        events.Invoke();
    }
}
