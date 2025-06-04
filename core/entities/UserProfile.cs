using core.enums;

namespace core.entities
{
    public class UserProfile : UserDetails
    {
        private int _friends_num;
        // first 10 friendships
        private List<Friendship> _friends;
        private RelationShipInfo _relationship_info;
        private List<Post> _posts;
        private List<Story> _stories;

        public UserProfile(
            Guid user_id,
            string username,
            string pfp_src,
            string email,
            string full_name,
            string bio,
            string location,
            string website,
            bool is_private,
            DateTime created_at,
            Role role,
            int friends_num,
            RelationShipInfo relationship_info
        ) : base(user_id, username, pfp_src, email, full_name, bio, location, website, is_private, created_at, role)
        {
            this._friends = new List<Friendship>();
            this._friends_num = friends_num;
            this._posts = new List<Post>();
            this._relationship_info = relationship_info;
            this._stories = new List<Story>();
        }
        // stories, friends and posts could be null

        public int friends_num => this._friends_num;
        public RelationShipInfo relationship_info => this._relationship_info;
        public List<Friendship> friends => this._friends;
        public List<Post> posts => this._posts;
        public List<Story> stories => this._stories;
        public void AddFriend(Friendship friend) => this._friends.Add(friend);
        public void AddPosts(List<Post> posts) => this._posts = posts;
        public void AddStories(List<Story> stories) => this._stories = stories;
    }
}
