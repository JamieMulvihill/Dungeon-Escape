using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// As there was a number of different enemy sounds for its different behaviours, a script was created that could control what had to be played

public class EnemySound : MonoBehaviour
{
    public AK.Wwise.Event enemySound = new AK.Wwise.Event(); // wwise event to play the enemy sounds

    // public function to set the switch to the needed switch value
    public void SetSwitchValue(AK.Wwise.Switch enemySwitch) {
        enemySwitch.SetValue(gameObject);
    }

    // function to play sound
    public void PlayEnemySound() {
        enemySound.Post(gameObject);
    }

    // function to stop sound
    public void StopEnemySound()
    {
        enemySound.Stop(gameObject);
    }
}
