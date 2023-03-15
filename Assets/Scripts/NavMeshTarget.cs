using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshTarget : MonoBehaviour
{

    public Transform targetTransform;

    public Transform GetTargetTransform() {
        return targetTransform;
    }
}
