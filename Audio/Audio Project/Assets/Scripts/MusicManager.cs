using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is essentially state machine to control music states and what should be playing

public class MusicManager : MonoBehaviour
{
    public EnemyMovement enemy;
    public PlayerMovement player;
    
    public AK.Wwise.State calmState; // wwise States to control which music should be playing in the game
    public AK.Wwise.State loseState;
    public AK.Wwise.State winState;
    public AK.Wwise.State chasingState;
    public AK.Wwise.State escapingState;

    public AK.Wwise.Event gameMusic = new AK.Wwise.Event(); // wwise event to play the music
    public AK.Wwise.Bank gameBank = new AK.Wwise.Bank(); // wwise soundbank for the in game sounds
    bool changedState = false;

    private void Awake()
    {
        gameBank.Load(); // on awake load the sound bank
    }
    
    void Start()
    {
        calmState.SetValue(); // set the music state to be calm music
        gameMusic.Post(gameObject); // post the music event
    }

    void Update()
    {
        if (player.hasEscaped)
        {
            winState.SetValue(); // if the player has escaped set the music to the win state
        }
        else
        {
            if (player.isAlive && !player.isEscaping)
            {
                if (enemy.intruderDetected && !changedState)
                {
                    chasingState.SetValue(); // if the enemy has detected the player and the music hasnt changed from the calm state, set the music to chasing state
                    changedState = true;
                }

                if (changedState && !enemy.intruderDetected)
                {
                    calmState.SetValue(); // if the enemy has lost the player and the music is still on chasing, set music to calm state
                    changedState = false;
                }
            }
            else if (player.isAlive && player.isEscaping)
            {

                escapingState.SetValue(); // if the player has collected all the collectibles and is able to escpae, set the music to the escape state
            }
            else
            {
                loseState.SetValue(); // if the player has died, set the misuci to the lose state
            }
        }
    }
}
