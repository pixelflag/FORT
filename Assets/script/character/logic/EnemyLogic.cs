using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��xview�ƃZ�b�g�ŏ����Ă��܂����B�����͂��ꂩ��B�����Ȃ����������ǁB

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
        // ���͂��̂܂ܕԂ��B
        return power;
    }

    public virtual void CancelMove()
    {

    }
}


// �X���C��
// �����E���E������B
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
                // �v���v������
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
                // �v�����Ƃ��Ȃ���ړ��B
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

// �o�b�^
// �W�����v�ړ�
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
                // �W�[���Ƃ��Ă�
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
                // �W�����v���Ĉړ��B
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
        // �L�����Z������̎��R���������̂����B
        self.h = 0;
    }
}

// �R�E����

// ������ł���B�n�`�𖳎�����B�O����`���ĉ���Ă���B
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
                // �W�[���Ƃ��Ă�
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
                // �o�T�o�T���Ȃ���ړ�
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

// ���[��
// �n���ɐ���B���̊Ԃ͌����Ȃ������G�ɂȂ�B
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
                // �����
                if (16 < progress)
                {
                    state = State.ShowMove;
                    progress = 0;
                    direction = Calculate.PositionToNomaliseVector(self.position, objects.player.position);
                    self.collision.objectCollisionDisabled = false;
                }
                break;
            case State.ShowMove:
                // ����������Ă���
                if (180 < progress)
                {
                    state = State.Hide;
                    prevCount = Global.count;
                    self.position += direction * 1;
                }
                break;
            case State.Hide:
                // �n�ʂɐ���
                if (16 < progress)
                {
                    state = State.HideMove;
                    progress = 0;
                    direction = Calculate.DigreeToVector(pRandom.Range(0, 360), 1);
                    self.collision.objectCollisionDisabled = true;
                }
                break;
            case State.HideMove:
                // �n�ʂ̒��ňړ��i���G�j
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

// �S�u����
// ������肤����Ă���B���E������B���E�ɂ͂���Ɗ���Ă���B
// �����U��B������ӂ�\������B����낫��댩�񂷁B
// �܂��A�������B���ƂŁB
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


// �}�b�h�}��
// ���[�p�[

// �R���h��
// �t�@�C�A�G�������g
// �A�C�X�G�������g

// �ւ�

// �A�C�X�S�[����
// �_�C�A�E���t

// �R�{���h
// �S�u����

/*
�ړ��ʒu�̒T��

�����_���i�͈́j
�T�[�N���i�󒆂̂݁j
�����ړ�
�ǂɂԂ���ƉE���

�E�ړ����@

�C�[�W���O
���C�i�[
�W�����v
�_�b�V��

�E�ҋ@����

���邭����i���E���񂷁j
�����Ƃ��Ă���B
�n���ɐ���B

�E���E�i�㉺���EN�}�X��T���A�͈͒T���j

����U��
�ˌ�
����

�E�n��A�󒆁A�n���t���O
�E�_���[�W

�Y�ށi����j
�k�ށB
*/