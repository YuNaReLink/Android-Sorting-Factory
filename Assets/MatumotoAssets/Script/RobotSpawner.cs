using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RobotSpawner : MonoBehaviour
{
    public static RobotSpawner main;

    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private GameObject robotPrefab;

    

    private bool isSpawning = false;
    private float timeSinceLastSpawn = 0;

    //サイト参考
    [SerializeField] GameObject robot = null;
    //弾を保持（プーリング）する空のオブジェクト
    Transform robots;

    List<GameObject> objectList = new List<GameObject>();


  // List<RobotManage> robotObjectList = new List<RobotManage>();  
  //サポーターの方が書いてくださったコードを残しています
    
    // Update is called once per frame
    private void Start()
    {     
      //  SetupPrefabs();
        StartCoroutine(StartWave());
       robots = new GameObject("RobotPut").transform;
        
    }
    private void Update()
    {
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

    //private void SetupPrefabs()
    //{
    //    for(int i = 0; i< robotMaxCount; i++)
    //    {
    //        var robot = new RobotManage(Instantiate(robotPrefab));
    //        robot.SetRobot(false);
    //    }
    //}

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
                return;
            }
         
        }

        //生成した時にrobotsの子にする
       Instantiate(robot, pos, rot,robots);
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
