export interface IPost {
    post_id: number;
    user_id: number;
    title: string;
    has_img: boolean;
    media_src?: string;
    content: string;
    created_at: Date;
}
