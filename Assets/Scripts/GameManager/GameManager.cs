using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject botonOptions;
    public GameObject menuOptions;
    public GameObject player;
    public SFXManager sfxManager;
    public AudioManager audioManager;
    private Animator playerAnimator;
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
            sfxManager.PlaySFX(9);
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
                case "Nivel2":
                    audioManager.PlayMusic(2);
                    break;
                case "Win":
                    audioManager.PlayMusic(3);
                    break;
                case "Game_Over":
                    audioManager.PlayMusic(4);
                    break;
                case "Credits":
                    audioManager.PlayMusic(5);
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
        sfxManager.PlaySFX(9);

        PlayerPrefs.DeleteKey("PlayerLife");

        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
        isPaused = false;
    }

    public void RestartLevel2()
    {
        sfxManager.PlaySFX(9);

        PlayerPrefs.DeleteKey("PlayerLife");

        SceneManager.LoadScene("Nivel2");
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
        sfxManager.PlaySFX(9);
        if (audioManager != null)
        {
            audioManager.MuteAll();
        }
    }

    public void UnmuteSound()
    {
        sfxManager.PlaySFX(9);
        if (audioManager != null)
        {
            audioManager.UnmuteAll();
        }
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs limpiados");
    }

    public void MenuLevel2()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1.0f;
    }
}
