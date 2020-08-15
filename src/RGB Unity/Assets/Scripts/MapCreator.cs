using Cinemachine;
using Objects;
using Objects.Map;
using UnityEngine;

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

    public CinemachineVirtualCamera mainCamera;
    public GameObject levelContainer;

    private string _levelName = "Level1E";

    private MapData _mapData;

    private void Awake()
    {
        var levelSelector = GameObject.FindGameObjectWithTag("LevelSelector");
        if (levelSelector)
        {
            _levelName = levelSelector.name;
            Destroy(levelSelector);
        }
    }

    private void Start () {
        CreateLevel(_levelName);
    }

    private void CreateLevel(string levelName)
    {
        Debug.Log("Creating level : " + levelName);
        _mapData = MapSaveLoad.LoadMap(levelName);
        Debug.Log(_mapData);
        if (_mapData != null)
        {
            foreach (var element in _mapData.GetElements())
            {
                switch (element.type)
                {
                    case MapElement.MapElementType.Game:
                        CreateGame((MapGame)element);
                        break;
                    case MapElement.MapElementType.Colorer:
                        CreateColorer((MapElementColored)element);
                        break;
                    case MapElement.MapElementType.Obstacle:
                        CreateObstacle((MapObstacle)element);
                        break;
                    case MapElement.MapElementType.Wall:
                        CreateWall(element);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void ChangeLevel(string levelName)
    {
        foreach(Transform child in levelContainer.transform)
        {
            Destroy(child.gameObject);
        }
        mainCamera.GetComponent<MainCameraController>().enabled = false;
        CreateLevel(levelName);
    }

    private GameObject GetChildWithTag(GameObject parent,string childTag)
    {
        GameObject child;
        for(var i = 0; i < parent.transform.childCount; i++)
        {
            child = parent.transform.GetChild(i).gameObject;
            if (child.CompareTag(childTag))
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
            obj.GetComponent<ColorElement>().ChangeColor(element.ColorSo);
        }
    }

    public void ModifyBackground(GameObject game, MapElement element)
    {
        var background = GetChildWithTag(game, "MainBackground");
        SetGameObjectTransform(background, element);
    }

    public void CreateColorer(MapElementColored element)
    {
        var colorer = Instantiate(colorerPrefab, levelContainer.transform);
        SetGameObjectTransform(colorer, element);
        ChangeGameObjectColor(colorer, element);
    }

    public void CreateGame(MapGame element)
    {
        var game = Instantiate(gamePrefab, levelContainer.transform);
        SetGameObjectTransform(game, element);
        ModifyBackground(game, element.background);
        ModifyMainCamera(game);
        ModifyLevelFinish(game,element.levelFinish);
        ModifyLevelStart(game, element.levelStart);
        ModifyPlayer(game, element.player);
        ModifyBeginCanvas(game);
        mainCamera.enabled = false;
        ModifyBeginCamera(game);
    }

    public void ModifyBeginCamera(GameObject game)
    {
        var mainBackground = GetChildWithTag(game, "MainBackground");
        var localScale = mainBackground.transform.localScale;
        var scaleX = localScale.x / 2.49f;
        var scaleY = localScale.y / 2f;
        var maxScale = Mathf.Max(scaleX, scaleY) * 10;
        Debug.Log("Max scale : " + maxScale);
        Camera.main.orthographicSize = maxScale;
        var beginCamera = GameObject.Find("BeginCamera");
        beginCamera.transform.position = mainBackground.transform.position;
    }

    public void ModifyBeginCanvas(GameObject game)
    {
        var beginCanvas = GetChildWithTag(game, "BeginCanvas");
        beginCanvas.GetComponent<BeginScript>().mainCamera = mainCamera;
        beginCanvas.GetComponent<BeginScript>().levelName = _mapData.GetLevelName();
    }

    public void ModifyMainCamera(GameObject game)
    {
        mainCamera.GetComponent<MainCameraController>().player = GetChildWithTag(game, "Player");
        mainCamera.GetComponent<MainCameraController>().enabled = true;
    }

    public void ModifyLevelFinish(GameObject game, MapLevelFinish element)
    {
        var levelFinish = game.GetComponentInChildren<LevelFinish>().gameObject;
        SetGameObjectTransform(levelFinish, element);
        levelFinish.GetComponent<LevelFinish>().nextLevel = element.nextLevel;
    }

    public void ModifyLevelStart(GameObject game, MapLevelStart element)
    {
        var levelStart = game.GetComponentInChildren<LevelStart>().gameObject;
        SetGameObjectTransform(levelStart, element);
        ChangeGameObjectColor(levelStart, element);
        var levelStartScript = levelStart.GetComponent<LevelStart>();
        levelStartScript.startX = element.startX;
        levelStartScript.startY = element.startY;
        levelStartScript.startRot = element.startRot;
    }

    public void CreateObstacle(MapObstacle element)
    {
        Debug.Log("Obstacle : " + element);
        switch (element.obstacleType)
        {
            case MapObstacle.MapObstacleType.Camera:
                CreateCamera((MapCamera)element);
                break;
            case MapObstacle.MapObstacleType.Detector:
                CreateDetector((MapDetector)element);
                break;
            case MapObstacle.MapObstacleType.Ray:
                CreateRay((MapRay)element);
                break;
            default:
                break;
        }
    }

    public void CreateCamera(MapCamera element)
    {
        var camera = Instantiate(cameraPrefab, levelContainer.transform);
        SetGameObjectTransform(camera, element);
        ChangeGameObjectColor(camera, element);
        var cameraController = camera.GetComponent<CameraController>();
        cameraController.degA = element.degA;
        cameraController.degB = element.degB;
        cameraController.rotSpeed = element.rotSpeed;
        cameraController.timeStop = element.timeStop;
        cameraController.dir = element.dir;
    }

    public void CreateDetector(MapDetector element)
    {
        var detector = Instantiate(detectorPrefab, levelContainer.transform);
        SetGameObjectTransform(detector, element);
        var detectorRay = detector.GetComponentInChildren<DetectorController>().gameObject;
        ChangeGameObjectColor(detectorRay, element);
        var detectorController = detectorRay.GetComponent<DetectorController>();
        detectorController.speed = element.speed;
        detectorController.timeStop = element.timeStop;
    }

    public void CreateRay(MapRay element)
    {
        var ray = Instantiate(rayPrefab, levelContainer.transform);
        SetGameObjectTransform(ray, element);
        ChangeGameObjectColor(ray, element);
        var rayController = ray.GetComponent<RayController>();
        rayController.speed = element.GetSpeed();
        rayController.range = element.GetRange();
        rayController.timeStop = element.GetTimeStop();
        rayController.dir = element.GetDir();
    }

    public void ModifyPlayer(GameObject game, MapPlayer element)
    {
        var player = game.GetComponentInChildren<PlayerController>().gameObject;
        SetGameObjectTransform(player, element);
        ChangeGameObjectColor(player, element);
        var playerController = player.GetComponent<PlayerController>();
        playerController.speed = element.speed;
        playerController.decceleration = element.decceleration;
        playerController.dead = element.dead;
        playerController.respawn = element.respawn;
        playerController.finish = element.finish;
        playerController.obstacleCollide = element.obstacleCollide;
        playerController.obstacleKill = element.obstacleKill;
        playerController.obstacle = element.obstacle;
    }

    public void CreateWall(MapElement element)
    {
        var wall = Instantiate(wallPrefab, levelContainer.transform);
        SetGameObjectTransform(wall, element);
    }

    public MapData GetMapData()
    {
        return _mapData;
    }
}
