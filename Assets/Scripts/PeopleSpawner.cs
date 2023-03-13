using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{
    public GameObject clownPrefab;

    public void SendInTheClowns(int clownsDown) {
        for(int i = 0 ; i < clownsDown; i++) {
            GameObject clown = Instantiate(clownPrefab, transform.position, Quaternion.identity);
            clown.GetComponent<RagdollController>().Init();
        }
    }
}
