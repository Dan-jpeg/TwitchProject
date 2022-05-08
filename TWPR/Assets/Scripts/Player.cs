using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void StatsChangeAction(int jobLvl, int wealthLvl, int pickpocketLvl, int team);
    public static event StatsChangeAction OnStatsChange;

    public delegate void TeamAction(float treasuryHp, float teamCash);
    public static event TeamAction OnTeamUpdate;

    public string playerName;
    public int jobLvl;
    public int wealthLvl;
    public int pickpocketLvl;
    public int team;

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





    private void OnEnable()
    {
        EventManager.OnChatMessage += Work;
        EventManager.OnChatMessage += Stay;
        EventManager.OnChatMessage += Attack;
    }
    private void OnDisable()
    {
        EventManager.OnChatMessage -= Work;
        EventManager.OnChatMessage -= Stay;
        EventManager.OnChatMessage -= Attack;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnTeamUpdate?.Invoke(5, 5);
            Debug.Log("EEEEEEEEEEEE");
            playerName = "NewName";
        }
    }

    public void Work(string pChatter, string pMessage)
    {
        if(pMessage =="!work" && playerName == pChatter)
        {
            jobLvl++;
            Debug.Log("I am working!");
        }
    }
    public void Stay(string pChatter, string pMessage)
    {
        if (pMessage == "!stay" && playerName == pChatter)
        {
            wealthLvl++;
            Debug.Log("I am staying!");
        }
    }
    public void Attack(string pChatter, string pMessage)
    {
        if (pMessage == "!attack" && playerName == pChatter)
        {
            pickpocketLvl++;
            Debug.Log("I am attacking!");
        }
    }
}
