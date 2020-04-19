using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    GameObject objective;

    public Camera cam;

    public GameObject missile;

    bool isMissileActive = false;

    // Start is called before the first frame update
    void Start()
    {
        MissileBehaviour.SelectObjective = SetMissileTarget;
        MissileBehaviour.DeactiveMissile = GetMissileDeactivated;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isMissileActive)
        {
            objective = SetObjective();
            isMissileActive = true;
            missile.SetActive(true);
        }
    }

    GameObject SetObjective()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            Debug.Log("You selected the " + hit.transform.name);
            return hit.transform.gameObject;
        }
        else return null;
    }

    GameObject SetMissileTarget()
    {
        return objective;
    }

    void GetMissileDeactivated()
    {
        isMissileActive = false;
    }
}
