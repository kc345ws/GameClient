using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 网络消息转发中心
/// </summary>
public class NetManager : ManagerBase {
    public static NetManager Instance = null;

    private ClientPeer client = null;

    public override void Execute(int eventcode, object message)
    {
        
        switch (eventcode)
        {
            case NetEvent.SENDMSG:
                client.SendMessage(message as SocketMsg);
                break;
        }
    }

    public NetManager()
    {
        client = new ClientPeer("127.0.0.1", 59800);  
    }

    void Awake()
    {
        Instance = this;

        Add(NetEvent.SENDMSG, this);
        Add(NetEvent.RECEIVEMSG, this);
    }
    // Use this for initialization
    void Start () {
        client.Connect();
	}
	
	// Update is called once per frame
	void Update () {
		if(client.msgQueue.Count <= 0)
        {
            return;
        }

        while(client.msgQueue.Count > 0)
        {
            SocketMsg msg = client.msgQueue.Dequeue();
            //TODO 处理数据
        }
	}
}
