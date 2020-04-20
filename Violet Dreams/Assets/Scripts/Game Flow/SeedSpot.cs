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
        deadZone.SetActive(true);
        liveZone.SetActive(false);
        seeded = false;
    }

    public void Seed()
    {
        deadZone.SetActive(false);
        liveZone.SetActive(true);
        seeded = true;
    }
}
