
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "XP Table", menuName = "Data/Xp Table", order = 1)]
public class XpTable : ScriptableObject
{
    [Serializable]
    struct Entry
    {
        public int level;
        public int increase;
    }

    [SerializeField]
    Entry[] xpTable = new[]
    {
        new Entry { level = 0, increase = 5 }
    };

    public int XpForLevel(int level)
    {
        int xp = 0;

        int nextEntryIndex = Mathf.Min(1, xpTable.Length - 1);
        Entry entry = xpTable[0];
        Entry nextEntry = xpTable[nextEntryIndex];

        for (int i = 0; i < level; i++)
        {
            if (i >= nextEntry.level)
            {
                if (nextEntryIndex + 1 < xpTable.Length)
                {
                    nextEntryIndex += 1;
                }

                entry = nextEntry;
                nextEntry = xpTable[nextEntryIndex];
            }

            xp += entry.increase;
        }

        return xp;
    }

    public int debugLevel = 1;

    [ContextMenu("Debug Calculation")]
    public void CalculateDebugLevel()
    {
        var xp = XpForLevel(debugLevel);
        Debug.LogFormat("Level {0} to {1} takes {2} xp", debugLevel - 1, debugLevel, xp);
    }
}
