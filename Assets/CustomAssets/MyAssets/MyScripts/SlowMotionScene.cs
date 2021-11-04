using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float timeSpeed = 1.0f;

    //private float defaultTimeScale = 1.0f;
    private float defaultFixedDeltaTime = 0.02f;
    void Start()
    {
        Time.timeScale = timeSpeed;
        Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
    }

}
