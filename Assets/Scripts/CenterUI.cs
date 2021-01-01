using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 中央のUIを制御する。
/// </summary>
public class CenterUI : MonoBehaviour
{
    /// <summary>
    /// 表示するメッセージのstringリスト。
    /// </summary>
    [SerializeField] [TextArea(1, 2)] private List<string> messages = default;

    /// <summary>
    /// 選択肢に応じたボタンのセット。
    /// </summary>
    [SerializeField] private List<GameObject> buttonSets = default;

    /// <summary>
    /// 表示するメッセージ。
    /// </summary>
    [SerializeField] private Text message = default;

    /// <summary>
    /// UIの状態（表示情報）を変更。
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(GameState state)
    {
        ChangeMessage(messages[(int)state]);

        buttonSets[0].SetActive(false);
        buttonSets[1].SetActive(false);

        if(state == GameState.HitOrStand)
        {
            buttonSets[0].SetActive(true);
        }
        else if( (state == GameState.YouWin) || (state == GameState.YouLose) )
        {
            buttonSets[1].SetActive(true);
        }
    }

    /// <summary>
    /// 中央に表示するテキストを変更する。
    /// </summary>
    /// <param name="message"></param>
    private void ChangeMessage(string message)
    {
        this.message.text = message;
    }
}
