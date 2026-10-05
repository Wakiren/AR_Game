using TMPro;
using UnityEngine;
using UnityEngine.XR.Templates.AR;

public class Game_Manager : MonoBehaviour
{
    public int money = 20;
    private static Game_Manager _instance;

    [Header("Pantallas")]
    [SerializeField] private GameObject pantallaMenu;
    [SerializeField] private GameObject pantallaInfo;
    [SerializeField] private GameObject pantallaVictoria;
    [SerializeField] private GameObject pantallaDerrota;

    [Header("Gameplay")]
    [SerializeField] private GoalManager goalManager;
    [SerializeField] private GameObject[] interfazJuego;
    [SerializeField] private TextMeshProUGUI textoDinero;

    [Header("Victoria")]
    [SerializeField] private float segundosParaGanar = 60f;

    private int dineroInicial;
    private bool[] estadoInicialInterfaz;
    public float tiempoAguantado;
    private bool nucleoColocado;
    private bool partidaTerminada;

    public static Game_Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindFirstObjectByType<Game_Manager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        dineroInicial = money;

        // Se guarda cómo empieza la interfaz del juego para poder dejarla igual al volver al menú
        estadoInicialInterfaz = new bool[interfazJuego.Length];
        for (int i = 0; i < interfazJuego.Length; i++)
        {
            estadoInicialInterfaz[i] = interfazJuego[i].activeSelf;
        }

        Time.timeScale = 1f;
        MostrarMenu();
    }

    void Update()
    {
        textoDinero.text = "Dinero: " + money;

        // El tiempo solo cuenta desde que el núcleo está colocado
        if (!nucleoColocado || partidaTerminada) return;

        tiempoAguantado += Time.deltaTime;
        if (tiempoAguantado >= segundosParaGanar)
        {
            Ganar();
        }
    }

    // Botón BACK (y estado inicial al abrir el juego)
    public void MostrarMenu()
    {
        pantallaMenu.SetActive(true);
        pantallaInfo.SetActive(false);
        pantallaVictoria.SetActive(false);
        pantallaDerrota.SetActive(false);
        textoDinero.gameObject.SetActive(false);
    }

    // Botón INFO
    public void MostrarInfo()
    {
        pantallaMenu.SetActive(false);
        pantallaInfo.SetActive(true);
    }

    // Botón PLAY
    public void Jugar()
    {
        pantallaMenu.SetActive(false);
        pantallaInfo.SetActive(false);
        textoDinero.gameObject.SetActive(true);

        money = dineroInicial;
        tiempoAguantado = 0f;
        nucleoColocado = false;
        partidaTerminada = false;
        Time.timeScale = 1f;

        // Arranca el gameplay de la plantilla AR (detectar superficies, colocar objetos...)
        goalManager.StartCoaching();
    }

    // Botón MENU de la pantalla de victoria
    public void VolverAlMenu()
    {
        // Se quita todo lo que se ha colocado o creado durante la partida
        foreach (Enemy enemigo in FindObjectsByType<Enemy>(FindObjectsSortMode.None)) Destroy(enemigo.gameObject);
        foreach (Tower torre in FindObjectsByType<Tower>(FindObjectsSortMode.None)) Destroy(torre.gameObject);
        foreach (Nucleus nucleo in FindObjectsByType<Nucleus>(FindObjectsSortMode.None)) Destroy(nucleo.gameObject);
        foreach (Projectile bala in FindObjectsByType<Projectile>(FindObjectsSortMode.None)) Destroy(bala.gameObject);

        for (int i = 0; i < interfazJuego.Length; i++)
        {
            interfazJuego[i].SetActive(estadoInicialInterfaz[i]);
        }

        Time.timeScale = 1f;
        MostrarMenu();
    }

    // Lo llama Nucleus al aparecer en la escena
    public void NucleoColocado()
    {
        nucleoColocado = true;
    }

    public void Ganar()
    {
        TerminarPartida(pantallaVictoria);
    }

    // Lo llama Nucleus cuando los enemigos lo destruyen
    public void Perder()
    {
        TerminarPartida(pantallaDerrota);
    }

    private void TerminarPartida(GameObject pantalla)
    {
        if (partidaTerminada) return;
        partidaTerminada = true;

        foreach (GameObject objeto in interfazJuego)
        {
            if (objeto != null) objeto.SetActive(false);
        }

        pantalla.SetActive(true);
        Time.timeScale = 0f;
    }
}
