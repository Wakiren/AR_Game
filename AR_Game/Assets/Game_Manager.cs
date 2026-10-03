using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public int money = 20;
    private static Game_Manager _instance;

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
        DontDestroyOnLoad(gameObject);
    }
}
