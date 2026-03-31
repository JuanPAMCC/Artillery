using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject balaPrefab;
    [SerializeField] GameObject particulaDisparo;

    GameObject Punta;
    float mov = 0f;

    CannonControls Inp;
    InputAction AApuntar;
    InputAction ADisparar;

    void Awake()
    {
        Inp = new CannonControls();
        AApuntar = Inp.Canon.Apuntar;
        ADisparar = Inp.Canon.Disparar;
    }

    void OnEnable()
    {
        AApuntar.Enable();
        ADisparar.Enable();
        ADisparar.performed += Disparar;
    }

    void OnDisable()
    {
        ADisparar.performed -= Disparar;
        AApuntar.Disable();
        ADisparar.Disable();
    }

    void Start()
    {
        Punta = GameObject.Find("SalidaBala");
    }

    void Update()
    {
        if (AdministradorJuego.Instancia == null) return;
        if (AdministradorJuego.Instancia.paused) return;

        mov += AApuntar.ReadValue<float>() * AdministradorJuego.Instancia.VelRot * Time.deltaTime;

        if (mov > 60f)
        {
            mov = 60f;
        }

        if (mov < 0f)
        {
            mov = 0f;
        }

        transform.eulerAngles = new Vector3(mov, 90f, 0f);
    }

    void Disparar(InputAction.CallbackContext ctx)
    {
        if (AdministradorJuego.Instancia == null) return;
        if (!AdministradorJuego.Instancia.IntentarGastarDisparo()) return;
        if (balaPrefab == null || Punta == null) return;

        GameObject bala = Instantiate(balaPrefab, Punta.transform.position, transform.rotation);
        Rigidbody rb = bala.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 dir = new Vector3(
                transform.rotation.eulerAngles.x,
                90f - transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.z
            );

            dir.Normalize();
            rb.linearVelocity = dir * AdministradorJuego.Instancia.VelBala;
        }

        if (particulaDisparo != null)
        {
            GameObject part = Instantiate(particulaDisparo, Punta.transform.position, Punta.transform.rotation);
            ParticleSystem ps = part.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                float t = ps.main.duration + ps.main.startLifetime.constantMax;
                Destroy(part, t);
            }
            else
            {
                Destroy(part, 3f);
            }
        }

        Debug.Log("Disparos restantes: " + AdministradorJuego.Instancia.DisparosPorJuego);
    }
}