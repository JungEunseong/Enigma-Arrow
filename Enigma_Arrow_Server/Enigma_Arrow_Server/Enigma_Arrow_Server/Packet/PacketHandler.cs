using Google.Protobuf;
using Server;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PacketHandler
{
    public static void C_TestHandler(PacketSession session, IMessage packet)
    {
        if (session == null) return;

        ClientSession CSession = session as ClientSession;

    }
}
