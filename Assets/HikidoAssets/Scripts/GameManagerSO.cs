using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameManager", menuName = "GameManagerSO")]
public class GameManagerSO : ScriptableObject
{
    [Header("gamestart���̏���")]
    public Action IngameStart;
    public Action OutGame;

    [Header("�t���O�Ǘ�")]
    public bool Ingameflg = false;
    public bool OutGameflg = false;

    //score
    public Action OnAddScore;

    //damage
    public Action<int> OnAddDamage;
    public Action MistakeDamage;

    //SE
    [Header("�A���h���C�h�j��")]
    public Action ScrapNormal;
    public Action ScrapDestoroyer;
    public Action ScrapBad;

    [Header("�V�X�e��")]
    public Action CheckSE;

    [Header("���o�[�֘ASE")]
    public Action PushLever;
    public Action PullLever;

}
