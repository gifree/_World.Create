using System;
/// <summary>
/// 基本信息
/// 由基础数值发生改变时触发
/// 三级数值，基础一级变化触发二级变化，二级触发三级
/// 数值计算采用两套计算，一套存储基础数值，一套用来存储演算数值，当基础数值变化时更新临时演算数值
/// 因为层级的设定出现，同层级属性不能出现互相影响。更不能出现高层级影响低层级的现象
/// 否则会死循环
/// </summary>
public class CharacterInfo
{
    #region basic data
    private string _name;
    private int _hp;
    private int _hpMax;
    private int _mp;
    private int _mpMax;
    private float _atk;
    private float _atkBasic;
    private float _def;
    private float _defBasic;
    private float _power;
    private float _powerBasic;
    private float _strength;
    private float _strengthBasic;
    private float _spirit;
    private float _spiritBasic;
    private float _intelligence;
    private float _intelligenceBasic;
    private float _atkPhysics;
    private float _atkPhysicsBasic;
    private float _atkMagic;
    private float _atkMagicBasic;
    private float _defPhysics;
    private float _defPhysicsBasic;
    private float _defMagic;
    private float _defMagicBasic;
    private float _atkNature;
    private float _atkNatureBasic;
    private float _atkFairy;
    private float _atkFairyBasic;
    private float _atkElement;
    private float _atkElementBasic;
    private float _defNature;
    private float _defNatureBasic;
    private float _defFairy;
    private float _defFairyBasic;
    private float _defElement;
    private float _defElementBasic;
    #endregion

    public CharacterInfo() { }
    public CharacterInfo(string name) => Name = name;

    /// <summary>
    /// 接口实例，实现数值更新同步
    /// </summary>
    private IWhileUpdCharacterInfoValue _whileUpdValue;

    /// <summary>
    /// 注册代理任务接口
    /// </summary>
    /// <param name="ichange"></param>
    public void RegisterTask(IWhileUpdCharacterInfoValue iUpd) => _whileUpdValue = iUpd;

    #region basic property
    public string Name { get => _name; private set => _name = value; }
    public int Hp { get => _hp; set {
            _hp = value <= 0 ? 0 : value;
        } }
    public int HpMax { get => _hpMax; set {
            _hpMax = value <= 0 ? 0 : value;
        } }
    public int Mp { get => _mp; set {
            _mp = value <= 0 ? 0 : value;
        } }
    public int MpMax { get => _mpMax; set {
            _mpMax = value <= 0 ? 0 : value;
        } }

    public float Atk { get => _atk; set {
            _atk = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdAtk(this);
        } }
    public float AtkBasic { get => _atkBasic; set {
            _atkBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdAtkBasic(this);
        } }
    public float Def { get => _def; set {
            _def = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDef(this);
        } }
    public float DefBasic { get => _defBasic; set {
            _defBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDefBasic(this);
        } }
    public float Power { get => _power; set {
            _power = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdPower(this);
        } }
    public float PowerBasic { get => _powerBasic; set {
            _powerBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdPowerBasic(this);
        } }
    public float Strength { get => _strength; set {
            _strength = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdStrength(this);
        } }
    public float StrengthBasic { get => _strengthBasic; set {
            _strengthBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdStrengthBasic(this);
        } }
    public float Spirit { get => _spirit; set {
            _spirit = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdSpirit(this);
        } }
    public float SpiritBasic { get => _spiritBasic; set {
            _spiritBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdSpiritBasic(this);
        } }
    public float Intelligence { get => _intelligence; set
        {
            _intelligence = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdIntelligence(this);
        } }
    public float IntelligenceBasic { get => _intelligenceBasic; set {
            _intelligenceBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdIntelligenceBasic(this);
        } }
    public float AtkPhysics { get => _atkPhysics; set {
            _atkPhysics = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdAtkPhysics(this);
        } }
    public float AtkPhysicsBasic { get => _atkPhysicsBasic; set {
            _atkPhysicsBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdAtkPhysicsBasic(this);
        } }
    public float AtkMagic { get => _atkMagic; set {
            _atkMagic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdAtkMagic(this);
        } }
    public float AtkMagicBasic { get => _atkMagicBasic; set {
            _atkMagicBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdAtkMagicBasic(this);
        } }
    public float DefPhysics { get => _defPhysics; set {
            _defPhysics = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDefPhysics(this);
        } }
    public float DefPhysicsBasic { get => _defPhysicsBasic; set {
            _defPhysicsBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDefPhysicsBasic(this);
        } }
    public float DefMagic { get => _defMagic; set {
            _defMagic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDefMagic(this);
        } }
    public float DefMagicBasic { get => _defMagicBasic; set {
            _defMagicBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDefMagicBasic(this);
        } }
    public float AtkNature { get => _atkNature; set {
            _atkNature = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdAtkNature(this);
        } }
    public float AtkNatureBasic { get => _atkNatureBasic; set {
            _atkNatureBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdAtkNatureBasic(this);
        } }
    public float AtkFairy { get => _atkFairy; set {
            _atkFairy = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdAtkFairy(this);
        } }
    public float AtkFairyBasic { get => _atkFairyBasic; set {
            _atkFairyBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdAtkFairyBasic(this);
        } }
    public float AtkElement { get => _atkElement; set {
            _atkElement = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdAtkElement(this);
        } }
    public float AtkElementBasic { get => _atkElementBasic; set {
            _atkElementBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDef(this);
        } }
    public float DefNature { get => _defNature; set {
            _defNature = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDefNature(this);
        } }
    public float DefNatureBasic { get => _defNatureBasic; set {
            _defNatureBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDefNatureBasic(this);
        } }
    public float DefFairy { get => _defFairy; set {
            _defFairy = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDefFairy(this);
        } }
    public float DefFairyBasic { get => _defFairyBasic; set {
            _defFairyBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDefFairyBasic(this);
        } }
    public float DefElement { get => _defElement; set {
            _defElement = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDefElement(this);
        } }
    public float DefElementBasic { get => _defElementBasic; set {
            _defElementBasic = value <= 0 ? 0 : value;
            _whileUpdValue?.UpdDefElementBasic(this);
        } }
    #endregion
}

