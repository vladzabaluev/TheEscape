using UnityEngine;

namespace _Scripts.Mechanics.Portal
{
    public class Portal : MonoBehaviour
    {
        private static bool isTeleported = false;
        public bool IsActivePortal = true;

        //������ ��� ������� � ���������� ���� ����� ������
        [SerializeField] private float minDistanceToTeleportAgain = 1f;

        //����� ������� �� ����� ������ ����� ����� ���, ��� ��
        //�������� ��������, � ������ ���, �� ������� ����� ���� ������(������ ������ ��� �������������� ���� �� ������)
        public Portal anotherPortal;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //��� ����� ��������� ������� �� ������ ������, � �� ���, � ������� �� ������
            if (anotherPortal.IsActivePortal)
            {
                if (!isTeleported)
                {
                    collision.gameObject.transform.position = anotherPortal.gameObject.transform.position;
                    isTeleported = true;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (isTeleported)
            {
                if (Vector2.Distance(collision.gameObject.transform.position,
                        anotherPortal.gameObject.transform.position) >= minDistanceToTeleportAgain)
                {
                    isTeleported = false;
                }
            }
        }
    }
}