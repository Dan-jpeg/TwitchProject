using UnityEngine;
using UnityEngine.AI;

public class PlayerMover : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject OpponentPoint;

    private void OnEnable()
    {
        EventManager.OnChatMessage += MovePlayer;
    }
    private void OnDisable()
    {
        EventManager.OnChatMessage -= MovePlayer;
    }

    PlayerEnter playerEnter;

    private void Start()
    {
        //playerEnter = PlayerEnter.Instance;
    }

    public void MovePlayer(string pChatter, string pMessage)
    {
        // сделать пул с игроками заранее

        // проверить сообщение 
        // вытащить из пула игрока с ником pChatter
        // подвинуть его в точку соответствующую pMessage
        // пусть игрок смотрит на точку через look at
        agent.SetDestination(OpponentPoint.transform.position);
    }

    public void GoDefendTreasury(string pChatter, string pMessage)
    {
        if (pMessage.Contains("!defend"))
        {

        }
    }
    public void GoForSteal(string pChatter, string pMessage)
    {
        if (pMessage.Contains("!attack"))
        {

        }
    }

    private void Update()
    {
        
    }
}
