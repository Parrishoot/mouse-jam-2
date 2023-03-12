using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownCounter : MonoBehaviour
{
    private int clownCount = 0;

    public void AddClown() {
        clownCount++;
    }

    public void LoseClown() {
        clownCount--;
    }

    public int GetClownCount() {
        return clownCount;
    }
}
