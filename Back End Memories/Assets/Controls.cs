using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controls : MonoBehaviour
{
    // Existing variables...
    public GameObject controlsPanel;
    private bool hasEnteredBefore = false;
    private bool playerIsClose;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            if (!hasEnteredBefore)
            {
                controlsPanel.SetActive(true);
                PlayerMovement.isUIOpen = true;
                StartCoroutine(HideControlsAfterDelay(10f)); // Optional auto-hide
                hasEnteredBefore = true;
            }
        }
    }

    void Update()
    {
        // Toggle screen...
        if (Input.GetKeyDown(KeyCode.O))
        {
            controlsPanel.SetActive(!controlsPanel.activeSelf);
            PlayerMovement.isUIOpen = true;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            controlsPanel.SetActive(false);
            PlayerMovement.isUIOpen = false;
        }
    }

    private IEnumerator HideControlsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        controlsPanel.SetActive(false);
        PlayerMovement.isUIOpen = false;
    }


}
