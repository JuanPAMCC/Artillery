using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject balaPrefab;

    GameObject Punta;

    void Start()
    {
        Punta = GameObject.Find("Punta");
    }

    void Update()
    {
        float mov = Input.GetAxis("Horizontal") * AdministradorJuego.Instancia.VelRot * Time.deltaTime;
        transform.Rotate(mov, 0f, 0f);

        Vector3 rot = transform.rotation.eulerAngles;

        if (rot.x > 60f && rot.x < 180f)
        {
            rot.x = 60f;
        }

        if (rot.x > 180f && rot.x < 360f)
        {
            rot.x = 0f;
        }

        transform.rotation = Quaternion.Euler(rot.x, 60f, 0f);

        if (Input.GetKeyDown(KeyCode.Space) && AdministradorJuego.Instancia.DisparosPorJuego > 0)
        {
            GameObject bala = Instantiate(balaPrefab, Punta.transform.position, Quaternion.identity);
            Rigidbody rb = bala.GetComponent<Rigidbody>();

            Vector3 dir = Quaternion.Euler(0f, 60f - transform.rotation.eulerAngles.x, 0f) * Vector3.right;
            dir.Normalize();

            rb.linearVelocity = dir * AdministradorJuego.Instancia.VelBala;

            AdministradorJuego.Instancia.DisparosPorJuego--;
            Debug.Log("Disparos restantes: " + AdministradorJuego.Instancia.DisparosPorJuego);
        }
    }
}