using System.Collections.Generic;

public class ModelDecorator : AbstractItemDecorator
{
    private int _model=0;
    private string _name;
    public ModelDecorator(Item baseItem, int model,string name) : base(baseItem)
    {
        _model=model;
        _name=name;
    }


    public override List<string> GetDecoratorsLists(){
        List<string> r = p_baseItem.GetDecoratorsLists();
        r.Add(_name);
        return r;
    }
    public override ItemRenderInfo GetRenderInfo(){
        return p_baseItem.GetRenderInfo().SetJunkModel(_model);
    }
}