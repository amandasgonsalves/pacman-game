using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Ghost[] ghosts;
    [SerializeField] private Pacman pacman;
    [SerializeField] private Transform pellets;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;

    private int ghostMultiplier = 1;
    private int lives = 3;
    private int score = 0;

    public int Lives => lives;
    public int Score => score;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() //funcao quando se da start
    {
        NewGame();
    }

    private void Update() //funcao para atualizar o jogo caso o jogador perca todas as vidas
    {
        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }
    }

    private void NewGame() //funcao para iniciar o jogo
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound() //funcao quando o jogador perde uma vida
    {
        gameOverText.enabled = false;

        foreach (Transform pellet in pellets) {
            pellet.gameObject.SetActive(true); //pellet se mantem inalterados
        }

        ResetState();
    }

    private void ResetState() //funcao para resetar a posicao do pacman e dos fantasmas
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
    }

    private void GameOver() //funcao caso o jogador perca todas as vidas
    {
        gameOverText.enabled = true;

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives) //funcao para marcar as vidas
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score) //funcao para marcar a pontuacao
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void PacmanEaten() //funcao caso o pacman seja comido
    {
        pacman.DeathSequence();

        SetLives(lives - 1); //perde uma vida

        if (lives > 0) {
            Invoke(nameof(ResetState), 3f); //se nao acabar todas as vidas, reseta a posicao do pacman e dos fantasmas depois de 3 segundos
        } else {
            GameOver(); //se acabar as vidas, acaba o jogo
        }
    }

    public void GhostEaten(Ghost ghost) //funcao caso um fantasma seja comido
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points); //incremento de pontos

        ghostMultiplier++; //multiplicador de pontos dos fantasmas
    }

    public void PelletEaten(Pellet pellet) //funcao para comer os pelleta
    {
        pellet.gameObject.SetActive(false);//faz o pellet sumir pois foi comido

        SetScore(score + pellet.points);//incrementa a pontuacao

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f); //novo round comeca
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier)); //cancela o invoke caso o tempo do powerpellet nao tenha acabado e outro seja comido
        Invoke(nameof(ResetGhostMultiplier), pellet.duration); //reseta o multiplicador quando o tempo do powerpellet acaba
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf) { //verifica se todos os pellets foram comidos
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }

}
