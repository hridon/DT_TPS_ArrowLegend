
using UnityEngine;

public class LearnLerp : MonoBehaviour
{
    Vector3 A = new Vector3(0, 0, 0);
    Vector3 b = new Vector3(10, 10, 10);
   public Color R = Color.red;
   public Color B = Color.blue;
   public Color N;
    private void Start()
    {
        //認識差值 Lerp
        print(Mathf.Lerp(0, 10, 0.5f));//數字間的差值運算
        print(Vector3.Lerp(A,b, 0.7f));//Vector3間的差值運算
        N = Color.Lerp(R, B, 0.8f);//Color間的差值運算
    }

}
