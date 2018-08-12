using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour {

    public float warningTimer;

    SpriteRenderer warningRange_Renderer;

    PolygonCollider2D collider2D_Temp;

	// Use this for initialization
	void Start () {
        if (transform.Find("WarningCircleRange") == null)
        {
            warningRange_Renderer = transform.Find("WarningSquareRange").GetComponent<SpriteRenderer>();
        }

        else
        {
            warningRange_Renderer = transform.Find("WarningCircleRange").GetComponent<SpriteRenderer>();
        }

        warningRange_Renderer.color = new Color(1f, 1f, 1f, 0.25f);

        StartCoroutine(WarningBlink());
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator WarningBlink()
    {
        float timer = 0f;
        float colorTimer = 0f;
        bool blinkCount = false;

        yield return null;

        while (true)
        {
            if (timer >= warningTimer)
            {
                break;
            }

            timer += Time.deltaTime;

            if (!blinkCount)
            {
                if (colorTimer >= 0.5f)
                {
                    blinkCount = true;
                    colorTimer = 0.5f;
                }

                warningRange_Renderer.color = new Color(1f, 1f, 1f, 0.25f + colorTimer);

                colorTimer += 0.5f * Time.deltaTime;
            }

            else if (blinkCount)
            {
                if (colorTimer <= 0f)
                {
                    blinkCount = false;
                    colorTimer = 0f;
                }

                warningRange_Renderer.color = new Color(1f, 1f, 1f, 0.25f + colorTimer);

                colorTimer -= 0.5f * Time.deltaTime;
            }

            yield return null;
        }

        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
    

    public void DestroyWarning()
    {
        Destroy(gameObject);
    }
    
}
