using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelMainMenu;
    [SerializeField]
    private GameObject panelInGame;

    private AudioSource audioSource;

    public bool IsGameStart { private set; get; } = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GameStart()
    {
        IsGameStart = true;

        panelMainMenu.SetActive(false);
        panelInGame.SetActive(true);
        audioSource.Play();
    }
}
