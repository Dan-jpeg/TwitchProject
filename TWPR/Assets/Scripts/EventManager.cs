using UnityEngine;
using System.Net.Sockets;
using System.IO;
using TMPro;
using System;
using System.Collections;

public class EventManager : MonoBehaviour
{
    public delegate void ChatAction(string chatter,string message);
    public static event ChatAction OnChatMessage;

    public delegate void TimerEndAction(bool timerGoing);
    public static event TimerEndAction OnTimerEndTrigger;
    private void OnEnable()
    {
        PlayerEnter.OnTimerTrigger += BeginTimer;
    }
    private void OnDisable()
    {
        PlayerEnter.OnTimerTrigger -= BeginTimer;
    }

    #region Timer
    public static EventManager instance;

    public TMP_Text timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing = false;

    private float elapsedTime;
    #endregion

    //public UnityEvent<string, string> OnChatMessage;
    TcpClient Twitch;
    StreamReader Reader;
    StreamWriter Writer;

    const string URL = "irc.chat.twitch.tv";
    const int PORT = 6667;

    string User = "DropDaniel";
    // https://twitchapps.com/tmi/
    string OAuth = "oauth:ikmavy9d15z5u3bspj9txlf126yvet";
    string Channel = "DropDaniel";//Channel name

    float PingCounter = 0;
    private void ConnectToTwitch()
    {
        Twitch = new TcpClient(URL, PORT);
        Reader = new StreamReader(Twitch.GetStream());
        Writer = new StreamWriter(Twitch.GetStream());

        Writer.WriteLine("PASS " + OAuth);
        Writer.WriteLine("NICK " + User.ToLower());
        Writer.WriteLine("Join #" + Channel.ToLower());
        Writer.Flush();
    }

    private void Start()
    {
        timeCounter.text = "Time: 00:00";
        timerGoing = false;
    }

    public void BeginTimer(int timerTime, int playersCount)
    {
        if (playersCount>=1)
        {
            timerGoing = true;
            elapsedTime = timerTime;
            Debug.Log(playersCount);
            StartCoroutine(UpdateTimer());
        }
    }
    public void EndTimer()
    {
        OnTimerEndTrigger?.Invoke(timerGoing);
        timerGoing = false;
    }
    private IEnumerator UpdateTimer()
    {
        while (timerGoing && elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
        EndTimer();
    }

    private void Awake()
    {
        ConnectToTwitch();
        instance = this;
    }
    void Update()
    {
        PingCounter += Time.deltaTime;
        if(PingCounter > 60)
        {
            Writer.WriteLine("PING " + URL);
            Writer.Flush();
            PingCounter = 0;
        }
        if (!Twitch.Connected)
        {
            ConnectToTwitch();
        }
        if(Twitch.Available > 0)
        {
            string message = Reader.ReadLine();

            if (message.Contains("PRIVMSG"))
            {
                int splitPoint = message.IndexOf("!");
                string chatter = message.Substring(1, splitPoint - 1);

                splitPoint = message.IndexOf(":", 1);
                string msg = message.Substring(splitPoint + 1);

                OnChatMessage?.Invoke(chatter, msg);
            }
            print(message);
        }
    }
}
