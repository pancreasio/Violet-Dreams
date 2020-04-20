using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpot : MonoBehaviour
{
    bool seeded;
    public GameObject deadZone;
    public GameObject liveZone;


    private void Start()
    {
        seeded = false;
        deadZone.SetActive(true);
        liveZone.SetActive(false);
    }

    public void Seed()
    {
        seeded = true;
        deadZone.SetActive(false);
        liveZone.SetActive(true);
    }
}
