using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    AudioSource onHitSound;
    public AudioClip onHitSFX;
    static GameManager _instance = null;
    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public int score = 0;
    int _score
    {
        get { return _score; }
        set
        {
            _score = value;
            Debug.Log("Current score is " + _score);
        }
    }

    public int maxLives = 3;
    int _lives = 3;
    public int lives
    {
        get { return _lives; }
        set
        {
            if (_lives > value)
            {

                //respawn code goes here

                Respawn();
                Debug.Log("Respawn code here");
            }
            _lives = value;
            if (_lives > maxLives)
            {
                _lives = maxLives;
            }
            else if (_lives <= 0)
            {
                _lives = 0;
                SceneManager.LoadScene("GameOver");
                //_lives = maxLives;
                //insert game end code here
            }
            Debug.Log("Current lives are " + _lives);
        }
    }

    public GameObject playerprefab;
    public GameObject playerInstance;
    public LevelManager currentLevel;
    bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == true)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Level")
            {
                SceneManager.LoadScene("TitleScreen");
            }
            else if (SceneManager.GetActiveScene().name == "TitleScreen")
            {
                SceneManager.LoadScene("Level");
            }
            else if (SceneManager.GetActiveScene().name == "GameOver")
            {
                SceneManager.LoadScene("TitleScreen");
            }
        }

        if (!onHitSound.isPlaying && isHit)
        {
            playerInstance.transform.position = currentLevel.spawnLocation.position;
            isHit = false;
            Time.timeScale = 1;
        }

        // Exit Game
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            QuitGame();
        }
    }

    public void spawnPlayer(Transform spawnLocation)
    {
        CameraFollow mainCamera = FindObjectOfType<CameraFollow>();

        if (mainCamera == true)
        {
            mainCamera.player = Instantiate(playerprefab, spawnLocation.position, spawnLocation.rotation);
            playerInstance = mainCamera.player;
        }
        else
        {
            spawnPlayer(spawnLocation);
        }
    }

    public void Respawn()
    {
        if(!onHitSound)
        {
            onHitSound = gameObject.AddComponent<AudioSource>();
            onHitSound.clip = onHitSFX;
            onHitSound.loop = false;
            onHitSound.Play();
        }
        else
        {
            onHitSound.Play();
        }
        
        isHit = true;
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit()
#endif
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
