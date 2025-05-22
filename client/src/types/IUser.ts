import { IBaseUser } from "./IBaseUser";
import { IRole } from "./IRole";
import { IPost } from "./post/IPost";

export interface IUser extends IBaseUser {
	email: string;
	password_hash: string;
	full_name: string;
	bio: string;
	location: string;
	website: string;
	is_private: boolean;
	created_at: Date;
	posts: IPost[];
	role: IRole;
	friends: IBaseUser[];
	friends_num: number;
	is_friend: boolean;
	is_self: boolean;
	request_direction: "sent" | "received" | null;
}
