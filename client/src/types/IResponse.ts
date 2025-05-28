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
	posts: IPost[] | null;
}

export interface IUserResponse extends IResponse {
	user: IUserContext[`user`];
}
