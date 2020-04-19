using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOnMinimap : MonoBehaviour
{
    public GameObject car;

    void Update()
    {
        transform.position = car.transform.position;
    }
}
