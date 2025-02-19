using Kusume;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RobotSpawner : MonoBehaviour
{
    public static RobotSpawner main;

    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private GameObject robotPrefab;

    

    private bool isSpawning = false;
    private float timeSinceLastSpawn = 0;

    //�T�C�g�Q�l
    [SerializeField] GameObject robot = null;
    //�e��ێ��i�v�[�����O�j�����̃I�u�W�F�N�g
    Transform robots;
    public Transform RobotsParent => robots;

    List<GameObject> objectList = new List<GameObject>();
    
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
        if (!isSpawning) { return; }
        if (spawnInterval < timeSinceLastSpawn)
        {
            InstRobot(transform.position, transform.rotation);
            timeSinceLastSpawn = 0;
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
                return;
            }
         
        }

        //������������robots�̎q�ɂ���
       GameObject r = Instantiate(robot, pos, rot,robots);
       SetRobotType(r.transform);
    }

    private void SetRobotType(Transform t)
    {
        Kusume.AndroidTypeController controller = t.GetComponent<Kusume.AndroidTypeController>();
        int num = UnityEngine.Random.Range(0, androidLedger.AndroidLedgerInfos.Length);
        controller.ChangeType(androidLedger.AndroidLedgerInfos[num]);

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
