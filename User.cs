namespace CommunityFoodWasteSharing
{
    public abstract class User
    {
        protected string Name { get; set; }

        protected User(string name = "")
        {
            Name = name;
        }
    }
}