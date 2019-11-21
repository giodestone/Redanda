using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public struct PlayerStat
{
    public bool IsTyping
    {
        get; set;
    }

    public int Ammunition
    {
        get; set;
    }

    public int MaxAmmuntion
    {
        get
        {
            return 36;
        }
    }

    public bool IsAlive
    {
        get; set;
    }
}
