using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileManager : MonoBehaviour
{
    Vector3 objectivePos;

    public Camera cam;
    public GameObject missile;
    public bool isMissileActive = false;
    public float missileRaycastDistance;

    // Start is called before the first frame update
    void Start()
    {
        MissileBehaviour.SelectObjective = SetMissileTarget;
        MissileBehaviour.DeactiveMissile = GetMissileDeactivated;
    }

    bool SetObjective()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, missileRaycastDistance))
        {
            if (hit.transform.gameObject != null)
            {
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

    public void ActivateMissile(Vector3 startPosition)
    {
        if (!isMissileActive)
        {
            if (SetObjective())
            {
                missile.transform.position = startPosition;
                isMissileActive = true;
                missile.SetActive(true);
            }
        }
    }
}
