using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UIText : MonoBehaviour {

    [SerializeField] private TextAsset skillDes;
    [SerializeField] private TextAsset skillName;

    private void Awake()
    {
        skillDes = Resources.Load("Text/Description") as TextAsset;
        string[] splitTextDes = skillDes.text.Split('/');
        skillName = Resources.Load("Text/SkillName") as TextAsset;
        string[] splitTextName = skillName.text.Split('/');

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
