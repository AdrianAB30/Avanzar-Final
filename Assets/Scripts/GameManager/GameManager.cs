using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject botonOptions;
    public GameObject menuOptions;
    public GameObject player;
    public AudioManager audioManager;
    private Animator playerAnimator;
    private Animator play;
    private Player playerScript;
    public bool isPaused = false;

    private void Awake()
    {
        if (player != null)
        {
            playerAnimator = player.GetComponent<Animator>();
            playerScript = player.GetComponent<Player>();
        }
    }
    private void OnEnable()
    {
        PlayMusicScene();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Credits")
            {
                SceneManager.LoadScene("Menu");
            }
            else
            {
                if (isPaused)
                {
                    Reanudar();
                }
                else
                {
                    Pausa();
                }
            }
        }
    }
    public void OnClicked(string name)
    {
        if (name == "Menu" || name == "Game" || name == "Credits" || name == "Options" || name == "Win" || name == "Game_Over")
        {
            SceneManager.LoadScene(name);
            PlayMusicScene(); 
            Time.timeScale = 1.0f;
        }
    }
    private void Start()
    {
        PlayMusicScene();   
    }
    public void PlayMusicScene()
    {
        if (audioManager != null)
        {
            audioManager.LoadMuteState();
            string sceneName = SceneManager.GetActiveScene().name;
            switch (sceneName)
            {
                case "Menu":
                    audioManager.PlayMusic(0);
                    break;
                case "Game":
                    audioManager.PlayMusic(1);
                    break;
                case "Win":
                    audioManager.PlayMusic(2);
                    break;
                case "Game_Over":
                    audioManager.PlayMusic(3);
                    break;
                case "Credits":
                    audioManager.PlayMusic(4);
                    break;
                default:
                    break;
            }
        }
    }
    public void Pausa()
    {
        Time.timeScale = 0;
        isPaused = true;

        if (botonOptions != null)
        {
            botonOptions.SetActive(false);
        }
        if (menuOptions != null)
        {
            menuOptions.SetActive(true);
        }
        if (playerAnimator != null)
        {
            playerAnimator.enabled = false;
        }
        if (playerScript != null)
        {
            playerScript.enabled = false;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
        isPaused = false;
    }
    public void Reanudar()
    {
        Time.timeScale = 1;
        isPaused = false;

        if (botonOptions != null)
        {
            botonOptions.SetActive(true);
        }
        if (menuOptions != null)
        {
            menuOptions.SetActive(false);
        }

        if (playerAnimator != null)
        {
            playerAnimator.enabled = true;
        }
        if (playerScript != null)
        {
            playerScript.enabled = true;
        }
    }
    public void Cerrar()
    {
        Application.Quit();
        Debug.Log("Saliendo del Juego Gozu");
    }
    public void MuteSound()
    {
        if(audioManager != null)
        {
            audioManager.MuteAll();
        }
    }
    public void UnmuteSound()
    {
        if(audioManager != null)
        {
            audioManager.UnmuteAll();
        }

    }
}
