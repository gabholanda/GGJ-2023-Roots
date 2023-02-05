using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuManager : MonoBehaviour
{
    public GameObject deathMenu;
    GameObject player;
    private void Awake()
    {
        deathMenu.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Stats>().OnDeath += TurnOnDeathMenu;
    }

    public void TurnOnDeathMenu()
    {
        deathMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        player.GetComponent<Stats>().OnDeath -= TurnOnDeathMenu;
    }
}
