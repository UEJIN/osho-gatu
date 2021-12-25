using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoffeeR.Paint
{
    public class IllustDrawer : MonoBehaviour
    {

        [Header("LineRenderer������Prefab�w��")]
        [SerializeField]
        LineRenderer lineRendererPrefab;

        [Header("���̑������w��")]
        [SerializeField]
        [Range(0.05f, 1.0f)]
        float lineWidth;

        [Header("���̃^�C�v���w��")]
        [SerializeField]
        EnumLineType lineType;

        [Header("�~����`�����̒��S�_��ݒ�")]
        [SerializeField]
        Vector3 centerPosition;

        /// <summary>
        /// �`��R���|�[�l���g�Q
        /// </summary>
        List<List<LineRenderer>> lineRendererMultipleList;

        void Start()
        {
            lineRendererMultipleList = new List<List<LineRenderer>>();
        }

        void Update()
        {
            var mousePosition = GetPostionOfInput();
            //Debug.Log(mousePosition);

            if (Input.GetMouseButtonDown(1))
            {
                UndoLine();
            }
            if (Input.GetMouseButtonDown(0))
            {
                CreateLineRendererObject(lineType);
                lineRendererMultipleList.Last().Last().SetPosition(0, mousePosition);
                lineRendererMultipleList.Last().Last().positionCount = 1;
            }
            if (Input.GetMouseButton(0))
            {
                //var mousePosition = GetPostionOfInput();
                DrawingLine(mousePosition);
            }
        }

        /// <summary>
        /// �����_���[�t���̃I�u�W�F�N�g���쐬����
        /// </summary>
        void CreateLineRendererObject(EnumLineType type)
        {
            lineRendererMultipleList.Add(new List<LineRenderer>());

            // �����쐬�������ݒ�
            int lineCount = 0;
            switch (type)
            {
                case EnumLineType.FreeHand:
                case EnumLineType.Circle:
                case EnumLineType.Spiral:
                    lineCount = 1;
                    break;
                case EnumLineType.LikeSquare:
                    lineCount = 4;
                    break;
                case EnumLineType.Symmetry:
                    lineCount = 12;
                    break;
                default:
                    Debug.LogError(type.ToString() + "�ł̐��̖{�����w�肳��Ă��܂���B");
                    break;
            }

            for (int i = 0; i < lineCount; i++)
            {
                // �`��R���|�[�l���g�������I�u�W�F�N�g���쐬
                LineRenderer line = Instantiate(lineRendererPrefab);

                // ������ݒ肷��
                line.startWidth = lineWidth;
                line.endWidth = lineWidth;

                // �q�I�u�W�F�N�g�ɐݒ�
                line.transform.parent = this.transform;

                // �쐬�����`��R���|�[�l���g�����̃N���X�ɃL���b�V������
                lineRendererMultipleList.Last().Add(line);
            }
        }

        void DrawingLine(Vector3 mousePosition)
        {

            int positionIndex = 0;
            Debug.Log(positionIndex);
            switch (lineType)
            {
                case EnumLineType.FreeHand:
                    lineRendererMultipleList.Last().Last().positionCount++;
                    positionIndex = lineRendererMultipleList.Last().Last().positionCount;
                    //lineRendererMultipleList.Last().Last().SetPosition(0, mousePosition);
                    lineRendererMultipleList.Last().Last().SetPosition(positionIndex-1, mousePosition);
                    break;

                case EnumLineType.Circle:
                    lineRendererMultipleList.Last().Last().positionCount++;
                    positionIndex = lineRendererMultipleList.Last().Last().positionCount;

                    if (positionIndex == 1)
                    {
                        lineRendererMultipleList.Last().Last().SetPosition(positionIndex - 1, centerPosition + mousePosition);
                    }
                    else
                    {
                        var firstPosition = lineRendererMultipleList.Last().Last().GetPosition(0);
                        lineRendererMultipleList.Last().Last().SetPosition(positionIndex - 1, centerPosition + Quaternion.Euler(0f, 0f, positionIndex * 5) * (firstPosition - centerPosition));
                    }
                    break;

                case EnumLineType.Spiral:
                    lineRendererMultipleList.Last().Last().positionCount++;
                    positionIndex = lineRendererMultipleList.Last().Last().positionCount;

                    if (positionIndex == 1)
                    {
                        lineRendererMultipleList.Last().Last().SetPosition(positionIndex - 1, centerPosition + mousePosition);
                    }
                    else
                    {
                        var firstPosition = lineRendererMultipleList.Last().Last().GetPosition(0);
                        lineRendererMultipleList.Last().Last().SetPosition(positionIndex - 1, centerPosition + Quaternion.Euler(0f, 0f, positionIndex * 5) * (firstPosition - centerPosition) / (1 + positionIndex * 0.025f));
                    }
                    break;

                case EnumLineType.Symmetry:
                    var degreeIndex = 0;
                    foreach (var line in lineRendererMultipleList.Last())
                    {
                        line.positionCount++;
                        positionIndex = line.positionCount;
                        line.SetPosition(positionIndex - 1, centerPosition + Quaternion.Euler(0f, 0f, degreeIndex * 360 / 12) * (mousePosition - centerPosition));
                        degreeIndex++;
                    }
                    break;

                case EnumLineType.LikeSquare:
                    for (int i = 0; i < 4; i++)
                    {
                        lineRendererMultipleList.Last()[i].positionCount++;
                    }
                    positionIndex = lineRendererMultipleList.Last().Last().positionCount;


                    if (positionIndex == 1)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            lineRendererMultipleList.Last()[i].SetPosition(positionIndex - 1, centerPosition + Quaternion.Euler(0f, 0f, (i - 1) * 90) * mousePosition);
                        }
                    }
                    else
                    {
                        var vertexPos = lineRendererMultipleList.Last()[0].GetPosition(0) - centerPosition;
                        lineRendererMultipleList.Last()[0].SetPosition(positionIndex - 1, centerPosition + Quaternion.Euler(0f, 0f, 0f) * vertexPos + positionIndex * Vector3.down * 0.1f);
                        lineRendererMultipleList.Last()[1].SetPosition(positionIndex - 1, centerPosition + Quaternion.Euler(0f, 0f, 90f) * vertexPos + positionIndex * Vector3.right * 0.1f);
                        lineRendererMultipleList.Last()[2].SetPosition(positionIndex - 1, centerPosition + Quaternion.Euler(0f, 0f, 180f) * vertexPos + positionIndex * Vector3.up * 0.1f);
                        lineRendererMultipleList.Last()[3].SetPosition(positionIndex - 1, centerPosition + Quaternion.Euler(0f, 0f, 270f) * vertexPos + positionIndex * Vector3.left * 0.1f);
                    }
                    break;

                default:
                    Debug.LogError(lineType.ToString() + "�ł̐��̕`�������w�肳��Ă��܂���B");
                    break;
            }
        }

        /// <summary>
        /// ��O�̏�Ԃɖ߂�
        /// </summary>
        void UndoLine()
        {
            try
            {
                var lastLineRendererList = lineRendererMultipleList.Last();
                foreach (var line in lastLineRendererList)
                {
                    Destroy(line.gameObject);
                }
                lineRendererMultipleList.Remove(lastLineRendererList);
            }
            catch (System.InvalidOperationException)
            {
                Debug.Log("�����Ȃ�����Undo����܂���ł���");
            }
        }

        /// <summary>
        /// ���͈ʒu��ԋp����
        /// </summary>
        /// <returns></returns>
        Vector2 GetPostionOfInput()
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
            return Camera.main.ScreenToWorldPoint(position);
        }
    }

    /// <summary>
    /// ���̃^�C�v
    /// </summary>
    internal enum EnumLineType
    {

        /// <summary>
        /// ���R��
        /// </summary>
        FreeHand,
        /// <summary>
        /// �Ώ̒�K
        /// </summary>
        Symmetry,
        /// <summary>
        /// �~
        /// </summary>
        Circle,
        /// <summary>
        /// ����
        /// </summary>
        Spiral,
        /// <summary>
        /// �����`��
        /// </summary>
        LikeSquare
    }
}