import { INotifications } from "./notifications/INotifications";

export interface IBaseUser {
	user_id: string;
	username: string;
	pfp_src: string;
	is_private: boolean;
	notifications: INotifications;
}
