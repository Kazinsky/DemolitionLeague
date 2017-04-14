using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : BulletInterface
{

    protected override void instantEffect()
    {

    }

    protected override void persistantEffect()
    {

    }

    protected override void OnCollisionEnter(Collision collision)
    {
        //base.OnCollisionEnter(collision);
        GameObject refer = transform.GetChild(1).gameObject;
        transform.GetChild(1).gameObject.SetActive(true);
        refer.transform.parent = null;
        refer.transform.localScale = Vector3.one;
        Destroy(gameObject);
        Destroy(refer, 1.0f);
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        //base.OnCollisionEnter(collision);
        GameObject refer = transform.GetChild(1).gameObject;
        transform.GetChild(1).gameObject.SetActive(true);
        refer.transform.parent = null;
        refer.transform.localScale = Vector3.one;
        Destroy(gameObject);
        Destroy(refer, 1.0f);
    }
}
