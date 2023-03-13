using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownCounter : MonoBehaviour
{


    public int clownCount = 8;

    public int clownsLostOnHit = 5;

    public void AddClown() {
        clownCount++;
    }

    public int LoseClowns() {
        int lostClowns = Mathf.Min(clownsLostOnHit, clownCount);
        clownCount -= lostClowns;
        return lostClowns;
    }

    public int GetClownCount() {
        return clownCount;
    }
}
