using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletHit : MonoBehaviour
{
    public Action Hit = delegate { };

    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].otherCollider.GetComponent<Rigidbody>())
        {
            Hit.Invoke();
        }
    }

}
