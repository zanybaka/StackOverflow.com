namespace ConsoleApp1
{
    public class JsonRootObject
    {
        public Entity[] Array;
    }

    public class Transaction
    {
        public string Version;
        public TranDtls TranDtls;
    }

    public class TranDtls
    {
        public string TaxSch;
        public string SupTyp;
        public string RegRev;
        public string EcmGstin;
        public string IgstOnIntra;
    }

    public class DocDtls
    {

    }
    public class Entity
    {
        public Transaction transaction;
        public DocDtls DocDtls;
    }
}