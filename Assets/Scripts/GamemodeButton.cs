using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemodeButton : MonoBehaviour
{
    private GamemodeCanvas gmcanvas;

    private void Awake()
    {
        gmcanvas = FindObjectOfType<GamemodeCanvas>();
    }

    public void OnMoveComplete()
    {
        gmcanvas.gameObject.SetActive(false);
    }
}
