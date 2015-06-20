using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

   public struct Player
    {
       public Player(string p_name,Color p_colour, GameObject p_player)
       {
           score = 0;
           name = p_name;
           colour = p_colour;
           player_go = p_player;
       }
        int score;
        string name;
        Color colour;
        GameObject player_go;
    }
    public static List<Player> Players = new List<Player>();
    public static GameObject addPlayer(string name, Color colour)
    {
        GameObject newPlayer = PhotonNetwork.Instantiate("player", Vector3.zero, Quaternion.identity, 0);
        newPlayer.GetComponent<PlayerController>().controllable = true;
        newPlayer.GetComponent<PlayerController>().setColour(colour);
        Players.Add(new Player(name, colour, newPlayer));
        return newPlayer;
    }
}
