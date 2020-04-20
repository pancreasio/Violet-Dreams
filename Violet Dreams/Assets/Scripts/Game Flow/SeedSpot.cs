using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpot : MonoBehaviour
{
    bool seeded;
    public GameObject deadZone;
    public GameObject liveZone;
    public GameObject mapMarker;

    private void Start()
    {
        mapMarker.SetActive(true);
        deadZone.SetActive(true);
        liveZone.SetActive(false);
        seeded = false;
    }

    public void Seed()
    {
        deadZone.SetActive(false);
        liveZone.SetActive(true);
        mapMarker.SetActive(false);
        seeded = true;
    }
}
