using UnityEngine; 
using System.Collections;

public class Shooting : MonoBehaviour 
{
	public float bulletSpeed = 10;
	public Rigidbody bullet;


	void Fire()
	{
        Rigidbody bulletClone = (Rigidbody) Instantiate(bullet, transform.position, transform.rotation);
		bulletClone.velocity = transform.forward * bulletSpeed;
		Destroy (bulletClone.gameObject, 1);
	}

	float dt = 0.0f;
	float delay = 0.5f;

	void Update () 
	{
		dt += Time.deltaTime;

		if (dt >= delay) {
			delay = Random.Range (0.1f, 2.0f);
			dt = 0.0f;
			Fire ();
		}
	}
}