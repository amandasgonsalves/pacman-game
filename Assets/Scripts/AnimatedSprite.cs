using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[0];

    public float animationTime = 0.25f; //tempo de um sprite para outro

    public bool loop = true; //determinar se deve ou nao entrar em loop o sprite

    public SpriteRenderer spriteRenderer { get; private set; } //metodo para referenciar o spiterenderer

    public int animationFrame { get; private set; } //indice de qual sprite esta

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void Start() //funcao para fazer a animacao 
    {
        InvokeRepeating(nameof(Advance), animationTime, animationTime);
    }

    private void Advance()
    {
        if (!spriteRenderer.enabled) { //verifica se o sprite renderer esta desativado
            return;
        }

        animationFrame++;

        if (animationFrame >= sprites.Length && loop) { //verifica se o frame atual eh o ultimo
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < sprites.Length) { //metodo que garante que sempre estara dentro do vetor
            spriteRenderer.sprite = sprites[animationFrame];
        }
    }

    public void Restart() //reinicia a animacao
    {
        animationFrame = -1;

        Advance();
    }

}
