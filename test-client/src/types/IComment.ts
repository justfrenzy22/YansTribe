export type IComment = {
    comment_id: string;
    user_id: string;
    username: string;
    pfp_src: string;
    created_at: string;
    content: string;
    liked: boolean;
    liked_count: number;
    reply_count: number;
}