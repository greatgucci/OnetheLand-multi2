using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_BulletQTrigger : MonoBehaviour {

    Rigidbody2D rgbd;

    public void IrisQMove()
    {
        rgbd = GetComponent<Rigidbody2D>();
        StartCoroutine(move());
    }

    IEnumerator move()
    {
        float timer = 0f;

        rgbd.velocity = new Vector2(80f, 0f);

        while (true)
        {
            if (timer >= 0.5f)
            {
                break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        Iris_BulletQ iris_BulletQ;

        if (c.tag == "Bullet")
        {
            iris_BulletQ = PhotonNetwork.Instantiate("Iris_BulletQ", c.transform.position, Quaternion.identity, 0)
                .GetComponent<Iris_BulletQ>();
            iris_BulletQ.Init_Iris_BulletQ(GameManager.instance.myPnum);
            c.GetComponent<Bullet>().DestroyToServer();
        }
    }
}
