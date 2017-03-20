using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : BulletInterface{

    protected override void instantEffect()
    {
        Debug.Log("once test");
    }

    protected override void persistantEffect()
    {

    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        BasicBulletHandler.collide(collision, gameObject);
    }
}
