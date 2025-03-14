using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

public class UDP_Client : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint RemoteEndPoint;
    public bool isServerConnected = false;


    public void StartUDPClient(string ipAddress,int port)

    {
        udpClient = new UdpClient();
        RemoteEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress),port);
        udpClient.BeginReceive(ReceiveData,null);
        SendData("Hello, Server!");
        isServerConnected = true;

    }

    private void  ReceiveData(IAsyncResult result)
    {
        byte[] receivedBytes = udpClient.EndReceive(result, ref RemoteEndPoint);
        string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);
        Debug.Log("receoved frpm server:" + receivedMessage);
        udpClient.BeginReceive(ReceiveData,null);
    }

     public void SendData(string message)
    {
    
          byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);
          udpClient.Send(sendBytes, sendBytes.Length, RemoteEndPoint);
        Debug.Log("Sent to client: " + message);

        
    }


}

