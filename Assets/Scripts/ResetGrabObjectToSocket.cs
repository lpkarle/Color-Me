using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class ResetGrabObjectToSocket : MonoBehaviour
{
    [SerializeField]
    Transform SocketAttachpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    public async void resetToAttachpoint()
    {
        await Delay(3000);
        this.transform.position = SocketAttachpoint.position;
        this.transform.rotation = SocketAttachpoint.rotation;

    }

    private async Task Delay(int milliseconds)
    {
        Debug.Log("START DELAY");

        await Task.Delay(milliseconds);
    }
}
