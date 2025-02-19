using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameManager", menuName = "GameManagerSO")]
public class GameManagerSO : ScriptableObject
{
    [Header("gamestart‚Ìˆ—")]
    public Action IngameStart;
    public Action OutGame;

    [Header("ƒtƒ‰ƒOŠÇ—")]
    public bool Ingameflg = false;
    public bool OutGameflg = false;

    //score
    public Action OnAddScore;

    //damage
    public Action<int> OnAddDamage;
    public Action MistakeDamage;
}
