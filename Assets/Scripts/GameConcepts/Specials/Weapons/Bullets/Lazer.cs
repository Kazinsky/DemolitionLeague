﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : BulletInterface
{

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
    }
}