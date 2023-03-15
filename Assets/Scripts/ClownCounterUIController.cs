using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClownCounterUIController : MonoBehaviour
{
    public ClownCounter clownCounter;

    public Color color;

    public TMP_Text clownCounterText;

    public Image border;
    public TMP_Text text;

    public void Start() {
        text.color = color;
        border.color = color;
    }

    public void Update() {
        clownCounterText.text = clownCounter.GetClownCount().ToString();
    }
}
