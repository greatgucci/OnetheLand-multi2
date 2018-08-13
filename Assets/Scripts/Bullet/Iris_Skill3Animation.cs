using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill3Animation : MonoBehaviour {

    Animator irisSkill3Animator;

    private void Awake()
    {
        irisSkill3Animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        irisSkill3Animator.Play("Iris_MagicCircle6");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
