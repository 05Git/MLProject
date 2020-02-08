using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private float m_Timer = 60f;
    [SerializeField]
    private Text m_Timer_UI;
    private bool m_RoundStart;
    private bool m_RoundEnd;
    private float m_RoundStart_Timer;
    private float m_RoundEnd_Timer;
    private int m_CurrentRound = 1;

    public GameObject player;
    public GameObject enemy;
    public Vector3 playerStartPosition;
    public Quaternion playerStartRotation;
    public Vector3 enemyStartPosition;
    public Quaternion enemyStartRotation;
    public Text KO;
    public Text time;
    public Text playerWins;
    public Text enemyWins;
    public Text draw;
    public Text round;
    public Text fight;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        m_RoundStart = true;
        m_RoundEnd = false;
        m_RoundStart_Timer = 3f;
        m_RoundEnd_Timer = 5f;
        player.transform.SetPositionAndRotation(playerStartPosition, playerStartRotation);
        enemy.transform.SetPositionAndRotation(enemyStartPosition, enemyStartRotation);
        player.GetComponent<StateScript>().SetCurrentState(StateScript.State.Idle);
        enemy.GetComponent<StateScript>().SetCurrentState(StateScript.State.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_RoundStart && !m_RoundEnd)
        {
            m_Timer -= Time.deltaTime;
            if (m_Timer_UI != null && m_Timer >= 0f)
            {
                m_Timer_UI.text = string.Format("{0:N0}", m_Timer);
            }
            if (player.GetComponent<HealthScript>().GetHeath() <= 0f
                || enemy.GetComponent<HealthScript>().GetHeath() <= 0f)
            {
                if (KO != null)
                {
                    KO.gameObject.SetActive(true);
                }
                m_RoundEnd = true;
                RoundEnd();
            }
            else if (m_Timer <= 0f)
            {
                if (time != null)
                {
                    time.gameObject.SetActive(true);
                }
                m_RoundEnd = true;
                RoundEnd();
            }
        }
        else if (m_RoundStart)
        {
            if (m_RoundStart_Timer == 3f)
            {
                if (round != null)
                {
                    round.text = string.Format("Round {0}", m_CurrentRound);
                }
                round.gameObject.SetActive(true);
            }
            m_RoundStart_Timer -= Time.deltaTime;
            if (m_RoundStart_Timer <= 1f && m_RoundStart_Timer > 0f)
            {
                if (round != null)
                {
                    round.gameObject.SetActive(false);
                }
                if (fight != null)
                {
                    fight.gameObject.SetActive(true);
                }
            }
            else if (m_RoundStart_Timer <= 0f)
            {
                if (fight != null)
                {
                    fight.gameObject.SetActive(false);
                }
                m_RoundStart = false;
                player.GetComponent<StateScript>().SetCurrentState(StateScript.State.Idle);
                enemy.GetComponent<StateScript>().SetCurrentState(StateScript.State.Idle);
            }
        }
        else if (m_RoundEnd)
        {
            m_RoundEnd_Timer -= Time.deltaTime;
            if (m_RoundEnd_Timer <= 3f && m_RoundEnd_Timer > 1f)
            {
                if (KO != null)
                {
                    KO.gameObject.SetActive(false);
                }
                if (time != null)
                {
                    time.gameObject.SetActive(false);
                }
                if (player.GetComponent<StateScript>().GetCurrentState() == StateScript.State.Win)
                {
                    if (playerWins != null)
                    {
                        playerWins.gameObject.SetActive(true);
                    }
                }
                else if (enemy.GetComponent<StateScript>().GetCurrentState() == StateScript.State.Win)
                {
                    if (enemyWins != null)
                    {
                        enemyWins.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (draw != null)
                    {
                        draw.gameObject.SetActive(true);
                    }
                }
            }
            else if (m_RoundEnd_Timer <= 1f)
            {
                if (playerWins != null)
                {
                    playerWins.gameObject.SetActive(false);
                }
                if (enemyWins != null)
                {
                    enemyWins.gameObject.SetActive(false);
                }
                if (draw != null)
                {
                    draw.gameObject.SetActive(false);
                }

                if (player.GetComponent<CharController>().GetRoundsWon() == 3
                || enemy.GetComponent<CharController>().GetRoundsWon() == 3)
                {
                    if (player.GetComponent<CharController>().GetRoundsWon() > enemy.GetComponent<CharController>().GetRoundsWon())
                    {
                        SceneManager.LoadScene("Victory_Player");
                    }
                    else if (enemy.GetComponent<CharController>().GetRoundsWon() > player.GetComponent<CharController>().GetRoundsWon())
                    {
                        SceneManager.LoadScene("Victory_Enemy");
                    }
                    else
                    {
                        SceneManager.LoadScene("Victory_Draw");
                    }
                }
                else
                {
                    player.transform.SetPositionAndRotation(playerStartPosition, playerStartRotation);
                    enemy.transform.SetPositionAndRotation(enemyStartPosition, enemyStartRotation);
                    player.GetComponent<HealthScript>().SetHealth(100);
                    enemy.GetComponent<HealthScript>().SetHealth(100);
                    player.GetComponent<StateScript>().SetCurrentState(StateScript.State.Idle);
                    enemy.GetComponent<StateScript>().SetCurrentState(StateScript.State.Idle);

                    m_Timer = 60;
                    if (m_Timer_UI != null && m_Timer >= 0)
                    {
                        m_Timer_UI.text = string.Format("{0:N0}", m_Timer);
                    }
                    m_RoundStart = true;
                    m_RoundStart_Timer = 3;
                    m_RoundEnd = false;
                }
            }
        }
    }

    public void RoundEnd()
    {
        if (player.GetComponent<HealthScript>().GetHeath() <= 0
            || player.GetComponent<HealthScript>().GetHeath() <= enemy.GetComponent<HealthScript>().GetHeath())
        {
            player.GetComponent<StateScript>().SetCurrentState(StateScript.State.Lose);
        }
        else
        {
            player.GetComponent<StateScript>().SetCurrentState(StateScript.State.Win);
        }

        if (enemy.GetComponent<HealthScript>().GetHeath() <= 0
            || enemy.GetComponent<HealthScript>().GetHeath() <= player.GetComponent<HealthScript>().GetHeath())
        {
            enemy.GetComponent<StateScript>().SetCurrentState(StateScript.State.Lose);
        }
        else
        {
            enemy.GetComponent<StateScript>().SetCurrentState(StateScript.State.Win);
        }

        if (player.GetComponent<StateScript>().GetCurrentState() == StateScript.State.Win)
        {
            player.GetComponent<CharController>().AddRoundWin();
        }
        else if (enemy.GetComponent<StateScript>().GetCurrentState() == StateScript.State.Win)
        {
            enemy.GetComponent<CharController>().AddRoundWin();
        }
        else
        {
            player.GetComponent<CharController>().AddRoundWin();
            enemy.GetComponent<CharController>().AddRoundWin();
        }
        
        m_RoundEnd_Timer = 5f;
        m_CurrentRound++;
    }

    public bool GetRoundStart()
    {
        return m_RoundStart;
    }

    public bool GetRoundEnd()
    {
        return m_RoundEnd;
    }
}
