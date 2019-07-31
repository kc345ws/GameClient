using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : ManagerBase {
    public static NetManager Instance = null;

    private ClientPeer client = null;

    public void Connected(string ip , int port)
    {
        client = new ClientPeer(ip, port);
    }

    void Awake()
    {
        Instance = null;
    }
    // Use this for initialization
    void Start () {
        Connected("127.0.0.1", 59800);
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
