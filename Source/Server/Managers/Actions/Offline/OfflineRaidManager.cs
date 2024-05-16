﻿using Shared;
using static Shared.CommonEnumerators;

namespace GameServer
{
    public static class OfflineRaidManager
    {
        public static void ParseRaidPacket(ServerClient client, Packet packet)
        {
            RaidData raidData = (RaidData)Serializer.ConvertBytesToObject(packet.contents);

            switch (raidData.raidStepMode)
            {
                case RaidStepMode.Request:
                    SendRequestedMap(client, raidData);
                    break;

                case RaidStepMode.Deny:
                    //Do nothing
                    break;
            }
        }

        private static void SendRequestedMap(ServerClient client, RaidData raidData)
        {
            if (!MapManager.CheckIfMapExists(raidData.targetTile))
            {
                raidData.raidStepMode = RaidStepMode.Deny;
                Packet packet = Packet.CreatePacketFromJSON(nameof(PacketHandler.RaidPacket), raidData);
                client.listener.EnqueuePacket(packet);
            }

            else
            {
                SettlementFile settlementFile = SettlementManager.GetSettlementFileFromTile(raidData.targetTile);

                if (UserManager.CheckIfUserIsConnected(settlementFile.owner))
                {
                    raidData.raidStepMode = RaidStepMode.Deny;
                    Packet packet = Packet.CreatePacketFromJSON(nameof(PacketHandler.RaidPacket), raidData);
                    client.listener.EnqueuePacket(packet);
                }

                else
                {
                    MapFileData mapData = MapManager.GetUserMapFromTile(raidData.targetTile);
                    raidData.mapData = Serializer.ConvertObjectToBytes(mapData);

                    Packet packet = Packet.CreatePacketFromJSON(nameof(PacketHandler.RaidPacket), raidData);
                    client.listener.EnqueuePacket(packet);
                }
            }
        }
    }
}
