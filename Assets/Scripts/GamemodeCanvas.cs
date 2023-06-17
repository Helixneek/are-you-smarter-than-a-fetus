using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemodeCanvas : MonoBehaviour
{
    public void OnMoveComplete()
    {
        gameObject.SetActive(false);
    }
}
