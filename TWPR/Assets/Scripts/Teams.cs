using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams : MonoBehaviour
{
    public float treasuryHP_A;
    [SerializeField]private float defaultTreasuryHP_A = 4;

    public float teamCash_A;
    [SerializeField] private float defaultTeamCash_A;

    public float treasuryHP_B;
    [SerializeField] private float defaultTreasuryHP_B = 4;

    public float teamCash_B;
    [SerializeField] private float defaultTeamCash_B;



    private void Start()
    {
        treasuryHP_A = defaultTreasuryHP_A;
        teamCash_A = defaultTeamCash_A;

        treasuryHP_B = defaultTreasuryHP_B;
        teamCash_B = defaultTeamCash_B;
    }
    private void OnEnable()
    {
        Player.OnTeamUpdate += UpdateTeamStats;
    }

    private void OnDisable()
    {
        Player.OnTeamUpdate -= UpdateTeamStats;
    }

    private void UpdateTeamStats(float hp, float cash, int team)
    {
        if (team == 0)
        {

        }
        else
        {

        }
    }
}
