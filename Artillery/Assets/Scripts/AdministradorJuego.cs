using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class AdministradorJuego : MonoBehaviour
{
    public static AdministradorJuego Instancia;

    public float VelBala = 20f;
    public float VelRot = 60f;
    public int DisparosPorJuego = 10;
    public GameObject LoseScreen;
    public GameObject Pause;
    public bool paused;
    public TextMeshProUGUI TxtDisparos;

    CannonControls Inp;
    InputAction APausar;
    bool perdio;

    void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Debug.LogError("Ya existe un AdministradorJuego");
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        Inp = new CannonControls();
        APausar = Inp.Canon.Pausar;
        ResumeGame();
    }

    void OnEnable()
    {
        if (APausar == null) return;

        APausar.Enable();
        APausar.performed += PausarJuego;
    }

    void OnDisable()
    {
        if (APausar == null) return;

        APausar.performed -= PausarJuego;
        APausar.Disable();
    }

    void OnDestroy()
    {
        if (Instancia == this)
        {
            Instancia = null;
        }
    }

    void Start()
    {
        ActualizarTextoDisparos();

        if (LoseScreen != null)
        {
            LoseScreen.SetActive(false);
        }

        if (Pause != null)
        {
            Pause.SetActive(false);
        }
    }

    void Update()
    {
        ActualizarTextoDisparos();
    }

    void PausarJuego(InputAction.CallbackContext ctx)
    {
        if (perdio) return;

        TogglePause();
    }

    public void TogglePause()
    {
        if (paused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (perdio) return;

        paused = true;
        Time.timeScale = 0f;

        if (Pause != null)
        {
            Pause.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1f;

        if (Pause != null)
        {
            Pause.SetActive(false);
        }
    }

    public bool IntentarGastarDisparo()
    {
        if (perdio) return false;

        if (DisparosPorJuego <= 0)
        {
            Perder();
            return false;
        }

        DisparosPorJuego--;
        ActualizarTextoDisparos();
        return true;
    }

    void Perder()
    {
        perdio = true;
        paused = true;
        Time.timeScale = 0f;

        if (Pause != null)
        {
            Pause.SetActive(false);
        }

        if (LoseScreen != null)
        {
            LoseScreen.SetActive(true);
        }
    }

    void ActualizarTextoDisparos()
    {
        if (TxtDisparos != null)
        {
            TxtDisparos.text = "Disparos Restantes: " + DisparosPorJuego;
        }
    }
}