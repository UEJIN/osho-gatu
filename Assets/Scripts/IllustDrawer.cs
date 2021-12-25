using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoffeeR.Paint
{
    public class IllustDrawer : MonoBehaviour
    {

        [Header("LineRendererがついたPrefab指定")]
        [SerializeField]
        LineRenderer lineRendererPrefab;

        [Header("線の太さを指定")]
        [SerializeField]
        [Range(0.05f, 1.0f)]
        float lineWidth;

        [Header("線のタイプを指定")]
        [SerializeField]
        EnumLineType lineType;

        [Header("円等を描く時の中心点を設定")]
        [SerializeField]
        Vector3 centerPosition;

        /// <summary>
        /// 描画コンポーネント群
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
        /// レンダラー付きのオブジェクトを作成する
        /// </summary>
        void CreateLineRendererObject(EnumLineType type)
        {
            lineRendererMultipleList.Add(new List<LineRenderer>());

            // 線を作成する個数を設定
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
                    Debug.LogError(type.ToString() + "での線の本数が指定されていません。");
                    break;
            }

            for (int i = 0; i < lineCount; i++)
            {
                // 描画コンポーネントがついたオブジェクトを作成
                LineRenderer line = Instantiate(lineRendererPrefab);

                // 太さを設定する
                line.startWidth = lineWidth;
                line.endWidth = lineWidth;

                // 子オブジェクトに設定
                line.transform.parent = this.transform;

                // 作成した描画コンポーネントをこのクラスにキャッシュする
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
                    Debug.LogError(lineType.ToString() + "での線の描き方が指定されていません。");
                    break;
            }
        }

        /// <summary>
        /// 一つ前の状態に戻す
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
                Debug.Log("線がないためUndoされませんでした");
            }
        }

        /// <summary>
        /// 入力位置を返却する
        /// </summary>
        /// <returns></returns>
        Vector2 GetPostionOfInput()
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 1.0f);
            return Camera.main.ScreenToWorldPoint(position);
        }
    }

    /// <summary>
    /// 線のタイプ
    /// </summary>
    internal enum EnumLineType
    {

        /// <summary>
        /// 自由線
        /// </summary>
        FreeHand,
        /// <summary>
        /// 対称定規
        /// </summary>
        Symmetry,
        /// <summary>
        /// 円
        /// </summary>
        Circle,
        /// <summary>
        /// 螺旋
        /// </summary>
        Spiral,
        /// <summary>
        /// 正方形状
        /// </summary>
        LikeSquare
    }
}