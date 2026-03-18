using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject balaPrefab;

    GameObject Punta;
    float mov = 0f;

    void Start()
    {
        Punta = GameObject.Find("SalidaBala");
    }

    void Update()
    {
        mov += Input.GetAxis("Horizontal") * AdministradorJuego.Instancia.VelRot * Time.deltaTime;

        if (mov > 60f)
        {
            mov = 60f;
        }

        if (mov < 0f)
        {
            mov = 0f;
        }

        transform.eulerAngles = new Vector3(mov, 90f, 0f);

        if (Input.GetKeyDown(KeyCode.Space) && AdministradorJuego.Instancia.DisparosPorJuego > 0)
        {
            GameObject bala = Instantiate(balaPrefab, Punta.transform.position, transform.rotation);
            Rigidbody rb = bala.GetComponent<Rigidbody>();

            Vector3 dir = new Vector3(
                transform.rotation.eulerAngles.x,
                90f - transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.z
            );

            dir.Normalize();
            rb.linearVelocity = dir * AdministradorJuego.Instancia.VelBala;

            AdministradorJuego.Instancia.DisparosPorJuego--;
            Debug.Log("Disparos restantes: " + AdministradorJuego.Instancia.DisparosPorJuego);
        }
    }
}