// import { Role } from "@/enums/Role";
// import { IRole } from "@/types/IRole";

// class User {
// 	private _user_id: number;
// 	private _username: string;
// 	private _email: string;
// 	private _password_hash: string;
// 	private _full_name: string;
// 	private _bio: string;
// 	private _pfp_src: string;
// 	private _location: string;
// 	private _website: string;
// 	private _is_private: boolean;
// 	private _created_at: Date;
// 	private _role: IRole;

// 	constructor(
// 		user_id: number,
// 		username: string,
// 		email: string,
// 		password_hash: string,
// 		full_name: string,
// 		bio: string,
// 		pfp_src: string,
// 		location: string,
// 		website: string,
// 		is_private: boolean,
// 		created_at: Date,
// 		role: IRole
// 	) {
// 		this._user_id = user_id;
// 		this._username = username;
// 		this._email = email;
// 		this._password_hash = password_hash;
// 		this._full_name = full_name;
// 		this._bio = bio;
// 		this._pfp_src = pfp_src;
// 		this._location = location;
// 		this._website = website;
// 		this._is_private = is_private;
// 		this._created_at = created_at;
// 		this._role = role;
// 	}

// 	public ParseRole(value: string): IRole {
// 		return (Object.values(Role) as string[]).includes(value)
// 			? (value as unknown as IRole)
// 			: (() => {
// 					throw new Error("Invalid role value");
// 			  })();
// 	}
// }
