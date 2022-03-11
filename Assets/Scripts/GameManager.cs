using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public float waitAfterDeath = 2f;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Removes the mouse while in-game
    }

    void Update()
    {
        
    }

    public void PlayerDeath() //controls player death
    {
        StartCoroutine(PlayerDiedCo());
    }

    public IEnumerator PlayerDiedCo() //when player dies (create coroutine)
    {
        yield return new WaitForSeconds(waitAfterDeath);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
