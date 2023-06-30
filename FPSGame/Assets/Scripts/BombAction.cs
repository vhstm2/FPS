using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;                //���� ����Ʈ ������

    public int attackPower = 10;                          //����ź �Ŀ�

    public float explosionRadius = 5f;              //����ź ���� �ݰ�

    // public GameObject bombTarget;

    [SerializeField]  private AudioSource bombAudio;            //����� �ҽ� ������Ʈ
    private void OnCollisionEnter(Collision collision)
    {
        bombAudio.PlayOneShot(bombAudio.clip);

        //overlapSphere : Ư�� ��ǥ�� �������� �ݰ�ȿ� �ִ� ������Ʈ�� �ö��̴� ������ �迭�� �޾ƿ´�.
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 10);
        //���� ȿ�� �����ȿ� �ִ� ���ʹ� ������Ʈ�鿡�� �������� ����.
        foreach (Collider col in cols)
        {
            col.GetComponent<EnemyFSM>().HitEnemy(attackPower);
        }

        GameObject eff = Instantiate(bombEffect);                                                               //����Ʈ ������ ����
        eff.transform.position = transform.position;                                                            //����ź ��ġ�� ����Ʈ ��ġ�� �����ϰ� ����

        transform.GetChild(0).gameObject.SetActive(false);                                          //����ź��  �ڽ� �𵨿� �ö��̴� �߰� �� ��Ȱ��ȭ 
        Destroy(gameObject , bombAudio.clip.samples);                                                       //�浹 ������  ������� ��ŭ ��ٸ� �� ���� ���� 
    }
}
