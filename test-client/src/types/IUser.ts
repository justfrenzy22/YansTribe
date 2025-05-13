import { IBaseUser } from "./IEssentialsUser";
import { IRole } from "./IRole";

export interface IUser extends IBaseUser {
	email: string;
	password_hash: string;
	full_name: string;
	bio: string;
	location: string;
	website: string;
	is_private: boolean;
	created_at: Date;
	role: IRole;
}
