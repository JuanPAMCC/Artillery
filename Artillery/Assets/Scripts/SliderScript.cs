using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] Slider SliderF;
    [SerializeField] Image FillImg;
    [SerializeField] float VelBarra = 0.6f;
    [SerializeField] float VelColor = 8f;
    [SerializeField] float FuerzaMin = 10f;
    [SerializeField] float FuerzaMax = 30f;
    [SerializeField] Color ColGreen = Color.green;
    [SerializeField] Color ColYellow = Color.yellow;
    [SerializeField] Color ColRed = Color.red;

    CannonControls Inp;
    InputAction AModF;

    void Awake()
    {
        Inp = new CannonControls();
        AModF = Inp.Canon.ModificarFuerza;
    }

    void OnEnable()
    {
        AModF.Enable();
    }

    void OnDisable()
    {
        AModF.Disable();
    }

    void Start()
    {
        if (SliderF != null)
        {
            SliderF.minValue = 0f;
            SliderF.maxValue = 1f;
        }

        AplicarFuerza();
        AplicarColor(true);
    }

    void Update()
    {
        if (SliderF == null)
        {
            return;
        }

        float mod = AModF.ReadValue<float>();
        SliderF.value -= mod * VelBarra * Time.deltaTime;
        SliderF.value = Mathf.Clamp(SliderF.value, SliderF.minValue, SliderF.maxValue);

        AplicarFuerza();
        AplicarColor(false);
    }

    void AplicarFuerza()
    {
        if (AdministradorJuego.Instancia == null || SliderF == null)
        {
            return;
        }

        float t = Mathf.InverseLerp(SliderF.minValue, SliderF.maxValue, SliderF.value);
        AdministradorJuego.Instancia.VelBala = Mathf.Lerp(FuerzaMin, FuerzaMax, t);
    }

    void AplicarColor(bool instantaneo)
    {
        if (FillImg == null || SliderF == null)
        {
            return;
        }

        float t = Mathf.InverseLerp(SliderF.minValue, SliderF.maxValue, SliderF.value);

        Color colObj;

        if (t <= 0.5f)
        {
            colObj = Color.Lerp(ColGreen, ColYellow, t / 0.5f);
        }
        else
        {
            colObj = Color.Lerp(ColYellow, ColRed, (t - 0.5f) / 0.5f);
        }

        if (instantaneo)
        {
            FillImg.color = colObj;
        }
        else
        {
            FillImg.color = Color.Lerp(FillImg.color, colObj, VelColor * Time.deltaTime);
        }
    }
}