using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
    }

    [Header("UI오브젝트")]
    public UImanager uIManager;


    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }
    private Player _player;

}
