using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RockController : MonoBehaviour
{
    private List<GameObject> rockQueue;
    private List<GameObject> rockQueueCopy;
    private List<GameObject> rockResult;
    private List<Vector3> rockInitialPosition;
    private GameObject[] stoneList;
    private TextMeshProUGUI dialogTextMesh;
    private TextMeshProUGUI heightTextMesh;
    private TextMeshProUGUI currStoneTextMesh;
    private Animator dialogAnimator;
    private Animator charAnimator;
    private Animator congratsAnimator;
    private Animator endAnimator;
    private AudioSource source;
    private int targetHeight = 3;
    private bool won = false;
    [SerializeField] private GameObject dialogText;
    [SerializeField] private GameObject heightText;
    [SerializeField] private GameObject currStoneText;
    [SerializeField] private GameObject congratsScreen;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject stone1;
    [SerializeField] private GameObject stone2;
    [SerializeField] private GameObject stone3;
    [SerializeField] private GameObject stone4;
    [SerializeField] private GameObject stone5;
    [SerializeField] private GameObject stone6;
    [SerializeField] private GameObject stone7;
    [SerializeField] private GameObject stone8;
    [SerializeField] private AudioClip victory;
    [SerializeField] private AudioClip push;
    [SerializeField] private AudioClip pop;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> randomList = new List<GameObject>();
        rockQueue = new List<GameObject>();
        rockResult = new List<GameObject>();
        rockInitialPosition = new List<Vector3>();
        heightTextMesh = heightText.GetComponentInChildren<TextMeshProUGUI>();
        currStoneTextMesh = currStoneText.GetComponentInChildren<TextMeshProUGUI>();
        dialogTextMesh = dialogText.GetComponentInChildren<TextMeshProUGUI>();
        dialogAnimator = dialogText.GetComponent<Animator>();
        charAnimator = GetComponent<Animator>();
        charAnimator.SetTrigger("hold");
        congratsAnimator = congratsScreen.GetComponent<Animator>();
        endAnimator = endScreen.GetComponent<Animator>();
        endAnimator.SetTrigger("close");
        source = GetComponent<AudioSource>();
        stoneList = new GameObject[] { stone1, stone2, stone3, stone4, stone5, stone6, stone7, stone8 };
        for (int i = 0; i < 8; i++)
        {
            randomList.Add(stoneList[i]);
            Vector3 ithStone = stoneList[i].GetComponent<Transform>().position;
            rockInitialPosition.Add(new Vector3(ithStone.x, ithStone.y));
        }
        for (int i = 1; i < 9; i++)
        {
            int index = Random.Range(0, randomList.Count);
            rockQueue.Add(randomList[index]);
            randomList.RemoveAt(index);
        }

        heightTextMesh.text = "Target Height: " + targetHeight.ToString();
        while (CheckValidity(rockQueue) < targetHeight)
        {
            rockQueue.Clear();
            for (int i = 0; i < 8; i++)
            {
                randomList.Add(stoneList[i]);
            }
            for (int i = 1; i < 9; i++)
            {
                int index = Random.Range(0, randomList.Count);
                rockQueue.Add(randomList[index]);
                randomList.RemoveAt(index);
            }
        }
        rockQueueCopy = new List<GameObject>(rockQueue);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject nextRock = GetNextRock();
        if (nextRock == null)
        {
            currStoneTextMesh.text = "Current Stone: ";
            return;
        }
        currStoneTextMesh.text = "Current Stone: " + nextRock.name[5].ToString();
    }

    public GameObject GetNextRock()
    {
        if (rockQueue.Count > 0)
            return rockQueue[0];
        else
            return null;
    }

    public void Push()
    {
        source.PlayOneShot(push);
        rockResult.Add(rockQueue[0]);
        rockQueue.RemoveAt(0);
        won = CheckWinCondition();
    }

    public void Pop()
    {
        if (rockResult.Count <= 0)
        {
            return;
        }
        source.PlayOneShot(pop);
        rockResult.RemoveAt(rockResult.Count - 1);
    }

    public List<GameObject> GetRockResult()
    {
        return rockResult;
    }

    public void Reset()
    {
        source.Play();
        rockQueue = new List<GameObject>(rockQueueCopy);
        rockResult.Clear();

        for (int i = 0; i < 8; i++)
        {
            if (stoneList[i].activeSelf == false)
            {
                continue;
            }
            stoneList[i].GetComponent<Animator>().Play("None", -1, 0f);
            stoneList[i].GetComponent<Transform>().position = new Vector3(rockInitialPosition[i].x, rockInitialPosition[i].y, 0);
            stoneList[i].SetActive(false);
        }
    }

    public void HardReset()
    {
        if (won)
        {
            targetHeight++;
            won = false;
            List<GameObject> randomList = new List<GameObject>();
            rockQueue = new List<GameObject>();
            rockResult = new List<GameObject>();
            charAnimator.SetTrigger("hold");
            for (int i = 0; i < 8; i++)
            {
                randomList.Add(stoneList[i]);
            }
            for (int i = 1; i < 9; i++)
            {
                int index = Random.Range(0, randomList.Count);
                rockQueue.Add(randomList[index]);
                randomList.RemoveAt(index);
            }

            heightTextMesh.text = "Target Height: " + targetHeight.ToString();
            while (CheckValidity(rockQueue) < targetHeight)
            {
                rockQueue.Clear();
                for (int i = 0; i < 8; i++)
                {
                    randomList.Add(stoneList[i]);
                }
                for (int i = 1; i < 9; i++)
                {
                    int index = Random.Range(0, randomList.Count);
                    rockQueue.Add(randomList[index]);
                    randomList.RemoveAt(index);
                }
            }
            rockQueueCopy = new List<GameObject>(rockQueue);
            for (int i = 0; i < 8; i++)
            {
                if (stoneList[i].activeSelf == false)
                {
                    continue;
                }
                stoneList[i].GetComponent<Animator>().Play("None", -1, 0f);
                stoneList[i].GetComponent<Transform>().position = new Vector3(rockInitialPosition[i].x, rockInitialPosition[i].y, 0);
                stoneList[i].SetActive(false);
            }
        }

    }

    public List<Vector3> GetRockInitialPosition()
    {
        return rockInitialPosition;
    }

    public int CheckValidity(List<GameObject> queue)
    {
        Stack<int> temp = new Stack<int>();
        int max = 0;
        for (int i = 0; i < queue.Count; i++)
        {
            int curr = int.Parse(queue[i].name[5].ToString());
            if (temp.Count == 0 || temp.Peek() > curr)
            {
                temp.Push(curr);
            }
            else
            {
                temp.Pop();
                i--;
            }
            max = Mathf.Max(max, temp.Count);
        }
        return max;
    }

    public bool CheckWinCondition()
    {
        if (targetHeight == 6 && rockResult.Count >= targetHeight)
        {
            source.PlayOneShot(victory);
            endAnimator.SetTrigger("open");
            targetHeight = 2;
            return true;
        }
        if (rockResult.Count >= targetHeight)
        {
            source.PlayOneShot(victory);
            congratsAnimator.SetTrigger("open");
            return true;
        }
        return false;
    }

    public bool GetWon()
    {
        return won;
    }

    public Animator GetCharAnimator()
    {
        return charAnimator;
    }
}
