using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClownCounterUIController : MonoBehaviour
{
    public ClownCounter clownCounter;

    public TMP_Text clownCounterText;

    public void Update() {
        clownCounterText.text = clownCounter.GetClownCount().ToString();
    }

}
