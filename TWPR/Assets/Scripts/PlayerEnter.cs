using System.Collections.Generic;
using UnityEngine;

public class PlayerEnter : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject spawnPoint;
    
    public List<Player> players = new List<Player>();
    public static List<GameObject> playersObjects = new List<GameObject>();

    public delegate void TimerAction(int timerTime, int playersCount);
    public static event TimerAction OnTimerTrigger;

    public delegate void TeamAction(float treasuryHp, float teamCash);
    public static event TeamAction OnTeamUpdate;

    #region Default Player Variables

    [SerializeField] private int jobLvl = 1;
    [SerializeField] private int wealthLvl = 1;
    [SerializeField] private int pickpocketLvl = 1;
    [SerializeField] private int teamSize = 2;

    #endregion




    private void UpdateTeams()
    {

    }

    #region Subscriptions
    private void OnEnable()
    {
        EventManager.OnChatMessage += JoinTheGame;
        //Player.OnStatsChange += UpdatePlayerStats;
    }
    private void OnDisable()
    {
        EventManager.OnChatMessage -= JoinTheGame;

        //Player.OnStatsChange += UpdatePlayerStats;
    }
    #endregion

    public void JoinTheGame(string pChatter, string pMessage)
    {
        if (pMessage == "!join") //pMessage.Contains("!join"))
        {

            if(players.Count > 0)
                /*
                
                    if there ARE players THEN
                    we check each one of them
                    if they have different names

                */
            {
                for (int i = 0; i <= players.Count; i++)
                {
                    if(players[i].name == pChatter)
                    {
                        Debug.Log("two or more players have the same name!");
                        break;
                    }
                    
                    else if(players.Count == i && players[i].name != pChatter)
                    {
                        bool timerIsNotGoing = true; // it makes it so timer only goes off once.
                        if (players.Count > 4 && timerIsNotGoing)
                        {
                            OnTimerTrigger?.Invoke(5, players.Count); // first you put seconds amount for timer, then amount of players
                            timerIsNotGoing = false;
                        }
                        /*

                            if a player is the last in the list
                            AND the name is correct
                            THEN we add him to the game

                        */
                        Debug.Log("players count = " + players.Count);


                        players.Add(new Player(pChatter, jobLvl, wealthLvl, pickpocketLvl, players.Count % teamSize));
                        Debug.Log("players count = " + players.Count);
                        CreatePlayerStats(players);
                    }
                }
            }
            else // if there are no players we add one
            {
                players.Add(new Player(pChatter, jobLvl, wealthLvl, pickpocketLvl, players.Count % teamSize));
                //playersObjects.Add(new GameObject(pChatter));

                CreatePlayerStats(players);

            }
        }

        Debug.Log("players count = " + players.Count);
    }

    public void CreatePlayerStats(List<Player> players)
    {
        GameObject NewPlayer = Instantiate(
            playerPrefab, 
            new Vector3(spawnPoint.transform.position.x + 2 * (players.Count - 1),spawnPoint.transform.position.y,spawnPoint.transform.position.z), 
            Quaternion.identity
            );

        
        NewPlayer.GetComponent<Player>().playerName = players[players.Count - 1].playerName;
        NewPlayer.GetComponent<Player>().jobLvl = players[players.Count - 1].jobLvl;
        NewPlayer.GetComponent<Player>().wealthLvl = players[players.Count - 1].wealthLvl;
        NewPlayer.GetComponent<Player>().pickpocketLvl = players[players.Count - 1].pickpocketLvl;
        NewPlayer.GetComponent<Player>().team = players[players.Count - 1].team;
    }
    
}
