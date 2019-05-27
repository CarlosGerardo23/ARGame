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
    [SerializeField] List<GameObject> sushiPrefabs;
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

        CreateAllSushi();
        UpdateAllTimers(Time.deltaTime);
        MoveAllSushi();
        if (!CanReturnCupstick)
            MoveChopstick();
        else
            MoveBackChopstick();

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
            if (sushi.CheckFinish(endCenterPoint.transform.position, pointDistanceSushi) || sushi.destoyObject)
            {
                sushi.canMove = false;
                centerSushi.RemoveAt(i);
            }

            sushi.MoveTo(endCenterPoint.transform.position);

        }
    }

    private void MoveLeftSushi()
    {
        for (int i = 0; i < leftSushi.Count; i++)
        {
            SushiData sushi = leftSushi[i].GetComponent<SushiData>();
            if (sushi.CheckFinish(endLeftPoint.transform.position, pointDistanceSushi) || sushi.destoyObject)
            {
                sushi.canMove = false;
                leftSushi.RemoveAt(i);
            }

            sushi.MoveTo(endLeftPoint.transform.position);

        }
    }

    private void MoveRightSushi()
    {
        for (int i = 0; i < rightSushi.Count; i++)
        {
            SushiData sushi = rightSushi[i].GetComponent<SushiData>();
            if (sushi.CheckFinish(endRightPoint.transform.position, pointDistanceSushi) || sushi.destoyObject)
            {
                sushi.canMove = false;
                rightSushi.RemoveAt(i);
            }

            sushi.MoveTo(endRightPoint.transform.position);

        }
    }



    //Create sushi

    private void CreateAllSushi()
    {
        CreateCenterSushi(0, 5);
        CreateLeftSushi(0, 4);
        CreatRightSushi(0, 2);
    }

    private void CreateCenterSushi(int sushiIndex, float newTime)
    {
        if (centerTimer.TimeFinish)
        {
            GameObject sushi = Instantiate(sushiPrefabs[sushiIndex], startCenterPoint.transform.position, Quaternion.identity);
            sushi.transform.parent = transform;
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
        centerTimer.SetTime(3);
        centerTimer.StartTimer = true;

        //Right time
        rightTimer = new Timer();
        rightTimer.SetTime(3);
        rightTimer.StartTimer = true;

        //left time
        leftTimer = new Timer();
        leftTimer.SetTime(3);
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
        if (!startMoving)
            CheckSwipeEditor();
        else
            MoveToPointState();
#endif
#if UNITY_ANDROID

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
            float distanceSwipe = Mathf.Abs(touchStart.x - touchEnd.x);
            if (distanceSwipe > minimumSwipe)
            {
                CheckDirection();
                startMoving = true;
            }
        }
    }
    private void CheckSwipeAndroid()
    {

    }

    private void CheckDirection()
    {
        if (startMoving)
            return;
        if (touchStart.x - touchEnd.x > 0)
            direction = "left";
        else if (touchStart.x - touchEnd.x < 0)
            direction = "right";

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
        chopstick.transform.parent = transform;
        
    }

    void MoveBackChopstick()
    {
        ChopstickData ch = chopstick.GetComponent<ChopstickData>();
        if (ch.CheckFinish(endCenterPoint.transform.position, pointDistanceChopstick))
        {
            CanReturnCupstick = false;
            return;
        }
           
        ch.MoveTo(endCenterPoint.transform.position);
    }

}
