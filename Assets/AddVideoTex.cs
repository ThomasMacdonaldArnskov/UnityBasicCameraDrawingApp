using UnityEngine;
using System.Collections;

public class AddVideoTex : MonoBehaviour {

    WebCamTexture webCamTex;

    Texture2D newTex;
    Texture2D paintTex;
    //Texture2D medTex;

    Color[] inputPix;
    Color[] outputPix;
    //Color[] medianTest;
    Color[] painting;

    //Color[,] pixel2D;


    public GameObject output;
    public GameObject test;
    //public GameObject medianBlur;

	// Use this for initialization
	void Start () {
        webCamTex = new WebCamTexture();
        webCamTex.Play();

      

        inputPix = new Color[webCamTex.width * webCamTex.height];
        outputPix = new Color[webCamTex.width * webCamTex.height];
        //pixel2D = GetPixels2D(medTex);
        //medianTest = new Color[webCamTex.width * webCamTex.height];

        painting = new Color[webCamTex.width*webCamTex.height];

        newTex = new Texture2D(webCamTex.width, webCamTex.height);
        paintTex = new Texture2D(webCamTex.width, webCamTex.height);
        //medTex = new Texture2D(webCamTex.width, webCamTex.height);

        GetComponent<Renderer>().material.mainTexture = webCamTex;
        output.GetComponent<Renderer>().material.mainTexture = newTex;
        test.GetComponent<Renderer>().material.mainTexture = paintTex;
      //  medianBlur.GetComponent<Renderer>().material.mainTexture = medTex;



    }

    // Update is called once per frame
    void Update () {

        inputPix = webCamTex.GetPixels();


        //NORMALIZE COLORS
        for (int i = 0; i < inputPix.Length; i++)
        {
            if ((inputPix[i].r + inputPix[i].g + inputPix[i].b) > 0.2f) {
                outputPix[i].r = inputPix[i].r / (inputPix[i].r + inputPix[i].g + inputPix[i].b);
                outputPix[i].g = inputPix[i].g / (inputPix[i].r + inputPix[i].g + inputPix[i].b);
                outputPix[i].b = inputPix[i].b / (inputPix[i].r + inputPix[i].g + inputPix[i].b);
            } else {
                outputPix[i] = Color.black;
            }


            //IF RED COLOR IS DETECTED
            if (inputPix[i].r > 0.6f && inputPix[i].g < 0.3f && inputPix[i].b < 0.3f)
            {
                outputPix[i] = Color.red;
                painting[i] = Color.red;

            }
            //IF GREEN COLOR IS DETECTED
            else if (inputPix[i].g > 0.6f && inputPix[i].r < 0.3f && inputPix[i].b < 0.3f)
            {
                outputPix[i] = Color.green;
                painting[i] = Color.green;
            }
            //IF BLUE COLOR IS DETECTED
            else if (inputPix[i].g < 0.3f && inputPix[i].r < 0.3f && inputPix[i].b > 0.6f)
            {
                outputPix[i] = Color.blue;
                painting[i] = Color.blue;
            } else
            {
                outputPix[i] = Color.black;
            }


            /*
            for (int h = 0; h < webCamTex.height; h++)
            {
                for (int w = 0; w < webCamTex.width; w++)
                {
                    pixel2D[w, h] = outputPix[h * webCamTex.width + w];
                }
            }*/

            //pixel2D = MedianFilter(pixel2D,5);
            //SetPixels2D(pixel2D, medTex);
            /*
            for (int t = 0; t < 2; t++)
                for (int j = 0; j < 2; j++)
                {
                    medianTest[j + t * 2] = pixel2D[t, j];
                }
            */


        }
        //medTex.SetPixels(medianTest);
        //medTex.Apply();
        paintTex.SetPixels(painting);
        paintTex.Apply();
        newTex.SetPixels(outputPix);
        newTex.Apply();

	}
    /*
    public Color[,] GetPixels2D(Texture2D t)
    {
        Color[,] texture2d = new Color[t.width, t.height];
        Color[] texture1d = t.GetPixels();

        for (int h = 0; h < t.height; h++)
        {
            for (int w = 0; w < t.width; w++)
            {
                texture2d[w, h] = texture1d[h * t.width + w];
            }
        }
        return texture2d;
    }

    public void SetPixels2D(Color[,] i, Texture2D t)
    {
        Color[] texture1d = new Color[i.Length];

        int width = i.GetLength(0);
        int height = i.GetLength(1);

        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                texture1d[h * width + w] = i[w, h];
            }
        }
        t.SetPixels(texture1d);
        t.Apply();
    }

    public Color[,] MedianFilter(Color[,] i, int k)
    {
        Color[,] tmp = i;

        int kw = k / 2;
        int kh = k / 2;

        for (int w = 0 + kw; w < i.GetLength(0) - kw; w++)
        {
            for (int h = 0 + kh; h < i.GetLength(1) - kh; h++)
            {
                int count = 0;

        float[] neighborhood = new float[k * k];

        for (int j = -kw; j <= +kw; j++)
                {
                    for (int l = -kh; l <= +kh; l++)
                    {
                        neighborhood[count] = (tmp[w + j, h + l].r + tmp[w + j, h + l].g + tmp[w + j, h + l].b) / 3;
                        count++;
                    }
                }
        
                float result = 0f;

                        neighborhood = BubbleSort(neighborhood);
                        result = neighborhood[(int)neighborhood.Length / 2];

                i[w, h].r = result;
                i[w, h].g = result;
                i[w, h].b = result;
            }
        }
        return i;
    }

    public float[] BubbleSort(float[] input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input.Length - 1 - i; j++)
            {
                float tmp;
                if (input[j] < input[j + 1])
                {
                    tmp = input[j];
                    input[j] = input[j + 1];
                    input[j + 1] = tmp;
                }
            }
        }
        return input;
    }
    */
}
