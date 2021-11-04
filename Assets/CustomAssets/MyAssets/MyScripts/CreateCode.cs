using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Randomize Code for level 3
        GameObject.Find("CodeCanvas1").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Mathf.Round(Random.Range(1, 5)).ToString();
        GameObject.Find("CodeCanvas2").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Mathf.Round(Random.Range(1, 5)).ToString();
        GameObject.Find("CodeCanvas3").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Mathf.Round(Random.Range(1, 5)).ToString();
    }
}
