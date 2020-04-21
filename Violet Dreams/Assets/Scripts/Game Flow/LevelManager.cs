using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<SeedSpot> zoneList;


    private void Update()
    {
        int totalZones= 0;
        int seededZones=0;

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        foreach (SeedSpot zone in zoneList)
        {
            totalZones++;
            if (zone.seeded)
                seededZones++;
        }

        if (seededZones >= totalZones)
        {
            Endgame();
        }
    }

    private void Endgame()
    {
        SceneManager.LoadScene(4);
    }
}
