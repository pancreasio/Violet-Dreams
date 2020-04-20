using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBehaviour : EnemyIA
{
    public Transform missile;

    public Transform mTarget;

    MissileBehaviour mBeh;
    public Quaternion originalRot;
    public Vector3 originalRelativePos;

    // Start is called before the first frame update
    public override void Start() 
    {
        mBeh = missile.GetComponent<MissileBehaviour>();
        originalRot = missile.rotation;
        originalRelativePos = transform.localPosition;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        mBeh.target = mTarget.position;
        if (itsShootingTime)
        {
            missile.gameObject.SetActive(true);
        }
        base.Update();
    }
}
