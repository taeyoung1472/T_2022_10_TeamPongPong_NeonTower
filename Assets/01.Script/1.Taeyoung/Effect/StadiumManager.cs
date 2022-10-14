using System;
using System.Collections.Generic;
using UnityEngine;

public class StadiumManager : MonoSingleTon<StadiumManager>
{
    [SerializeField] private StadiumMatch[] stadiumMatches;
    private Dictionary<BossType, Stadium> stadiumDic = new();

    public Stadium GetStadiumByType(BossType key)
    {
        if (stadiumDic.Count == 0)
        {
            foreach (StadiumMatch match in stadiumMatches)
            {
                stadiumDic.Add(match.type, match.stadium);
            }
        }
        return stadiumDic[key];
    }

    [Serializable]
    public class StadiumMatch
    {
        public BossType type;
        public Stadium stadium;
    }
}
public enum BossType
{
    Boss1,
    Boss2,
    Boss3,
    Boss4,
    Boss5,
}