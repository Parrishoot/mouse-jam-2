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

    public AudioSource audioSource;

    public AudioClip winClip;

    public AudioClip loseClip;

    public void CollectScores() {
        List<ClownCounter> clownCounters = new List<ClownCounter>(GameObject.FindObjectsOfType<ClownCounter>());
        clownCounters.Sort();

        lastPlace.text = clownCounters[0].GetScoreText();
        thirdPlace.text = clownCounters[1].GetScoreText();
        secondPlace.text = clownCounters[2].GetScoreText();
        firstPlace.text = clownCounters[3].GetScoreText();

        winLose.text = clownCounters[3].clownCarType == ClownCounter.ClownCarType.YOU ? "YOU WIN" : "YOU LOSE";
        audioSource.clip = clownCounters[3].clownCarType == ClownCounter.ClownCarType.YOU ? winClip : loseClip;
    }

}
