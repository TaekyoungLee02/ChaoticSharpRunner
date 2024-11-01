using UnityEngine;

public class CoinItem : ItemBase
{

    private bool magnet;

    public override void Use()
    {
        // 점수 오르는 코드 작성
    }

    private void FixedUpdate()
    {
        if(magnet)
        {
            //플레이어 쪽으로 다가가기
        }
    }
}
