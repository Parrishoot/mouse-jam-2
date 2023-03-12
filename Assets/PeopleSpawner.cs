using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour
{

    public int population = 5;

    public GameObject clownPrefab;

    public void SendInTheClowns() {
        for(int i = 0 ; i < population; i++) {
            GameObject clown = Instantiate(clownPrefab, transform.position, Quaternion.identity);
            clown.GetComponent<RagdollController>().Init();
        }
    }
}
