using System.Collections.Generic;

public class Key
{
    public int amount { get; set; }
    public List<int> key_offsets { get; set; }
    public string k_image { get; set; }
}

public class Vin
{
    public Key key { get; set; }
}

public class Target
{
    public string key { get; set; }
}

public class Vout
{
    public int amount { get; set; }
    public Target target { get; set; }
}

public class EcdhInfo
{
    public string amount { get; set; }
}

public class RctSignatures
{
    public int type { get; set; }
    public int txnFee { get; set; }
    public List<EcdhInfo> ecdhInfo { get; set; }
    public List<string> outPk { get; set; }
}

public class Bp
{
    public string A { get; set; }
    public string S { get; set; }
    public string T1 { get; set; }
    public string T2 { get; set; }
    public string taux { get; set; }
    public string mu { get; set; }
    public List<string> L { get; set; }
    public List<string> R { get; set; }
    public string a { get; set; }
    public string b { get; set; }
    public string t { get; set; }
}

public class MG
{
    public List<List<string>> ss { get; set; }
    public string cc { get; set; }
}

public class RctsigPrunable
{
    public int nbp { get; set; }
    public List<Bp> bp { get; set; }
    public List<MG> MGs { get; set; }
    public List<string> pseudoOuts { get; set; }
}

public class RootObject
{
    public int version { get; set; }
    public int unlock_time { get; set; }
    public List<Vin> vin { get; set; }
    public List<Vout> vout { get; set; }
    public List<int> extra { get; set; }
    public RctSignatures rct_signatures { get; set; }
    public RctsigPrunable rctsig_prunable { get; set; }
}