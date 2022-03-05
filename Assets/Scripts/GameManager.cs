using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
    // private int numTentativas;        // armazena as tentativas válidas da rodada 
    // private int maxnumTentativas;     // Número máximo de tentativas para a Forca ou Salvação
    private int numErros;            // armazena as tentativas validas com letras erradas
    private int maxNumErros;        // Número máximo de erros para a Forca
    int score = 0;

    public GameObject letra;          // prefab da letra do Game
    public GameObject centro;         // objeto de texto que indica o centro da tela

    private string palavraOculta = ""; // palavra oculta a ser descoberta
    // private string[] palavrasOcultas = new string[] { "carro", "elefante", "futebol" }; // array de palavras ocultas
    
    private int tamanhoPalavraOculta;  // tamanho da palavra oculta
    char[] letrasOcultas;              // letras da palavra oculta
    bool[] letrasDescobertas;          // indicador de quais letras foram descobertas

    // Start is called before the first frame update
    void Start()
    {
        centro = GameObject.Find("centroDaTela");

        InitGame();
        InitLetras();
        // numTentativas = 0;
        // maxnumTentativas = 10;
        numErros = 0;      
        maxNumErros = 8;   
        PlayerPrefs.SetInt("score", 0);
        // UpdateNumTentativas();
        UpdateNumErros();
        UpdateScore();


    }

    // Update is called once per frame
    void Update()
    {
        checkTeclado();
    }

    void InitLetras() 
    {
        int numLetras = tamanhoPalavraOculta;
        for (int i=0; i<numLetras; i++)
        {
            Vector3 novaPosicao;
            novaPosicao = new Vector3(centro.transform.position.x + ((i-numLetras/2.0f)*80), centro.transform.position.y, centro.transform.position.z);
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = "letra" + (i + 1);              // nomeia na hierarquia a GameObject com letra-(iésima + 1), i =  1..numLetra 
            l.transform.SetParent(GameObject.Find("Canvas").transform);   // prosiciona-se como filho do GameObject Canvas
        }
    }

    void InitGame()
    {
        // palavraOculta = "Elefante";             // define a palavra a ser descoberta   (usado no Lab 1 - parte A)
        // int numeroAleatorio = Random.Range(0, palavrasOcultas.Length);   // sorteamos um numero dentro do numero de palavras do array
        // palavraOculta = palavrasOcultas[numeroAleatorio];     // seleciona uma palavra sorteada

        palavraOculta = PegaUmaPalavraDoArquivo();
        tamanhoPalavraOculta = palavraOculta.Length;     // determina-se o numero de letras da palavra oculta
        palavraOculta = palavraOculta.ToUpper();         // transforma-se a palavra em maiuscula
        letrasOcultas = new char[tamanhoPalavraOculta];      // instancia-se o array char das letras da palavra
        letrasDescobertas = new bool[tamanhoPalavraOculta];     // instancia-se o array bool do indicador de letras certas 
        letrasOcultas = palavraOculta.ToCharArray();      // copia-se a palavra no array de letras;
    }

    void checkTeclado()
    {
        if(Input.anyKeyDown)
        {
            char letraTeclada = Input.inputString.ToCharArray()[0];
            int letraTecladaComoInt = System.Convert.ToInt32(letraTeclada);
            int letraCerta = 0;    // detectar se a letra digitada esta na palavra oculta

            if(letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122)
            {
                // numTentativas++;
                // UpdateNumTentativas();
                // if (numTentativas  > maxnumTentativas)
                // {
                //    SceneManager.LoadScene("Derrota_Pagina5");
                // }
                for (int i = 0; i<=tamanhoPalavraOculta; i++)
                {
                    if (i == tamanhoPalavraOculta)   // verifica se já passou pela palavra toda
                    {
                        VerificaSeOuveErro(letraCerta);
                        letraCerta = 0;
                    }
                    if (!letrasDescobertas[i])
                    {
                        letraTeclada = System.Char.ToUpper(letraTeclada);
                        if (letrasOcultas[i] == letraTeclada)
                        {
                            letraCerta++;
                            letrasDescobertas[i] = true;
                            GameObject.Find("audioAcerto").GetComponent<AudioSource>().Play();    // toca a musica quando a pessoa digita uma letra que tenha na palavra oculta
                            GameObject.Find("letra" + (i + 1)).GetComponent<Text>().text = letraTeclada.ToString();
                            score = PlayerPrefs.GetInt("score");
                            score = score + 100;
                            PlayerPrefs.SetInt("score", score);
                            UpdateScore();
                            VerificaSePalavraDescoberta();
                        }
                    }
                }
            }
        }
    }

    // void UpdateNumTentativas()
    // {
    //    GameObject.Find("numTentativas").GetComponent<Text>().text = numTentativas + " | " + maxnumTentativas;
    // }

    void UpdateNumErros()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = numErros + " | " + maxNumErros;
    }

    void UpdateScore()
    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score" + score;
    }

    /*
     * verifica se a palavra foi descoberta e se foi manda para a tela de vitoria
     * junto com a palavra descoberta
     */
    void VerificaSePalavraDescoberta()
    {
        bool condicao = true;
        for (int i = 0; i < tamanhoPalavraOculta; i++)
        {
            condicao = condicao && letrasDescobertas[i];
        }
        if (condicao)
        {
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);
            SceneManager.LoadScene("Vitoria_Pagina6");
        }
    }

    /*
     * Pega uma palavra do arquivo para ser a palavra oculta
     */
    string PegaUmaPalavraDoArquivo()
    {
        TextAsset t1 = (TextAsset)Resources.Load("palavras1", typeof(TextAsset));
        string s = t1.text;
        string[] palavras = s.Split(' ');
        int palavraAleatoria = Random.Range(0, palavras.Length + 1);
        return (palavras[palavraAleatoria]);
    }

    /*
     * mostra a parte do corpo conforme a pessoa vai errando
     * as letras
     */
    void UpdateBonecoForca(int Erros)
    {
        switch (Erros)
        {
            case 1:
                GameObject.Find("cabeca").GetComponent<Text>().text = "o";
                break;

            case 2:
                GameObject.Find("corpo").GetComponent<Text>().text = "|";
                break;

            case 3:
                GameObject.Find("bracoDireito").GetComponent<Text>().text = "/";
                break;

            case 4:
                GameObject.Find("bracoDireito").GetComponent<Text>().text = "";
                GameObject.Find("bracoEsquerdo").GetComponent<Text>().text = "v";
                break;

            case 5:
                GameObject.Find("pernaDireita").GetComponent<Text>().text = "/";
                break;

            case 6:
                GameObject.Find("pernaDireita").GetComponent<Text>().text = "";
                GameObject.Find("pernaEsquerda").GetComponent<Text>().text = "v";
                break;

            case 7:
                GameObject.Find("olhos").GetComponent<Text>().text = "..";
                break;

            case 8:
                GameObject.Find("boca").GetComponent<Text>().text = "v";
                break;

            default:
                break;
        }
    }

    /*
     * Verifica se a letra digitada não estava errada
     * e se nao chegou no limite de erros possivel
     * levando para a tela de derrota se chegar
     */
    void VerificaSeOuveErro(int letraCerta)
    {
        if (letraCerta == 0)
        {
            GameObject.Find("audioAcerto").GetComponent<AudioSource>().Stop();  //para a musica de acerto de letra digitada
            GameObject.Find("audioErro").GetComponent<AudioSource>().Play();    // toca a musica de erro de letra digitada
            numErros++;
        }
        UpdateBonecoForca(numErros);
        UpdateNumErros();
        if (numErros == maxNumErros)
        {
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);
            SceneManager.LoadScene("Derrota_Pagina5");
        }
    }

}
