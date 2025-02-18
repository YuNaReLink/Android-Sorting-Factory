using System;

namespace Kusume
{
    /*
     * �^�C�}�[�̏������s���N���X
     * �l�X�ȕ����Ŏg�p���Ă���
     */
    [System.Serializable ]
    public class Timer
    {
        //��x����̃A�N�V����
        public event Action OnceEnd;
        //���x�ł��g����A�N�V����
        public event Action OnEnd;
        //���݂̃J�E���g
        private float current = 0;

        public float Current => current;
        //�J�E���g�����̃t���O
        private bool up = false;
        //�J�E���g�����̐ݒ�
        public void SetCountUp( bool u)
        {
            up = u;
        }
        //�J�E���g�̃X�^�[�g
        public void Start(float time)
        {
            current = time;
        }
        //�J�E���g�̍X�V
        public void Update(float time)
        {
            if (up)
            {
                current += time;
            }
            else
            {
                if (current <= 0) { return; }
                current -= time;
                if (current <= 0)
                {
                    current = 0;
                    End();
                }
            }
        }
        //�J�E���g�̏I��
        public void End()
        {
            current = 0;
            OnEnd?.Invoke();
            OnceEnd?.Invoke();
            OnceEnd = null;
        }
        //�J�E���g���I����Ă��邩
        public bool IsEnd() { return current <= 0; }
        //�J�E���g�𕪂ɕϊ�
        public int GetMinutes()
        {
            return (int)current / 60;
        }
        //�J�E���g��b�ɕϊ�
        public int GetSecond()
        {
            return (int)current % 60;
        }
    }
}
