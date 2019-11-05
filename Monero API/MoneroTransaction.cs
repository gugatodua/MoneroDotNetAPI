using System.Collections.Generic;

namespace Monero_API
{
    public class Gen
{
    public int height { get; set; }
}

public class Vin
{
    public Gen gen { get; set; }
}

public class Target
{
    public string key { get; set; }
}

public class Vout
{
    public object amount { get; set; }
    public Target target { get; set; }
}

public class MinerTx
{
    public int version { get; set; }
    public int unlock_time { get; set; }
    public List<Vin> vin { get; set; }
    public List<Vout> vout { get; set; }
    public List<int> extra { get; set; }
    public List<object> signatures { get; set; }
}

public class BlockContent
{
    public int major_version { get; set; }
    public int minor_version { get; set; }
    public int timestamp { get; set; }
    public string prev_id { get; set; }
    public long nonce { get; set; }
    public MinerTx miner_tx { get; set; }
    public List<object> tx_hashes { get; set; }
}
    
}