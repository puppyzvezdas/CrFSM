using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public enum UserType : short
    {
        Manager     = 0x1,
        Worker      = 0x2,
    }

    public enum EntityType : short
    {
        User            = 0x1,
        CrewMember      = 0x2,
        Crew            = 0x3,
        Assignment      = 0x4,
        Shift           = 0x5,
        WorkingDay      = 0x6,
        NonWorkingDay   = 0x7,
        Vacation        = 0x8,
    }

    public enum Skills : short
    {
        OperateInSubstation         = 0x1,
        OperateOutOfSubstation      = 0x2,
        PerformPlannedWork          = 0x3,
        PerformSwitching            = 0x4,
        PerformUnplannedWork        = 0x5,
    }

    public enum AvaliabilityState : short
    {
        Avaliable       = 0x1,
        NotAvaliable    = 0x2,
        StandBy         = 0x3,
    }
}
