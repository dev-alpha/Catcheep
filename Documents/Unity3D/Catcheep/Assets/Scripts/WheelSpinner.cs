﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WheelSpinner : MonoBehaviour
{

    public GameObject wheelOfLuck;
    public GameObject triangl;
    public GameObject[] items;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spin()
    {
        StartCoroutine(wheelTurned());
    }

    IEnumerator wheelTurned()
    {
        int i = Random.Range(5,10);
        do
        {
            wheelOfLuck.GetComponent<Rigidbody2D>().AddTorque(10);
            //wheelOfLuck.GetComponent<RectTransform>().transform.Rotate(0f,0f,10f);//RotateAround(wheelOfLuck.transform.position,Vector3.back,10);
            i++;
            yield return new WaitForSeconds(0.01f);
        } while (i < 50);

        yield return new WaitForSeconds(1f);
        do
        {
            wheelOfLuck.GetComponent<Rigidbody2D>().angularVelocity-=2;
            yield return new WaitForSeconds(0.001f);
        } while (wheelOfLuck.GetComponent<Rigidbody2D>().angularVelocity > 0);

        float distence = 0;
        int index = 0;
        for (int j = 0; j < items.Length; j++)
        {
            if (j == 0) distence = Vector3.Distance(triangl.transform.position, items[j].transform.position);
            if (Vector3.Distance(triangl.transform.position, items[j].transform.position) < distence)
            {
                distence = Vector3.Distance(triangl.transform.position, items[j].transform.position);
                index = j;
            }
        }

        print(items[index].gameObject.name);
        
    }
}