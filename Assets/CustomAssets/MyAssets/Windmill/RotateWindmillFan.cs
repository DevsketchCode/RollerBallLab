using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWindmillFan : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 50f) * Time.deltaTime);
    }
}

