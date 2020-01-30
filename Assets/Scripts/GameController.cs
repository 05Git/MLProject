using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<StateScript>().GetCurrentState() == StateScript.State.Win
            && enemy.GetComponent<StateScript>().GetCurrentState() == StateScript.State.Lose)
        {
            Victory(player, enemy);
        }
        else if (player.GetComponent<StateScript>().GetCurrentState() == StateScript.State.Lose
            && enemy.GetComponent<StateScript>().GetCurrentState() == StateScript.State.Win)
        {
            Victory(enemy, player);
        }
    }

    public void Victory(GameObject winner, GameObject loser)
    {
        winner.GetComponent<StateScript>().SetState(StateScript.State.Win);
        loser.GetComponent<StateScript>().SetState(StateScript.State.Lose);
    }
}
