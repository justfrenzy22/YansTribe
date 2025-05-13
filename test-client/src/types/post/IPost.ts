import { IPostMedia } from "./IPostMedia";

export interface IPost {
	comment_count: number;
	content: string;
	created_at: Date;
	edited: boolean;
	edited_at: Date;
	like_count: number;
	media: IPostMedia[];
	post_id: string;
	user_id: string;
	username: string;
	pfp_src: string;
}
