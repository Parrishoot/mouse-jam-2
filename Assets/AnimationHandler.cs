using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationHandler : MonoBehaviour
{
    public UnityEvent onAnimationBegun;

    public UnityEvent onAnimatorFinished;

    public void FinishAnimation() {
        onAnimatorFinished?.Invoke();
    }

    public void BeginAnimation() {
        onAnimationBegun?.Invoke();
    }
}
