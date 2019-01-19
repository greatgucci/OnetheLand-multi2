using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        DataManager.Initalization();
    }

}
