using UnityEngine;
using  TMPro;
using UnityEngine.UI;


public class UDP_SERVERUI : MonoBehaviour
{
    public int serverPort = 5555;
    [SerializeField] private UDP_Server _server;
    [SerializeField] private TMP_InputField messageInput;


     public void SendServerMessage()
    {
        if(!_server.isServerRunning)
        {
            Debug.Log("The client is not connected");
            return;
        }

        if(messageInput.text ==""){
            Debug.Log("The chat entry is empty");
            return;
        }


        string message = messageInput.text;
        _server.SendData(message);
    }

    public void StartServer()
    {
        _server.StartUDPServer(serverPort);
    }

}
