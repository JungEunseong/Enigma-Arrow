using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;

class PacketManager
{
	#region Singleton
	static PacketManager _instance = new PacketManager();
	public static PacketManager Instance { get { return _instance; } }
	#endregion

	PacketManager()
	{
		Register();
	}

	Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>> _onRecv = new Dictionary<ushort, Action<PacketSession, ArraySegment<byte>, ushort>>();
	Dictionary<ushort, Action<PacketSession, IMessage>> _handler = new Dictionary<ushort, Action<PacketSession, IMessage>>();
		
	public Action<PacketSession, IMessage, ushort> CustomHandler { get; set; }

	public void Register()
	{		
		_onRecv.Add((ushort)MsgId.SMatchingRes, MakePacket<S_MatchingRes>);
		_handler.Add((ushort)MsgId.SMatchingRes, PacketHandler.S_MatchingResHandler);		
		_onRecv.Add((ushort)MsgId.PSpawnReq, MakePacket<P_SpawnReq>);
		_handler.Add((ushort)MsgId.PSpawnReq, PacketHandler.P_SpawnReqHandler);		
		_onRecv.Add((ushort)MsgId.PSpawnRes, MakePacket<P_SpawnRes>);
		_handler.Add((ushort)MsgId.PSpawnRes, PacketHandler.P_SpawnResHandler);		
		_onRecv.Add((ushort)MsgId.PMove, MakePacket<P_Move>);
		_handler.Add((ushort)MsgId.PMove, PacketHandler.P_MoveHandler);		
		_onRecv.Add((ushort)MsgId.PAttackReq, MakePacket<P_AttackReq>);
		_handler.Add((ushort)MsgId.PAttackReq, PacketHandler.P_AttackReqHandler);
	}

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer)
	{
		ushort count = 0;

		ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
		count += 2;
		ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
		count += 2;

		Action<PacketSession, ArraySegment<byte>, ushort> action = null;
		if (_onRecv.TryGetValue(id, out action))
			action.Invoke(session, buffer, id);
	}

	void MakePacket<T>(PacketSession session, ArraySegment<byte> buffer, ushort id) where T : IMessage, new()
	{
		T pkt = new T();
		pkt.MergeFrom(buffer.Array, buffer.Offset + 4, buffer.Count - 4);

		if (CustomHandler != null)
		{
			CustomHandler.Invoke(session, pkt, id);
		}
		else
		{
			Action<PacketSession, IMessage> action = null;
			if (_handler.TryGetValue(id, out action))
				action.Invoke(session, pkt);
		}
	}

	public Action<PacketSession, IMessage> GetPacketHandler(ushort id)
	{
		Action<PacketSession, IMessage> action = null;
		if (_handler.TryGetValue(id, out action))
			return action;
		return null;
	}
}