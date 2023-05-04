using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMeUnitManager : MonoBehaviour
{
    public static ColorMeUnitManager Instance;

    [SerializeField]
    private GameObject[] slimeGameObjects;

    private void Awake() 
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitGame()
    {
        var slimeInstance = Instantiate(this.slimeGameObjects[0]);
        slimeInstance.SetActive(true);
        ColorMeGameManager.instance.UpdateGameState(GameState.MENU_PAUSE);
    }
}
