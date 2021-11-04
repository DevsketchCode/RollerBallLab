using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCamFilter : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraCanvasPanel;

    public void Start()
    {
        // turn off the camera filter on start.
        cameraCanvasPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!cameraCanvasPanel.activeInHierarchy)
        {
            cameraCanvasPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (cameraCanvasPanel.activeInHierarchy)
        {
            cameraCanvasPanel.SetActive(false);
        }
    }
}
