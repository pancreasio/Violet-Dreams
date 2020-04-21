using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpot : MonoBehaviour
{
    public bool seeded;
    public GameObject deadZone;
    public GameObject liveZone;
    public GameObject mapMarker;
    public GameObject truckArray;

    private void Start()
    {
        mapMarker.SetActive(true);
        deadZone.SetActive(true);
        liveZone.SetActive(false);
        seeded = false;
        truckArray.SetActive(false);
    }

    public void Seed()
    {
        deadZone.SetActive(false);
        liveZone.SetActive(true);
        mapMarker.SetActive(false);
        truckArray.SetActive(true);
        seeded = true;
    }
}
