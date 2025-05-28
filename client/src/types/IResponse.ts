import { IComment } from "./comment/IComment";
import IUserContext from "./context/IProfileContext";
import { IPost } from "./post/IPost";

export interface IResponse {
	status: number;
	message: string;
}

export interface IProfileResponse extends IResponse {
	profile: IUserContext[`profile`];
}

export interface IPostResponse extends IResponse {
	posts: IPost[];
}

export interface IUserResponse extends IResponse {
	user: IUserContext[`user`];
}

export interface IGetCommentsResponse extends IResponse {
	comments: IComment[] | [];
}
