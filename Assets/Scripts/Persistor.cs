using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistor : MonoBehaviour
{
    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
