using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEndHandler : MonoBehaviour
{
    public UnityEvent onAnimatorFinished;

    public void FinishAnimation() {
        onAnimatorFinished?.Invoke();
    }
}
