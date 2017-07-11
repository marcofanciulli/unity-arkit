using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	[SerializeField]
	private GameObject[] characters;

	[SerializeField]
	private Transform[] targetPoints;

    [SerializeField]
    private float runSpeed = 1.0f;

    [SerializeField]
    private float spawnTime = 2.0f;

    [SerializeField]
    private float deadTime = 4.0f;

    private GameObject selectCharacter;
    private Animator selectAnimator;
	private Collider selectCharacterCollider;
    private Transform selectPoint;

    private float distanceRun;
    private Transform startRunPoint;
    private float startRunTime;
    private int state = 0; //0 - Spawn, 1 - Running, 2 - Shooting, 3 - killing

    private float dt = 0.0f;
    private float delay = 0.5f;

    void Start()
    {
        
    }

	void Update()
	{
        //selectCharacterCollider.
        switch (state)
        {
            case 0:
                dt += Time.deltaTime;

                if (dt >= spawnTime)
                {
                    dt = 0.0f;
                    SpawnCharacter();
                }
                break;
            case 1:
                float distanceCovered = (Time.time - startRunTime) * runSpeed;
                float trackPosition = distanceCovered / distanceRun;
                selectCharacter.transform.position = Vector3.Lerp(startRunPoint.position, selectPoint.position, trackPosition);

                if (trackPosition >= 1)
                {
                    state = 2;
                    selectAnimator.SetTrigger("Shoot");
                    selectCharacter.GetComponentInChildren<Shooting>().enabled = true;
                }
                break;
            case 2:
                break;
            case 3:
                dt += Time.deltaTime;

                if (dt >= deadTime)
                {
                    dt = 0.0f;
                    state = 0;
                }
                break;
        }
	}

    private void SpawnCharacter()
    {
        int characterNumber = Random.Range(0, characters.Length - 1);
        int pointNumber = Random.Range(0, targetPoints.Length - 1);

        selectCharacter = Instantiate(characters[characterNumber],transform.position,new Quaternion(), transform);
        selectPoint = targetPoints[pointNumber];
        selectCharacter.transform.LookAt(selectPoint);
        selectAnimator = selectCharacter.GetComponent<Animator>();
		selectCharacterCollider = selectCharacter.GetComponent<Collider>();
        selectCharacter.GetComponent<BulletHit>().Hit += DeadCharacter;
        startRunTime = Time.time;
        startRunPoint = transform;
        distanceRun = Vector3.Distance(transform.position, selectPoint.position);
        state = 1;
        selectAnimator.SetTrigger("Run");
    }

    private void DeadCharacter()
    {
        selectCharacterCollider.enabled = false;
        selectCharacter.GetComponent<BulletHit>().Hit -= DeadCharacter;
        selectCharacter.GetComponentInChildren<Shooting>().enabled = false;
        state = 3;
        selectAnimator.SetTrigger("Dead");
        Destroy(selectCharacter, deadTime);
    }
}
