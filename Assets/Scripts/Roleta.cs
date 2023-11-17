using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Roleta : MonoBehaviour
{
    public Image roletaImage;                   //UI Image da roleta q vai girar.
    public int voltasAteParar = 9;              //Numero de giros ate o angulo desejado.

    [Range (0.3f, 0.99f)]
    public float suavidadeDoGiro = 0.97f;       //Controla a suavidade em que vai parando de girar.

    /// <summary>
    /// funcao responsavel por gira a roleta e definir o angulo que ela para e quantas voltas ela da ate parar.
    /// </summary>
    /// <param name="targetAngle"></param>
    /// <param name="onFinished"></param>
    public void GirarParaAngulo(float targetAngle, System.Action onFinished)
    {
        StartCoroutine(IGirarParaAngulo(targetAngle, onFinished));
    }

    private IEnumerator IGirarParaAngulo(float angle, System.Action onFinished)
    {
        int voltasAngulo = 360 * voltasAteParar;
        float step = 0;

        while (step < 1f)
        {
            //step vai de 0 a 1, essa variavel vai de 0 ao angulo desejado conforme o valor da variavel step.
            float stepAngle = Mathf.Lerp(0f, angle + voltasAngulo, step);

            // valor de 0 a 1 dependendo do angulo que estiver, aplico esse valor ao passo da rotacao (step) para roleta ir parando suavemente.
            float suavizacao = Mathf.InverseLerp(0, angle + voltasAngulo, stepAngle) * suavidadeDoGiro;
            step += Time.deltaTime * (1f - suavizacao) * 0.7f;

            //aplica a rotacao no transform da imagem da roleta.
            Vector3 rotImage = roletaImage.transform.localEulerAngles;
            rotImage.z = -stepAngle;
            roletaImage.transform.localEulerAngles = rotImage;

            yield return new WaitForEndOfFrame();
        }

        Debug.Log("A roleta parou em: " + angle + " graus");
        onFinished?.Invoke();
    }

}
