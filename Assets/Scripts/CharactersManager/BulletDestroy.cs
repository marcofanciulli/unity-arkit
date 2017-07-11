using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour {

    [SerializeField]
    private GameObject sparkles;

    void OnCollisionEnter(Collision collision)
    {
        var sparkle = Instantiate(sparkles, transform.position, new Quaternion());
        Destroy(this.gameObject);
        Destroy(sparkle, 1f);
    }
}
