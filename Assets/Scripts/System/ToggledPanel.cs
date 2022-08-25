using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggledPanel : MonoBehaviour
{
    public void TogglePanel() => gameObject.SetActive(!gameObject.activeSelf);
}
