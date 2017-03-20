using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BulletInterface {

    protected override void instantEffect()
    {

    }

    protected override void persistantEffect()
    {
        transform.localScale += Time.deltaTime * Vector3.one;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        LaserHandler.collide(collision, gameObject);
    }
}
