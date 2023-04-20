using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMeGameManager : MonoBehaviour
{
    public GameObject currentSlime;

    //private Vector3 slimeInitSpawnPosition = new Vector3(1.3f, 1.3f, -4.5f);
    //private Quaternion slimeInitSpawnRotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {

        // Erzeuge eine Instanz des Slime-Prefabs
        //GameObject newSlime = Instantiate(slimePrefabs[0], Vector3.zero, Quaternion.identity);

        // Setze die Position des Slimes
        //newSlime.transform.position = slimeInitSpawnPosition;
        //newSlime.transform.rotation = slimeInitSpawnRotation;


        ChangeStateTo(SlimeAnimationState.Walk);
    }

    void Idle()
    {
        currentSlime.GetComponent<EnemyAi>().CancelGoNextDestination();
        ChangeStateTo(SlimeAnimationState.Idle);
    }

    public void ChangeStateTo(SlimeAnimationState state)
    {
        /* if (currentSlime == null) 
            return;

        if (state == currentSlime.GetComponent<EnemyAi>().currentState) 
            return; */

        Debug.Log("CHANGE STATE: " + state);

        currentSlime.GetComponent<EnemyAi>().currentState = state ;
    }
}
