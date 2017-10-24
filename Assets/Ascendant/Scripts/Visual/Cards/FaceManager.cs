using UnityEngine;

namespace Ascendant.Visual.Cards {
    public class FaceManager : MonoBehaviour {
        public GameObject frontFace;
        public GameObject backFace;
//        private bool first = true;

        public void Update () {
            bool facing = Vector3.Dot(this.transform.forward, this.transform.position - Camera.main.transform.position) > 0;
            this.frontFace.SetActive(facing);
            this.backFace.SetActive(!facing);
//            if (this.first) {
//                this.first = false;
//                print(string.Format("card forward: {0}, card pos: {1}, cam pos: {2}", this.transform.forward, this.transform.position, Camera.main.transform.position));
//                print(string.Format("dist vector: {0}", this.transform.position - Camera.main.transform.position));
//                print(string.Format("dot product: {0}", Vector3.Dot(this.transform.forward, this.transform.position - Camera.main.transform.position)));
//            }
        }
    }
}
