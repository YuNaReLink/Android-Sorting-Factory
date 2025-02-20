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

    //サイト参考
    [SerializeField] 
    private GameObject robot = null;
    //弾を保持（プーリング）する空のオブジェクト
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
    //サポーターの方が書いてくださったコードを残しています
    
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
            Debug.Log("一発アウト出現");
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
        //インターバルチェック
        if (spawnInterval > timeSinceLastSpawn) { return; }
        //敵のタイプをランダムに決定
        index = UnityEngine.Random.Range(0, androidLedger.AndroidLedgerInfos.Length);
        //一度スポーン選択可能フラグを無効に
        isSpawning = false;
        //タイプが一発アウトなら単体スポーン
        if (index == (int)AndroidType.VeryBad)
        {
            singleSpawn = true;
        }
        //そうじゃなければ
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
    ///　オブジェクトプールのロボットで使えるものがあれば有効化
    /// </summary>
    private void InstRobot(Vector3 pos, quaternion rot)
    {
        foreach(Transform t in robots)
        {
            if (!t.gameObject.activeSelf)
            {
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(pos, rot);
                //アクティブにする
                t.gameObject.SetActive(true);
                SetRobotType(t);
                isSpawning = true;
                return;
            }
         
        }

        //生成した時にrobotsの子にする
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
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(pos, rot);
                //アクティブにする
                t.gameObject.SetActive(true);
                SetRobotType(t);
                return;
            }

        }

        //生成した時にrobotsの子にする
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
//            Debug.LogWarning("ロボットのプレハブにRobotMoveが存在しません。");
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
