using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerLevelup : MonoBehaviour
{
    private PlayerStats _stats;
    public XpTable xpTable;

    public int Level { get; private set; } = 0;
    public int XpForNextLevel { get; private set; } = 0;

    private void Start()
    {
        _stats = GetComponent<PlayerStats>();
        XpForNextLevel = xpTable.XpForLevel(1);

        StartCoroutine(HandleLevelup());
    }

    IEnumerator HandleLevelup()
    {
        var waitForEndOfFrame = new WaitForEndOfFrame();

        while (true)
        {
            if (_stats.currentXp >= XpForNextLevel)
            {
                Level += 1;
                _stats.currentXp -= XpForNextLevel;
                XpForNextLevel = xpTable.XpForLevel(Level + 1);
                yield return new WaitForSeconds(0.1f);
            }

            yield return waitForEndOfFrame;
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
