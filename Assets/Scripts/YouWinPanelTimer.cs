using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouWinPanelTimer : MonoBehaviour
{
    private Text timeSinceStarted;
    void Start() {
        timeSinceStarted = GetComponent<Text>();
    }

    void Update()
    {
        timeSinceStarted.text = "You finished in " + Time.timeSinceLevelLoad.ToString("#0.0") + " seconds!";
    }
}
