using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    private CreditsCanvas CDcanvas;

    private void Awake()
    {
        CDcanvas = FindObjectOfType<CreditsCanvas>();
    }

    public void OnMoveComplete()
    {
        CDcanvas.gameObject.SetActive(false);
    }
}
