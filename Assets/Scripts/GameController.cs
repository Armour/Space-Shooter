using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public bool hardMode;
    [HideInInspector] public bool gameOver;

	[SerializeField] public TextMeshProUGUI scoreText;
	[SerializeField] public TextMeshProUGUI restartText;
	[SerializeField] public TextMeshProUGUI gameOverText;
    private int score;
    private bool restart;

    void Start()
    {
        restart = false;
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine("SpawnWaves");
    }

    void Update()
    {
        if (restart)
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene("Main");
                }
            }
            else
            {
                if (Input.GetButton("Fire1"))
                {
                    SceneManager.LoadScene("Main");
                }
            }
        }
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                if (SystemInfo.deviceType == DeviceType.Desktop)
                {
                    gameOverText.text = "";
                    restartText.text = "Press 'R' for Restart";
                }
                else
                {
                    gameOverText.text = "";
                    restartText.text = "Touch for Restart";
                }
                restart = true;
                break;
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!!";
        gameOver = true;
    }
}
