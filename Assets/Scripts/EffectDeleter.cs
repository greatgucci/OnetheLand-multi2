using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDeleter : MonoBehaviour
{
    public float time=1;
    private void Awake()
    {
        Destroy(this.gameObject, time);
    }
}
