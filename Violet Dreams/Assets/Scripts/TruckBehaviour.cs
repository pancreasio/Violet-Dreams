using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBehaviour : EnemyIA
{
    public Transform missile;

    public Transform mTarget;

    MissileBehaviour mBeh;

    // Start is called before the first frame update
    public override void Start() 
    {
        mBeh = missile.GetComponent<MissileBehaviour>();
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        mBeh.target = mTarget.position;
        base.Update();
    }

    public void FixedUpdate()
    {
        if(itsShootingTime)
        {
            missile.gameObject.SetActive(true);
        }
    }
}
