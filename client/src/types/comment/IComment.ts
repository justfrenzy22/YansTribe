import { IBaseUser } from "../IBaseUser";

export interface IComment extends IBaseComment {
	comment_id: string;
	parent_id?: string;
	edited: boolean;
	edited_at?: Date;
	reply_count: number;
	is_hidden: boolean;
	liked: boolean;
	likes_count: number;
}

export interface IBaseComment {
	post_id: string;
	user: IBaseUser;
	content: string;
	created_at: string;
}
