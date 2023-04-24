using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 一度viewとセットで書いてしまおう。分離はそれから。分離なさそうだけど。

public abstract class EnemyLogic : DIMonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites = default;

    protected int prevCount = 0;

    protected Enemy self;
    private BattleParameters battleData;

    protected PRandom pRandom;

    public virtual void Initialize(Enemy self, BattleParameters battleData, uint seed)
    {
        this.self = self;
        this.battleData = battleData;

        pRandom = new PRandom(seed);
    }

    public virtual void Execute()
    {
        // override;
    }

    public int GetAttack()
    {
        return battleData.attack;
    }

    public virtual int Damage(int power)
    {
        // 今はそのまま返す。
        return power;
    }

    public virtual void CancelMove()
    {

    }
}


// スライム
// ただウロウロする。
public class SlimeLogic : EnemyLogic
{
    private int waitCount = 60;

    private Vector3 vector;
    private int moveTotalFrame = 16;
    private int progress;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private State state;
    private enum State
    {
        Wait,
        Move,
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Wait:
                // プルプルする
                if (waitCount < Global.count - prevCount)
                {
                    progress = 0;

                    prevCount = Global.count;
                    state = State.Move;

                    Vector3 movePosition = Calculate.DigreeToVector(pRandom.Range(0, 360), 16);
                    startPosition = self.position;
                    targetPosition = self.position + movePosition;
                }
                break;
            case State.Move:
                // プルっとしながら移動。
                Vector3 current = Easing2D.CubicOut(progress, moveTotalFrame, startPosition, targetPosition); 
                Vector3 next = Easing2D.CubicOut(progress+1, moveTotalFrame, startPosition, targetPosition);
                vector = next - current;
                self.position += vector;

                self.view.SetSprite((int)progress / 4);

                progress++;
                if(progress <= moveTotalFrame)
                {
                    state = State.Wait;
                }
                break;
        }
    }

    public override void CancelMove()
    {
        prevCount = Global.count;
        state = State.Wait;
    }
}

// バッタ
// ジャンプ移動
public class LocustLogic : EnemyLogic
{
    private EasingState easing;

    private int waitCount = 60;
    private float jumpHeight = 32;
    private int totalFrame = 64;

    private State state;
    private enum State
    {
        Wait,
        Move,
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Wait:
                // ジーっとしてる
                if (waitCount < Global.count - prevCount)
                {
                    state = State.Move;

                    Vector3 movePosition = Calculate.DigreeToVector(pRandom.Range(0, 360), 32);
                    Vector3 startPosition = self.position;
                    Vector3 targetPosition = self.position + movePosition;

                    easing = new EasingState(startPosition, targetPosition, EasingType.CubicOut, totalFrame);
                }
                break;
            case State.Move:
                // ジャンプして移動。
                if (easing.Next())
                {
                    prevCount = Global.count;
                    state = State.Wait;
                    self.h = 0;
                }
                else
                {
                    self.position = easing.position;
                    self.h = Easing.SineInOut(easing.frame, totalFrame, 0, jumpHeight);
                }
                break;
        }
    }

    public override void CancelMove()
    {
        prevCount = Global.count;
        state = State.Wait;
        // キャンセルからの自然落下をそのうち。
        self.h = 0;
    }
}

// コウモリ

// 低空を飛んでいる。地形を無視する。軌道を描いて回っている。
public class BatLogic : EnemyLogic
{
    private EasingState easing;

    private int waitCount = 128;
    private int totalFrame = 128;

    private State state;
    private enum State
    {
        Wait,
        Move,
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Wait:
                // ジーっとしてる
                if (waitCount < Global.count - prevCount)
                {
                    state = State.Move;

                    Vector3 movePosition = Calculate.DigreeToVector(pRandom.Range(0, 360), 128);
                    Vector3 startPosition = self.position;
                    Vector3 targetPosition = self.position + movePosition;

                    easing = new EasingState(startPosition, targetPosition, EasingType.QuadInOut, totalFrame);
                }
                break;
            case State.Move:
                // バサバサしながら移動
                if (easing.Next())
                {
                    prevCount = Global.count;
                    state = State.Wait;
                    self.h = 0;
                }
                else
                {
                    self.position = easing.position;
                }
                break;
        }
    }

    public override void CancelMove()
    {
        prevCount = Global.count;
        state = State.Wait;
        self.h = 0;
    }
}

// ワーム
// 地中に潜る。その間は見えない＆無敵になる。
public class WormLogic : EnemyLogic
{
    public int progress { get; private set; }
    private Vector3 direction;

    private State state;
    private enum State
    {
        Show,
        ShowMove,
        Hide,
        HideMove,
    }

    public override void Execute()
    {
        switch (state)
        {
            case State.Show:
                // 現れる
                if (16 < progress)
                {
                    state = State.ShowMove;
                    progress = 0;
                    direction = Calculate.PositionToNomaliseVector(self.position, objects.player.position);
                    self.collision.objectCollisionDisabled = false;
                }
                break;
            case State.ShowMove:
                // ゆっくり寄ってくる
                if (180 < progress)
                {
                    state = State.Hide;
                    prevCount = Global.count;
                    self.position += direction * 1;
                }
                break;
            case State.Hide:
                // 地面に潜る
                if (16 < progress)
                {
                    state = State.HideMove;
                    progress = 0;
                    direction = Calculate.DigreeToVector(pRandom.Range(0, 360), 1);
                    self.collision.objectCollisionDisabled = true;
                }
                break;
            case State.HideMove:
                // 地面の中で移動（無敵）
                if (180 < progress)
                {
                    state = State.Hide;
                    progress=0;
                    self.position += direction * 2;
                }
                break;
        }
        progress++;
    }

    public override void CancelMove()
    {
        prevCount = Global.count;
        state = State.Hide;
        progress = 0;
    }
}

// ゴブリン
// ゆっくりうろついている。視界がある。視界にはいると寄ってくる。
// 武器を振る。武器をふる予備動作。きょろきょろ見回す。
// まだ、早いか。あとで。
public class GoblinsLogic : EnemyLogic
{
}

public struct EasingState
{
    public int totalFrame;
    public int frame;

    public Vector3 startPosition;
    public Vector3 targetPosition;
    public EasingType type;

    public bool isComplete { get { return totalFrame <= frame; } }

    public EasingState(Vector3 startPosition, Vector3 targetPosition, EasingType type, int totalFrame)
    {
        this.startPosition = startPosition;
        this.targetPosition = targetPosition;
        this.totalFrame = totalFrame;
        this.type = type;
        frame = 0;
    }

    public bool Next()
    {
        frame++;
        frame = isComplete ? totalFrame : frame;
        return isComplete;
    }

    public Vector3 position
    {
        get
        {
            return Easing2D.ForType(type, frame, totalFrame, startPosition, targetPosition);
        }
    }
}


// マッドマン
// ルーパー

// コンドル
// ファイアエレメント
// アイスエレメント

// へび

// アイスゴーレム
// ダイアウルフ

// コボルド
// ゴブリン

/*
移動位置の探索

ランダム（範囲）
サークル（空中のみ）
直線移動
壁にぶつかると右回り

・移動方法

イージング
ライナー
ジャンプ
ダッシュ

・待機動作

くるくる回る（視界を回す）
じっとしている。
地中に潜る。

・視界（上下左右Nマスを探索、範囲探索）

武器攻撃
突撃
逃避

・地上、空中、地中フラグ
・ダメージ

産む（分裂）
縮む。
*/