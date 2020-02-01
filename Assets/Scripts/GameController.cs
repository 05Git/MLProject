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
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<HealthScript>().GetHeath() <= 0)
        {
            Victory(enemy, player);
        }
        else if (enemy.GetComponent<HealthScript>().GetHeath() <= 0)
        {
            Victory(player, enemy);
        }
    }

    public void Victory(GameObject winner, GameObject loser)
    {
        winner.GetComponent<StateScript>().SetNextState(StateScript.State.Win);
        loser.GetComponent<StateScript>().SetNextState(StateScript.State.Win);
    }
}
