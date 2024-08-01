using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public int damage = 10;
    public bool isPlayer;

    public GameObject explotion;

    private void Start()
    {
        //Destroy(gameObject, lifeTime);
        StartCoroutine(DeactiveGame());
    }

    IEnumerator DeactiveGame()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPlayer == true && other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHealth>().DecreaseHealth();
            Instantiate(explotion,transform.position,transform.rotation);
            other.GetComponent<EnemyAI>().BulletResoreToPool(this.gameObject);

            //Destroy(gameObject);
        }

        if (isPlayer == false && other.gameObject.CompareTag("Player"))
        {
            //other.GetComponent<EnemyHealth>().DecreaseHealth();
            Instantiate(explotion, transform.position, transform.rotation);
            gameObject.SetActive(false);
            // Destroy(gameObject);
        }
    }

}
