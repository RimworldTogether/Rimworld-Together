﻿using System;
using System.Collections.Generic;
using static Shared.CommonEnumerators;

namespace Shared
{
    [Serializable]
    public class VisitData
    {
        public VisitStepMode visitStepMode;

        public string visitorName;
        public string fromTile;
        public string targetTile;

        public List<byte[]> mapHumans = new List<byte[]>();
        public List<byte[]> mapAnimals = new List<byte[]>();

        public List<byte[]> caravanHumans = new List<byte[]>();
        public List<byte[]> caravanAnimals = new List<byte[]>();

        public List<string> pawnActionDefNames = new List<string>();
        public List<string> actionTargetA = new List<string>();
        public List<int> actionTargetIndex = new List<int>();
        public ActionTargetType[] actionTargetType = new ActionTargetType[0];

        public List<bool> isDrafted = new List<bool>();
        public List<string> positionSync = new List<string>();
        public List<int> rotationSync = new List<int>();

        public int mapTicks;
        public byte[] mapDetails;
        public List<string> mapMods = new List<string>();
    }
}
