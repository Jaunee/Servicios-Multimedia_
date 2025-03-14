using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

using UnityEngine.UI;


public class TCPClient : MonoBehaviour
{

    private TcpClient tcpClient;
    private NetworkStream networksStream;
    private byte[] receiveBuffer;
    
    public bool isServerConnected;


  public void ConnectedToServer(string ipAddress, int port)
  
  {
        tcpClient = new TcpClient();
        tcpClient.Connect(IPAddress.Parse(ipAddress), port);
        networksStream = tcpClient.GetStream();
        receiveBuffer = new byte[tcpClient.ReceiveBufferSize];
        networksStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, ReceiveData,null);
        isServerConnected =true;
    
    }

     private void ReceiveData(IAsyncResult result)
    {
        int bytesRead = networksStream.EndRead(result);

        byte[] receivedBytes = new byte[bytesRead];
        Array.Copy(receiveBuffer, receivedBytes, bytesRead);
        string receivedMessage = System.Text.Encoding.UTF8.GetString(receivedBytes);
        Debug.Log("Received from client: "+ receivedMessage);
        networksStream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, ReceiveData, null);
    }  

    
    public void SendData(string message)
    {
        try{
            byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(message);
            networksStream.Write(sendBytes, 0 ,sendBytes.Length);
            networksStream.Flush();
            Debug.Log("Sent to server: " + message);
        }
        catch{

            Debug.Log("There is no server to send the message:"  + message);

        }
    }

    


  }

