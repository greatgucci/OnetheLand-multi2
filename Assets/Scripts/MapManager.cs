using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager instance = null;
    public static MapManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    float minX = -18f;
    float maxX = 18f;
    float minY = -2.5f;
    float maxY = 2.5f;

    public bool CheckMapBoundary(Transform t)
    {
        bool isBoundary = false;

        if (t.position.x <= minX || t.position.x >= maxX
            || t.position.y <= minY || t.position.y >= maxY)
            isBoundary = true;

        return isBoundary;
    }
}
