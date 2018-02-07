using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCreator : MonoBehaviour{

    public GameObject backgroundPrefab;
    public GameObject colorerPrefab;
    public GameObject gamePrefab;
    public GameObject levelFinishPrefab;
    public GameObject levelStartPrefab;
    public GameObject playerPrefab;
    public GameObject wallPrefab;
    public GameObject cameraPrefab;
    public GameObject detectorPrefab;
    public GameObject rayPrefab;

    public Camera mainCamera;

    private MapData mapData;

	void Start () {
        mapData = MapSaveLoad.LoadMap();
        if (mapData != null)
        {
            foreach (MapElement element in mapData.GetElements())
            {
                switch (element.GetElementType())
                {
                    case MapElement.MapElementType.GAME:
                        CreateGame((MapGame)element);
                        break;
                    case MapElement.MapElementType.COLORER:
                        CreateColorer((MapElementColored)element);
                        break;
                    case MapElement.MapElementType.OBSTACLE:
                        CreateObstacle((MapObstacle)element);
                        break;
                    case MapElement.MapElementType.WALL:
                        CreateWall(element);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private GameObject GetChildWithTag(GameObject parent,string tag)
    {
        GameObject child;
        for(int i = 0; i < parent.transform.childCount; i++)
        {
            child = parent.transform.GetChild(i).gameObject;
            if (child.CompareTag(tag))
                return child;
        }
        return null;
    }

    private void SetGameObjectTransform(GameObject obj, MapElement element)
    {
        if (obj != null && element != null)
        {
            obj.transform.position = element.GetPosition();
            obj.transform.rotation = element.GetRotation();
            obj.transform.localScale = element.GetScale();
        }
    }

    private void ChangeGameObjectColor(GameObject obj, MapElementColored element)
    {
        if (obj != null && element != null && obj.GetComponent<ColorElement>() != null)
        {
            obj.GetComponent<ColorElement>().ChangeColor(element.GetColor());
        }
    }

    public void ModifyBackground(GameObject game, MapElement element)
    {
        GameObject background = GetChildWithTag(game, "MainBackground");
        SetGameObjectTransform(background, element);
    }

    public void CreateColorer(MapElementColored element)
    {
        GameObject colorer = Instantiate(colorerPrefab);
        SetGameObjectTransform(colorer, element);
        ChangeGameObjectColor(colorer, element);
    }

    public void CreateGame(MapGame element)
    {
        GameObject game = Instantiate(gamePrefab);
        SetGameObjectTransform(game, element);
        ModifyBackground(game, element.GetBackground());
        ModifyMainCamera(game);
        ModifyLevelFinish(game,element.GetLevelFinish());
        ModifyLevelStart(game, element.GetLevelStart());
        ModifyPlayer(game, element.GetPlayer());
        ModifyBeginCanvas(game);
        mainCamera.enabled = false;
        game.GetComponent<PauseScript>().levelName = mapData.GetLevelName();
    }

    public void ModifyBeginCanvas(GameObject game)
    {
        GameObject beginCanvas = GetChildWithTag(game, "BeginCanvas");
        beginCanvas.GetComponent<BeginScript>().mainCamera = mainCamera;
        beginCanvas.GetComponent<BeginScript>().levelName = mapData.GetLevelName();
    }

    public void ModifyMainCamera(GameObject game)
    {
        mainCamera.GetComponent<MainCameraController>().player = GetChildWithTag(game, "Player");
    }

    public void ModifyLevelFinish(GameObject game, MapLevelFinish element)
    {
        GameObject levelFinish = game.GetComponentInChildren<LevelFinish>().gameObject;
        SetGameObjectTransform(levelFinish, element);
        levelFinish.GetComponent<LevelFinish>().nextLevel = element.GetNextLevel();
    }

    public void ModifyLevelStart(GameObject game, MapLevelStart element)
    {
        GameObject levelStart = game.GetComponentInChildren<LevelStart>().gameObject;
        SetGameObjectTransform(levelStart, element);
        ChangeGameObjectColor(levelStart, element);
        LevelStart levelStartScript = levelStart.GetComponent<LevelStart>();
        levelStartScript.startX = element.GetStartX();
        levelStartScript.startY = element.GetStartY();
        levelStartScript.startRot = element.GetStartRot();
        levelStartScript.mainCamera = mainCamera;
    }

    public void CreateObstacle(MapObstacle element)
    {
        Debug.Log("Obstacle : " + element);
        switch (element.GetObstacleType())
        {
            case MapObstacle.MapObstacleType.CAMERA:
                CreateCamera((MapCamera)element);
                break;
            case MapObstacle.MapObstacleType.DETECTOR:
                CreateDetector((MapDetector)element);
                break;
            case MapObstacle.MapObstacleType.RAY:
                CreateRay((MapRay)element);
                break;
            default:
                break;
        }
    }

    public void CreateCamera(MapCamera element)
    {
        GameObject camera = Instantiate(cameraPrefab);
        SetGameObjectTransform(camera, element);
        ChangeGameObjectColor(camera, element);
        CameraController cameraController = camera.GetComponent<CameraController>();
        cameraController.degA = element.GetDegA();
        cameraController.degB = element.GetDegB();
        cameraController.rotSpeed = element.GetRotSpeed();
        cameraController.timeStop = element.GetTimeStop();
        cameraController.dir = element.GetDir();
    }

    public void CreateDetector(MapDetector element)
    {
        GameObject detector = Instantiate(detectorPrefab);
        SetGameObjectTransform(detector, element);
        GameObject detectorRay = detector.GetComponentInChildren<DetectorController>().gameObject;
        ChangeGameObjectColor(detectorRay, element);
        DetectorController detectorController = detectorRay.GetComponent<DetectorController>();
        detectorController.speed = element.GetSpeed();
        detectorController.timeStop = element.GetTimeStop();
    }

    public void CreateRay(MapRay element)
    {
        GameObject ray = Instantiate(rayPrefab);
        SetGameObjectTransform(ray, element);
        ChangeGameObjectColor(ray, element);
        RayController rayController = ray.GetComponent<RayController>();
        rayController.speed = element.GetSpeed();
        rayController.range = element.GetRange();
        rayController.timeStop = element.GetTimeStop();
        rayController.dir = element.GetDir();
    }

    public void ModifyPlayer(GameObject game, MapPlayer element)
    {
        GameObject player = game.GetComponentInChildren<PlayerController>().gameObject;
        SetGameObjectTransform(player, element);
        ChangeGameObjectColor(player, element);
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.speed = element.GetSpeed();
        playerController.decceleration = element.GetDecceleration();
        playerController.dead = element.GetDead();
        playerController.respawn = element.GetRespawn();
        playerController.finish = element.GetFinish();
        playerController.obstacleCollide = element.GetObstacleCollide();
        playerController.obstacleKill = element.GetObstacleKill();
        playerController.obstacle = element.GetObstacle();
    }

    public void CreateWall(MapElement element)
    {
        GameObject wall = Instantiate(wallPrefab);
        SetGameObjectTransform(wall, element);
    }
}
