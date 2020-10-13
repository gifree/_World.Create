/// <summary>
/// 当ROLE数值发生改变时调用，作为中间层，实现低层级与高层间的映射更新
/// </summary>
public interface IWhileUpdCharacterInfoValue
{
    void UpdAtkBasic(CharacterInfo info);
    void UpdAtk(CharacterInfo info);
    void UpdDefBasic(CharacterInfo info);
    void UpdDef(CharacterInfo info);
    void UpdPowerBasic(CharacterInfo info);
    void UpdPower(CharacterInfo info);
    void UpdStrengthBasic(CharacterInfo info);
    void UpdStrength(CharacterInfo info);
    void UpdSpiritBasic(CharacterInfo info);
    void UpdSpirit(CharacterInfo info);
    void UpdIntelligenceBasic(CharacterInfo info);
    void UpdIntelligence(CharacterInfo info);
    void UpdAtkPhysicsBasic(CharacterInfo info);
    void UpdAtkPhysics(CharacterInfo info);
    void UpdAtkMagicBasic(CharacterInfo info);
    void UpdAtkMagic(CharacterInfo info);
    void UpdDefPhysicsBasic(CharacterInfo info);
    void UpdDefPhysics(CharacterInfo info);
    void UpdDefMagicBasic(CharacterInfo info);
    void UpdDefMagic(CharacterInfo info);
    void UpdAtkNatureBasic(CharacterInfo info);
    void UpdAtkNature(CharacterInfo info);
    void UpdAtkFairyBasic(CharacterInfo info);
    void UpdAtkFairy(CharacterInfo info);
    void UpdAtkElementBasic(CharacterInfo info);
    void UpdAtkElement(CharacterInfo info);
    void UpdDefNatureBasic(CharacterInfo info);
    void UpdDefNature(CharacterInfo info);
    void UpdDefFairyBasic(CharacterInfo info);
    void UpdDefFairy(CharacterInfo info);
    void UpdDefElementBasic(CharacterInfo info);
    void UpdDefElement(CharacterInfo info);
}
/// <summary>
/// 当外部改变ROLE数值时调用
/// </summary>
public interface IWhileChangeCharacterInfoValue
{
    void UpdAtkBasic(float atk);
    void UpdAtk();
    void UpdDefBasic(float def);
    void UpdDef();
    void UpdPowerBasic(float power);
    void UpdPower();
    void UpdStrengthBasic(float basic);
    void UpdStrength();
    void UpdSpiritBasic(float spirit);
    void UpdSpirit();
    void UpdIntelligenceBasic(float intelligence);
    void UpdIntelligence();
    void UpdAtkPhysicsBasic(float atkPhysics);
    void UpdAtkPhysics();
    void UpdAtkMagicBasic(float atkMagic);
    void UpdAtkMagic();
    void UpdDefPhysicsBasic(float defPhysics);
    void UpdDefPhysics();
    void UpdDefMagicBasic(float defMagic);
    void UpdDefMagic();
    void UpdAtkNatureBasic(float atkNature);
    void UpdAtkNature();
    void UpdAtkFairyBasic(float atkFairy);
    void UpdAtkFairy();
    void UpdAtkElementBasic(float atkElement);
    void UpdAtkElement();
    void UpdDefNatureBasic(float defNature);
    void UpdDefNature();
    void UpdDefFairyBasic(float defFairy);
    void UpdDefFairy();
    void UpdDefElementBasic(float defElement);
    void UpdDefElement();
}