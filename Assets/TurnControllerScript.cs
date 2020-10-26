using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int players_number;
    public int turn;
    public int player_turn;

    void Start()
    {
        players_number = 4; //select the number of players
        turn = 1;
        player_turn=1; //loops between 1-players_number
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
