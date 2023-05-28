using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCredit : MonoBehaviour
{
    private bool hasBeenTriggered = false;
    public GameObject creditCanvas;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasBeenTriggered)
        {
            creditCanvas.SetActive(true);
            Time.timeScale = 1f;
        }
    }
}