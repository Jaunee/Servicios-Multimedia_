using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class UDP_Server : MonoBehaviour
{
    private UdpClient udp_server;
    private IPEndPoint RemoteEndPoint;

    public bool isServerRunning = false;

    public void StartUDPServer(int port)
    {
        udp_server = new UdpClient(port);
        RemoteEndPoint = new IPEndPoint(IPAddress.Any, port);
        Debug.Log("Server started. Waiting for messages...");
        udp_server.BeginReceive(ReceiveData,null);
        isServerRunning = true;
    }

     private void ReceiveData(IAsyncResult result)
    {

        byte[] receivedBytes = udp_server.EndReceive(result,ref RemoteEndPoint);
        string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);
        Debug.Log("Received from Client: " + receivedMessage);
        udp_server.BeginReceive(ReceiveData, null);
        
    }

     public void SendData(string message)
    {
        try{
        
          byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);
          udp_server.Send(sendBytes, sendBytes.Length, RemoteEndPoint);
           Debug.Log("Sent to client: " + message);
        }
         catch{

            Debug.Log("There is no client to send the message:"  + message);

        }
    
            
        
    }
}
