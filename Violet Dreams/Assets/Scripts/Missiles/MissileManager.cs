using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    Vector3 objectivePos;

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
            if (SetObjective())
            {
                isMissileActive = true;
                missile.SetActive(true);
            }
        }
    }

    bool SetObjective()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.transform.gameObject != null)
            {
                Debug.Log("You selected the " + hit.transform.name);
                objectivePos = hit.point;
                return true;
            }
        }
        
        return false;
    }

    Vector3 SetMissileTarget()
    {
        return objectivePos;
    }

    void GetMissileDeactivated()
    {
        isMissileActive = false;
    }
}
