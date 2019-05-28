using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiController : MonoBehaviour
{
    [Header("Start points")]
    [SerializeField] GameObject startCenterPoint;
    [SerializeField] GameObject startLeftPoint;
    [SerializeField] GameObject startRightPoint;

    [Header("End points")]
    [SerializeField] GameObject endCenterPoint;
    [SerializeField] GameObject endLeftPoint;
    [SerializeField] GameObject endRightPoint;

    [Header("Sushi Behaviour")]
    public List<GameObject> sushiPrefabs;
    [SerializeField] float pointDistanceSushi;


    [Header("Chopsticks Behaviour")]
    [SerializeField] GameObject chopstick;
    [SerializeField] float minimumSwipe;
    [SerializeField] bool startMoving;
    [SerializeField] string direction = "";
    [SerializeField] Vector3 touchStart;
    [SerializeField] Vector3 touchEnd;
    [SerializeField] float pointDistanceChopstick;
    bool CanReturnCupstick = false;


    //Private values
    List<GameObject> centerSushi;
    List<GameObject> leftSushi;
    List<GameObject> rightSushi;


    //Timers
    Timer centerTimer;
    Timer leftTimer;
    Timer rightTimer;

    //Chpstick Movement


    void Start()
    {
        centerSushi = new List<GameObject>();
        leftSushi = new List<GameObject>();
        rightSushi = new List<GameObject>();
        SetAllTimers();
    }


    void Update()
    {
        if (!chopstick.GetComponent<ChopstickData>().haveSushi)
        {
            CreateAllSushi();
            UpdateAllTimers(Time.deltaTime);
            MoveAllSushi();
            if (!CanReturnCupstick)
                MoveChopstick();
            else
                MoveBackChopstick();
        }
    }

    //Move sushi
    void MoveAllSushi()
    {
        MoveCenterSushi();
        MoveLeftSushi();
        MoveRightSushi();
    }

    private void MoveCenterSushi()
    {
        for (int i = 0; i < centerSushi.Count; i++)
        {
            
            SushiData sushi = centerSushi[i].GetComponent<SushiData>();
            if(sushi.destoyObject)
                centerSushi.RemoveAt(i);
            if (sushi.CheckFinish(endCenterPoint.transform.position, pointDistanceSushi) || sushi.destoyObject)
            {
                sushi.canMove = false;
                //centerSushi.RemoveAt(i);
            }

            sushi.MoveTo(endCenterPoint.transform.position);

        }
    }

    private void MoveLeftSushi()
    {
        for (int i = 0; i < leftSushi.Count; i++)
        {
            SushiData sushi = leftSushi[i].GetComponent<SushiData>();
            if (sushi.destoyObject)
                leftSushi.RemoveAt(i);
            if (sushi.CheckFinish(endLeftPoint.transform.position, pointDistanceSushi) || sushi.destoyObject)
            {
                sushi.canMove = false;
                //leftSushi.RemoveAt(i);
            }

            sushi.MoveTo(endLeftPoint.transform.position);

        }
    }

    private void MoveRightSushi()
    {
        for (int i = 0; i < rightSushi.Count; i++)
        {
            SushiData sushi = rightSushi[i].GetComponent<SushiData>();
            if (sushi.destoyObject)
                rightSushi.RemoveAt(i);
            
            if (sushi.CheckFinish(endRightPoint.transform.position, pointDistanceSushi) || sushi.destoyObject)
            {
                sushi.canMove = false;
                // rightSushi.RemoveAt(i);
            }

            sushi.MoveTo(endRightPoint.transform.position);

        }
    }



    //Create sushi

    private void CreateAllSushi()
    {
        int centerSushiIndex = GetRandomNumber(0, sushiPrefabs.Count);
        int leftSushiIndex = GetRandomNumber(0, sushiPrefabs.Count);
        int rightSushiIndex = GetRandomNumber(0, sushiPrefabs.Count);

        int centerTime = GetRandomNumber(2, 5);
        int leftTime = GetRandomNumber(2, 5);
        int rightTime = GetRandomNumber(2, 5);

        CreateCenterSushi(centerSushiIndex, centerTime);
        CreateLeftSushi(leftSushiIndex, leftTime);
        CreatRightSushi(rightSushiIndex, rightTime);
    }

    private void CreateCenterSushi(int sushiIndex, float newTime)
    {
        if (centerTimer.TimeFinish)
        {
            GameObject sushi = Instantiate(sushiPrefabs[sushiIndex], startCenterPoint.transform.position, Quaternion.identity);
            sushi.transform.parent = transform;
            sushi.transform.localScale = new Vector3(1, 1, 1);
            centerTimer.RestartTime(newTime);
            sushi.GetComponent<SushiData>().canMove = true;
            centerSushi.Add(sushi);
        }
    }

    private void CreateLeftSushi(int sushiIndex, float newTime)
    {
        if (leftTimer.TimeFinish)
        {
            GameObject sushi = Instantiate(sushiPrefabs[sushiIndex], startLeftPoint.transform.position, Quaternion.identity);
            sushi.transform.parent = transform;
            sushi.transform.localScale = new Vector3(1, 1, 1);
            leftTimer.RestartTime(newTime);
            sushi.GetComponent<SushiData>().canMove = true;
            leftSushi.Add(sushi);
        }
    }

    private void CreatRightSushi(int sushiIndex, float newTime)
    {
        if (rightTimer.TimeFinish)
        {
            GameObject sushi = Instantiate(sushiPrefabs[sushiIndex], startRightPoint.transform.position, Quaternion.identity);
            sushi.transform.parent = transform;
            sushi.transform.localScale = new Vector3(1, 1, 1);
            rightTimer.RestartTime(newTime);
            sushi.GetComponent<SushiData>().canMove = true;
            rightSushi.Add(sushi);
        }
    }

    //Timers
    private void SetAllTimers()
    {
        //Center time
        centerTimer = new Timer();
        centerTimer.SetTime(GetRandomNumber(2, 5));
        centerTimer.StartTimer = true;

        //Right time
        rightTimer = new Timer();
        rightTimer.SetTime(GetRandomNumber(2, 5));
        rightTimer.StartTimer = true;

        //left time
        leftTimer = new Timer();
        leftTimer.SetTime(GetRandomNumber(2, 5));
        leftTimer.StartTimer = true;


    }
    private void UpdateAllTimers(float time)
    {
        centerTimer.UpdateTime(time);
        rightTimer.UpdateTime(time);
        leftTimer.UpdateTime(time);
    }


    //Check swipe Chopstick
    private void MoveChopstick()
    {
        ChopstickData chopstikBehaviour = chopstick.GetComponent<ChopstickData>();
#if UNITY_EDITOR
        if (!startMoving&&!CanReturnCupstick)
            CheckSwipeEditor();
        else
            MoveToPointState();
#endif
#if UNITY_ANDROID
        if (!startMoving)
            CheckSwipeEditor();
        else
            MoveToPointState();
#endif
    }

    private void CheckSwipeEditor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
            touchEnd = Input.mousePosition;
        }
        touchEnd = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("realese");

            CheckDirection();
        }
    }
    private void CheckSwipeAndroid()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
                touchEnd = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                touchEnd = touch.position;
                CheckDirection();
            }

            //Detecta swipes completos solos
            if (touch.phase == TouchPhase.Ended)
            {
                touchEnd = touch.position;
                CheckDirection();
            }
        }
    }

    private void CheckDirection()
    {
        float distanceSwipe = Mathf.Abs(touchStart.x - touchEnd.x);
        if (distanceSwipe > minimumSwipe)
        {
            if (startMoving)
                return;
            if (touchStart.x - touchEnd.x > 0)
                direction = "left";
            else if (touchStart.x - touchEnd.x < 0)
                direction = "right";

            startMoving = true;
        }

    }

    //Move Chopstick
    private void MoveToPointState()
    {
        if (!startMoving)
            return;
        State newState = State.CENTER;
        State currentState = chopstick.GetComponent<ChopstickData>().chopstickState;
        ChopstickData chopstikBehaviour = chopstick.GetComponent<ChopstickData>();
        Vector3 goTo = Vector3.zero;

        if (direction == "left")
        {
            switch (currentState)
            {
                case State.CENTER:
                    goTo = endLeftPoint.transform.position;
                    newState = State.LEFT;
                    break;
                case State.RIGHT:
                    goTo = endCenterPoint.transform.position;
                    newState = State.CENTER;
                    break;
                case State.LEFT:
                    Debug.Log("Can't move");
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (currentState)
            {
                case State.CENTER:
                    goTo = endRightPoint.transform.position;
                    newState = State.RIGHT;
                    break;
                case State.RIGHT:
                    Debug.Log("Can't move");
                    break;
                case State.LEFT:
                    goTo = endCenterPoint.transform.position;
                    newState = State.CENTER;
                    break;
                default:
                    break;
            }
        }
        chopstikBehaviour.MoveTo(goTo);

        if (chopstikBehaviour.CheckFinish(goTo, pointDistanceChopstick))
        {
            startMoving = false;
            chopstikBehaviour.chopstickState = newState;
        }
    }

    public void ReturnChopstick()
    {
        CanReturnCupstick = true;
        startMoving = false;
        chopstick.transform.parent = transform;

    }

    void MoveBackChopstick()
    {
        ChopstickData ch = chopstick.GetComponent<ChopstickData>();
        if (ch.CheckFinish(endCenterPoint.transform.position, pointDistanceChopstick))
        {
            ch.chopstickState = State.CENTER;
            CanReturnCupstick = false;
            return;
        }

        ch.MoveTo(endCenterPoint.transform.position);
    }

    //Other stuff


    private int GetRandomNumber(int rangeStart, int rangeEnd)
    {
        return Random.Range(rangeStart, rangeEnd);
    }

    public void UnableWater()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }
    public void GrabSushi()
    {
        GetComponent<MeshRenderer>().enabled = false;
        FindSushiInLane(centerSushi);
        FindSushiInLane(leftSushi);
        FindSushiInLane(rightSushi);
        ClearWater();
    }
    private void ClearWater()
    {
        ClearAllLanes();
        SetAllTimers();
    }
    private void ClearAllLanes()
    {
        ClearLane(centerSushi);
        ClearLane(leftSushi);
        ClearLane(rightSushi);
    }
    private void ClearLane(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Destroy(list[i]);
        }
        list.Clear();
    }
    private void FindSushiInLane(List<GameObject> lane)
    {
        for (int i = 0; i < lane.Count; i++)
        {
            if (lane[i] == chopstick.GetComponent<ChopstickData>().sushi)
            {
                lane.RemoveAt(i);
                break;
            }
        }
    }
    private void SetIdSushi()
    {
        for (int i = 0; i < sushiPrefabs.Count; i++)
        {
            sushiPrefabs[i].GetComponent<SushiData>().id = i;
        }
    }
}
