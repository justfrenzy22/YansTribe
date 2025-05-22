import { IBaseUser } from "../IBaseUser";
import { IMedia } from "./IMedia";

export interface IPost {
	comment_count: number;
	content: string;
	created_at: Date;
	edited: boolean;
	edited_at: Date;
	is_liked_requester: boolean;
	like_count: number;
	media: IMedia[];
	post_id: string;
	user: IBaseUser;
}
