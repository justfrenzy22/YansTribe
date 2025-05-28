export interface IComment {
    comment_id: string;
    post_id: string;
    commenter_id: string;
    parent_id?: string;
    content: string;
    created_at: Date;
    is_hidden: boolean;
    
}