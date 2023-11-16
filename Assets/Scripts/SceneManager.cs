using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    public Player Player;
    public List<Enemie> Enemies;
    public GameObject Lose;
    public GameObject Win;
    public GameObject LvlUpPanel;
    public AudioSource lvlUpSound;
    public TextMeshProUGUI CurrentWaveTMP;

    private int currWave = 0;
    public int lvl = 1;
    [SerializeField] private LevelConfig Config;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnWave();
    }

    private void FixedUpdate()
    {
        if (currWave > lvl)
        {
            LvlUpPanel.SetActive(true);
        }
    }

    public void AddEnemie(Enemie enemie)
    {
        Enemies.Add(enemie);
    }

    public void RemoveEnemie(Enemie enemie)
    {
        Enemies.Remove(enemie);
        if (Enemies.Count == 0)
        {
            SpawnWave();
        }
    }

    public void GameOver()
    {
        Lose.SetActive(true);
    }

    private void SpawnWave()
    {
        if (currWave >= Config.Waves.Length)
        {
            Win.SetActive(true);
            return;
        }

        var wave = Config.Waves[currWave];
        foreach (var character in wave.Characters)
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Instantiate(character, pos, Quaternion.identity);
        }
        currWave++;
        CurrentWaveTMP.text = currWave.ToString();
        lvlUpSound.Play();
    }

    public void UpPlayerHealth(float addHP)
    {
        Player.Hp += addHP;
        Player.IncreaseScale();
        LvlUpPanel.SetActive(false);
        Player.AttackRange += 1;
        lvl++;
    }

    public void UpPlayerDamage(float addDamage)
    {
        Player.Damage += addDamage;
        LvlUpPanel.SetActive(false);
        lvl++;
    }

    public void UpPlayerAttackSpeed(float addSpeed)
    {
        Player.AtackSpeed *= addSpeed;
        LvlUpPanel.SetActive(false);
        lvl++;
    }


public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
  
}
