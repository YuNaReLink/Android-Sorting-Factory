using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameManager", menuName = "GameManagerSO")]
public class GameManagerSO : ScriptableObject
{
    [Header("gamestart時の処理")]
    public Action IngameStart;
    public Action OutGame;

    [Header("フラグ管理")]
    public bool Ingameflg = false;
    public bool OutGameflg = false;

    //score
    public Action OnAddScore;

    //damage
    public Action<int> OnAddDamage;
    public Action MistakeDamage;

    //SE
    [Header("アンドロイド破壊")]
    public Action ScrapNormal;
    public Action ScrapDestoroyer;
    public Action ScrapBad;

    [Header("システム")]
    public Action CheckSE;

    [Header("レバー関連SE")]
    public Action PushLever;
    public Action PullLever;

}
