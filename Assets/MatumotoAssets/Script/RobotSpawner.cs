using Kusume;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    public static RobotSpawner main;

    [SerializeField] 
    private float spawnInterval = 1f;
    [SerializeField]
    private float consecutiveSpawnInterval;
    private Timer consecutiveSpawnIntervalTimer = new Timer();
    [SerializeField]
    private int consecutiveSpawnCount = 1;

    private int minSpawnCount = 1;
    private int maxSpawnCount = 6;

    private bool singleSpawn = false;


    [SerializeField] 
    private GameObject robotPrefab;

    

    private bool isSpawning = false;
    private float timeSinceLastSpawn = 0;

    private int index = 0;

    //�T�C�g�Q�l
    [SerializeField] 
    private GameObject robot = null;
    //�e��ێ��i�v�[�����O�j�����̃I�u�W�F�N�g
    private Transform robots;
    public Transform RobotsParent => robots;

    private List<GameObject> objectList = new List<GameObject>();
    
    [SerializeField]
    private Kusume.AndroidLedger androidLedger;


    [SerializeField]
    private float androidSpeed;

    [SerializeField]
    private bool moveflag;
    public void SetMoveFlag(bool stop) {  moveflag = stop; }


    // List<RobotManage> robotObjectList = new List<RobotManage>();  
    //�T�|�[�^�[�̕��������Ă����������R�[�h���c���Ă��܂�
    
    // Update is called once per frame
    private void Start()
    {     
      //  SetupPrefabs();
        StartCoroutine(StartWave());
       robots = new GameObject("RobotPut").transform;

        moveflag = true;

        androidSpeed = GameLevelManager.AndroidSpeeds[(int)GameLevelManager.GameLevel];

        spawnInterval = GameLevelManager.SpawnInterval[(int)GameLevelManager.GameLevel];
    }
    private void Update()
    {
        if (!moveflag) { return; }
        timeSinceLastSpawn += Time.deltaTime;
        consecutiveSpawnIntervalTimer.Update(Time.deltaTime);
        if (!isSpawning)
        {
            Spawn();
        }
        else
        {
            SpawnCheck();
        }
    }

    private void Spawn()
    {
        if (singleSpawn)
        {
            Debug.Log("�ꔭ�A�E�g�o��");
            InstRobot(transform.position, transform.rotation);
            timeSinceLastSpawn = 0;
            spawnInterval = 2.5f;
            singleSpawn = false;
            isSpawning = true;
        }
        else
        {
            if (!consecutiveSpawnIntervalTimer.IsEnd()) { return; }
            InstConsecutiveRobot(transform.position, transform.rotation);
            consecutiveSpawnIntervalTimer.Start(consecutiveSpawnInterval);
            consecutiveSpawnCount--;
            if(consecutiveSpawnCount <= 0)
            {
                isSpawning = true;
                timeSinceLastSpawn = 0;
                spawnInterval = GameLevelManager.SpawnInterval[(int)GameLevelManager.GameLevel];
            }
        }
    }

    private void SpawnCheck()
    {
        //�C���^�[�o���`�F�b�N
        if (spawnInterval > timeSinceLastSpawn) { return; }
        //�G�̃^�C�v�������_���Ɍ���
        index = UnityEngine.Random.Range(0, androidLedger.AndroidLedgerInfos.Length);
        //��x�X�|�[���I���\�t���O�𖳌���
        isSpawning = false;
        //�^�C�v���ꔭ�A�E�g�Ȃ�P�̃X�|�[��
        if (index == (int)AndroidType.VeryBad)
        {
            singleSpawn = true;
        }
        //��������Ȃ����
        else
        {
            singleSpawn = false;
            consecutiveSpawnCount = UnityEngine.Random.Range(minSpawnCount, maxSpawnCount);
        }
    }


    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(spawnInterval);
        isSpawning = true;
    }

    /// <summary>
    ///�@�I�u�W�F�N�g�v�[���̃��{�b�g�Ŏg������̂�����ΗL����
    /// </summary>
    private void InstRobot(Vector3 pos, quaternion rot)
    {
        foreach(Transform t in robots)
        {
            if (!t.gameObject.activeSelf)
            {
                //��A�N�e�B�u�ȃI�u�W�F�N�g�̈ʒu�Ɖ�]��ݒ�
                t.SetPositionAndRotation(pos, rot);
                //�A�N�e�B�u�ɂ���
                t.gameObject.SetActive(true);
                SetRobotType(t);
                isSpawning = true;
                return;
            }
         
        }

        //������������robots�̎q�ɂ���
       GameObject r = Instantiate(robot, pos, rot,robots);
       SetRobotType(r.transform);
       isSpawning = true;
    }

    private void InstConsecutiveRobot(Vector3 pos, quaternion rot)
    {
        foreach (Transform t in robots)
        {
            if (!t.gameObject.activeSelf)
            {
                //��A�N�e�B�u�ȃI�u�W�F�N�g�̈ʒu�Ɖ�]��ݒ�
                t.SetPositionAndRotation(pos, rot);
                //�A�N�e�B�u�ɂ���
                t.gameObject.SetActive(true);
                SetRobotType(t);
                return;
            }

        }

        //������������robots�̎q�ɂ���
        GameObject r = Instantiate(robot, pos, rot, robots);
        SetRobotType(r.transform);
    }

    private void SetRobotType(Transform t)
    {
        Kusume.AndroidTypeController controller = t.GetComponent<Kusume.AndroidTypeController>();
        index = UnityEngine.Random.Range(0, androidLedger.AndroidLedgerInfos.Length);
        controller.ChangeType(androidLedger.AndroidLedgerInfos[index]);

        RobotMove robotMove = controller.GetComponent<RobotMove>();
        robotMove.SetMoveSpeed(androidSpeed);
    }
}

//public class RobotManage
//{
//    public GameObject RobotGameObejct;
//    public RobotMove RobotMove;
//    public bool IsActive;

//    public RobotManage(GameObject robot)
//    {
//        RobotGameObejct = robot;
//        IsActive = false;

//        if (!RobotGameObejct.TryGetComponent<RobotMove>(out RobotMove))
//        {
//            Debug.LogWarning("���{�b�g�̃v���n�u��RobotMove�����݂��܂���B");
//            return;
//        }

//        RobotMove.OnFinishEvent += SetRobot;
//    }

//    ~RobotManage()
//    {
//        if (RobotMove == null) return;
//        RobotMove.OnFinishEvent -= SetRobot;
//    }

//    public void SetRobot(bool isUse)
//    {
//        if(isUse)
//        {
//            RobotGameObejct.SetActive(true);
//            IsActive = true;
//        }
//        else
//        {
//            RobotGameObejct.SetActive (false);
//            IsActive = false;
//        }
//    }
//}
