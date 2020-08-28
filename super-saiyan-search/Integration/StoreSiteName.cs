public class StoreSiteName
{
    private StoreSiteName(string value) { Value = value; }
    override public string ToString() => this.Value;
    public string Value { get; set; }
    public static StoreSiteName Takealot { get { return new StoreSiteName("Takealot"); } }
    public static StoreSiteName IncredibleConnection { get { return new StoreSiteName("IncredibleConnection"); } }
    public static StoreSiteName Game { get { return new StoreSiteName("Game"); } }
    public static StoreSiteName Makro { get { return new StoreSiteName("Makro"); } }
    public static StoreSiteName HifiCorp { get { return new StoreSiteName("HifiCorp"); } }
    public static StoreSiteName PickNPay { get { return new StoreSiteName("PickNPay"); } }
    public static StoreSiteName Shoprite { get { return new StoreSiteName("Shoprite"); } }
    public static StoreSiteName Checkers { get { return new StoreSiteName("Checkers"); } }
    public static StoreSiteName Clicks { get { return new StoreSiteName("Clicks"); } }
    public static StoreSiteName Dischem { get { return new StoreSiteName("Dischem"); } }
    public static StoreSiteName MatrixWarehouse { get { return new StoreSiteName("MatrixWarehouse"); } }
    public static StoreSiteName Woolworths { get { return new StoreSiteName("Woolworths"); } }
}