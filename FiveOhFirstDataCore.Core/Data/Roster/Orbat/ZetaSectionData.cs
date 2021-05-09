﻿using FiveOhFirstDataCore.Core.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveOhFirstDataCore.Core.Data.Roster
{
    public class ZetaSectionData : IAssignable<Trooper>
    {
        public Trooper Commander { get; set; }
        public Trooper Subordinate { get; set; }
        public ZetaSquadData[] Squads { get; private set; } = new ZetaSquadData[] { new(), new(), new() };

        public void Assign(Trooper item)
        {
            var val = (int)item.Slot % 10;
            if(val == 0)
            {
                switch(item.Role)
                {
                    case Role.Commander:
                        Commander = item;
                        break;
                    case Role.Subordiante:
                        Subordinate = item;
                        break;
                }
            }
            else
            {
                Squads[val - 1].Assign(item);
            }
        }
    }

    public class ZetaUTCSectionData : IAssignable<Trooper>
    {
        public Trooper Commander { get; set; }
        public Trooper Subordinate { get; set; }
        public ZetaUTCSquadData[] Squads { get; private set; } = new ZetaUTCSquadData[] { new(), new(), new(), new() };

        public void Assign(Trooper item)
        {
            var val = (int)item.Slot % 10;
            if (val == 0)
            {
                switch (item.Role)
                {
                    case Role.Commander:
                        Commander = item;
                        break;
                    case Role.Subordiante:
                        Subordinate = item;
                        break;
                }
            }
            else
            {
                Squads[val - 1].Assign(item);
            }
        }
    }
}
