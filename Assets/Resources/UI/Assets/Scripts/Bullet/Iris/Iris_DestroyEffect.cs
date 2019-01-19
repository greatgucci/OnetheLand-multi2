using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_DestroyEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Destroy());
	}
	
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
