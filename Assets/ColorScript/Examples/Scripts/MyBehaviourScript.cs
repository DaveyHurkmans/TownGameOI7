using UnityEngine;

namespace ExampleCompany.ExampleGame
{
    /// <summary>
    /// Color Script example monoBehaviour
    /// 
    /// Color Script Pro is available on the Unity Asset Store here:
    /// https://assetstore.unity.com/packages/slug/165351
    ///
    /// This script was written on a mac, so it will have Unix line endings.
    /// If you're on a PC, they'll be highlighted in red
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MyBehaviourScript : MonoBehaviour
    {

        [Range(0.1f, 1)]
        public float colorCycleDuration = 1.0f;


        //private member fields that aren't serialized
        //or exposed on the GameObject instance
        //but can still be edited in Color Script! (Pro only)
        private Color color1 = new Color(1.00f, 0.50f, 0.91f, 1.00f);
        private Color color2 = new Color32(0, 255, 43, 255);

        private Material mMaterial;
        private float mfAnimationStartTime;


        // Start is called before the first frame update
        void Start()
        {
            name = "Color Cycling Cube";
            mMaterial = GetComponent<MeshRenderer>().sharedMaterial;
            mfAnimationStartTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            float t = (Time.time - mfAnimationStartTime) / colorCycleDuration;
            mMaterial.color = Color.Lerp(color1, color2, t % 1);
        }
    }
}
