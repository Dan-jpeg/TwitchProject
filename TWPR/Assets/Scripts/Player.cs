using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void StatsChangeAction(int jobLvl, int wealthLvl, int pickpocketLvl, int team);
    public static event StatsChangeAction OnStatsChange;

    public delegate void TeamAction(float treasuryHp, float teamCash, int team);
    public static event TeamAction OnTeamUpdate;

    public string playerName;
    public int jobLvl;
    public int wealthLvl;
    public int pickpocketLvl;
    public int team;

    public static int numberOfPlayers;

    public Player
        (string newName,
        int newJobLvl,
        int newWealthLvl,
        int newPickpocketLvl,
        int newTeam)
    {
        playerName = newName;
        jobLvl = newJobLvl;
        wealthLvl = newWealthLvl;
        pickpocketLvl = newPickpocketLvl;
        team = newTeam;
    }


    private void Start()
    {
        numberOfPlayers++;
    }
    private void OnEnable()
    {
        EventManager.OnTimerEndTrigger += CheckTimer;

        EventManager.OnChatMessage += Work;
        EventManager.OnChatMessage += Stay;
        EventManager.OnChatMessage += Attack;
    }
    private void OnDisable()
    {
        EventManager.OnTimerEndTrigger -= CheckTimer;

        EventManager.OnChatMessage -= Work;
        EventManager.OnChatMessage -= Stay;
        EventManager.OnChatMessage -= Attack;
    }

    private void Update()
    {

    }

    public void Work(string pChatter, string pMessage)
    {
        if(pMessage =="!work" && playerName == pChatter)
        {
            OnTeamUpdate?.Invoke(0, jobLvl, team);
            jobLvl++;
            Debug.Log("I am working!");
        }
    }
    public void Stay(string pChatter, string pMessage)
    {
        if (pMessage == "!stay" && playerName == pChatter)
        {
            OnTeamUpdate?.Invoke(wealthLvl, 0, team);
            wealthLvl++;
            Debug.Log("I am staying!");
        }
    }
    public void Attack(string pChatter, string pMessage)
    {
        if (pMessage == "!attack" && playerName == pChatter)
        {
            OnTeamUpdate?.Invoke(-pickpocketLvl, 0, 1 - team);
            pickpocketLvl++;
            Debug.Log("I am attacking!");
        }
    }

    public void CheckTimer()
    {

    }
}
