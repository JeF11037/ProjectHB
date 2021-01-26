using SQLite;

namespace ProjectHB.Data
{
    public class MessagePreset
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string MsgContent { get; set; }
    }
}
