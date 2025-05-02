export interface IPost {
    post_id: string;
    user_id: string;
    title: string;
    has_img: boolean;
    media_src?: string;
    content: string;
    created_at: Date;
}
