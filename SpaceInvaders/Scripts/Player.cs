using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{   

    // public variables
    public bool canTripleShot = false;
    public bool canSpeedBoost = false;
    public bool shieldsActive = false;
    public int life = 3;

    // private variables
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private GameObject[] _engines;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    private float _speed = 5.0f;
    private int hitCount = 0;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        
        if(_uiManager != null) 
        {
            _uiManager.UpdateLives(life);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

        if(_spawnManager != null) 
        {
            _spawnManager.StartSpawnRoutines();
        }

        _audioSource = GetComponent<AudioSource>();
        hitCount = 0;
    }

    private void Update()
    {
        Movements();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)) 
        {
            Shoot();
        }
    }

    public void Damage() 
    {
        if(shieldsActive == true)
        {
            shieldsActive = false;
            _shieldGameObject.SetActive(false);
        }
        else {
            hitCount++;

            if(hitCount == 1) 
            {
                _engines[0].SetActive(true);
            }
            else if (hitCount == 2) 
            {
                _engines[1].SetActive(true);
            }
            
            life = life-1;
            _uiManager.UpdateLives(life);
            if (life <= 0) 
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _gameManager.gameOver = true;
                _uiManager.ShowTitleScreen();
                Destroy(this.gameObject);
            }
        }
    }


    private void Shoot() 
    {   
        if (Time.time > _canFire) {
            _audioSource.Play();
            if (canTripleShot == true) 
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                _canFire = Time.time + _fireRate;
            } 
            else if (canTripleShot == false) {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                _canFire = Time.time + _fireRate;
            }            
        }     
    }

    private void Movements() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (canSpeedBoost == true) 
        {
            transform.Translate(Vector3.right * 2.0f * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * 2.0f * _speed * verticalInput * Time.deltaTime);            
        }
        else if (canSpeedBoost == false)
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }


        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f) 
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
        else if (transform.position.x > 9.5f) 
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f) 
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }        
    }


    public void ShieldsPowerupOn()
    {
        shieldsActive = true;
        _shieldGameObject.SetActive(true);
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotShutDownRoutine());
    }

    public void SpeedBoostPowerupOn ()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostShutDownRoutine());
    }


    public IEnumerator TripleShotShutDownRoutine()   
    {
        yield return new WaitForSeconds(7.0f);

        canTripleShot = false;
    }
    public IEnumerator SpeedBoostShutDownRoutine()
    {
        yield return new WaitForSeconds(7.0f);

        canSpeedBoost = false;
    }


}
