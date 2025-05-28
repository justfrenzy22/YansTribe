import { IBaseUser } from "@/types/IBaseUser";

export class BaseComment {
    public post_id: string;
    public user: IBaseUser;
    public content: string;
    public created_at: Date;

    constructor(
        post_id: string,
        user: IBaseUser,
        content: string,
        created_at: Date
    ) {
        this.post_id = post_id;
        this.user = user;
        this.content = content;
        this.created_at = created_at;
    }
}

export class Comment extends BaseComment {
    public comment_id: string;
    public parent_id?: string;
    public edited: boolean;
    public edited_at?: Date;
    public reply_count: number;
    public is_hidden: boolean;
    public liked: boolean;
    public likes_count: number;

    constructor(
        post_id: string,
        user: IBaseUser,
        content: string,
        comment_id: string,
        parent_id?: string,
        created_at: Date = new Date(),
        edited = false,
        edited_at?: Date,
        reply_count = 0,
        is_hidden = false,
        liked = false,
        likes_count = 0
    ) {
        super(post_id, user, content, created_at);
        this.comment_id = comment_id;
        this.parent_id = parent_id;
        this.edited = edited;
        this.edited_at = edited_at;
        this.reply_count = reply_count;
        this.is_hidden = is_hidden;
        this.liked = liked;
        this.likes_count = likes_count;
    }
}