/*
 
# -*- coding: utf-8 -*-
# @Time    : 2019/5/1
# @Author  : water66
# @Site    :
# @File    : notepad.py
# @Version : 1.0
# @Python Version : 3.6
# @Software: PyCharm
 
 
from tkinter import *
from tkinter.filedialog import *
from tkinter.messagebox import *
from tkinter import scrolledtext
import os
 
filename=''
 
 
def author():
    showinfo(title="作者", message="water66")
 
 
def power():
    showinfo(title="版权信息", message="water66")
 
 
def new_file(*args):
    global top, filename, textPad
    top.title("未命名文件")
    filename = None
    textPad.delete(1.0, END)
 
 
def open_file(*args):
    global filename
    filename = askopenfilename(defaultextension=".txt")
    if filename == "":
        filename = None
    else:
        top.title(""+os.path.basename(filename))
        textPad.delete(1.0, END)
        f = open(filename, 'r', encoding="utf-8")
        textPad.insert(1.0, f.read())
        f.close()
 
 
def click_open(event):
    global filename
    top.title("" + os.path.basename(filename))
    textPad.delete(1.0, END)
    f = open(filename, 'r', encoding="utf-8")
    textPad.insert(1.0, f.read())
    f.close()
 
 
def save(*args):
    global filename
    try:
        f=open(filename, 'w', encoding="utf-8")
        msg=textPad.get(1.0, 'end')
        f.write(msg)
        f.close()
    except:
        save_as()
 
 
def save_as(*args):
    global filename
    f = asksaveasfilename(initialfile="未命名.txt", defaultextension=".txt")
    filename = f
    fh = open(f, 'w', encoding="utf-8")
    msg = textPad.get(1.0, END)
    fh.write(msg)
    fh.close()
    top.title(""+os.path.basename(f))
 
 
def rename(newname):
    global filename
    name = os.path.basename(os.path.splitext(filename)[0])
    oldpath = filename
    newpath = os.path.dirname(oldpath)+'/'+newname+'.txt'
    os.rename(oldpath, newpath)
    filename = newpath
    refresh()
        
        
def rename_file(*args):
    global filename
    t = Toplevel()
    t.geometry("260x80+200+250")
    t.title('重命名')
    frame = Frame(t)
    frame.pack(fill=X)
    lable = Label(frame, text="文件名")
    lable.pack(side=LEFT, padx=5)
    var = StringVar()
    e1 = Entry(frame, textvariable=var)
    e1.pack(expand=YES, fill=X, side=RIGHT)
    botton = Button(t, text="确定", command=lambda: rename(var.get()))
    botton.pack(side=BOTTOM, pady=10)
 
 
def delete(*args):
    global filename, top
    choice = askokcancel('提示', '要执行此操作吗')
    if choice:
        if os.path.exists(filename):
            os.remove(filename)
            textPad.delete(1.0, END)
            top.title("记事本")
            filename = ''
 
 
def cut():
    global textPad
    textPad.event_generate("<<Cut>>")
 
 
def copy():
    global textPad
    textPad.event_generate("<<Copy>>")
 
 
def paste():
    global textPad
    textPad.event_generate("<<Paste>>")
 
 
def undo():
    global textPad
    textPad.event_generate("<<Undo>>")
 
 
def redo():
    global textPad
    textPad.event_generate("<<Redo>>")
 
 
def select_all():
    global textPad
    textPad.tag_add("sel", "1.0", "end")
 
 
def find(*agrs):
    global textPad
    t = Toplevel(top)
    t.title("查找")
    t.geometry("260x60+200+250")
    t.transient(top)
    Label(t, text="查找：").grid(row=0, column=0, sticky="e")
    v = StringVar()
    e = Entry(t, width=20, textvariable=v)
    e.grid(row=0, column=1, padx=2, pady=2, sticky="we")
    e.focus_set()
    c = IntVar()
    Checkbutton(t, text="不区分大小写", variable=c).grid(row=1, column=1, sticky='e')
    Button(t, text="查找所有", command=lambda: search(v.get(), c.get(), textPad, t, e)).grid\
        (row=0, column=2, sticky="e"+"w", padx=2, pady=2)
    
    def close_search():
        textPad.tag_remove("match", "1.0", END)
        t.destroy()
    t.protocol("WM_DELETE_WINDOW", close_search)
 
 
def mypopup(event):
    global editmenu
    editmenu.tk_popup(event.x_root, event.y_root)
 
 
def search(needle, cssnstv, textPad, t, e):
    textPad.tag_remove("match", "1.0", END)
    count = 0
    if needle:
        start = 1.0
        while True:
            pos = textPad.search(needle, start, nocase=cssnstv, stopindex=END)
            if not pos:
                break
            strlist = pos.split('.')
            left = strlist[0]
            right = str(int(strlist[1])+len(needle))
            lastpos = left+'.'+right
            textPad.tag_add("match", pos, lastpos)
            count += 1
            start = lastpos
            textPad.tag_config('match', background="yellow")
        e.focus_set()
        t.title(str(count)+"个被匹配")
 
 
def refresh():
    global top, filename
    if filename:
        top.title(os.path.basename(filename))
    else:
        top.title("记事本")
 
 
top = Tk()
top.title("记事本")
top.geometry("640x480+100+50")
 
menubar = Menu(top)
 
# 文件功能
filemenu = Menu(top)
filemenu.add_command(label="新建", accelerator="Ctrl+N", command=new_file)
filemenu.add_command(label="打开", accelerator="Ctrl+O", command=open_file)
filemenu.add_command(label="保存", accelerator="Ctrl+S", command=save)
filemenu.add_command(label="另存为", accelerator="Ctrl+shift+s", command=save_as)
filemenu.add_command(label="重命名", accelerator="Ctrl+R", command=rename_file)
filemenu.add_command(label="删除", accelerator="Ctrl+D", command=delete)
menubar.add_cascade(label="文件", menu=filemenu)
 
# 编辑功能
editmenu = Menu(top)
editmenu.add_command(label="撤销", accelerator="Ctrl+Z", command=undo)
editmenu.add_command(label="重做", accelerator="Ctrl+Y", command=redo)
editmenu.add_separator()
editmenu.add_command(label="剪切", accelerator="Ctrl+X", command=cut)
editmenu.add_command(label="复制", accelerator="Ctrl+C", command=copy)
editmenu.add_command(label="粘贴", accelerator="Ctrl+V", command=paste)
editmenu.add_separator()
editmenu.add_command(label="查找", accelerator="Ctrl+F", command=find)
editmenu.add_command(label="全选", accelerator="Ctrl+A", command=select_all)
menubar.add_cascade(label="编辑", menu=editmenu)
 
# 关于 功能
aboutmenu = Menu(top)
aboutmenu.add_command(label="作者", command=author)
aboutmenu.add_command(label="版权", command=power)
menubar.add_cascade(label="关于", menu=aboutmenu)
 
top['menu'] = menubar
 
shortcutbar = Frame(top, height=25, bg='Silver')
shortcutbar.pack(expand=NO, fill=X)
 
textPad=Text(top, undo=True)
textPad.pack(expand=YES, fill=BOTH)
scroll=Scrollbar(textPad)
textPad.config(yscrollcommand=scroll.set)
scroll.config(command=textPad.yview)
scroll.pack(side=RIGHT, fill=Y)
 
# 热键绑定
textPad.bind("<Control-N>", new_file)
textPad.bind("<Control-n>", new_file)
textPad.bind("<Control-O>", open_file)
textPad.bind("<Control-o>", open_file)
textPad.bind("<Control-S>", save)
textPad.bind("<Control-s>", save)
textPad.bind("<Control-D>", delete)
textPad.bind("<Control-d>", delete)
textPad.bind("<Control-R>", rename_file)
textPad.bind("<Control-r>", rename_file)
textPad.bind("<Control-A>", select_all)
textPad.bind("<Control-a>", select_all)
textPad.bind("<Control-F>", find)
textPad.bind("<Control-f>", find)
 
textPad.bind("<Button-3>", mypopup)
top.mainloop()
 */
