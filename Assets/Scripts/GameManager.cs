using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<RoletaItem> roletaItems = new List<RoletaItem>();   //cada item da roleta, se tiver 10 premios basta add 10 itens e seus angulos e premios aqui.

    [Tooltip(" -1 = aleatorio, se nao ele sempre escolhe o premio que estiver aqui, de acordo com os adicionados acima.")]
    public int premioEscolhidoIndex = -1;                           //Se for -1 eh aleatorio, se nao ele pega como escolhido o index que estiver aqui.

    public CanvasGroup roletaCanvasGroup, finalCanvasGroup;
    public TextMeshProUGUI premioText;
    public Image premioImg;

    private Roleta _roleta;
    private RoletaItem _roletaItemSorteado;

    private void Awake()
    {
        _roleta = GetComponent<Roleta>();
    }

    IEnumerator Start()
    {
        //Espera 1 segundo para girar a roleta quando carrega a cena.
        yield return new WaitForSeconds(1f);

        //Escolhe um numero aleatorio de 0 ate o numero de premios que ira servir de index para cada cor e premio da roleta.
        int resultadoRoletaIndex = UnityEngine.Random.Range(0, roletaItems.Count);

        if (premioEscolhidoIndex > -1 && premioEscolhidoIndex < roletaItems.Count)
            resultadoRoletaIndex = premioEscolhidoIndex;

        //seta o premio sorteado pela index dos premios.
        _roletaItemSorteado = roletaItems[resultadoRoletaIndex];

        //seta as informacoes do premio na ui.
        premioText.text = _roletaItemSorteado.mensagemPremio;
        premioImg.color = _roletaItemSorteado.cor;

        _roleta.GirarParaAngulo(_roletaItemSorteado.angulo, Final);
    }

    /// <summary>
    /// Chamado quando a roleta termina seu giro.
    /// </summary>
    public void Final()
    {
        StartCoroutine(IFinal());
    }

    private IEnumerator IFinal()
    {
        yield return new WaitForSeconds(1f);

        roletaCanvasGroup.gameObject.SetActive(false);
        finalCanvasGroup.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Inicio");
    }
}

[Serializable]
public class RoletaItem
{
    public Color cor;
    public int angulo;
    [TextArea]
    public string mensagemPremio;
}
