using core.enums;

namespace core.entities
{
    public class ProfileUser : SafeUser
    {
        private List<FriendUser> _friends;
        private int _friends_num;
        private bool _is_friend;
        private bool _is_self;
        private List<Post> _posts;
        private FriendStatus? _friendship_status;
        private string _request_direction;

        public ProfileUser(
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
            bool is_self,
            bool is_friend,
            int friends_num,
            FriendStatus? friendship_status,
            string request_direction
        ) : base(user_id, username, pfp_src, email, full_name, bio, location, website, is_private, created_at, role)
        {
            this._friends = new List<FriendUser>();
            this._friends_num = friends_num;
            this._is_friend = is_friend;
            this._is_self = is_self;
            this._posts = new List<Post>();
            this._friendship_status = friendship_status;
            this._request_direction = request_direction;
        }

        public List<FriendUser> friends => this._friends;
        public int friends_num => this._friends_num;
        public bool is_friend => this._is_friend;
        public bool is_self => this._is_self;
        public List<Post> posts => this._posts;
        public FriendStatus? friendship_status => this._friendship_status;
        public string request_direction => this._request_direction;

        public void AddFriend(FriendUser friend) => this._friends.Add(friend);
        public void AddFriends(List<FriendUser> friends) => this._friends = friends;
        public void AddPost(Post post) => this._posts.Add(post);
        public void AddPosts(List<Post> posts) => this._posts = posts;
    }
}
