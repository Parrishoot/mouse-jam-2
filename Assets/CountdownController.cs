using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownController : Singleton<CountdownController>
{

    private float totalGameTime = 0f;
    private float remainingGameTime = 0f;

    private Animator animator;

    public TMP_Text prevText;

    public TMP_Text currentText;

    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(totalGameTime != 0) {
            remainingGameTime = Mathf.Max(0, remainingGameTime - Time.deltaTime);
            CheckAnimateNexTime();
        }

        if(remainingGameTime <= 0) {
            GameManager.GetInstance().EndGame();
        }
    }

    public void CheckAnimateNexTime() {
        if(Mathf.Ceil(remainingGameTime).ToString() != currentText.text) {
            prevText.text = currentText.text;
            currentText.text = Mathf.Ceil(remainingGameTime).ToString();
            animator.Play("Timer");
        }
    }

    public void BeginTimer(float totalGameTime) {
        this.totalGameTime = totalGameTime;
        this.remainingGameTime = totalGameTime;
        currentText.text = totalGameTime.ToString();
    }
}
