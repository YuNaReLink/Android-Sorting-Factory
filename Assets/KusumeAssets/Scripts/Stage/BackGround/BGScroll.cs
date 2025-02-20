using UnityEngine;

namespace CreateScript
{
    /*
     * �o�b�N�O�����h�̃X�N���[�����s���N���X
     * backGrounds�ɃZ�b�g�����摜���E���獶�փX�N���[������
     */
    public class BGScroll : MonoBehaviour
    {
        //�w�i���X�N���[��������X�s�[�h
        [SerializeField] 
        private float           scrollSpeed;
        //�w�i�̃X�N���[�����J�n����ʒu
        [SerializeField] 
        private float           startLine;
        //�w�i�̃X�N���[�����I������ʒu
        [SerializeField] 
        private float           deadLine;
        //�����̔w�i�摜��z�u����ʒu�̊Ԋu
        [SerializeField]
        private float           imageInterval = 19f;

        [SerializeField]
        private GameObject[]    backGrounds = new GameObject[2];

        [SerializeField]
        private bool left;

        [SerializeField]
        private bool            scrollMove = true;
        public void SetScrollStop(bool stop) { scrollMove = stop; }

        public void SetScrollSpeed(float speed) { scrollSpeed = speed; }

        private void Update()
        {
            if (!scrollMove) { return; }
            for(int i = 0;i < backGrounds.Length; i++)
            {
                Scroll(i);
            }
        }
        //Update�ŃX�N���[������
        public void Scroll(int i)
        {
            float speed = scrollSpeed * Time.deltaTime;
            backGrounds[i].transform.Translate(speed, 0, 0); //x���W��scrollSpeed��������

            if (!left)
            {
                if (backGrounds[i].transform.localPosition.x < deadLine) //�����w�i��x���W���deadLine���傫���Ȃ�����
                {
                    backGrounds[i].transform.localPosition = new Vector3(startLine, backGrounds[i].transform.localPosition.y, backGrounds[i].transform.localPosition.z);//�w�i��startLine�܂Ŗ߂�
                }
            }
            else
            {
                if (backGrounds[i].transform.localPosition.x > deadLine) //�����w�i��x���W���deadLine���傫���Ȃ�����
                {
                    backGrounds[i].transform.localPosition = new Vector3(startLine, backGrounds[i].transform.localPosition.y, backGrounds[i].transform.localPosition.z);//�w�i��startLine�܂Ŗ߂�
                }
            }
        }
    }
}
