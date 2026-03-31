using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        if (AdministradorJuego.Instancia != null)
        {
            AdministradorJuego.Instancia.ResumeGame();
        }
        else
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        if (AdministradorJuego.Instancia != null)
        {
            AdministradorJuego.Instancia.ResumeGame();
        }
        else
        {
            Time.timeScale = 1f;
        }

        Application.Quit();
    }

    public void LoadMenu(string menu)
    {
        if (AdministradorJuego.Instancia != null)
        {
            AdministradorJuego.Instancia.ResumeGame();
        }
        else
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene(menu);
    }

    public void ResetScene()
    {
        if (AdministradorJuego.Instancia != null)
        {
            AdministradorJuego.Instancia.ResumeGame();
        }
        else
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseState()
    {
        if (AdministradorJuego.Instancia != null)
        {
            AdministradorJuego.Instancia.PauseGame();
        }
    }

    public void Resume()
    {
        if (AdministradorJuego.Instancia != null)
        {
            AdministradorJuego.Instancia.ResumeGame();
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}