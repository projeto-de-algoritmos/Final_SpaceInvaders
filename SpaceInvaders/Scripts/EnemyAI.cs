using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    [SerializeField]
    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }


    private void Update()
    {
        EnemyMovements();
    }

    private void EnemyMovements()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7.0f)
        {
            float randomX = Random.Range(-7.75f, 7.75f);
            transform.position = new Vector3(randomX, 7.0f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            if (other.transform.parent != null) 
            {
                Destroy(other.transform.parent.gameObject);
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _uiManager.UpdateScore();
            Destroy(other.gameObject);
        }

        else if (other.tag == "Player")
        {   
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }
}
