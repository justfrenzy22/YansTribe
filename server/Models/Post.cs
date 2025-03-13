public class Post
{
    private int _post_id;
    private int _user_id;
    private string _title;
    private bool _has_img;
    private string? _media_src;
    private string _content;
    private DateTime _created_at;

    public Post(int post_id, int user_id, string title, bool has_img, string? media_src, string content, DateTime created_at)
    {
        this._post_id = post_id;
        this._user_id = user_id;
        this._title = title;
        this._has_img = has_img;
        this._media_src = media_src;
        this._content = content;
        this._created_at = created_at;
    }

    public int post_id { get => this._post_id; }
    public int user_id { get => this._user_id; }
    public string title { get => this._title; }
    public bool has_img { get => this._has_img; }
    public string? media_src { get => this._media_src; }
    public string content { get => this._content; }
    public DateTime created_at { get => this.created_at; }
}