﻿using System;

namespace Shared
{
    [Serializable]
    public class RoadValuesFile
    {
        public bool AllowDirtPath = true;
        public bool AllowDirtRoad = true;
        public bool AllowStoneRoad = true;
        public bool AllowAsphaltPath = true;
        public bool AllowAsphaltHighway = true;

        public int DirtPathCost;
        public int DirtRoadCost;
        public int StoneRoadCost;
        public int AsphaltPathCost;
        public int AsphaltHighwayCost;
    }
}
