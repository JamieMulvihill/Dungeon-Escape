using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public PlayerMovement player;

    private void OnTriggerEnter(Collider other)
    {
        player.hasEscaped = true;
    }
}
