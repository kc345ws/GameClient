using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;

/// <summary>
/// 客户端的封装
/// </summary>
public class ClientPeer{
    private Socket clientSocket = null;

    public ClientPeer(string ip , int port)
    {
        databuffer = new byte[1024];
        dataCache = new List<byte>();
        msgQueue = new Queue<SocketMsg>();

        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
        try
        {
            clientSocket.Connect(IPAddress.Parse(ip), port);
            Debug.Log("连接服务器成功");

            startReceive();//开始接收数据
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
            throw;
        }
    }


    #region 接受数据
    /// <summary>
    /// 锁变量
    /// </summary>
    private bool isprocessReceive = false;

    /// <summary>
    /// 数据缓冲区
    /// </summary>
    private byte[] databuffer;

    private List<byte> dataCache;

    /// <summary>
    /// 存储的消息队列
    /// </summary>
    public Queue<SocketMsg> msgQueue { get; set; }

    /// <summary>
    /// 开始接受数据
    /// </summary>
    private void startReceive()
    {
        if (clientSocket == null && clientSocket.Connected)
        {
            Debug.Log("服务器连接失败，无法接受消息");
            return;
        }

        try
        {
            clientSocket.BeginReceive(databuffer, 0, 1024, SocketFlags.None, receiveCallback,clientSocket);
            //最后一个参数赋值给ar.AsyncState
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    /// <summary>
    /// 异步接收数据后的回调操作
    /// </summary>
    /// <param name="ar"></param>
    private void receiveCallback(IAsyncResult ar)
    {
        int length = clientSocket.EndReceive(ar);
        byte[] tmpbuffer = new byte[length];
        Buffer.BlockCopy(databuffer, 0, tmpbuffer, 0, length);

        dataCache.AddRange(tmpbuffer);

        if (!isprocessReceive)
        {
            processReceive();
        }
    }

    /// <summary>
    /// 处理接收到的数据
    /// </summary>
    private void processReceive()
    {
        isprocessReceive = true;

        byte[] data = EncodeTool.DecodeMessage(ref dataCache);

        if(data == null)
        {
            return;
        }

        SocketMsg msg = EncodeTool.DeCodeSocketMgr(data);
        msgQueue.Enqueue(msg);//保存到消息列表中

        //尾递归
        startReceive();
    }

    #endregion


    #region 发送数据

    public void SendMessage(int OpCode , int SubCode , object Value)
    {

        try
        {
            SocketMsg msg = new SocketMsg(OpCode, SubCode, Value);
            byte[] data = EncodeTool.EncodeSocketMgr(msg);
            byte[] packet = EncodeTool.EncodeMessage(data);

            clientSocket.Send(packet);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    #endregion
}
