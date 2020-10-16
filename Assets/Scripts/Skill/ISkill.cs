public class SkillInfo
{
    public int From;
    public int To;
}

public interface ISkillTrigger
{
    void Init(string args);
    void Start();
    void Update(SkillInfo info);
    void Shut();
    ISkillTrigger Clone();

    bool Enable { get; }
    bool Executing { get; }
}

public abstract class SkillTrigger : ISkillTrigger
{
    private bool _enable = false;
    protected bool executing = false;

    public abstract void Init(string args);
    public virtual void Start() => _enable = true;
    public abstract void Update(SkillInfo info);
    public virtual void Shut() => _enable = false;
    public abstract ISkillTrigger Clone();

    public bool Enable => _enable;
    public bool Executing => executing;
}



public class TriggerFaceToTarget : SkillTrigger
{
    public override ISkillTrigger Clone() => (ISkillTrigger)this.MemberwiseClone();

    public override void Init(string args)
    {
        
    }

    public override void Update(SkillInfo info)
    {
        executing = true;
    }
}


public interface ISkillEventDispatcher
{

}


public interface ISkillEventReceiver
{

}





/*
 以花为貌,以鸟为声,以月为神,以柳为态,以玉为骨,以冰雪为肤,以秋水为姿,以诗词为心。吾无间然矣。《幽梦影》张潮。
 花样，鸟声，月神，柳态，玉骨，冰肤，秋姿，诗心。
 */
