
public class RootObjectResponse
{
    public MasterDealer[] MasterDealers { get; set; }
}

public class MasterDealer
{
    public Address Address { get; set; }
    public int ContractId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string MasterDealerName { get; set; }
    public int MasterDealerId { get; set; }
}

public class Address
{
    public string City { get; set; }
    public string Country { get; set; }
    public string Street { get; set; }
    public string Zip { get; set; }
}





public class RootObjectRequest
{
    public string token { get; set; }
    public Data data { get; set; }
}

public class Data
{
    public string MasterDealerId { get; set; }
    public string MasterDealerName { get; set; }
    public string ContractId { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
    public int _repeat { get; set; }
}

