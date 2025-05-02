export interface IComment {
    comment_id: number;
    post_id: number;
    commenter_id: number;
    parent_id?: number;
    content: string;
    created_at: Date;
    is_hidden: boolean;
    
}