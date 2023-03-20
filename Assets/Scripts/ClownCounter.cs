using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownCounter : MonoBehaviour, IComparable<ClownCounter>
{


    public int clownCount = 8;

    public int clownsLostOnHit = 5;

    public enum ClownCarType {
        YOU,
        RED,
        YELLOW,
        PURPLE
    }

    public ClownCarType clownCarType;

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

    public string GetScoreText() {
        return clownCarType.ToString() + ": " + clownCount.ToString();
    }

    public int CompareTo(ClownCounter other)
    {
        if(clownCount.Equals(other.clownCount)) {
            return clownCarType == ClownCarType.YOU ? 1 : -1; 
        }

        return clownCount.CompareTo(other.clownCount);
    }
}
