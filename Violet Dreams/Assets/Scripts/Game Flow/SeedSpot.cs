using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpot : MonoBehaviour
{
    bool seeded;

    private void Start()
    {
        seeded = false;
    }

    public void Seed()
    {
        seeded = true;
    }
}
