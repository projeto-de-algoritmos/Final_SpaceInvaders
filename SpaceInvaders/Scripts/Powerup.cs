using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    // 0 = Triple Shot, 1 = Speed Boost, 2 = Shields
    private int powerupID;

    private void Update()
    {
       transform.Translate(Vector3.down * _speed * Time.deltaTime); 

       if(transform.position.y < -7) 
       {
        Destroy(this.gameObject);
       }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null) {
                if (powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                else if (powerupID == 1)
                {
                    player.SpeedBoostPowerupOn();
                }
                else if (powerupID == 2) 
                {
                    player.ShieldsPowerupOn();
                }
                
            }

            Destroy(this.gameObject);
        }
    }
    
}
