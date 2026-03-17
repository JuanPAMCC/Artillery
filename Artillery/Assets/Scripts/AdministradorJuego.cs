using UnityEngine;

public class AdministradorJuego : MonoBehaviour
{
    public static AdministradorJuego Instancia;

    public float VelBala = 20f;
    public float VelRot = 60f;
    public int DisparosPorJuego = 10;

    void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
        }
        else
        {
            Debug.LogError("Ya existe un AdministradorJuego");
        }
    }
}