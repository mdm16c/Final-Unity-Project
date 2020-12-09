using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text timeSinceStarted;
    void Start() {
        timeSinceStarted = GetComponent<Text>();
    }
    void Update()
    {
        timeSinceStarted.text = Time.timeSinceLevelLoad.ToString("#0.0");
    }
}
