using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ScoreController : MonoBehaviour
{
    
    public TMP_Text lastPlace;
    public TMP_Text thirdPlace;
    public TMP_Text secondPlace;
    public TMP_Text firstPlace;

    public TMP_Text winLose;

    public void CollectScores() {
        List<ClownCounter> clownCounters = new List<ClownCounter>(GameObject.FindObjectsOfType<ClownCounter>());
        List<ClownCounter> sortedClownCounters =  clownCounters.OrderBy(x => x.clownCount).ToList();

        lastPlace.text = sortedClownCounters[0].GetScoreText();
        thirdPlace.text = sortedClownCounters[1].GetScoreText();
        secondPlace.text = sortedClownCounters[2].GetScoreText();
        firstPlace.text = sortedClownCounters[3].GetScoreText();

        winLose.text = sortedClownCounters[3].clownCarType == ClownCounter.ClownCarType.YOU ? "YOU WIN" : "YOU LOSE";
    }

}
